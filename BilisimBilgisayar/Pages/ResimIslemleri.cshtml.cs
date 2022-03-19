using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace BilisimBilgisayar.Pages
{
    public class ResimIslemleriModel : PageModel
    {
        private IHostingEnvironment Environment;
        public string Message { get; set; }
        public List<FileModel> Files { get; set; }

        public ResimIslemleriModel(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public void OnGet(string sil = null)
        {//stackoverflow.com/questions/53350488/deleting-files-from-wwwroot-folder-what-i-am-doing-wrong
            if (sil != null)
            {
                string fullPath = Path.Combine(this.Environment.WebRootPath, "uploads/" + sil);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    Response.Redirect("/ResimIslemleri");//hala adreste durmasýn
                }
                else
                {
                    Message = sil + " bulunamadý...";
                }
            }

            dosyaListesiGetir();
        }

        //aspsnippets.com/Articles/ASPNet-Core-Razor-Pages-Display-List-of-Files-from-Folder-Directory.aspx
        private void dosyaListesiGetir()
        {
            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "uploads\\"));

            this.Files = new List<FileModel>();
            foreach (string filePath in filePaths)
            {
                this.Files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
            }
        }

        //aspsnippets.com/Articles/Upload-File-in-ASPNet-Core-Razor-Pages.aspx
        public void OnPostUpload(List<IFormFile> postedFiles)
        {
            string wwwPath = this.Environment.WebRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    this.Message += string.Format("<b>{0}</b> yollandý.<br />", fileName);
                }
            }
            dosyaListesiGetir();
        }
    }
}
public class FileModel
{
    public string FileName { get; set; }
}
