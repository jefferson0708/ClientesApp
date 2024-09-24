using Bogus;
using ClienteApp.Domain.Entities;
using ClientesApp.Infra.Data.SqlServer.Context;
using ClientesApp.Infra.Data.SqlServer.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CientesApp.Infra.Data.SqlServer.Tests
{
    public class ClienteRepositoryTest
    {
        private readonly Faker<Cliente> _fakerCliente;
        private readonly DataContext _dataContext;
        private readonly ClienteRepository _clienteRepository;

        public ClienteRepositoryTest()
        {
            _fakerCliente = new Faker<Cliente>("pt_BR")
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Nome, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Cpf, f => f.Random.Replace("###########"));

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ClientesAppTestsDB")
                .Options;

            _dataContext = new DataContext(options);

            _clienteRepository = new ClienteRepository(_dataContext);


        }

        [Fact]
        public async Task AddAsync_ShouldAddCliente()
        {
            var cliente = _fakerCliente.Generate();

            await _clienteRepository.AddAsync(cliente);

            var clienteCadastrado = await _clienteRepository.GetByIdAsync(cliente.Id);

            if (clienteCadastrado == null)
                Assert.True(false, "O cliente não foi cadastrado corretamente");
            //clienteCadastrado.Should().NotBeNull();
            clienteCadastrado.Nome.Should().BeSameAs(cliente.Nome);
            clienteCadastrado.Email.Should().BeSameAs(cliente.Email);
            clienteCadastrado.Cpf.Should().BeSameAs(cliente.Cpf);


        }
    }
}
