using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using WHyProject.Model.Conductor;

namespace WHyProject.Protocol
{
    class TransMsg
    {
        private MasterConductor objMasterConductor = MasterConductor.Master;
        //系统配置信息
        public static Dictionary<string, int> _IPWithDeviceID = null;
        public static Dictionary<int, byte> _dicCmdID = null;
        public static Dictionary<byte, int> _dicRspLen = null;
        private static int _rsplen3, _rsplen4, _dloggerlen, _rspdlogger;
        private static string[] _fixcmds = new string[3];   
        private static int[] _rsplens = new int[3];

        public static readonly TransMsg Instance = new TransMsg();

        public ArrayList _queCmd = new ArrayList();    // 待发送指令队列


        //轮询发送的消息
        private int cycleIndex = 1;

        private TransMsg()
        {
            _IPWithDeviceID = new Dictionary<string, int>();
            _dicCmdID = new Dictionary<int, byte>();
            _dicRspLen = new Dictionary<byte, int>();
            configInitilization();
        }
        private static void configInitilization()
        {
             // 加载XML格式的配置文件
            XmlDocument cfgxml = new XmlDocument();
            cfgxml.Load(System.AppDomain.CurrentDomain.BaseDirectory + "configTCP.xml");
            XmlNode root = cfgxml.SelectSingleNode("config");
            //
            // 加载设备信息
            XmlNodeList nlist = root.SelectSingleNode("devices").ChildNodes;  // 获取devices信息节点
            for (int i = 0; i < nlist.Count; i++)
            {
                XmlNodeList xnl = nlist[i].ChildNodes;
                string devip = xnl[0].InnerText;
                byte devid = byte.Parse(xnl[1].InnerText);
                _IPWithDeviceID.Add(devip, devid);
            }
            //
            // 加载指令信息
            nlist = root.SelectSingleNode("commands").ChildNodes;  // 获取commands信息节点
            for (int i = 0; i < nlist.Count; i++)
            {
                XmlNodeList xnl = nlist[i].ChildNodes;
                byte cmdid = byte.Parse(xnl[0].InnerText);
                int cmdlen = int.Parse(xnl[1].InnerText);
                _dicCmdID.Add(cmdlen, cmdid);
                _dicRspLen.Add(cmdid, cmdlen);
                switch (nlist[i].LocalName)
                {
                    case "loopcmd3":
                        _rsplen3 = cmdlen;
                        _fixcmds[0] = xnl[2].InnerText;   // 获取3区查询指令字符串
                        _rsplens[0] = cmdlen;   // 20160126 add
                        break;
                    case "loopcmd4":
                        _rsplen4 = cmdlen;
                        _fixcmds[1] = xnl[2].InnerText;   // 获取4区查询指令字符串
                        _rsplens[1] = cmdlen;   // 20160126 add
                        break;
                    case "loopcmd5":            // 20160126 add
                        _fixcmds[2] = xnl[2].InnerText;   // 获取新4区查询指令字符串
                        _rsplens[2] = cmdlen;
                        break;
                    case "datalogger":
                        _rspdlogger = cmdlen;
                        _dloggerlen = int.Parse(xnl[2].InnerText);
                        _dicCmdID.Add(_dloggerlen, cmdid);
                        break;
                }
            }
        }



        public byte GetCmdIDByRespLen(byte[] data)
        {
            byte CmdId = 0x00;
            bool found = false;
            int len = data.Length;
            if (len < 7) CmdId = 0xDD;
            else
            {
                found = _dicCmdID.TryGetValue(len, out CmdId);
                if (found)
                {
                    return CmdId;
                }
                else CmdId = 0xDD;
            }
            return CmdId;

        }

        public byte[] SendCycleFixed()
        {
            string tempcmd = _fixcmds[cycleIndex];
            string[] tempcmdChars = tempcmd.Split(' ');

            byte[] CycleMsg = new byte[tempcmdChars.Length];
            for (int i = 0; i < tempcmdChars.Length; i++)
            {
                CycleMsg[i] = byte.Parse(tempcmdChars[i], System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            switch (cycleIndex)
            {
                case 0:
                    cycleIndex = 1;
                    break;
                case 1:
                    cycleIndex = 0;
                    break;

            }
            return CycleMsg;
        }

        public byte[] SetCommand()
        {
            byte[] Command = {0x01, 0x06, 0x00, 0xB2, 0x00, 0x00, 0x29, 0xED};
            return Command;
        }


        private void ParseCycleReadData(byte[] data)
        {
            int len = data.Length;
            byte head = data[0];
            byte ID = data[1];
            byte RecvLen = data[2];
            ushort crcresult = (ushort)((data[len - 2] << 8) + (data[len - 1]));
            ushort crccaculate = (ushort) CRC16.CRC16_Caculate(data, (ushort)(len - 2));
            if ((0x01 == head) && (0x04 == ID) && (RecvLen == len - 5) && (crccaculate == crcresult))
            {
                byte[] m_sRegisterThree = data.Skip(3).Take(RecvLen).ToArray();
                SetFPCDebug(m_sRegisterThree);
                SetFPCMode(m_sRegisterThree);
                SetFPCStatus(m_sRegisterThree);
            }
        }

        private void SetFPCMode(byte[] data)
        {
            int len = data.Length;
            if (len < 53 * 2) return;
            objMasterConductor.GenModuleModeForDisplay =
                (ushort) ((data[53 * 2 - 2] << 8) + data[53 * 2 - 1]);

            if (len < 55 * 2) return;
            objMasterConductor.GridModuleModeForDisplay =
                (ushort)((data[55 * 2 - 2] << 8) + data[55 * 2 - 1]);

            if (len < 57 * 2) return;
            objMasterConductor.McuModuleModeForDisplay =
                (ushort)((data[57 * 2 - 2] << 8) + data[57 * 2 - 1]);
        }

        private void SetFPCStatus(byte[] data)
        {
            int len = data.Length;
            if (len < 52 * 2) return;
            objMasterConductor.GenModuleStatusForDisplay =
                (ushort)((data[52 * 2 - 2] << 8) + data[52 * 2 - 1]);

            if (len < 54 * 2) return;
            objMasterConductor.GridModuleStatusForDisplay =
                (ushort)((data[54 * 2 - 2] << 8) + data[54 * 2 - 1]);

            if (len < 56 * 2) return;
            objMasterConductor.McuModuleStatusForDisplay =
                (ushort)((data[56 * 2 - 2] << 8) + data[56 * 2 - 1]);
        }
        private void SetFPCDebug(byte[] data)
        {
            int len = data.Length;
            if (len < 14) return;

            ushort value = 0;
            
            //设置
            double[] temp1= new double[8];
            for (var i = 0; i < 14; i += 2)
            {
                value = (ushort) ((data[i] << 8) + (data[i + 1]));
                temp1[i/2] = (double) value;
            }
            objMasterConductor.Control1 = temp1;

            if (data.Length < 62)
                return;
            
            double[] temp2 = new double[17];
            for (var i = 0; i < 34; i += 2)
            {
                value = (ushort) ((data[i + 28] << 8) + (data[i + 29]));
                temp2[i/2] = (double) value;
            }
            objMasterConductor.Control2 = temp2;

            if (data.Length < 47 * 2)
                return;

            double[] temp3 = new double[16];
            for (var i = 0; i < 32; i += 2)
            {
                value = (ushort) ((data[i + 62] << 8) + (data[i + 63]));
                temp3[i / 2] = (double) value;
            }
            objMasterConductor.Control3 = temp3;

        }

       

        public void ParseClientReceivedMsg(byte CmdId,byte[] data)
        {
            switch (CmdId)
            {
                case 0:
                   // ParseCycleReadData(data);
                    break;
                case 3:
                    ParseCycleReadData(data);
                    break;
                case 4:
                    break;
                case 5:
                 //   ParseCycleReadData(data);
                    break;
                default:
                    break;
            }
        }
    }
}
