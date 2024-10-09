using ClientesApp.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Interfaces.Messages
{
    public interface IMessagePublisher
    {
        Task Send(ClienteCadastradoEvent @event);
    }
}
