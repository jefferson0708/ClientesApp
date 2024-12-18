﻿using ClientesApp.Application.Interfaces.Logs;
using ClientesApp.Application.Models;
using ClientesApp.Infra.Data.MongoDB.Contexts;
using MongoDB.Driver;
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

        public async Task<List<LogClienteModel>> GetAsync(Guid clienteId, int pageNumber, int pageSize)
        {
            var filter = Builders<LogClienteModel>.Filter.Eq(log => log.ClienteId, clienteId);
            var result = await _mongoBDContext.LogClientes
                .Find(filter)
                .Skip((pageNumber -1 ) * pageSize)
                .Limit(pageSize)
                .SortByDescending(log => log.DataOperacao)
                .ToListAsync();
            return result;
        }

        public async Task<int> GetTotalCountAsync(Guid clienteId)
        {
            var filter = Builders<LogClienteModel>.Filter.Eq(log => log.ClienteId, clienteId);

            return(int)await _mongoBDContext.LogClientes.CountDocumentsAsync(filter);
        }
    }
}
