using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterServiceAPI.Model
{
    public class Payment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string BarCode { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}