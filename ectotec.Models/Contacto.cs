using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ectotec.Models
{
    [Table("contacto")]
    public class Contacto
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [Column("nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El mail es requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Mail no válido")]
        [Column("email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El telefono es requerido")]
        [Column("telefono")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Fecha es requerido")]
        [Column("fecha", TypeName = "datetime")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "CiudadEdo es requerido")]
        [Column("ciudadedo")]
        public string CiudadEdo { get; set; }

    }
}
