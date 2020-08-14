
using System;
using System.IO;

namespace ConsoleAppMixAudio
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            string path1 = Environment.CurrentDirectory + "\\Voice\\qqqqqqq.001In";
            string path2 = Environment.CurrentDirectory + "\\Voice\\qqqqqqq.001Out";

            string path3 = Environment.CurrentDirectory + "\\Voice\\123.pcm16";
            FileInfo fileInfo = new FileInfo(path3);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            //AudioMixer.AudioMixing(path1, path2, path3);

            byte[] array_In = null;
            FileInfo fileInfo_In = new FileInfo(path1);
            if (fileInfo_In.Exists)
            {
                FileStream fs = new FileStream(path1, FileMode.Open, FileAccess.Read, FileShare.None);

                //获取文件大小
                long size = fs.Length;
                array_In = new byte[size];

                //将文件读到byte数组中
                fs.Read(array_In, 0, array_In.Length);

                fs.Close();

            }

            byte[] array_Out = null;
            FileInfo fileInfo_Out = new FileInfo(path2);
            if (fileInfo_Out.Exists)
            {
                FileStream fs = new FileStream(path2, FileMode.Open, FileAccess.Read, FileShare.None);

                //获取文件大小
                long size = fs.Length;
                array_Out = new byte[size];

                //将文件读到byte数组中
                fs.Read(array_Out, 0, array_Out.Length);

                fs.Close();

            }

            if (array_In != null && array_Out != null)
            {
                byte[] src = AudioMixer.DataMixing(array_In, array_Out);

                FileStream fs = new FileStream(path3, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                fs.Write(src, 0, src.Length);
                fs.Close();
            }
        }
        
    }
    
}
