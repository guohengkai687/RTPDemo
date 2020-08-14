using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsVoIP
{
    public class Rtp
    {

    }

    public class RtpPacket
    {
        public RtpHeader Header;
        public byte[] Payload;

        public RtpPacket(byte[] packet)
        {
            Header = new RtpHeader(packet);
            Payload = new byte[packet.Length - Header.Length];
            Array.Copy(packet, Header.Length, Payload, 0, Payload.Length);
        }
    }

    public class RtpHeader
    {
        public const int MIN_HEADER_LEN = 12;                   //The RTP header has a minimum size of 12 bytes.  ED137 header is 20 bytes

        public const int RTP_VERSION = 2;

        public int Version = RTP_VERSION;						// 2 bits.
        public int PaddingFlag = 0;								// 1 bit.
        public int HeaderExtensionFlag = 0;						// 1 bit.
        public int CSRCCount = 0;								// 4 bits   CSRC 记数（CC） 表示CSRC 标识的数目。CSRC 标识紧跟在RTP 固定头部之后，用来表示RTP 数据报的来源，RTP 协议允许在同一个会话中存在多个数据源，它们可以 通过RTP混合器合并为一个数据源。
        public int MarkerBit = 0;								// 1 bit.
        public int PayloadType = (int)RTPPayloadTypesEnum.PCMU;	// 7 bits.  标明RTP 负载的格式，包括所采用的编码算法、采样频率、承载通道等。
        public UInt16 SequenceNumber;							// 16 bits. 序列号(Sequence Number)一个16位的无符号二进制整数，并且以1个步长逐步递增。当序列号递增到最大值时，会自动恢复为0。 用来为接收方提供探测数据丢失的方法，但如何处理丢失的数据则是应用程序自己的事情，RTP 协议本身并不负责数据的重传。
        public uint Timestamp;									// 32 bits. 时间戳 记录了负载中第一个字节的采样时间，根据时钟采样频率计算时间，ADUIO大部分为8000采样率（1/8000 = 0.000125 sec） VIDEO 大部分为90000，接收方根据时间戳能够确定数据的到达是否受到了延迟抖动的影响，但具体如何来补偿延迟抖动则是应用程序自己的事情。
        public uint SyncSource;									// 32 bits. SSRC 同步源标识符（Synchronization Source identifier）同一个RTP会话中的同步源将是唯一的

        public int[] CSRCList;									// 32 bits.

        public UInt16 ExtensionProfile;                         // 16 bits.
        public UInt16 ExtensionLength;                          // 16 bits, length of the header extensions in 32 bit words.
        public byte[] ExtensionPayload;                         // ED137 occupy 4 bytes

        public int Length
        {
            get { return MIN_HEADER_LEN + (CSRCCount * 4) + ((HeaderExtensionFlag == 0) ? 0 : 4 + (ExtensionLength * 4)); }
        }

        /// <summary>
        /// Extract and load the RTP header from an RTP packet.
        /// </summary>
        /// <param name="packet"></param>
        public RtpHeader(byte[] packet)
        {
            if (packet.Length < MIN_HEADER_LEN)
            {
                throw new ApplicationException("The packet did not contain the minimum number of bytes for an RTP header packet.");
            }

            UInt16 firstWord = BitConverter.ToUInt16(packet, 0);

            if (BitConverter.IsLittleEndian)
            {
                firstWord = NetConvert.DoReverseEndian(firstWord);
                SequenceNumber = NetConvert.DoReverseEndian(BitConverter.ToUInt16(packet, 2));
                Timestamp = NetConvert.DoReverseEndian(BitConverter.ToUInt32(packet, 4));
                SyncSource = NetConvert.DoReverseEndian(BitConverter.ToUInt32(packet, 8));
            }
            else
            {
                SequenceNumber = BitConverter.ToUInt16(packet, 2);
                Timestamp = BitConverter.ToUInt32(packet, 4);
                SyncSource = BitConverter.ToUInt32(packet, 8);
            }

            Version = firstWord >> 14;
            PaddingFlag = (firstWord >> 13) & 0x1;
            HeaderExtensionFlag = (firstWord >> 12) & 0x1;
            CSRCCount = (firstWord >> 8) & 0xf;
            MarkerBit = (firstWord >> 7) & 0x1;
            PayloadType = firstWord & 0x7f;

            int headerAndCSRCLength = 12 + 4 * CSRCCount;

            if (HeaderExtensionFlag == 1 && (packet.Length >= (headerAndCSRCLength + 4)))
            {
                if (BitConverter.IsLittleEndian)
                {
                    ExtensionProfile = NetConvert.DoReverseEndian(BitConverter.ToUInt16(packet, 12 + 4 * CSRCCount));
                    ExtensionLength = NetConvert.DoReverseEndian(BitConverter.ToUInt16(packet, 14 + 4 * CSRCCount));
                }
                else
                {
                    ExtensionProfile = BitConverter.ToUInt16(packet, 12 + 4 * CSRCCount);
                    ExtensionLength = BitConverter.ToUInt16(packet, 14 + 4 * CSRCCount);
                }

                if (ExtensionLength > 0 && packet.Length >= (headerAndCSRCLength + 4 + ExtensionLength * 4))
                {
                    ExtensionPayload = new byte[ExtensionLength * 4];
                    Buffer.BlockCopy(packet, headerAndCSRCLength + 4, ExtensionPayload, 0, ExtensionLength * 4);
                }
            }
        }
    }

}
