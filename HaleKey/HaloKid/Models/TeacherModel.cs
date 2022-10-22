using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaloKid.Models
{
    public class TeacherModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }

        [Column("enter_at")]
        public DateTime? EnterOn { get; set; }

        [Column("leave_at")]
        public DateTime? LeaveOn { get; set; }

        [Column("create_at")]
        public DateTime CreateAt { get; set; }

        [Required]
        [Column("creator_id")]
        public int CreatorId { get; set; }

        [Column("update_at")]
        public DateTime? UpdateAt { get; set; }

        [Column("updater_id")]
        public int UpdaterId { get; set; }

        public TeacherModel()
        {
            Gender = Gender.Unknown;
            CreateAt = DateTime.Now;
        }
    }
}
