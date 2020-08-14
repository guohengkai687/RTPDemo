using SharpPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsVoIP
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private CaptureDeviceList _devices = null;
        private int _preTimestamp = 0;
        private int _prePacketLength = 0;
        private void MainForm_Load(object sender, EventArgs e)
        {
            //获取网络设备
            _devices = CaptureDeviceList.Instance;

            if (_devices.Count < 1)
            {
                System.Diagnostics.Debug.Print("找不到网络设备");
                return;
            }
            else
            {
                foreach (ICaptureDevice dev in _devices)
                {
                    cb_NetworkCard.Items.Add(dev.ToString());
                }
                cb_NetworkCard.SelectedIndex = 1; //选择以太网
            }
            btn_Monitor.Text = "开启监控";
        }

        private void btn_Monitor_Click(object sender, EventArgs e)
        {
            btn_Monitor.Text = btn_Monitor.Text == "开启监控" ? "关闭监控" : "开启监控";

            ICaptureDevice dev = _devices.FirstOrDefault(x => x.ToString() == cb_NetworkCard.Text.ToString());
            if (dev != null)
            {
                int readTimeoutMilliseconds = 1000;
                if (btn_Monitor.Text == "关闭监控")
                {
                    dev.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);
                    if (dev is SharpPcap.WinPcap.WinPcapDevice)
                    {
                        var winPcap = dev as SharpPcap.WinPcap.WinPcapDevice;
                        winPcap.Open(SharpPcap.WinPcap.OpenFlags.DataTransferUdp | SharpPcap.WinPcap.OpenFlags.NoCaptureLocal, readTimeoutMilliseconds);
                        //winPcap.Open(SharpPcap.WinPcap.OpenFlags.Promiscuous, readTimeoutMilliseconds);
                    }
                    if (dev is SharpPcap.LibPcap.CaptureFileReaderDevice)
                    {
                        var reader = dev as SharpPcap.LibPcap.CaptureFileReaderDevice;
                        reader.Open();
                    }
                    // Start the capturing process
                    dev.StartCapture();
                }
                else
                {
                    dev.OnPacketArrival -= new PacketArrivalEventHandler(device_OnPacketArrival);
                    if (dev is SharpPcap.WinPcap.WinPcapDevice)
                    {
                        var winPcap = dev as SharpPcap.WinPcap.WinPcapDevice;
                        winPcap.Close();
                    }
                    if (dev is SharpPcap.LibPcap.CaptureFileReaderDevice)
                    {
                        var reader = dev as SharpPcap.LibPcap.CaptureFileReaderDevice;
                        reader.Close();
                    }
                    // Stop the capturing process
                    dev.StopCapture();
                }
                
            }
        }

        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            var time = e.Packet.Timeval.Date;
            var len = e.Packet.Data.Length;

            var pack = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            if (pack is PacketDotNet.EthernetPacket)
            {
                PacketDotNet.EthernetPacket ethPack = pack as PacketDotNet.EthernetPacket;
                if (ethPack.PayloadPacket is PacketDotNet.IPv4Packet)
                {
                    PacketDotNet.IPv4Packet ipPack = ethPack.PayloadPacket as PacketDotNet.IPv4Packet;
                    //TCP
                    if (ipPack.PayloadPacket is PacketDotNet.TcpPacket)
                    {
                        PacketDotNet.TcpPacket tcpPack = ipPack.PayloadPacket as PacketDotNet.TcpPacket;
                        if ((ipPack.DestinationAddress.ToString().Equals("172.25.25.50") && tcpPack.DestinationPort == 8001) ||
                            (ipPack.SourceAddress.ToString().Equals("172.25.25.50") && tcpPack.SourcePort == 8001))
                        {
                            //Console.WriteLine("TCP:{0}:{1}-{2}:{3}",ipPack.SourceAddress, tcpPack.SourcePort,ipPack.DestinationAddress,tcpPack.DestinationPort);
                            if (tcpPack.PayloadData != null && tcpPack.PayloadData.Length > 0)
                            {
                                Console.WriteLine("读取数据:{0}", System.Text.Encoding.UTF8.GetString(tcpPack.PayloadData));
                            }

                        }
                    }
                    //UDP
                    else if (ipPack.PayloadPacket is PacketDotNet.UdpPacket)
                    {
                        PacketDotNet.UdpPacket udp = ipPack.PayloadPacket as PacketDotNet.UdpPacket;
                        if (ipPack.DestinationAddress.ToString().Equals("172.25.25.69") && udp.DestinationPort == 5060)
                        {
                            if (udp.PayloadData != null && udp.PayloadData.Length > 0)
                            {
                                Console.WriteLine("读取数据:{0}", System.Text.Encoding.UTF8.GetString(udp.PayloadData));
                            }

                        }
                        //if (ipPack.DestinationAddress.ToString().Equals("172.25.25.69") && udp.DestinationPort == 18038)
                        if (ipPack.DestinationAddress.ToString().Equals("172.25.25.66") && udp.DestinationPort == 18132)
                        {
                            if (udp.PayloadData != null && udp.PayloadData.Length > 100)
                            {

                                RtpPacket rtpPacket = new RtpPacket(udp.PayloadData);
                                RtpHeader rtpHeader = new RtpHeader(udp.PayloadData);
                                int packetRate = RTPPayloadTypes.GetSamplingFrequency((RTPPayloadTypesEnum)Enum.ToObject(typeof(RTPPayloadTypesEnum), rtpHeader.PayloadType)); //8000

                                int minSec = ((int)rtpHeader.Timestamp - _preTimestamp - _prePacketLength) / (packetRate / 1000);
                                _preTimestamp = (int)rtpHeader.Timestamp;
                                _prePacketLength = rtpPacket.Payload.Length;

                                //写文件
                                string fileName = "F:\\" + "testRtp9" + ".wav";
                                PCMU m_PCMU = new PCMU();
                                //语音包
                                using (System.IO.FileStream fs = new System.IO.FileStream(fileName,
                                    System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.None))
                                {
                                    byte[] temp = null; //空白语音
                                    byte[] dec = null;  //payload荷载数据
                                    byte[] data = null; //完整数据
                                    //空白
                                    if (minSec > 0)
                                    {
                                        //temp = new byte[16 * minSec];
                                        //dec = m_PCMU.Decode_pcm8(rtpPacket.Payload, 0, rtpPacket.Payload.Length);

                                        //data = new byte[temp.Length + dec.Length];
                                        //Array.Copy(temp, 0, data, 0, temp.Length);
                                        //Array.Copy(dec, 0, data, temp.Length, dec.Length);

                                        temp = new byte[8 * minSec];
                                        for (int i = 0; i < temp.Length; i++)
                                        {
                                            temp[i] = 0xFE;
                                        }
                                        dec = rtpPacket.Payload;

                                        data = new byte[temp.Length + dec.Length];

                                        Array.Copy(temp, 0, data, 0, temp.Length);
                                        Array.Copy(dec, 0, data, temp.Length, dec.Length);
                                        data = m_PCMU.Decode_ulaw_pcm8(data, 0, data.Length);
                                    }
                                    else
                                    {
                                        data = m_PCMU.Decode_ulaw_pcm8(rtpPacket.Payload, 0, rtpPacket.Payload.Length);
                                    }

                                    fs.Position = fs.Length;

                                    fs.Write(data, 0, data.Length);
                                }
                            }
                        }
                    }
                }

            }
        }
    }
}
