using OutliersIdentifier.DTOs;
using OutliersIdentifier.Models;

namespace OutliersIdentifier.Services.Interfaces
{
    public interface IOutliersService
    {
        Dictionary<string, IEnumerable<DataPoint>> GetFirst30DataPoints(CSVExtractRequest request);
        Dictionary<string, IEnumerable<DataPoint>> GetOutliers(CSVExtractRequest input);
    }
}
