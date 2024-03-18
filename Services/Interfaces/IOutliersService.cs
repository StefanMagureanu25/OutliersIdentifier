using OutliersIdentifier.DTOs;
using OutliersIdentifier.Models;

namespace OutliersIdentifier.Services.Interfaces
{
    /// <summary>
    /// Interface for creating our service methods required for retrieving required data
    /// </summary>
    public interface IOutliersService
    {
        Dictionary<string, IEnumerable<DataPoint>> GetFirst30DataPoints(CSVExtractRequest request);
        Dictionary<string, IEnumerable<DataPoint>> GetOutliers(CSVExtractRequest input);
    }
}
