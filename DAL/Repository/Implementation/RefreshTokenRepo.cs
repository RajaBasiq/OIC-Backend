using DAL.Dto;
using DAL.Model;
using DAL.Repository.Interfaces;

namespace DAL.Repository.Implementation
{
    public class RefreshTokenRepo: BaseRepository<RefreshToken, RefreshTokenDto>, IRefreshTokenRepo
    {
        public RefreshTokenRepo(ApplicationDbContext context, AutoMapper.IMapper mapper) : base(context, mapper)
        {
        }
        public  RefreshTokenDto Get(string refreshToken)
        {
            var entity = _dbSet.FirstOrDefault(x => x.Token == refreshToken);
            return entity == null ? null : _mapper.Map<RefreshTokenDto>(entity);
        }
    }
}
