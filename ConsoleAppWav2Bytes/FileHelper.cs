using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleAppWav2Bytes
{
    public class FileHelper
    {
        /// <summary>
        /// ERROR MESSAGE
        /// </summary>
        const string ERRFILENOTEXITS = "File is Not Exits.";
        const string ERRFILEISNOTWAVE = "File is not Wava.";

        /// <summary>
        /// Wave Hander information
        /// </summary>
        struct HeaderType
        {
            public byte[] riff;                 /*RIFF类资源文件头部 4byte*/
            public uint file_len;                /*文件长度4byte*/
            public byte[] wave;                  /*"WAVE"标志4byte*/
            public byte[] fmt;                   /*"fmt"标志4byte*/
            public uint NI1;                     /*过渡字节4byte*/
            public ushort format_type;           /*格式类别(10H为PCM形式的声音数据)2byte*/
            public ushort Channels;              /*Channels 1 = 单声道; 2 = 立体声2byte*/
            public uint frequency;               /*采样频率4byte*/
            public uint trans_speed;             /*音频数据传送速率4byte*/
            public ushort dataBlock;             /*数据块的调整数（按字节算的）2byte*/
            public ushort sample_bits;           /*样本的数据位数(8/16) 2byte*/
            public byte[] data;                  /*数据标记符"data" 4byte*/
            public uint wav_len;                 /*语音数据的长度 4byte*/
        }

        private HeaderType wavHander;       //定义一个头结构体
        private byte[] buff = new byte[44]; //header byte
        private byte[] databuff;            //data byte


        /// <summary>
        /// 初始化结构体中的数组长度，分配内存
        /// </summary>
        public void InitialStruct()
        {
            wavHander.riff = new byte[4];//RIFF
            wavHander.wave = new byte[4];//WAVE
            wavHander.fmt = new byte[4];//fmt 
            wavHander.data = new byte[4];//data
        }

        /// <summary>
        /// 为PCM文件构建文件头，准备转换为WAV文件
        /// </summary>
        /// <returns>构建成功返回真</returns>
        public bool InitHeader()
        {
            wavHander.riff = Encoding.ASCII.GetBytes("RIFF");   /*RIFF类资源文件头部 4byte*/
            wavHander.file_len = (uint)(databuff.Length);              /*文件长度4byte*/
            wavHander.wave = Encoding.ASCII.GetBytes("WAVE");     /*"WAVE"标志4byte*/
            wavHander.fmt = Encoding.ASCII.GetBytes("fmt ");      /*"fmt"标志4byte*/
            wavHander.NI1 = 0x10;                               /*过渡字节4byte*/
            wavHander.format_type = 0x01;                       /*格式类别(10H为PCM形式的声音数据)2byte*/
            wavHander.Channels = 0x01;                          /*Channels 1 = 单声道; 2 = 立体声2byte*/
            wavHander.frequency = 0x1F40;                       /*采样频率4byte*/
            wavHander.trans_speed = 0x3E80;                     /*音频数据传送速率4byte*/
            wavHander.dataBlock = 0x02;                         /*数据块的调整数（按字节算的）2byte*/
            wavHander.sample_bits = 0x10;                       /*样本的数据位数(8/16) 2byte*/
            wavHander.data = Encoding.ASCII.GetBytes("data");   /*数据标记符"data" 4byte*/
            wavHander.wav_len = (uint)(databuff.Length - 44);                /*语音数据的长度 4byte*/
            byte[] byt2;//临时变量 ，保存2位的整数
            byte[] byt4;//临时变量， 保存4位的整数
            Encoding.ASCII.GetBytes(Encoding.ASCII.GetString(wavHander.riff), 0, 4, databuff, 0);/*RIFF类资源文件头部 4byte*/
            byt4 = BitConverter.GetBytes(wavHander.file_len); /*文件长度4byte*/
            Array.Copy(byt4, 0, databuff, 4, 4);
            Encoding.ASCII.GetBytes(Encoding.ASCII.GetString(wavHander.wave), 0, 4, databuff, 8);/*"WAVE"标志4byte*/
            Encoding.ASCII.GetBytes(Encoding.ASCII.GetString(wavHander.fmt), 0, 4, databuff, 12);/*"fmt"标志4byte*/
            byt4 = BitConverter.GetBytes(wavHander.NI1);/*过渡字节4byte*/
            Array.Copy(byt4, 0, databuff, 16, 4);
            byt2 = BitConverter.GetBytes(wavHander.format_type);/*格式类别(10H为PCM形式的声音数据)2byte*/
            Array.Copy(byt2, 0, databuff, 20, 2);
            byt2 = BitConverter.GetBytes(wavHander.Channels);/*Channels 1 = 单声道; 2 = 立体声2byte*/
            Array.Copy(byt2, 0, databuff, 22, 2);
            byt4 = BitConverter.GetBytes(wavHander.frequency);/*采样频率4byte*/
            Array.Copy(byt4, 0, databuff, 24, 4);
            byt4 = BitConverter.GetBytes(wavHander.trans_speed);/*音频数据传送速率4byte*/
            Array.Copy(byt4, 0, databuff, 28, 4);
            byt2 = BitConverter.GetBytes(wavHander.dataBlock);/*数据块的调整数（按字节算的）2byte*/
            Array.Copy(byt2, 0, databuff, 32, 2);
            byt2 = BitConverter.GetBytes(wavHander.sample_bits);/*样本的数据位数(8/16) 2byte*/
            Array.Copy(byt2, 0, databuff, 34, 2);
            Encoding.ASCII.GetBytes(Encoding.ASCII.GetString(wavHander.data), 0, 4, databuff, 36);/*数据标记符"data" 4byte*/
            byt4 = BitConverter.GetBytes(wavHander.wav_len); /*语音数据的长度 4byte*/
            Array.Copy(byt4, 0, databuff, 40, 4);
            return true;
        }
        /// <summary>
        /// 读取PCM中数据，
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns>读取成功返回真</returns>
        public bool readPcm(string filepath)
        {
            String fileName = filepath;//临时保存文件名
            if (File.Exists(fileName) == false)//文件不存在
            {
                throw new Exception(ERRFILENOTEXITS);
            }
            //自读方式打开
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
            if (file == null)//打开成功
            {
                file.Close();//关闭文件
                throw new Exception(ERRFILEISNOTWAVE);
            }
            int filelen = (int)file.Length;//获取文件长度
            databuff = new byte[filelen + 44];//分配 内存
            file.Read(databuff, 44, filelen);//读取文件，保存在内存中
            file.Close();//关闭文件
            return true;
        }

        /// <summary>
        /// 读取WAVE文件，包括文件头和数据部分
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns>读取成功返回真</returns>
        public bool readWav(string filepath)
        {
            String fileName = filepath;//保存文件名
            if (File.Exists(fileName) == false)//文件不存在
            {
                throw new Exception(ERRFILENOTEXITS);
            }
            //只读方式打开文件
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
            if (file == null || file.Length < 44) //长度少于44，或者打开失败
            {
                file.Close();//
                throw new Exception(ERRFILEISNOTWAVE);
            }
            file.Read(buff, 0, 44);//读取文件头
            if (fixedData(buff) == false)//按位置保存文件头信息到结构体中
                throw new Exception(ERRFILEISNOTWAVE);
            databuff = new byte[wavHander.wav_len];//分配内存
            try
            {
                file.Read(databuff, 0, databuff.Length);//读取文件数据去数据
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                file.Close();//关闭文件
            }
        }
        /// <summary>
        /// 写文件操作
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <param name="pbuff">文件数据</param>
        public void WriteFile(string filename, byte[] pbuff)
        {
            if (File.Exists(filename) == true)
                File.Delete(filename);
            FileStream sw = File.OpenWrite(filename);
            if (pbuff != null && sw != null)
            {
                sw.Write(pbuff, 0, pbuff.Length);
                sw.Close();
            }
        }

        /// <summary>
        /// 把文件头数组信息保存到结构体中
        /// </summary>
        /// <param name="pbuff">文件头数组</param>
        /// <returns>保存成功返回真</returns>
        bool fixedData(byte[] pbuff)
        {

            Array.Copy(pbuff, 0, wavHander.riff, 0, 4);//
            if (Encoding.ASCII.GetString(wavHander.riff) != "RIFF")//确定文件是WAVA类型
                return false;
            wavHander.file_len = BitConverter.ToUInt32(pbuff, 4);
            Array.Copy(pbuff, 8, wavHander.wave, 0, 4);
            Array.Copy(pbuff, 12, wavHander.fmt, 0, 4);
            wavHander.NI1 = BitConverter.ToUInt32(pbuff, 16);
            wavHander.format_type = BitConverter.ToUInt16(pbuff, 20);
            wavHander.Channels = BitConverter.ToUInt16(pbuff, 22);
            wavHander.frequency = BitConverter.ToUInt32(pbuff, 24);
            wavHander.trans_speed = BitConverter.ToUInt32(pbuff, 28);
            wavHander.dataBlock = BitConverter.ToUInt16(pbuff, 32);
            wavHander.sample_bits = BitConverter.ToUInt16(pbuff, 34);
            Array.Copy(pbuff, 36, wavHander.data, 0, 4);
            wavHander.wav_len = BitConverter.ToUInt32(pbuff, 40);
            return true;
        }
    }
}
