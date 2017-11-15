using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using WHyProject.Sockets;
using GalaSoft.MvvmLight.Messaging;
using WHyProject.Protocol;


namespace WHyProject.Server
{
    /// <summary>
    /// 服务器端
    /// </summary>
    public class MyServer
    {
        private TCPListener Listener;
       // private RingBuffer ReceiveBuffer = new RingBuffer(50);
        //private Dictionary<Guid, byte[]> ReceiveCache;

        private TransMsg myMsg = TransMsg.Instance;


        private bool _enableTxFix = true;               // 标记是否永久停止固定发送
        //发送线程
        // 关于发送线程
        protected Thread _txThread = null;                // 周期性的指令发送线程
        protected ManualResetEvent _txThreadON = null;    // 
        private bool _txEnabled = true;                 // 允许指令下发

        private ManualResetEvent _txDelayOut = null;      // 
        private ManualResetEvent _txWaitOut = null;
        private int _txDelay = 500;
        private int _delayTxFix = 1500;                  // 3/4区轮询间隔时间，默认为150ms
        public MyServer()
        {
           Listener = new TCPListener();
            //ReceiveMsg = new TransMsg();
        }

        /// <summary>
        /// 面向终端Socket的循环发送指令线程
        /// 固有发送任务：350ms的定期下发指令
        /// 其他发送任务：来自客户端的指令请求
        /// </summary>
        /// 
        private void OnSendCycleTerm()
        {
            _txThreadON.WaitOne(1000);
            // 等待1秒，启动发送线程
            while (IsConnected)
            {
                if (_txEnabled)
                {
                    if (myMsg._queCmd.Count > 0)
                    {
                        //发送一条用户指令
                        TxUserCmd();
                        _txWaitOut.Reset(); //终止线程，等待回复确认
                        _txWaitOut.WaitOne(_txDelay); // // 设置等待回复延时（500ms），接收到有效回复或超时，则当前线程继续
                        if (myMsg._queCmd.Count == 0)
                        {
                            _txDelayOut.Reset();
                            _txDelayOut.WaitOne(500);
                        } // 否则继续发送下一条用户指令
                        else
                        {
                            // 为保证安全处理，设置10ms时间间隔
                            Thread.Sleep(10);
                        }
                    }
                    else if(_enableTxFix)
                    {
                        TxFixCmd();
                        _txWaitOut.Reset();
                        Thread.Sleep(1500);                    // 轮询的时间间隔至少为150ms
                        _txWaitOut.WaitOne(_delayTxFix);      // 最多等待(150+_delayTxFix)ms，接收到有效回复时当前线程继续
                    }
                    else
                    {
                        _txDelayOut.Reset();                        // 若无发送任务，则每次阻塞等待1s，允许别的线程中断等待并继续执行
                        _txDelayOut.WaitOne(1000);
                    }
                }
            }
        }

        public void AddUserCmd(byte[] data)
        {
            myMsg._queCmd.Add(data);
            _txDelayOut.Set();
        }
        private void TxFixCmd()
        {
            byte[] tempCmd = myMsg.SendCycleFixed();
            SendAll(tempCmd);
        }
        private void TxUserCmd()
        {
            byte[] tempCmd = null;
            lock (myMsg._queCmd)
            {
                tempCmd = (byte[]) (myMsg._queCmd[0]);
                myMsg._queCmd.RemoveAt(0);
            }
            SendAll(tempCmd);
        }
        public MyServer(ushort port)
        {
            Listener = new TCPListener();
            Listener.Port = port;
            Listener.ReceiveCompleted += Listener_ReceivedCompleted;
          
          //  ReceiveCache = new Dictionary<Guid, byte[]>();
        }

        private void Listener_ReceivedCompleted(object Sender, SocketEventArgs e)
        {
            byte cmdId = myMsg.GetCmdIDByRespLen(e.Data);
            myMsg.ParseClientReceivedMsg(cmdId, e.Data);
            _txWaitOut.Set();
        }

        public void Start()
        {
            Listener.Start();
            StartTxThread();
        }

        public void Stop()
        {
            Listener.Stop();
            _txEnabled = false;
            if (_txThread != null)
            {
                _txThreadON.Set();
                _txThread.Join(2000);
                _txThread = null;
            }

        }

        public bool IsStarted
        {
            get { return Listener.IsStarted; }
        }

        public bool IsConnected
        {
            get { return Listener.IsConnected; }
        }
        public int Port
        {
            get { return Listener.Port; }
        }

        private ushort data;

        public ushort Data
        {
            get { return data; }
            set
            {
                data = value;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "Data"));
            }
        }

        public string GetConnectClient()
        {
            var client = Listener.GetEnumerator();
            string result = null;
            while (client.MoveNext())
            {
                TCPListenerClient connClient = (TCPListenerClient) client.Current;
                IPEndPoint clientIP = connClient.RemoteEndPoint;
                string temp = clientIP.ToString();

                result += temp;
                result += "\r\n";
            }
            return result;
        }

        public void StartTxThread()
        {
            //
            // 初始化循环发送线程
            _txThreadON = new ManualResetEvent(false);
            _txThread = new Thread(this.OnSendCycleTerm);
            _txThread.IsBackground = true;
            _txThread.Start();
            //
            // 初始化发送等待/发送延时控制信号
            _txWaitOut = new ManualResetEvent(false);
            _txDelayOut = new ManualResetEvent(false);
        }
        public void SendAll(byte[] data)
        {
            Listener.SendAllData(data);
        }
    }
}
