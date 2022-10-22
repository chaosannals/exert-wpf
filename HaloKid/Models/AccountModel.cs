using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaloKid.Models
{
    [Table("hk_account")]
    public class AccountModel
    {
        [Key]
        [Column("id")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Column("account")]
        [Display(Name = "账号")]
        public string Account { get; set; }

        [Column("password")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Column("nickname")]
        [Display(Name = "昵称")]
        public string Nickname { get; set; }

        [Column("create_at")]
        public DateTime CreateAt { get; set; }

        [Required]
        [Column("creator_id")]
        public int CreatorId { get; set; }

        [Column("update_at")]
        public DateTime? UpdateAt { get; set; }

        [Column("updater_id")]
        public int UpdaterId { get; set; }

        public AccountModel()
        {
            CreateAt = DateTime.Now;
        }
    }
}
