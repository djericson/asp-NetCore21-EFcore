using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.mercaderias.WebAPI.Models
{
    [Table("Inventario", Schema = "Mercaderias")]
    public class Inventario
    {
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Tipo { get; set; }
        public string Tienda { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
