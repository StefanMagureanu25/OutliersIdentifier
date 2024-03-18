using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using OutliersIdentifier.DTOs;
using OutliersIdentifier.Models;
using OutliersIdentifier.Services.Interfaces;
using System.Net;

namespace OutliersIdentifier.Controllers
{
    /// <summary>
    /// Controller for retrieving data points and identifying outliers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OutliersController : ControllerBase
    {
        private readonly IOutliersService _outliersService;

        public OutliersController(IOutliersService outliersService)
        {
            _outliersService = outliersService;
        }

        /// <summary>
        /// Retrieves the first 30 data points based on the specified criteria.
        /// I also added a sample response for better visualization of the output.
        /// </summary>
        /// <param name="request">CSV extract request containing the data points starting from a specific timestamp.</param>
        /// <returns>The first 30 data points.</returns>
        [HttpGet]
        [Route("dataPoints")]
        [ProducesResponseType(typeof(Dictionary<string,IEnumerable<DataPoint>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.RequestUriTooLong)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetDataPoints([FromQuery] CSVExtractRequest request)
        {
            try
            {
                var dataPoints = _outliersService.GetFirst30DataPoints(request);
                return Ok(dataPoints);
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (PathTooLongException ex)
            {
                return StatusCode((int)HttpStatusCode.RequestUriTooLong, ex.Message);
            }
            catch (CsvHelperException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (OutOfMemoryException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves outliers based on the specified criteria.
        /// I also added a sample response for better visualization of the output.
        /// </summary>
        /// <param name="request">CSV extract request containing the data points starting from a specific timestamp.</param>
        /// <returns>The outliers identified.</returns>
        [HttpGet]
        [Route("outliers")]
        [ProducesResponseType(typeof(Dictionary<string, IEnumerable<DataPoint>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.RequestUriTooLong)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetOutliers([FromQuery] CSVExtractRequest request)
        {
            try
            {
                var outliers = _outliersService.GetOutliers(request);
                return Ok(outliers);
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (PathTooLongException ex)
            {
                return StatusCode((int)HttpStatusCode.RequestUriTooLong, ex.Message);
            }
            catch (CsvHelperException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (OutOfMemoryException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
