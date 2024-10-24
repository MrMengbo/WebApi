using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.GameManagement.Entites.ReponseType
{
    public static class PagedExtensions
    {
        public static PagedList<TSource> ToPagedList<TSource>(this IQueryable<TSource> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();
            return new PagedList<TSource>(items, count, pageNumber, pageSize);
        }
    }
}
