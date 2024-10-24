using Zhaoxi.GameManagement.Entites;

namespace Zhaoxi.GameManagement.EntityFramework
{
    // 数据的模拟，用于后边数据库上下文进行调用，并自动插入至数据库中。
    /*
     ※※ 总结：这段代码定义了一个私有、静态、只读的Guid数组，数组在初始化时包含两个随机生成的Guid值。
    这样的数组可以用于各种需要唯一标识符的场景，例如，作为数据库记录的唯一键或用于标识特定的对象实例
     */
    public static class DataSeed
    {

        /*
        readonly：这个关键字表明_guids数组一旦被初始化之后，就不能再被重新赋值指向另一个数组。
        但是，这并不意味着数组内部的元素不能被修改（在这个特定的例子中，由于Guid是不可变的，所以即使可以修改数组元素，Guid值本身也无法被改变）。
        readonly仅限制了整个数组的引用不能被改变。

        Guid[]：指定了数组的类型，即数组中的元素是Guid类型。
        Guid（全局唯一标识符）是一种由32个十六进制数字组成的值，通常用于在软件应用程序中唯一标识信息，如数据库记录或对象实例。
         */
        private static readonly Guid[] _guids =
        {
            Guid.NewGuid(),
            Guid.NewGuid()
        };


        public static Player[] Players { get; } = {
            new Player
            {
                Id = _guids[0],
                Account = "mw2021",
                AccountType = "Free",
                DateCreated = DateTime.Now
            },
            new Player
            {
                Id = _guids[1],
                Account = "dc2021",
                AccountType = "Free",
                DateCreated = DateTime.Now
            }
        };

        public static Character[] Characters { get; } =
        {
            new Character
            {
                Id = Guid.NewGuid(),
                Nickname = "Code Man",
                Classes = "Mage",
                Level = 99,
                PlayerId = _guids[0],
                DateCreated = DateTime.Now
            },
            new Character
            {
                Id = Guid.NewGuid(),
                Nickname = "Iron Man",
                Classes = "Warrior",
                Level = 90,
                PlayerId = _guids[0],
                DateCreated = DateTime.Now
            },
            new Character
            {
                Id = Guid.NewGuid(),
                Nickname = "Spider Man",
                Classes = "Druid",
                Level = 80,
                PlayerId = _guids[0],
                DateCreated = DateTime.Now
            },
            new Character
            {
                Id = Guid.NewGuid(),
                Nickname = "Batman",
                Classes = "Death Knight",
                Level = 90,
                PlayerId = _guids[1],
                DateCreated = DateTime.Now
            },
            new Character
            {
                Id = Guid.NewGuid(),
                Nickname = "Superman",
                Classes = "Paladin",
                Level = 99,
                PlayerId = _guids[1],
                DateCreated = DateTime.Now
            }
        };
    }
}
