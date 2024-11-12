using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Models
{
    /*
     定义一个名为 TodoContext 的类，它继承自 DbContext 类。
     DbContext 是 Entity Framework Core 的核心类，用于表示与数据库的一个会话，
     它管理着实体实例的生命周期以及数据库查询和事务。
     */
    public class TodoContext:DbContext
    {
        // 定义一个构造函数，它接受一个 DbContextOptions<TodoContext> 类型的参数 options。
        // 这个参数包含了 Entity Framework Core 需要的所有配置信息，
        // 包括数据库连接字符串、日志记录级别等。
        // :base(options) 表示调用基类 DbContext 的构造函数，并将 options 参数传递给它。
        public TodoContext(DbContextOptions<TodoContext> options):base(options) 
        {
            // 构造函数体为空，因为所有的配置信息都通过 options 参数传递给了基类 DbContext。
        }


        // 定义一个名为 TodoItems 的公共属性，它的类型是 DbSet<TodoItem>。
        // DbSet<TEntity> 是 Entity Framework Core 中表示数据库表的一个泛型集合。
        // 在这里，它表示数据库中名为 TodoItems 的表（或视图），并且这个表中的每一行都映射到一个 TodoItem 实体实例。
        public DbSet<TodoItem> TodoItems { get; set; }
        /*
         定义一个名为 TodoContext 的类，它继承自 DbContext 类。
         DbContext 是 Entity Framework Core 的核心类，用于表示与数据库的一个会话，
         它管理着实体实例的生命周期以及数据库查询和事务。
         */
        //public DbSet<TodoItemDTO> TodoItemDTO { get; set; } 
        
    }
}
