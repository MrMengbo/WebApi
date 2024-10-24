using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.GameManagement.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhaoxi.GameManagement.Entites;

namespace Zhaoxi.GameManagement.EntityFramework.Mappings
{
    /*
     这段代码是在使用Entity Framework Core（EF Core）框架进行数据库上下文（DbContext）的配置时的一部分。
    EF Core是微软提供的一个对象关系映射（ORM）框架，它允许开发者使用.NET对象来操作数据库，而无需直接编写SQL语句。
    这种方法提高了开发效率，并使代码更加清晰和易于维护。
     */
    public class PlayerMap : IEntityTypeConfiguration<Player>

    {
        /*
        Configure()方法接受一个参数，类型为EntityTypeBuilder<Player>，名为builder。
        EntityTypeBuilder<TEntity>是EF Core中的一个类，用于配置实体类型TEntity的各个方面，比如属性、关系、索引等。
        在这个例子中，Entity是Player类型，意味着这个配置是专门为Player实体类型设计的。
         */
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            //player => player.Account的含义是：这是一个Lambda表达式，用于从Player类型的实例中访问其Account属性。
            //在这个上下文中，它被用作EntityTypeBuilder<Player>的Property方法的参数，以指定要配置的Player实体的属性。
            builder.Property(player => player.Account).HasMaxLength(50);
            builder.Property(player => player.AccountType).HasMaxLength(10);
            builder.HasIndex(player => player.Account).IsUnique(); // 设置索引
        }
    }
}

