using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.GameManagement.Entites.Dtos
{
    // 客户端（前端）传递给后端的DTO，也就创建新用户时的DTO
    public class PlayerForCreationDto
    {
        [Required(ErrorMessage = "账号不能为空")]
        [StringLength(20, ErrorMessage = "账号长度不能大于50")]
        public string Account { get; set; }

        [Required(ErrorMessage = "账号类型不能为空")]
        [StringLength(10, ErrorMessage = "账号类型不能大于10")]
        public string AccountType { get; set; }
    }
}
