using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using baseNetApi.models.basic;

namespace baseNetApi.models;

public class Groups : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string group_name { get; set; } 
}