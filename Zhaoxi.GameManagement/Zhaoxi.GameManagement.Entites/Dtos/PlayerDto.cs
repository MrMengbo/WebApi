using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.GameManagement.Entites.Dtos
{
    // 服务器传输数据给客户端的时候，需要的DTO，实现数据库表项对应的实体类到前端（客户端）的映射。
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string AccountType { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
