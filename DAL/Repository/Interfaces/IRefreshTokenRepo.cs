using DAL.Dto;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IRefreshTokenRepo:IBaseRepository<RefreshTokenDto>
    {
        public RefreshTokenDto Get(string refreshToken);

    }
}
