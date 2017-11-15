using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WHyProject.Sockets
{
    /// <summary>
    /// TCP客户端
    /// </summary>
    public class TCPClient : SocketBase
    {
        /// <summary>
        /// 实例化TCP客户端。
        /// </summary>
        public TCPClient()
            : base(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), new SocketHandler())
        {
        }

        

        #region 连接

        /// <summary>
        /// 连接至服务器。
        /// </summary>
        /// <param name="endpoint">服务器终结点。</param>
        public void Connect(IPEndPoint endpoint)
        {
            //判断是否已连接
            if (IsConnected)
                throw new InvalidOperationException("已连接至服务器。");
            if (endpoint == null)
                throw new ArgumentNullException("endpoint");
            //锁定自己，避免多线程同时操作
            lock (this)
            {
                SocketAsyncState state = new SocketAsyncState();
                //Socket异步连接
                Socket.BeginConnect(endpoint, EndConnect, state).AsyncWaitHandle.WaitOne();
                //等待异步全部处理完成
                while (!state.Completed)
                {
                    Thread.Sleep(1);
                }
            }
        }

        /// <summary>
        /// 异步连接至服务器。
        /// </summary>
        /// <param name="endpoint"></param>
        public void ConnectAsync(IPEndPoint endpoint)
        {
            //判断是否已连接
            if (IsConnected)
                throw new InvalidOperationException("已连接至服务器。");
            if (endpoint == null)
                throw new ArgumentNullException("endpoint");
            //锁定自己，避免多线程同时操作
            lock (this)
            {
                SocketAsyncState state = new SocketAsyncState();
                //设置状态为异步
                state.IsAsync = true;
                //Socket异步连接
                Socket.BeginConnect(endpoint, EndConnect, state);
            }
        }

        private void EndConnect(IAsyncResult result)
        {
            SocketAsyncState state = (SocketAsyncState) result.AsyncState;

            try
            {
                Socket.EndConnect(result);
            }
            catch
            {
                //出现异常，连接失败。
                state.Completed = true;
                //判断是否为异步，异步则引发事件
                if (state.IsAsync && ConnectCompleted != null)
                    ConnectCompleted(this, new SocketEventArgs(this, SocketAsyncOperation.Connect));
                return;
            }
            //连接成功。
            //创建Socket网络流
            Stream = new NetworkStream(Socket);
           
            //连接完成
            state.Completed = true;
            if (state.IsAsync && ConnectCompleted != null)
            {
                ConnectCompleted(this, new SocketEventArgs(this, SocketAsyncOperation.Connect));
            }

            //开始接收数据
            Handler.BeginReceive(Stream, EndReceive, state);
        }

        #endregion

        /// <summary>
        /// 连接完成时引发事件。
        /// </summary>
        public event EventHandler<SocketEventArgs> ConnectCompleted;
    }
}
