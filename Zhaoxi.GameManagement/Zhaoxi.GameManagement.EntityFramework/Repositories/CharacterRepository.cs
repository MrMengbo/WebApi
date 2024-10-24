using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.GameManagement.Contracts;
using Zhaoxi.GameManagement.Entites;

namespace Zhaoxi.GameManagement.EntityFramework.Repositories
{
    //仓储的实现类
    // 普通类实现了抽象类和接口类
    public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
    {
        public CharacterRepository(GameManagementDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
