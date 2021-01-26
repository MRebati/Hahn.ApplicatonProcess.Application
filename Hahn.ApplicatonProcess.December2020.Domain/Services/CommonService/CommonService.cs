using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Hahn.ApplicationProcess.December2020.Domain.Configuration;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Infrastructure;
using Hahn.ApplicationProcess.December2020.Domain.Utilities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicationProcess.December2020.Domain.Services.CommonService
{
    public class CommonService : BaseService, ICommonService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CommonService> _logger;
        private readonly ICacheManagement _cacheManagement;

        public CommonService(
            IConfiguration configuration, 
            ILogger<CommonService> logger, 
            ICacheManagement cacheManagement)
        {
            _configuration = configuration;
            _logger = logger;
            _cacheManagement = cacheManagement;
        }

        public async Task<Country> GetCountryByName(string countryName)
        {
            var apiUrl = _configuration.GetValue<string>(AppConst.ExternalApis.CountryCheck.Url);
            var response = await apiUrl
                .AllowAnyHttpStatus()
                .AppendPathSegment(countryName)
                .SetQueryParam("fullText",true)
                .GetAsync();

            if (response.StatusCode < 300)
            {
                var result = await response.GetJsonAsync<List<Country>>();
                _logger.LogDebug("get country by name: ");
                Console.WriteLine($"Success! {result}");
                return result.FirstOrDefault();
            }
            else if (response.StatusCode < 500)
            {
                var error = await response.GetJsonAsync<ExternalServiceHttpResponse>();
                Console.WriteLine($"You did something wrong! {error}");
            }
            else
            {
                var error = await response.GetJsonAsync<ExternalServiceHttpResponse>();
                Console.WriteLine($"We did something wrong! {error}");
            }
            return null;
        }
        
        public List<Country> GetAllCountries()
        {
            var result = new List<Country>();

            var apiUrl = _configuration.GetValue<string>(AppConst.ExternalApis.CountryCheck.Url);
            var response = apiUrl
                .AllowAnyHttpStatus()
                .GetAsync().Result;

            if (response.StatusCode < 300)
            {
                result = response.GetJsonAsync<List<Country>>().Result;
                _logger.LogDebug("get country by name: ");
                Console.WriteLine($"Success! {result}");
                return result.ToList();
            }
            else if (response.StatusCode < 500)
            {
                var error = response.GetJsonAsync<ExternalServiceHttpResponse>();
                Console.WriteLine($"You did something wrong! {error}");
            }
            else
            {
                var error = response.GetJsonAsync<ExternalServiceHttpResponse>().Result;
                Console.WriteLine($"We did something wrong! {error}");
            }
            return null;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return _cacheManagement.CacheCall<>(
                AppConst.Cache.CountryList, 
                AppConst.Cache.DefaultTimeoutInSeconds, 
                 GetAllCountriesFromApi);
        }

        private async Task<List<Country>> GetAllCountriesFromApi()
        {
            var apiUrl = _configuration.GetValue<string>(AppConst.ExternalApis.CountryCheck.Url);
            var response = await apiUrl
                .AllowAnyHttpStatus()
                .GetAsync();

            if (response.StatusCode < 300)
            {
                var result = await response.GetJsonAsync<List<Country>>();
                _logger.LogDebug("get country by name: ");
                Console.WriteLine($"Success! {result}");
                return result.ToList();
            }
            else if (response.StatusCode < 500)
            {
                var error = await response.GetJsonAsync<ExternalServiceHttpResponse>();
                Console.WriteLine($"You did something wrong! {error}");
            }
            else
            {
                var error = await response.GetJsonAsync<ExternalServiceHttpResponse>();
                Console.WriteLine($"We did something wrong! {error}");
            }
            return null;
        }

    }

    public interface ICommonService
    {
        Task<Country> GetCountryByName(string countryName);
        List<Country> GetAllCountries();
        Task<List<Country>> GetAllCountriesAsync();
    }
}
