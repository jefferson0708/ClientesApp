using AutoMapper;
using ClienteApp.Domain.Entities;
using ClienteApp.Domain.Interfaces.Services;
using ClienteApp.Domain.Services;
using ClientesApp.Application.Commands;
using ClientesApp.Application.Dtos;
using ClientesApp.Application.Interfaces.Applications;
using ClientesApp.Application.Interfaces.Messages;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Services
{

    public class ClienteAppService : IClienteAppService
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly IClienteDomainService _clienteDomainService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ClienteAppService(IClienteDomainService clienteDomainService, IMapper mapper, IMediator mediator, IMessagePublisher messagePublisher)
        {
            _clienteDomainService = clienteDomainService;
            _mapper = mapper;
            _mediator = mediator;
            _messagePublisher = messagePublisher;
        }

        public async Task<ClienteResponseDto> AddAsync(ClienteRequestDto request)
        {
            var cliente = _mapper.Map<Cliente>(request);
            cliente.Id = Guid.NewGuid();

            await _mediator.Send(new ClienteCommand
            {
                LogCliente = new Models.LogClienteModel
                {
                    Id = Guid.NewGuid(),
                    DataOperacao = DateTime.Now,
                    TipoOperacao = Models.TipoOperacao.Add,
                    ClienteId = cliente.Id,
                    DadosCliente = JsonConvert.SerializeObject(cliente)
                }
            });

            await _messagePublisher.Send(new Events.ClienteCadastradoEvent
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                DataCadastro = DateTime.Now,
                MensagemCadastro = $"Olá, {cliente.Nome} sua conta foi criada com sucesso!",
            });

            var result = await _clienteDomainService.AddAsync(cliente);
            return _mapper.Map<ClienteResponseDto>(result);
        }

        public async Task<ClienteResponseDto> UpdateAsync(Guid id, ClienteRequestDto request)
        {
            var cliente = _mapper.Map<Cliente>(request);
            cliente.Id = id;

            var result = await _clienteDomainService.UpdateAsync(cliente);

            await _mediator.Send(new ClienteCommand
            {
                LogCliente = new Models.LogClienteModel
                {
                    Id = Guid.NewGuid(),
                    DataOperacao = DateTime.Now,
                    TipoOperacao = Models.TipoOperacao.Update,
                    ClienteId = cliente.Id,
                    DadosCliente = JsonConvert.SerializeObject(cliente)
                }
            });

            return _mapper.Map<ClienteResponseDto>(result);
        }

        public async Task<ClienteResponseDto> DeleteAsync(Guid id)
        {
            var result = await _clienteDomainService.DeleteAsync(id);

            await _mediator.Send(new ClienteCommand
            {
                LogCliente = new Models.LogClienteModel
                {
                    Id = Guid.NewGuid(),
                    DataOperacao = DateTime.Now,
                    TipoOperacao = Models.TipoOperacao.Delete,
                    ClienteId = id
                }
            });

            return _mapper.Map<ClienteResponseDto>(result);
        }

        public async Task<List<ClienteResponseDto>> GetManyAsync(string nome)
        {
            var result = await _clienteDomainService.GetManyAsync(nome);
            return _mapper.Map<List<ClienteResponseDto>>(result);
        }

        public async Task<ClienteResponseDto?> GetByIdAsync(Guid id)
        {
            var result = await _clienteDomainService.GetByIdAsync(id);
            return _mapper.Map<ClienteResponseDto>(result);
        }

        public void Dispose()
        {
            _clienteDomainService.Dispose();
        }
    }

}
