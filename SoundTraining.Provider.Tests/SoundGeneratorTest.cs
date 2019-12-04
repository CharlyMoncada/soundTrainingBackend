using NAudio.Wave;
using NUnit.Framework;
using SoundTraining.Provider;

namespace SoundTraining.Provider.Tests
{
    [TestFixture]
    public class SoundGeneratorTest
    {
        private SoundGenerator soundGenerator;
        //private IWaveProvider provider;

        [SetUp]
        public void Setup()
        {
            //provider = new WaveProvider16();
            soundGenerator = new SoundGenerator();
        }

        [Test]
        public void SoundGeneratorTestShould()
        {
            soundGenerator.jajaj();

        }
    }
}
