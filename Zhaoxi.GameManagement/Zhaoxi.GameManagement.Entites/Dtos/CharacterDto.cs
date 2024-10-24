namespace Zhaoxi.GameManagement.Entites.Dtos
{
    // 服务器传输数据给客户端的时候，需要的DTO，实现数据库表项对应的实体类到前端的映射。
    public class CharacterDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string Classes { get; set; }
        public int Level { get; set; }
        public DateTime DateCreated { get; set; }
    }

    
}
