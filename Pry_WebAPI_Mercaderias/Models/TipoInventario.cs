using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.mercaderias.WebAPI.Models
{
    [Table("Tipos_Inventario_ext")]
    public class TipoInventario
    {
        [Key]
        public int Id { get; set; }
        public string NombreTipo { get; set; }
    }
}
