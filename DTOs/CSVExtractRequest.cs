using System.ComponentModel.DataAnnotations;

namespace OutliersIdentifier.DTOs
{
    /// <summary>
    /// Data Transfer Object used for extracting 30 data points starting from a specific timestamp
    /// </summary>
    public class CSVExtractRequest
    {
        [Required]
        [Range(1,2, ErrorMessage = "You must enter a number of stock files to be processed between 1 and 2!!")]
        public int NumberOfStockFiles { get; set; }

        [Required]
        public string Timestamp { get; set; }

    }
}
