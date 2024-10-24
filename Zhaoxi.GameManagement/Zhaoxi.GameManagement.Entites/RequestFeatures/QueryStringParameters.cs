using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.GameManagement.Entites.RequestFeatures
{
    public abstract class QueryStringParameters  // 抽象类，查询的参数
    {
        private const int maxPageSize = 100;  //最大页面尺寸
        public int PageNumber { get; set; } = 1;  //页码

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
