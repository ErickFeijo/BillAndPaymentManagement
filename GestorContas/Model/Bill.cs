using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorContas.Model
{
    public class Bill
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string BarCode { get; set; }

        [Required]
        public DateTime Expiration { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Situation { get; set; }

    }
}
