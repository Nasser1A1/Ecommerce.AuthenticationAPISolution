﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Application.DTOs
{
    public record LoginDTO
    (
        string Email,
        string Password
    );
}
