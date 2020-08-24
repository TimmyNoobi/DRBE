using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRBE
{
    public class Receiver
    {
        public double ID = 0;
        public double Center_freq = 0;
        public double Bandwidth = 0;
        public double Pulsewidth = 0;
        public double Pulse_repetition_interval = 0;
        public double Coherent_processing_interval = 0;
        public double Sample_period = 0;
        public double Fractional_sample_period = 0;
        public double Update_period = 0;

        public List<string> Property_value = new List<string>();
        public List<string> Property_string = new List<string>();
        public Receiver(List<double> x)
        {
            ID = x[0];
            Center_freq = x[1];
            Bandwidth = x[2];
            Pulsewidth = x[3];
            Pulse_repetition_interval = x[4];
            Coherent_processing_interval = x[5];
            Sample_period = x[6];
            Fractional_sample_period = x[7];
            Update_period = x[8];

            Edit_pstring();
            Edit_pvalue();
        }

        private void Edit_pstring()
        {
            Property_string = new List<string>();
            Property_string.Add("ID:         ");
            Property_string.Add("Center_freq:  ");
            Property_string.Add("Bandwidth: ");
            Property_string.Add("Pulsewidth: ");
            Property_string.Add("Pulse_repetition_interval: ");
            Property_string.Add("Coherent_processing_interval:  ");
            Property_string.Add("Sample_period: ");
            Property_string.Add("Fractional_sample_period: ");
            Property_string.Add("Update_period: ");
            //Property_string.Add("Acceleration X");
            //Property_string.Add("Acceleration Y");
            //Property_string.Add("Acceleration Z");

        }

        private void Edit_pvalue()
        {
            Property_value.Add(ID.ToString());
            Property_value.Add(Center_freq.ToString());
            Property_value.Add(Bandwidth.ToString());
            Property_value.Add(Pulsewidth.ToString());
            Property_value.Add(Pulse_repetition_interval.ToString());
            Property_value.Add(Coherent_processing_interval.ToString());
            Property_value.Add(Sample_period.ToString());
            Property_value.Add(Fractional_sample_period.ToString());
            Property_value.Add(Update_period.ToString());
        }
    }
}
