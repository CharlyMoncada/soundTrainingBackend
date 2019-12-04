namespace SoundTraining.Domain
{
    using System.Collections.Generic;

    public class FrequencyByUser
    {
        public int UserID { get; set; }

        public List<FrequencyPercentaje> Frequency { get; set; }
    }
}
