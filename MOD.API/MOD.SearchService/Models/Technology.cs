using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOD.SearchService.Models
{
    [Table("Technologies")]
    public class Technology
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Toc { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Prerequisites { get; set; }

        public Training Training { get; set; }
    }
}
