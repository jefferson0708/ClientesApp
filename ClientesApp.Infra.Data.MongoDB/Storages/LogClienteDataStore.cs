using ClientesApp.Application.Interfaces.Logs;
using ClientesApp.Application.Models;
using ClientesApp.Infra.Data.MongoDB.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.MongoDB.Storages
{
    public class LogClienteDataStore : ILogClienteDataStore
    {
        private readonly MongoDBContext _mongoBDContext;
        public LogClienteDataStore(MongoDBContext mongoBDContext)
        {
            this._mongoBDContext = mongoBDContext;
        }

        public async Task AddSync(LogClienteModel model)
        {
            await _mongoBDContext.LogClientes.InsertOneAsync(model);
        }
    }
}
