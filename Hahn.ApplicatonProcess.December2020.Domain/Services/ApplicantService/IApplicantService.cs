using System.Collections.Generic;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Infrastructure;
using Hahn.ApplicationProcess.December2020.Domain.Services.ApplicantService.Dto;

namespace Hahn.ApplicationProcess.December2020.Domain.Services.ApplicantService
{
    public interface IApplicantService: IBaseService
    {
        /// <summary>
        /// get applicant list
        /// </summary>
        /// <returns><see cref="List{T}"/> where T is <seealso cref="ApplicantResponseDto"/></returns>
        Task<List<ApplicantResponseDto>> GetApplicantList();

        /// <summary>
        /// get an applicant by id
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns><seealso cref="ApplicantResponseDto"/></returns>
        Task<ApplicantResponseDto> GetApplicant(int applicantId);

        /// <summary>
        /// insert an applicant
        /// </summary>
        /// <param name="model"><see cref="ApplicantInputDto"/></param>
        /// <returns><seealso cref="ApplicantResponseDto"/> with Id on database</returns>
        Task<ApplicantResponseDto> InsertApplicant(ApplicantInputDto model);
        
        /// <summary>
        /// update an applicant
        /// </summary>
        /// <param name="model"><see cref="ApplicantInputDto"/></param>
        /// <returns><seealso cref="ApplicantResponseDto"/> with updated properties</returns>
        Task<ApplicantResponseDto> UpdateApplicant(ApplicantInputDto model);

        /// <summary>
        /// delete an applicant
        /// </summary>
        /// <param name="applicantId"></param>
        /// <returns><seealso cref="bool"/>, if the operation is successful returns true else returns false</returns>
        Task<bool> DeleteApplicant(int applicantId);
    }
}