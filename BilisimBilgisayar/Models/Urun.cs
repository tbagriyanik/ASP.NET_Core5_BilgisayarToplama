using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilisimBilgisayar.Models
{
    [Table("Urunler_Tablosu")]
    public class Urun
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Ürün Adı")]
        public string UrunAdi { get; set; }
        [Display(Name = "Ürün Resim Dosyası")]
        public string UrunResimDosyasi { get; set; }
        public string UrunTeknikBilgileri { get; set; }
        public float UrunFiyat { get; set; }

        public int kategoriID { get; set; }
        public Kategori kategori { get; set; }

    }
}
