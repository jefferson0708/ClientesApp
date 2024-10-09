using ClientesApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Interfaces.Logs
{
    public interface ILogClienteDataStore
    {
        Task AddSync(LogClienteModel model);
    }
}
