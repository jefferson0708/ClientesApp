﻿using ClienteApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteApp.Domain.Interfaces.Repositories
{
    public interface IClienteRepository:IBaseRepository<Cliente, Guid>
    {

    }
}
