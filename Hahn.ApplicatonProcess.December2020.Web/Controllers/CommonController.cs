using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Services.CommonService;
using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicationProcess.December2020.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController: ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        /// <summary>
        /// Returns a country by name [if exists]
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/common/country/germany
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpGet("country/{countryName}")]
        public async Task<IActionResult> GetCountryName(string countryName)
        {
            var result = await _commonService.GetCountryByNameAsync(countryName);
            return Ok(result);
        }

        /// <summary>
        /// Returns list of all countries
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/common/all-countries
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpGet("all-countries")]
        public async Task<IActionResult> GetAllValidCountries()
        {
            var result = await _commonService.GetAllCountriesAsync();
            var namesOnly = result.Select(x => 
                new
                {
                    x.Name, 
                    x.NativeName
                }).ToList();
            return Ok(namesOnly);
        }
    }
}