using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P1_AP1_AngelGuerrero.Models
{
    public class EntradasHuacalesDetalles
    {
        [Range(0, int.MaxValue)]
        public int Cantidad { get; set; }
        [Key]
        public int DetalleId { get; set; }
        public int IdEntrada { get; set; }

        public double Precio { get; set; }

        public int TipoId { get; set; }
    }
}