using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilisimBilgisayar.Models
{
    [Table("Kategoriler_Tablosu")]
    public class Kategori
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name ="Katergori Adı")]
        public string KategoriAdi { get; set; }

    }
}
