using BAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;


namespace BAL.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginDto> Login(LoginRequest dto);
    }
}
