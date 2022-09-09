using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CSServerDemo.Models;

[Table("cs_user_account")]
[Index(nameof(Account), Name = "ACCOUNT_UNIQUE", IsUnique = true)]
public class UserAccount
{
    [Column("id")]
    public long Id { get; set; }

    [Column("account", TypeName ="VARCHAR(100)")]
    public string Account { get; set; } = null!;

    [Column("nickname", TypeName ="VARCHAR(100)")]
    public string? Nickname { get; set; }


    [Column("password", TypeName ="CHAR(64)")]
    public string? Password { get; set; }

    [Column("create_at")]
    //[DefaultValue(typeof(DateTime), "CURRENT_TIMESTAMP")]
    public DateTime CreateAt { get; set; }

    [Column("create_by")]
    public long? CreateBy { get; set; }

    [Column("update_at")]
    public DateTime? UpdateAt { get; set; }

    [Column("update_by")]
    public long? UpdateBy { get; set; } 

    [Column("last_login_at")]
    public DateTime? LastLoginAt { get; set; }
}
