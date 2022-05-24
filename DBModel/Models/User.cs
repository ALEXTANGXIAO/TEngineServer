using SqlSugar;

/***********************
 *
 * 数据库Model存放地址
 *
 ************************/

namespace DBModel.Models
{
    [SugarTable("User")]
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int UserId { get; set; }

        public string? RoleName { get; set; }
    }
}
