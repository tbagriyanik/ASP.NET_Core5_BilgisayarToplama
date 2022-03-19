using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BilisimBilgisayar.Data;
using BilisimBilgisayar.Models;
using Microsoft.AspNetCore.Http;

namespace BilisimBilgisayar.Pages
{
    public class PCToplaModel : PageModel
    {
        private readonly BilisimBilgisayar.Data.ApplicationDbContext _context;

        public PCToplaModel(BilisimBilgisayar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Urun> Urun { get; set; }
        public string mesaj { get; set; } = "Hoş Geldiniz";
        public String[] urunListesi { get; set; }

        public async Task OnGetAsync(int sec = 0, int sil = 0)
        {
            int bayatlayacakDakika = 60;
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(bayatlayacakDakika);

            if (sec != 0)
            {
                mesaj = string.Format("{0} numaralı ürün eklendi", sec);

                if (Request.Cookies["sepettekiler"] != null)
                {
                    var oncekiCookie = Request.Cookies["sepettekiler"];
                    Response.Cookies.Append("sepettekiler", oncekiCookie + "-" + sec.ToString(), option);
                }
                else
                {
                    Response.Cookies.Append("sepettekiler", sec.ToString(), option);
                }

                sepetGuncelle(sec);
            }
            else
            {
                sepetGuncelle(0);
            }

            if (sil > 0)
            {
                urunListesi = Request.Cookies["sepettekiler"].Split("-", StringSplitOptions.RemoveEmptyEntries);
                string silSonrasi="";

                foreach (var item in urunListesi)
                {
                    if (item == sil.ToString())
                    {
                        mesaj = string.Format("{0} numaralı ürün sepetten silindi", sil);
                        sil = 0;//diğerlerini silmesin
                        continue;
                    }
                    else
                    {
                        silSonrasi += "-" + item;
                    }
                }
                Response.Cookies.Append("sepettekiler", silSonrasi, option);
                urunListesi = silSonrasi.Split("-", StringSplitOptions.RemoveEmptyEntries);                
            }

            //Kategori ID ile sıralı gelsin, önce işlemci, RAM seçsin
            Urun = await _context.Urun
                .OrderBy(p => p.kategoriID)
                .Include(u => u.kategori).ToListAsync();
        }

        public string urunAdiGetir(string kimlik)
        {
            var sonuc = _context.Urun
                .Where(u => u.ID == Convert.ToInt32(kimlik))
                .Select(u => u.UrunAdi)
                .ToList();
            return sonuc[0].ToString();
        }

        public string urunFiyatiGetir(string kimlik)
        {
            var sonuc = _context.Urun
               .Where(u => u.ID == Convert.ToInt32(kimlik))
               .Select(u => u.UrunFiyat)
               .ToList();
            return sonuc[0].ToString();
        }

        private void sepetGuncelle(int secim)
        {
            string sepettekiler;
            if (Request.Cookies["sepettekiler"] != null)
            {
                //eskisi geliyor
                var oncekiCookie = Request.Cookies["sepettekiler"];
                if (secim > 0)
                {
                    sepettekiler = oncekiCookie + "-" + secim.ToString();
                }
                else
                {
                    sepettekiler = oncekiCookie;
                }

                urunListesi = sepettekiler.Split("-", StringSplitOptions.RemoveEmptyEntries);
                if (urunListesi.Length == 0)
                {

                }
            }
            else
            {
                sepettekiler = "Ürün Seçiniz...";
            }
        }
    }
}
