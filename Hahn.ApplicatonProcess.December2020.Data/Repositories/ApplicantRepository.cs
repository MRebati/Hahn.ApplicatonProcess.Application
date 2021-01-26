using Hahn.ApplicationProcess.December2020.Data.Infrastructure;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.December2020.Data.Repositories
{
    public class ApplicantRepository: Repository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(DbContext context) : base(context)
        {
        }
    }
}
