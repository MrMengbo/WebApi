using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.GameManagement.Contracts
{
    // 接口类只有方法但是没有具体的实现。
    // 这是一个仓储接口基类
    public interface IBaseRepository<T>
    {
        IQueryable<T> FindAll();  // 查询
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression); //条件查询
        void Create(T entity);  //增加
        void Update(T entity);  //更新修改
        void Delete(T entity);   //删除
    }
}
