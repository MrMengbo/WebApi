using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhaoxi.GameManagement.Entites;

namespace Zhaoxi.GameManagement.EntityFramework.Mappings
{
    // 这个Map.cs文件是EF Core中的一个实体类型配置类，用于定义Character实体类在数据库中的映射规则，包括最大长度，是否唯一

    // 公共类Character实现了EF Core中的接口，用于特定实体类的配置
    public class CharacterMap : IEntityTypeConfiguration<Character> // IEntityTypeConfiguration接口是EF Core提供的，用于定义特定实体的配置。
    {

        //它接受一个EntityTypeBuilder<Character>类型的参数，这个参数用于构建和配置Character实体的映射。
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            // 配置了Character实体的Nickname属性。它指定了Nickname属性在数据库中的最大长度为20个字符。
            builder.Property(character => character.Nickname).HasMaxLength(20);

            // 这行代码配置了Classes属性，也设置了最大长度为20个字符。
            builder.Property(character => character.Classes).HasMaxLength(20);

            // Nickname属性创建了一个唯一索引。这意味着在数据库中，Nickname的值必须是唯一的，不能有重复。
            builder.HasIndex(character => character.Nickname).IsUnique();

            /*
             这几行代码配置了Character实体和Player实体之间的关系。Character实体通过PlayerId属性与Player实体相关联。
             这是一个一对多的关系：一个Player可以有多个Character，但每个Character只能属于一个Player。
             */
            builder.HasOne(c => c.Player)
                .WithMany(p => p.Characters)
                .HasForeignKey(c => c.PlayerId);
        }
    }
}
