using System.Collections.Generic;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Entities;

namespace Hahn.ApplicationProcess.December2020.Domain.Services.CommonService
{
    public interface ICommonService
    {
        /// <summary>
        /// returns a country data [if exist]
        /// </summary>
        /// <param name="countryName">Name of the country</param>
        /// <returns><see cref="Task{Country}"/> where TResult is <see cref="Country"/></returns>
        Task<Country> GetCountryByNameAsync(string countryName);

        /// <summary>
        /// returns all countries which exist on the API
        /// </summary>
        /// <returns><see cref="List{Country}"/> where T is <see cref="Country"/> ( As a <see cref="Task{TResult}"/>)</returns>
        Task<List<Country>> GetAllCountriesAsync();
    }
}