namespace DotnetAPI;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column(name:"Created_at")]
    public DateTime CreatedAt { get; set; }

    public string Name { get; set; } = "";
}
