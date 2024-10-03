using ClienteApp.Domain.Entities;
using ClienteApp.Domain.Exceptions;
using ClienteApp.Domain.Interfaces.Repositories;
using ClienteApp.Domain.Interfaces.Services;
using ClienteApp.Domain.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteApp.Domain.Services
{
    public class ClienteDomainService : IClienteDomainService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<Cliente> _validator;

        public ClienteDomainService(IClienteRepository clienteRepository, IValidator<Cliente> validator)
        {
            _clienteRepository = clienteRepository;
            _validator = validator;
        }

        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            var validationResult = await _validator.ValidateAsync(cliente);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _clienteRepository.AddAsync(cliente);
            return cliente;
        }

        public async Task<Cliente> UpdateAsync(Cliente cliente)
        {
            if (!await _clienteRepository.VerifyExistsAsync(c => c.Id == cliente.Id))
                throw new ClienteNotFoundException(cliente.Id);

            if (_validator is ClienteValidator validator)
                validator.SetCurrentClienteId(cliente.Id);

            var validationResult = await _validator.ValidateAsync(cliente);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _clienteRepository.UpdateAsync(cliente);
            return cliente;
        }

        public async Task<Cliente> DeleteAsync(Guid id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
                throw new ClienteNotFoundException(id);

            await _clienteRepository.DeleteAsync(cliente);
            return cliente;
        }

        public async Task<IEnumerable<Cliente>> GetManyAsync(string nome)
        {
            return await _clienteRepository.GetManyAsync(c => c.Nome.Contains(nome));
        }

        public async Task<Cliente?> GetByIdAsync(Guid id)
        {
            return await _clienteRepository.GetByIdAsync(id);
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
        }
    }
}



