using System.Collections.Generic;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Infrastructure;
using Hahn.ApplicationProcess.December2020.Domain.Services.ApplicantService.Dto;

namespace Hahn.ApplicationProcess.December2020.Domain.Services.ApplicantService
{
    public interface IApplicantService: IBaseService
    {
        Task<ApplicantResponseDto> GetApplicant(int applicantId);

        Task<ApplicantResponseDto> InsertApplicant(ApplicantInputDto model);
        
        Task<ApplicantResponseDto> UpdateApplicant(ApplicantInputDto model);

        Task<bool> DeleteApplicant(int applicantId);

        Task<List<ApplicantResponseDto>> GetApplicantList();
    }
}