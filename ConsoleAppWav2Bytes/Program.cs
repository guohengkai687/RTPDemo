using NAudio.Wave;
using System;
using System.IO;
using System.Text;

namespace ConsoleAppWav2Bytes
{
    class Program
    {
        private static FileHelper fileHelper = new FileHelper();
        static void Main(string[] args)
        {
            uint a = 0xFFFFFFFF;
            a = a + 1;

            //Console.WriteLine("Hello World!");
            //string wavPath = Environment.CurrentDirectory + "\\Wav\\EmptyVoice1ms.pcm";
            //FileInfo fileInfo = new FileInfo(wavPath);
            //if (fileInfo.Exists)
            //{
            //    FileStream fs = new FileStream(wavPath, FileMode.Open, FileAccess.Read, FileShare.None);

            //    //获取文件大小
            //    long size = fs.Length;
            //    byte[] array = new byte[size];

            //    //将文件读到byte数组中
            //    fs.Read(array, 0, array.Length);

            //    fs.Close();

            //}
            fileHelper.InitialStruct();


            string path_pcm = Environment.CurrentDirectory + "\\Wav\\testRtp8.wav";
            string path_wav = Environment.CurrentDirectory + "\\Wav\\testRtp8 (2).wav";


            using (var reader1 = new WaveFileReader(path_wav))
            {

            }

            using (var reader2 = new FileStream(path_pcm, FileMode.Open, FileAccess.Read, FileShare.None))
            {

            }


        }
    }
}
