using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Extensions;
using Hahn.ApplicationProcess.December2020.Domain.Infrastructure;
using Hahn.ApplicationProcess.December2020.Domain.Services.ApplicantService.Dto;
using Hahn.ApplicationProcess.December2020.Domain.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Hahn.ApplicationProcess.December2020.Domain.Services.ApplicantService
{
    public class ApplicantService : BaseService, IApplicantService
    {
        #region props

        private readonly IManagementUnitOfWork _managementUnitOfWork;
        private readonly ILogger<ApplicantService> _logger; 
        
        #endregion

        #region ctor

        public ApplicantService(
    IManagementUnitOfWork managementUnitOfWork,
    ILogger<ApplicantService> logger)
        {
            _managementUnitOfWork = managementUnitOfWork;
            _logger = logger;
        } 
        
        #endregion

        public async Task<List<ApplicantResponseDto>> GetApplicantList()
        {
            try
            {
                var applicantsResult = await _managementUnitOfWork.ApplicantRepository.GetAllAsync();
                _logger.LogInformation("getting applicant list was successful");
                return applicantsResult.MapToList<ApplicantResponseDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"getting applicant list failed");
                throw;
            }
        }

        public async Task<ApplicantResponseDto> GetApplicant(int applicantId)
        {
            try
            {
                var result = await _managementUnitOfWork.ApplicantRepository.GetByIdAsync(applicantId);
                return result.MapTo<ApplicantResponseDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"getting applicant with id: {applicantId} failed", applicantId);
                throw;
            }
        }

        public async Task<ApplicantResponseDto> InsertApplicant(ApplicantInputDto model)
        {
            try
            {
                var applicantModel = model.MapTo<Applicant>();
                applicantModel = _managementUnitOfWork.ApplicantRepository.Create(applicantModel);
                await _managementUnitOfWork.SaveAsync();
                _logger.LogInformation("insert applicant was successful, applicantId: {applicantId}", applicantModel.Id);
                return applicantModel.MapTo<ApplicantResponseDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "inserting applicant failed");
                throw;
            }
        }

        public async Task<ApplicantResponseDto> UpdateApplicant(ApplicantInputDto model)
        {
            try
            {
                var applicantModel = model.MapTo<Applicant>();

                _managementUnitOfWork.ApplicantRepository.Update(applicantModel);
                await _managementUnitOfWork.SaveAsync();
                _logger.LogInformation("update applicant was successful, applicantId: {applicantId}", applicantModel.Id);
                return applicantModel.MapTo<ApplicantResponseDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "updating applicant failed, applicantId: {applicantId}", model.Id);
                throw;
            }
        }

        public async Task<bool> DeleteApplicant(int applicantId)
        {
            try
            {
                _managementUnitOfWork.ApplicantRepository.Delete(applicantId);
                await _managementUnitOfWork.SaveAsync();
                _logger.LogInformation("deleting applicant was successful, applicantId: {applicantId}", applicantId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"insert applicant failed, applicantId: {applicantId}", applicantId);
                throw;
            }
        }
    }
}
