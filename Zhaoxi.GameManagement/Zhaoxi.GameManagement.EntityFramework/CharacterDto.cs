using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.GameManagement.Entites.Dtos;

namespace Zhaoxi.GameManagement.EntityFramework
{
    public class CharacterDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string Classes { get; set; }
        public int Level { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class PlayerWithCharactersDto : PlayerDto
    {
        public IEnumerable<CharacterDto> Characters { get; set; }
    }
}
