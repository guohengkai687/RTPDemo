using System;
using System.Collections.Generic;
using System.Text;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace ConsoleAppMixAudio
{
    public static class AudioMixer
    {
        /// <summary>
        /// 音频混合（混音）
        /// </summary>
        /// <param name="filePath1">音频文件路径</param>
        /// <param name="filePath2">音频文件路径</param>
        /// <param name="mixedPath">混合音频文件路径</param>
        public static void AudioMixing(string filePath1, string filePath2, string mixedPath)
        {
            using (var reader1 = new AudioFileReader(filePath1))
            using (var reader2 = new AudioFileReader(filePath2))
            {
                var mixer = new MixingSampleProvider(new[] { reader1, reader2 });
                WaveFileWriter.CreateWaveFile16(mixedPath, mixer);
            }
        }

        internal static byte[] DataMixing(byte[] array_In, byte[] array_Out)
        {
            byte[] maxb = null;
            byte[] minb = null;
            byte[] src = null;
            if (array_In.Length > array_Out.Length)
            {
                maxb = array_In;
                minb = array_Out;
            }
            else
            {
                maxb = array_Out;
                minb = array_In;
            }
            src = new byte[maxb.Length];

            for (int i = 0; i < maxb.Length; i++)
            {
                if (i < minb.Length)
                {
                    src[i] = (byte)(maxb[i] + minb[i]);
                }
                else
                {
                    src[i] = maxb[i];
                }
            }

            return src;
        }
    }
}
