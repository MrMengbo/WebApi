using Zhaoxi.GameManagement.Entites;
using Zhaoxi.GameManagement.Entites.ReponseType;
using Zhaoxi.GameManagement.Entites.RequestFeatures;

namespace Sample.GameManagement.Contracts
{
    public interface IPlayerRepository : IBaseRepository<Player>  //接口之间的继承，子接口不需要重写父接口的方法
    {
        //仓储用户接口类
        PagedList<Player> GetPlayers(PlayerParameter parameter);
        Task<Player?> GetPlayerById(Guid playerId);
        Task<Player?> GetPlayerWithCharacters(Guid playerId);

        Task<List<Player>> GetAllPlayers();
    }
}
