using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.mercaderias.WebAPI.Models
{
    [Table("Articulo", Schema ="Mercaderias")]
    public class Articulo
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int Stock { get; set; }
        public int IdInventario { get; set; }
    }
}
