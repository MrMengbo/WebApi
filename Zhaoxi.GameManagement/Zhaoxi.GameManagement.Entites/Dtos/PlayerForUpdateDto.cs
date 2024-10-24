using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.GameManagement.Entites.Dtos
{
    // 更新时的DTO，客户端传递给 服务端
    public class PlayerForUpdateDto 
    {
        [Required(ErrorMessage = "账号类型不能为空")]
        [StringLength(10, ErrorMessage = "账号类型不能大于10")]
        public string AccountType { get; set; }
    }
}
