﻿using RestoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoApp.Application.Resto
{
    public interface IOrderRepository
    {
        Task<string?> OrderMenu(Order order);
    }
}
