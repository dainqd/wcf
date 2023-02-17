using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using baseNetApi.models.basic;

namespace baseNetApi.models;

public class Contacts : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string name { get; set; }
    public string number { get; set; }
    public string avt { get; set; } = "";
    public string description { get; set; } = "";
    public ContactStatus status { get; set; } = ContactStatus.INACTIVE;
    public int group_id { get; set; } = 1;
}