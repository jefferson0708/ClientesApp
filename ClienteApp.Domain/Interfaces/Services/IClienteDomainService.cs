using ClienteApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteApp.Domain.Interfaces.Services
{
    public interface IClienteDomainService: IDisposable
    {
        Task<Cliente> AddAsync(Cliente cliente);
        Task<Cliente> UpdateAsync(Cliente cliente);
        Task<Cliente> DeleteAsync(Guid id);
        Task<IEnumerable<Cliente>> GetManyAsync(string nome);
        Task<Cliente?> GetByIdAsync(Guid id);
    }
}
