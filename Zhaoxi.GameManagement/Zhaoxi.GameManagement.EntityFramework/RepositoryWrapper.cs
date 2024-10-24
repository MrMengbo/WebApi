using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.GameManagement.Contracts;
using Zhaoxi.GameManagement.EntityFramework.Repositories;

/*
实现一个名为RepositoryWrapper的类，该类是数据访问层（Data Access Layer, DAL）的一部分，在软件架构中负责处理与数据库的交互。
它特别适用于使用Entity Framework（EF）作为ORM（对象关系映射）工具的.NET应用程序中。下面是对代码的详细解释：
 
 */

//这是 设计模式中的 仓库模式，理解有困难，可以先放一放
namespace Zhaoxi.GameManagement.EntityFramework
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        //GameManagementDbContext是Entity Framework的上下文类，负责管理数据库连接、跟踪实体状态等。
        private readonly GameManagementDbContext _gameDbContext;
        private IPlayerRepository _player;
        private ICharacterRepository _character;

        // 这两个仓储具有同一个数据上下文
        public IPlayerRepository Player {
            get { return _player ??= new PlayerRepository(_gameDbContext); }
        }

        // 这两个仓储具有同一个数据上下文
        public ICharacterRepository Character {
            get { return _character ??= new CharacterRepository(_gameDbContext); }
        }
        
        //通过构造函数注入上下文实例
        public RepositoryWrapper(GameManagementDbContext gameDbContext)
        {
            _gameDbContext = gameDbContext;
        }

        // Task是什么意思呢
        public Task<int> Save()
        {
            return _gameDbContext.SaveChangesAsync();
        }
    }
}
