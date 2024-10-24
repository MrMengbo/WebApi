using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Zhaoxi.GameManagement.Entites;
using Zhaoxi.GameManagement.EntityFramework.Mappings;

namespace Zhaoxi.GameManagement.EntityFramework
{
    //关于数据上下文的配置
    public class GameManagementDbContext : DbContext  // 继承自 DbContext，表示这是一个与数据库交互的上下文类。
    {
        /*
        构造函数接受一个 DbContextOptions<GameManagementDbContext> 类型的参数，该参数包含了数据库连接字符串和其他配置信息。
        这个参数通过依赖注入的方式传递给 GameManagementDbContext 实例。
         */
        public GameManagementDbContext(DbContextOptions<GameManagementDbContext> options)
            : base(options)
        {
        }

        // DbSet 会把实体类映射到数据库中，实体类名就是表名
        // 定义了一个 DbSet<Player> 属性。表示数据库中的一个表，该表与 Player 实体类映射。
        public DbSet<Player> Players { get; set; }
        public DbSet<Character> Characters { get; set; }

        // 该方法用于对模型（实体）进行配置
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            这两行代码通过 ModelBuilder 应用了 PlayerMap 和 CharacterMap 类中的配置。
            这些配置类通常定义了实体类与数据库表之间的映射关系，如主键、外键、索引等
            */
            modelBuilder.ApplyConfiguration(new PlayerMap());
            modelBuilder.ApplyConfiguration(new CharacterMap());

            modelBuilder.Entity<Player>()  //为表中添加初始的种子数据
                .HasData(DataSeed.Players);

            modelBuilder.Entity<Character>()
                .HasData(DataSeed.Characters);
        }

    }
}
