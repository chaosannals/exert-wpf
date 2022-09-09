using System.ComponentModel.DataAnnotations.Schema;

namespace CSServerDemo.Models;

[Table("cs_business_client")]
public class BusinessClient
{
    [Column("id")]
    public long Id { get; set; }

    [Column("create_at")]
    public DateTime CreateAt { get; set; }

    [Column("create_by")]
    public long? CreateBy { get; set; }

    [Column("update_at")]
    public DateTime? UpdateAt { get; set; }

    [Column("update_by")]
    public long? UpdateBy { get; set; }


}
