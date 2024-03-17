using OutliersIdentifier.Models;
using System.ComponentModel.DataAnnotations;

namespace OutliersIdentifier.DTOs
{
    public class OutlierFormat
    {
        [Required]
        public DataPoint DataPoint { get; set; }
        [Required]
        public double Mean { get; set; }

        // actual stock price - mean
        [Required]
        public double Difference { get; set; }
        [Required]
        public double PercentageDeviation { get; set; }

        public OutlierFormat(DataPoint dataPoint, double mean, double difference, double percentageDeviation)
        {
            DataPoint = dataPoint;
            Mean = mean;
            Difference = difference;
            PercentageDeviation = percentageDeviation;
        }
    }
}
