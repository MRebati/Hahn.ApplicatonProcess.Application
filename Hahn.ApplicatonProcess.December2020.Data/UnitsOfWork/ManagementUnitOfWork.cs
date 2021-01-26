using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Data.Infrastructure;
using Hahn.ApplicationProcess.December2020.Domain.Repositories;
using Hahn.ApplicationProcess.December2020.Domain.UnitsOfWork;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.December2020.Data.UnitsOfWork
{
    public class ManagementUnitOfWork: UnitOfWork, IManagementUnitOfWork
    {
        public ManagementUnitOfWork(DbContext context, IApplicantRepository applicantRepository) : base(context)
        {
            ApplicantRepository = applicantRepository;
        }

        public IApplicantRepository ApplicantRepository { get; }
    }
}
