using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundTraining.Provider
{
    public class SoundGenerator
    {
        public void jajaj()
        {
            var provider = new WaveFormat();
            var waveOut = new WaveOutEvent();
            //waveOut.Init(new ISampleProvider[] { new });
            var sound = new WaveFormat(4000, 1);

            var devices = waveOut.DeviceNumber;

            var offset = CreateSignalGenerator(500, 1);
            waveOut.Init(offset.ToWaveProvider());
            waveOut.Play();

            int bytesRead;
            var buffer = new float[800000 * 2];
            long total = 0;
            while ((bytesRead = offset.Read(buffer, 0, buffer.Length)) > 0)
            {
                total += bytesRead;
            }



            //var resampler = new WdlResamplingSampleProvider(offset, 5400);
            //var buffer = new float[5400 * 1];
            //Debug.WriteLine(String.Format("From {0} to {1}", 4400, 5400));
            //for (int n = 0; n < 10; n++)
            //{
            //    var read = offset.Read(buffer, 0, buffer.Length);
            //    Debug.WriteLine(String.Format("read {0}", read));
            //}

        }

        private static SignalGenerator CreateSignalGenerator(int freq, int channels)
        {
            var signalGenerator = new SignalGenerator();
            signalGenerator.Type = SignalGeneratorType.Pink;
            signalGenerator.SweepLengthSecs = 800000;
            //signalGenerator.Frequency = freq;
            signalGenerator.Gain = 1f;
            return signalGenerator;
        }
    }
}
