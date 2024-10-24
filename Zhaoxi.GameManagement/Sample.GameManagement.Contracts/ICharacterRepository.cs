using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.GameManagement.Entites;

namespace Sample.GameManagement.Contracts
{
    //仓储用户接口类
    public interface ICharacterRepository : IBaseRepository<Character>
    {
    }
}
