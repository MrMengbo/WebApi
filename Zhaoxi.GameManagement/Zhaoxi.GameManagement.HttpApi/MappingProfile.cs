using AutoMapper;
using Zhaoxi.GameManagement.Entites;
using Zhaoxi.GameManagement.Entites.Dtos;

namespace Zhaoxi.GameManagement.HttpApi
{
    public class MappingProfile : Profile  //AutoMaper.Profile
    {
        /*
        AutoMapper是一个对象-对象映射器，它提供了一种简单的方式来在.NET类型之间转换对象，无需手动编写转换代码。
        这在将复杂对象模型从数据库实体转换为用于前端显示的DTO（数据传输对象）时特别有用，反之亦然。
        */
        public MappingProfile()
        {
            // CreateMap<Player, PlayerDto>();：这行代码定义了一个从Player类型到PlayerDto类型的映射。
            // 这意味着AutoMapper知道如何将Player对象转换为PlayerDto对象。  这是服务端实体类到DTO的映射
            CreateMap<Player, PlayerDto>();
            CreateMap<Player, PlayerWithCharactersDto>();
            CreateMap<Character, CharacterDto>();

            // 下面这两个是客户端DTO到服务端实体类的映射，负责客户端到服务端的数据传递。
            CreateMap<PlayerForCreationDto, Player>();
            CreateMap<PlayerForUpdateDto, Player>();
        }
    }
}
