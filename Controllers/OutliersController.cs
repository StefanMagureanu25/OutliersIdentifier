using Microsoft.AspNetCore.Mvc;
using OutliersIdentifier.DTOs;
using OutliersIdentifier.Exceptions;
using OutliersIdentifier.Services.Interfaces;
using System.Net;

namespace OutliersIdentifier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutliersController : ControllerBase
    {
        IOutliersService _outliersService;
        public OutliersController(IOutliersService outliersService)
        {
            _outliersService = outliersService;
        }
        [HttpGet]
        [Route("dataPoints")]
        public IActionResult GetDataPoints([FromQuery] CSVExtractRequest request)
        {
            try
            {
                var dataPoints = _outliersService.GetFirst30DataPoints(request);
                return Ok(dataPoints);
            }
            catch (InsufficientDataPointsException ex)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("outliers")]
        public IActionResult GetOutliers([FromQuery] CSVExtractRequest request)
        {
            try
            {
                var outliers = _outliersService.GetOutliers(request);
                return Ok(outliers);
            }
            catch (InsufficientDataPointsException ex)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
