﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IDbInitializer
    {
        Task InitializAsync();
        Task InitializIdentityAsync();

    }
}
