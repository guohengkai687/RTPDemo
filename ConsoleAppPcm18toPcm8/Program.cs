using NAudio.Wave;
using System;

namespace ConsoleAppPcm18toPcm8
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            string inPutPath = Environment.CurrentDirectory + "\\Wav\\testRtp1-pcm16.wav";
            string outPutPath = Environment.CurrentDirectory + "\\Wav\\testRtp1-pcm8.wav";

            WavPcm16ToPcm8(inPutPath, outPutPath);
        }

        public static void WavPcm16ToPcm8(string outPutPath_pcm16, string outPutPath)
        {
            using (WaveFileReader reader = new WaveFileReader(outPutPath_pcm16))
            {
                var newFormat = new WaveFormat(8000, 8, 1);
                using (var conversionStream = new WaveFormatConversionStream(newFormat, reader))
                {
                    WaveFileWriter.CreateWaveFile(outPutPath, conversionStream);
                }
            }

        }
    }
}
