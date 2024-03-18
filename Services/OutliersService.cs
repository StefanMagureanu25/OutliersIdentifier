using CsvHelper;
using CsvHelper.Configuration;
using OutliersIdentifier.DTOs;
using OutliersIdentifier.Helpers.Enums;
using OutliersIdentifier.Models;
using OutliersIdentifier.Services.Interfaces;
using System.Globalization;
using System.Security.AccessControl;

namespace OutliersIdentifier.Services
{
    public class OutliersService : IOutliersService
    {
        private IEnumerable<DataPoint> ReadFileCsv(string fileName, string timestamp)
        {
            var dataConfig = new CsvConfiguration(CultureInfo.InvariantCulture) { HeaderValidated = null, MissingFieldFound = null };
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, dataConfig))
            {
                var dataPoints = csv.GetRecords<DataPoint>().ToList()
                    .Where(x => DateTime.ParseExact(x.Timestamp, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                    >= DateTime.ParseExact(timestamp, "dd-MM-yyyy", CultureInfo.InvariantCulture)).Take(30);
                return dataPoints;
            }
        }
        private void WriteFileCsv(string fileName, IEnumerable<OutlierFormat> dataPoints)
        {
            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(dataPoints);
            }
        }
        public Dictionary<string, IEnumerable<DataPoint>> GetFirst30DataPoints(CSVExtractRequest request)
        {
            Dictionary<string, IEnumerable<DataPoint>> dataResult = new Dictionary<string, IEnumerable<DataPoint>>();
            string[] subDirectories = Directory.GetDirectories(Constants.StockExchangeDirectory);
            foreach (string subDirectory in subDirectories)
            {
                string[] files = Directory.GetFiles(subDirectory);
                for (int i = 0; i < Math.Min(request.NumberOfStockFiles, files.Length); i++)
                {
                    var dataPoints = ReadFileCsv(files[i], request.Timestamp);
                    if (dataPoints.Count() == 30)
                    {
                        dataResult.Add(files[i], dataPoints);
                    }
                }
            }
            return dataResult;
        }
        public Dictionary<string, IEnumerable<DataPoint>> GetOutliers(CSVExtractRequest request)
        {
            Dictionary<string, IEnumerable<DataPoint>> dataSets = GetFirst30DataPoints(request);
            Dictionary<string, IEnumerable<DataPoint>> outliersResult = new Dictionary<string, IEnumerable<DataPoint>>();
            foreach (var dataSet in dataSets)
            {
                List<DataPoint> outliers = new List<DataPoint>();
                double mean = CalculateMean(dataSet.Value);
                double standardDeviation = CalculateStandardDeviation(dataSet.Value, mean);
                double threshold = mean + 2 * standardDeviation;
                foreach (var dataPoint in dataSet.Value)
                {
                    double stockPrice = double.Parse(dataPoint.StockPrice, CultureInfo.InvariantCulture);
                    if (stockPrice >= threshold)
                    {
                        outliers.Add(dataPoint);
                    }
                }
                if (outliers.Count() > 0)
                {
                    outliersResult.Add(dataSet.Key, outliers);
                    List<OutlierFormat> outputOutliers = new List<OutlierFormat>();
                    foreach (var outlier in outliers)
                    {
                        double stockPrice = double.Parse(outlier.StockPrice, CultureInfo.InvariantCulture);
                        double difference = stockPrice - mean;
                        double deviation = stockPrice - threshold;
                        double percentageDeviation = (deviation / threshold) * 100;
                        OutlierFormat outlierStream = new OutlierFormat(outlier, mean, difference, percentageDeviation);
                        outputOutliers.Add(outlierStream);
                    }
                    string filePath = Path.Combine(Constants.StockExchangeOutliersDirectory, dataSet.Key);

                    WriteFileCsv(filePath, outputOutliers);
                }
            }
            return outliersResult;
        }
        private double CalculateMean(IEnumerable<DataPoint> dataPoints)
        {
            double sum = 0;
            foreach (var dataPoint in dataPoints)
            {
                double stockPrice = double.Parse(dataPoint.StockPrice, CultureInfo.InvariantCulture);
                sum += stockPrice;
            }
            return (double)sum / dataPoints.Count();
        }

        private double CalculateStandardDeviation(IEnumerable<DataPoint> dataPoints, double mean)
        {
            double sumOfSquares = 0;
            foreach (var dataPoint in dataPoints)
            {
                double stockPrice = double.Parse(dataPoint.StockPrice, CultureInfo.InvariantCulture);
                sumOfSquares += Math.Pow(stockPrice - mean, 2);
            }
            double variance = sumOfSquares / dataPoints.Count();
            return Math.Sqrt(variance);
        }
    }
}
