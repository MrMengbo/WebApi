using Sample.GameManagement.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhaoxi.GameManagement.Entites;
using Zhaoxi.GameManagement.Entites.ReponseType;
using Zhaoxi.GameManagement.Entites.RequestFeatures;
using System.Reflection.Metadata;
namespace Zhaoxi.GameManagement.EntityFramework.Repositories
{
    ////仓储的实现类
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(GameManagementDbContext repositoryContext) : base(repositoryContext)
        {
        }
        // GetPlayers、GetAllPlayers、GetPlayerById、GetPlayerWithCharacters是IPlayerRepository接口类方法的具体的实现
        public PagedList<Player> GetPlayers(PlayerParameter parameter)  
        {
            return FindAll()  //FindAll、FindByCondition 是 BaseRepository基类接口的实现类的方法。
                .OrderBy(player => player.DateCreated)
                .ToPagedList(parameter.PageNumber, parameter.PageSize);

        }

        public async Task<List<Player>> GetAllPlayers()
        {
            return await FindAll()
                .OrderBy(player => player.DateCreated)
                .ToListAsync();
                
        }
        /*
        这段代码是C#中用于从数据库中异步获取具有特定ID的玩家信息的示例。
        它使用了Entity Framework Core（一个流行的.NET ORM框架）和LINQ（Language Integrated Query）表达式。下面是对代码的详细解释： 
        */

        /*
         
         返回类型：Task<Player?>。这是一个异步方法，它返回一个Task，该任务在完成时可能包含Player类型的对象或null。
        Player?表示Player类型可以为空（C# 8.0及以上版本的语法，需要启用可为空引用类型功能）。
         参数：Guid playerId。这是一个唯一标识符，用于指定要查找的玩家。
         */
        public async Task<Player?> GetPlayerById(Guid playerId)
        {
            /*
            调用FindByCondition方法，并传递一个LINQ表达式作为参数。
            FindByCondition方法返回一个IQueryable<T>对象，其中T在这个上下文中是Player类型。
            使用.FirstOrDefaultAsync()扩展方法异步执行查询，并返回查询结果中的第一个元素（如果存在）或null（如果不存在）。 
            */
            return await FindByCondition(player => player.Id == playerId)
                .FirstOrDefaultAsync();
            /*
                player => player.Id == playerId 是一个Lambda表达式，它定义了一个匿名函数。
                player 是输入参数，它的类型是Player。在调用FindByCondition方法时，这个参数对应于Func<T, bool>中的T，在这个例子中T是Player。
                player.Id == playerId 是函数的主体，它返回一个布尔值。这个表达式比较了Player对象的Id属性和方法参数playerId是否相等。
                当这个Lambda表达式作为参数传递给FindByCondition方法时，它告诉Entity Framework Core只选择那些其Id属性与提供的playerId相匹配的Player实体。
             */
        }

        public async Task<Player?> GetPlayerWithCharacters(Guid playerId)
        {
            return await FindByCondition(player => player.Id == playerId)
                .Include(player => player.Characters)
                .FirstOrDefaultAsync();
        }


    }
}
