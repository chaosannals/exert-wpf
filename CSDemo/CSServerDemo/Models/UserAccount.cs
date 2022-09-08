using System.ComponentModel.DataAnnotations.Schema;

namespace CSServerDemo.Models;

[Table("user_account")]
public class UserAccount
{
    [Column("id")]
    public long Id { get; set; }

    [Column("account")]
    public string Account { get; set; } = null!;

    [Column("nickname")]
    public string? Nickname { get; set; }


    [Column("password")]
    public string? Password { get; set; }

    [Column("create_at")]
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
