using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace WHyProject.Sockets
{
    public class SocketEventArgs: EventArgs
    {
        /// <summary>
        /// 实例化Socket事件参数
        /// </summary>
        /// <param name="socket">相关Socket</param>
        /// <param name="operation">操作类型</param>
        public SocketEventArgs(ISocket socket, SocketAsyncOperation operation)
        {
            if (socket == null)
                throw new ArgumentNullException("socket");
            Socket = socket;
            Operation = operation;
        }

        /// <summary>
        /// 获取或设置事件相关数据。
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 获取数据长度。
        /// </summary>
        public int DataLength { get { return Data == null ? 0 : Data.Length; } }

        /// <summary>
        /// 获取事件相关Socket
        /// </summary>
        public ISocket Socket { get; private set; }

        /// <summary>
        /// 获取事件操作类型。
        /// </summary>
        public SocketAsyncOperation Operation { get; private set; }
    }
}
