using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace DBModels.Models
{
    [SugarTable("User")]
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Password { get; set; }

        public string UserId { get; set; }

        public string? Name { get; set; }
    }
}
