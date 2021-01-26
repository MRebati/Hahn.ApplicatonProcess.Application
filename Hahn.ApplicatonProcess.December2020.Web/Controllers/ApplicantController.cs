using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Services.ApplicantService;
using Hahn.ApplicationProcess.December2020.Domain.Services.ApplicantService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicationProcess.December2020.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        #region props
        private readonly IApplicantService _applicantService;
        private readonly ILogger<ApplicantController> _logger; 
        #endregion

        #region ctor

        public ApplicantController(
    IApplicantService applicantService,
    ILogger<ApplicantController> logger)
        {
            _applicantService = applicantService;
            _logger = logger;
        } 

        #endregion

        /// <summary>
        /// Returns an Applicants by specific ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/applicant/{id}
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApplicantResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                LogMethodFired(nameof(_applicantService.GetApplicant));
                var applicant = await _applicantService.GetApplicant(id);
                return Ok(applicant);
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(_applicantService.GetApplicant));
                return StatusCode(500, ex);
            }
        }

        

        /// <summary>
        /// Returns a list of Applicants
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/applicant
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ApplicantResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                LogMethodFired(nameof(_applicantService.GetApplicantList));
                var items = await _applicantService.GetApplicantList();
                return Ok(items);
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(_applicantService.GetApplicantList));
                return StatusCode(500, ex);
            }
        }

        ///<summary>
        /// Creates an applicant.
        ///</summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/applicant {
        ///     Name,
        ///     FamilyName,
        ///     EmailAddress,
        ///     CountryOfOrigin,
        ///     Address,
        ///     Age,
        ///     Hired
        /// }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ApplicantResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(ApplicantInputDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }

                LogMethodFired(nameof(_applicantService.InsertApplicant));
                var result = _applicantService.InsertApplicant(model);
                return Ok(StatusCode(201, result));
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(_applicantService.InsertApplicant));
                return StatusCode(500, ex);
            }
        }

        ///<summary>
        /// Updates an applicant.
        ///</summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/applicant {
        ///     Name,
        ///     FamilyName,
        ///     EmailAddress,
        ///     CountryOfOrigin,
        ///     Address,
        ///     Age,
        ///     Hired
        /// }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, ApplicantInputDto model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest();

                if (!ModelState.IsValid)
                {
                    return ValidationProblem(ModelState);
                }

                LogMethodFired(nameof(_applicantService.UpdateApplicant));
                var result = await _applicantService.UpdateApplicant(model);
                return Ok(StatusCode(201, result));
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(_applicantService.UpdateApplicant));
                return StatusCode(500, ex);
            }
        }

        ///<summary>
        /// Deletes an applicant by ID.
        ///</summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /api/applicant/{id}
        /// </remarks>
        /// <param name="id">id of the applicant</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                LogMethodFired(nameof(_applicantService.DeleteApplicant));
                var result = await _applicantService.DeleteApplicant(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(_applicantService.DeleteApplicant));
                return StatusCode(500, ex);
            }
        }

        #region private methods
        
        private void LogError(Exception exception, string methodName)
        {
            _logger.LogError("an error occurred on action \"{methodName}\" on datetime: {date}, Ex: {ex}", methodName,DateTime.Now, exception);
        }

        private void LogMethodFired(string methodName)
        {
            _logger.LogError("\"{methodName}\" fired on datetime: {date}", methodName,DateTime.Now);
        }
        
        #endregion
    }
}
