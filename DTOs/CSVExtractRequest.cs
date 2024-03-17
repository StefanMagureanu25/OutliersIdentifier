using System.ComponentModel.DataAnnotations;

namespace OutliersIdentifier.DTOs
{
    public class CSVExtractRequest
    {
        [Required]
        [Range(1,2, ErrorMessage = "You must enter a number of stock files to be processed between 1 and 2!!")]
        public int NumberOfStockFiles { get; set; }

        [Required]
        public string Timestamp { get; set; }

    }
}
