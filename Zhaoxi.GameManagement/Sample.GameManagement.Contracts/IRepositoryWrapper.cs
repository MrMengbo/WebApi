using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.GameManagement.Contracts
{

    /*
     这段代码定义了一个名为 IRepositoryWrapper 的接口，它位于 Sample.GameManagement.Contracts 命名空间中。
    这个接口被设计为游戏管理系统中的一个组件，用于封装和简化对多个数据仓库（Repository）的访问。
    数据仓库模式（Repository Pattern）是一种常见的设计模式，用于将数据访问逻辑与应用程序的其余部分分离。
     */
    public interface IRepositoryWrapper
    {
        IPlayerRepository Player { get; }  //IplayerRepository和ICharacterRepository是接口类
        ICharacterRepository Character { get; }
        Task<int> Save();
    }
}
