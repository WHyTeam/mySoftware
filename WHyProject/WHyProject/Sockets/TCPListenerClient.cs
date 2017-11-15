using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace WHyProject.Sockets
{
    public class TCPListenerClient : SocketBase
    {
        internal TCPListenerClient(TCPListener listener, Socket socket)
            : base(socket, listener.Handler)
        {
            //创建Socket网络流
            Stream = new NetworkStream(socket);

            //设置服务器
            Listener = listener;

            //开始异步接收数据
            SocketAsyncState state = new SocketAsyncState();
            Handler.BeginReceive(Stream, EndReceive, state);
        }

        public TCPListener Listener { get; private set; }
    }
}
