using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.GameManagement.Contracts;

namespace Zhaoxi.GameManagement.EntityFramework.Repositories
//仓储的实现类
{
    // 定义了一个泛型接口IBaseRepository<T>，用于定义基本的数据库操作，并通过一个抽象类BaseRepository<T>提供了这些操作的具体实现。
    // 这样的设计允许开发者通过继承BaseRepository<T>来轻松地创建针对特定实体类型的仓库类，同时保持代码的整洁和可重用性。
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class  //抽象类实现接口 ，注意抽象类不能被实例化
    {
        //GameDbContext，这是一个GameManagementDbContext类型的属性，用于访问数据库上下文。
        //GameManagementDbContext很可能是Entity Framework Core的DbContext的一个子类，用于管理数据库操作。
        protected GameManagementDbContext GameDbContext { get; set; }

        // 构造函数：接受一个GameManagementDbContext类型的参数，并将其赋值给GameDbContext属性。
        // 这样，每个BaseRepository<T>的实例都会有一个与之关联的数据库上下文。
        protected BaseRepository(GameManagementDbContext repositoryContext)
        {
            GameDbContext = repositoryContext;
        }

        // 实现了接口IBaseRepository的方法
        public IQueryable<T> FindAll()
        {
            return GameDbContext.Set<T>().AsNoTracking();  
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return GameDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        //上述这两个Find()方法使用GameDbContext.Set<T>()来获取代表数据库中T类型实体的DbSet<T>，然后分别调用AsNoTracking()和Where(expression).AsNoTracking()来执行查询。
        // AsNoTracking()表示查询结果不会被Entity Framework Core跟踪，这对于只读查询来说可以提高性能。


        public void Create(T entity)
        {
            GameDbContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            GameDbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            GameDbContext.Set<T>().Remove(entity);
        }
    }

}
