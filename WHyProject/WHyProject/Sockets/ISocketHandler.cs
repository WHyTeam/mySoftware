using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace WHyProject.Sockets
{
    public interface ISocketHandler
    {
        /// <summary>
        /// 开始接收
        /// </summary>
        /// <param name="stream">Socket网络流</param>
        /// <param name="callback">回调函数</param>
        /// <param name="state">自定义状态</param>
        /// <returns>异步结果</returns>
        IAsyncResult BeginReceive(Stream stream, AsyncCallback callback, object state);
        /// <summary>
        /// 结束接收
        /// </summary>
        /// <param name="asyncResult">异步结果</param>
        /// <returns>接收到的数据</returns>
        byte[] EndReceive(IAsyncResult asyncResult);
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
        IAsyncResult BeginSend(byte[] data, int offset, int count, Stream stream, AsyncCallback callback, object state);
        /// <summary>
        /// 结束发送
        /// </summary>
        /// <param name="asyncResult">异步结果</param>
        /// <returns>发送是否成功</returns>
        bool EndSend(IAsyncResult asyncResult);
    }
}
