using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageEncryption;

namespace ImageEncryptionWebSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
               
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);

                var fileEncryptStream = DEncrypt4ImageHelper.EncryptFile(file.InputStream, "helloworld");
                var fs = System.IO.File.OpenWrite(path);
                foreach (byte b in fileEncryptStream.ToArray())
                {
                    fs.WriteByte(b);
                }
                fs.Close();
                fileEncryptStream.Close();
             
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public FileStreamResult Download(string fileName)
        {
            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
            var stream = DEncrypt4ImageHelper.DecryptFile(path, "helloworld");
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/jpg")
                                 {
                                     FileDownloadName = fileName
                                 };
        }
    }
}