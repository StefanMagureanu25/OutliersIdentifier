using OutliersIdentifier.Models;
using System.ComponentModel.DataAnnotations;

namespace OutliersIdentifier.DTOs
{
    /// <summary>
    /// Data Transfer Object used for successfully adding outliers into the csv files.
    /// </summary>
    public class OutlierFormat
    {
        [Required]
        public DataPoint DataPoint { get; set; }
        [Required]
        public double Mean { get; set; }

        /// <summary>
        /// actual stock price - mean
        /// </summary>
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
