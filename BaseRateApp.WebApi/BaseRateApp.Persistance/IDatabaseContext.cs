using BaseRateApp.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BaseRateApp.Persistance
{
    public interface IDatabaseContext
    {
        DbSet<Agreement> Agreements { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
