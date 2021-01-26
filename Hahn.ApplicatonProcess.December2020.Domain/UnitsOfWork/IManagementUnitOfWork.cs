using Hahn.ApplicationProcess.December2020.Data.Infrastructure;
using Hahn.ApplicationProcess.December2020.Domain.Repositories;

namespace Hahn.ApplicationProcess.December2020.Domain.UnitsOfWork
{
    public interface IManagementUnitOfWork: IUnitOfWork
    {
        IApplicantRepository ApplicantRepository { get; }
    }
}