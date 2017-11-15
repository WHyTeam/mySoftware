using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WHyProject.Sockets
{
    class SocketHandler: ISocketHandler
    {
        //异步处理关系集合
        private Dictionary<IAsyncResult, SocketHandlerState> StateSet;
        //发送队列
        private List<SocketHandlerState> SendQueue;

        public SocketHandler()
        {
            StateSet = new Dictionary<IAsyncResult, SocketHandlerState>();
            SendQueue = new List<SocketHandlerState>();
        }

        /// <summary>
        /// 开始接收
        /// </summary>
        /// <param name="stream">Socket网络流</param>
        /// <param name="callback">回调函数</param>
        /// <param name="state">自定义状态</param>
        /// <returns>异步结果</returns>
        public IAsyncResult BeginReceive(Stream stream, AsyncCallback callback, object state)
        {
            //stream不能为null
            if (stream == null)
                throw new ArgumentNullException("stream");
            //回调函数不能为null
            if (callback == null)
                throw new ArgumentNullException("callback");
            //stream异常
            if (!stream.CanRead)
                throw new ArgumentException("stream不支持读取。");

            SocketAsyncResult result = new SocketAsyncResult(state);

            //初始化SocketHandlerState
            SocketHandlerState shs = new SocketHandlerState();
            shs.Data = new byte[1024];
            shs.AsyncResult = result;
            shs.Stream = stream;
            shs.AsyncCallBack = callback;
            shs.Completed = true;
            //开始异步接收长度为2的头信息
            //该头信息包含要接收的主要数据长度
            try
            {
                stream.BeginRead(shs.Data, 0, 1024, EndRead, shs);
            }
            catch
            {
                result.CompletedSynchronously = true;
                shs.Data = new byte[0];
                shs.Completed = false;
                lock (StateSet)
                    StateSet.Add(result, shs);
                ((AutoResetEvent)result.AsyncWaitHandle).Set();
                callback(result);
            }
            return result;
        }

        //stream异步结束读取
        private void EndRead(IAsyncResult ar)
        {
            SocketHandlerState state = (SocketHandlerState)ar.AsyncState;
            int dataLength;
            try
            {
                dataLength = state.Stream.EndRead(ar);
            }
            catch
            {
                dataLength = 0;
            }
            //dataLength为0则表示Socket断开连接
            if (dataLength == 0)
            {
                lock (StateSet)
                    StateSet.Add(state.AsyncResult, state);
                //设定接收到的数据位空byte数组
                state.Data = new byte[0];
                //允许等待线程继续
                ((AutoResetEvent)state.AsyncResult.AsyncWaitHandle).Set();
                //执行异步回调函数
                state.AsyncCallBack(state.AsyncResult);
                return;
            }
            

            ////如果是已完成状态，则表示state.Data的数据是头信息
            //if (state.Completed)
            //{
            //    //设定状态为未完成
            //    state.Completed = false;
            //    //已接收得数据长度为0
            //    state.DataLength = 0;
            //    //获取主要数据长度
            //    var length = BitConverter.ToUInt16(state.Data, 0);
            //    //初始化数据的byte数组
            //    state.Data = new byte[length];
            //    try
            //    {
            //        //开始异步接收主要数据
            //        state.Stream.BeginRead(state.Data, 0, length, EndRead, state);
            //    }
            //    catch
            //    {
            //        //出现Socket异常
            //        lock (StateSet)
            //            StateSet.Add(state.AsyncResult, state);
            //        state.Data = new byte[0];
            //        ((AutoResetEvent)state.AsyncResult.AsyncWaitHandle).Set();
            //        state.AsyncCallBack(state.AsyncResult);
            //    }
            //    return;
            //}
            ////接收到主要数据
            //else
            //{
            //    //判断是否接收了完整的数据
            //    if (dataLength + state.DataLength != state.Data.Length)
            //    {
            //        //增加已接收数据长度
            //        state.DataLength += dataLength;
            //        try
            //        {
            //            //继续接收数据
            //            state.Stream.BeginRead(state.Data, state.DataLength, state.Data.Length - state.DataLength, EndRead, state);
            //        }
            //        catch
            //        {
            //            //出现Socket异常
            //            lock (StateSet)
            //                StateSet.Add(state.AsyncResult, state);
            //            state.Data = new byte[0];
            //            ((AutoResetEvent)state.AsyncResult.AsyncWaitHandle).Set();
            //            state.AsyncCallBack(state.AsyncResult);
            //            return;
            //        }
            //        return;
            //    }
                //接收完成
                state.Completed = true;
                byte[] dest = new byte[dataLength];
            Array.Copy(state.Data, dest, dataLength);
            state.Data= new byte[dataLength];
            state.Data = dest;
                lock (StateSet)
                    StateSet.Add(state.AsyncResult, state);
                ((AutoResetEvent)state.AsyncResult.AsyncWaitHandle).Set();
                state.AsyncCallBack(state.AsyncResult);
            //}
        }

        /// <summary>
        /// 结束接收
        /// </summary>
        /// <param name="asyncResult">异步结果</param>
        /// <returns>接收到的数据</returns>
        public byte[] EndReceive(IAsyncResult asyncResult)
        {
            //判断异步操作状态是否属于当前处理程序
            SocketHandlerState state;
            lock (StateSet)
            {
                if (!StateSet.ContainsKey(asyncResult))
                    throw new ArgumentException("无法识别的asyncResult。");
                state = StateSet[asyncResult];
                StateSet.Remove(asyncResult);
            }
            return state.Data;
        }

        /// <summary>
        /// 开始发送
        /// </summary>
        /// <param name="data">要发送的数据</param>
        /// <param name="offset">数据偏移</param>
        /// <param name="count">发送长度</param>
        /// <param name="stream">Socket网络流</param>
        /// <param name="callback">回调函数</param>
        /// <param name="state">自定义状态</param>
        /// <returns>异步结果</returns>
        public IAsyncResult BeginSend(byte[] data, int offset, int count, Stream stream, AsyncCallback callback, object state)
        {
            //data不能为null
            if (data == null)
                throw new ArgumentNullException("data");
            //offset不能小于0和超过data长度
            if (offset > data.Length || offset < 0)
                throw new ArgumentOutOfRangeException("offset");
            //count不能大于65535
            if (count <= 0 || count > data.Length - offset || count > ushort.MaxValue)
                throw new ArgumentOutOfRangeException("count");
            //stream不能为null
            if (stream == null)
                throw new ArgumentNullException("stream");
            //回调函数不能为null
            if (callback == null)
                throw new ArgumentNullException("callback");
            //stream异常
            if (!stream.CanWrite)
                throw new ArgumentException("stream不支持写入。");

            SocketAsyncResult result = new SocketAsyncResult(state);

            //初始化SocketHandlerState
            SocketHandlerState shs = new SocketHandlerState();
            shs.Data = data;
            shs.AsyncResult = result;
            shs.Stream = stream;
            shs.AsyncCallBack = callback;
            shs.DataLength = data.Length;

            //锁定SendQueue
            //避免多线程同时发送数据
            lock (SendQueue)
            {
                //添加状态
                SendQueue.Add(shs);
                //如果SendQueue数量大于1，则表示有数据尚未发送完成
                if (SendQueue.Count > 1)
                    return result;
            }

            ////获取数据长度
            ////ushort的最大值为65535
            ////转换为byte[]长度为2
            //var dataLength = BitConverter.GetBytes((ushort)data.Length);
            ////向对方发送长度为2的头信息，表示接下来要发送的数据长度
            //try
            //{
            //    stream.Write(dataLength, 0, dataLength.Length);
            //}
            //catch
            //{
            //    result.CompletedSynchronously = true;
            //    shs.Completed = false;
            //    lock (StateSet)
            //        StateSet.Add(result, shs);
            //    ((AutoResetEvent)result.AsyncWaitHandle).Set();
            //    callback(result);
            //}
            //开始异步发送数据
            try
            {
                stream.BeginWrite(shs.Data, 0, shs.Data.Length, EndWrite, shs);
            }
            catch
            {
                result.CompletedSynchronously = true;
                shs.Completed = false;
                lock (StateSet)
                    StateSet.Add(result, shs);
                ((AutoResetEvent)result.AsyncWaitHandle).Set();
                callback(result);
            }
            return result;
        }

        //stream异步结束写入
        private void EndWrite(IAsyncResult ar)
        {
            SocketHandlerState state = (SocketHandlerState)ar.AsyncState;

            //锁定StateSet
            lock (StateSet)
                StateSet.Add(state.AsyncResult, state);

            try
            {
                state.Stream.EndWrite(ar);
            }
            catch
            {
                //出现Socket异常，发送失败
                state.Completed = false;
                //允许等待线程继续
                ((AutoResetEvent)state.AsyncResult.AsyncWaitHandle).Set();
                //执行异步回调函数
                state.AsyncCallBack(state.AsyncResult);
                return;
            }
            //发送成功
            state.Completed = true;
            //允许等待线程继续
            ((AutoResetEvent)state.AsyncResult.AsyncWaitHandle).Set();
            //执行异步回调函数
            state.AsyncCallBack(state.AsyncResult);

            //锁定SendQueue
            lock (SendQueue)
            {
                SocketHandlerState prepare = null;
                //移除当前发送完成的数据
                SendQueue.Remove(state);
                //如果SendQueue还有数据存在，则继续发送
                if (SendQueue.Count > 0)
                {
                    prepare = SendQueue[0];
                }
                if (prepare != null)
                {
                    //获取数据长度
                    //ushort的最大值为65535
                    //转换为byte[]长度为2
                    //var dataLength = BitConverter.GetBytes((ushort)prepare.Data.Length);
                    //向对方发送长度为2的头信息，表示接下来要发送的数据长度
                    try
                    {
                      //  prepare.Stream.Write(dataLength, 0, dataLength.Length);
                        //开始异步发送数据
                        prepare.Stream.BeginWrite(prepare.Data, 0, prepare.Data.Length, EndWrite, prepare).AsyncWaitHandle.WaitOne();
                    }
                    catch
                    {
                        prepare.Completed = false;
                        ((AutoResetEvent)prepare.AsyncResult.AsyncWaitHandle).Set();
                        prepare.AsyncCallBack(prepare.AsyncResult);
                    }
                }
            }
        }

        /// <summary>
        /// 结束发送
        /// </summary>
        /// <param name="asyncResult">异步结果</param>
        /// <returns>发送是否成功</returns>
        public bool EndSend(IAsyncResult asyncResult)
        {
            //判断异步操作状态是否属于当前处理程序
            SocketHandlerState state;
            lock (StateSet)
            {
                if (!StateSet.ContainsKey(asyncResult))
                    throw new ArgumentException("无法识别的asyncResult。");
                state = StateSet[asyncResult];
                StateSet.Remove(asyncResult);
            }
            return state.Completed;
        }


    }


    internal class SocketHandlerState
    {
        /// <summary>
        /// 数据
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// 异步结果
        /// </summary>
        public IAsyncResult AsyncResult { get; set; }
        /// <summary>
        /// Socket网络流
        /// </summary>
        public Stream Stream { get; set; }
        /// <summary>
        /// 异步回调函数
        /// </summary>
        public AsyncCallback AsyncCallBack { get; set; }
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool Completed { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength { get; set; }
    }
}
