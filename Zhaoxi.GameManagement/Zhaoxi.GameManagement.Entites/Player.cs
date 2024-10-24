namespace Zhaoxi.GameManagement.Entites
{
    // 为什么要创建类库来当做实体类，因为前端和后端传输数据，使用统一的类结构能实现代码的复用。
    public class Player
    {
        // id
        public Guid Id { get; set; }
        // 账号
        public string Account { get; set; }
        // 账号类型
        public string AccountType { get; set; }

        //创建时间
        public DateTime DateCreated { get; set; }

        //角色
        public ICollection<Character> Characters { get; set; }

        public Player()
        {
            DateCreated = DateTime.Now;
        }
    }
}
