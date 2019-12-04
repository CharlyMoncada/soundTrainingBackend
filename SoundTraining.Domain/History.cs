namespace SoundTraining.Domain
{
    using System;

    public class History
    {
        public int ID { get; set; }

        public int Frequency { get; set; }

        public int UserID { get; set; }

        public AppTypes AppType { get; set; }

        public DateTime DateSaved { get; set; }
    }
}
