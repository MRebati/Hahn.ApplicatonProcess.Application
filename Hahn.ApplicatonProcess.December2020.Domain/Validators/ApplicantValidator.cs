using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Services.ApplicantService.Dto;
using Hahn.ApplicationProcess.December2020.Domain.Services.CommonService;

namespace Hahn.ApplicationProcess.December2020.Domain.Validators
{
    public class ApplicantValidator : AbstractValidator<ApplicantInputDto> {
        private readonly ICommonService _commonService;

        public ApplicantValidator(ICommonService commonService) {
            _commonService = commonService;

            RuleFor(x => x.Name).NotEmpty().WithMessage("please specify a name (first name)").MinimumLength(5).WithMessage("name must be at least 5 characters long");
            RuleFor(x => x.FamilyName).NotEmpty().WithMessage("please specify a family name (last name)").MinimumLength(5).WithMessage("family name must be at least 5 characters long");
            RuleFor(x => x.Age).NotEmpty().InclusiveBetween(20, 60).WithMessage("age must be between 20 to 60");
            //RuleFor(x => x.Hired).NotEmpty();
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Email Address must be in valid form");
            RuleFor(x => x.Address).NotEmpty().MinimumLength(10).WithMessage("Address must be at least 10 characters long");
            RuleFor(x => x.CountryOfOrigin).NotEmpty().MustAsync((x, y, z) =>
            {
                return BeAValidCountry(y);
            });
        }

        private async Task<bool> BeAValidCountry(string countryOfRegion)
        {

            countryOfRegion = countryOfRegion.ToLower();
            var countries = await _commonService.GetAllCountriesAsync();
            return countries.Any(x => x.Name.ToLower() == countryOfRegion);
        }
    }
}
