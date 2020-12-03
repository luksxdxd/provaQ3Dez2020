using encriptDecript.Context;
using encriptDecript.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace encriptDecript.Controllers
{
    public class TextoController : Controller
    {
        private Contexto db = new Contexto();
        private static string AesIV128BD = @"%j?TmFP6$BbMnY$@";
        private static string AesKey256 = @"rxmBUJy]&,;3jKwDTzf(cui$<nc2EQr)";

        // GET: Texto
        public ActionResult Index()
        {

            return View(db.textos.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TextoModel textoModels)
        {
            if (ModelState.IsValid)
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.IV = Encoding.UTF8.GetBytes(AesIV128BD);
                aes.Key = Encoding.UTF8.GetBytes(AesKey256);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] src = Encoding.Unicode.GetBytes(textoModels.Textoss);

                var teste = textoModels.Textoss;
                textoModels.CopyTextoss = teste;

                using (ICryptoTransform encrypt = aes.CreateEncryptor())
                {
                    byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);

                    textoModels.Textoss = Convert.ToBase64String(dest);
                }


                db.textos.Add(textoModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(textoModels);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextoModel textoModel = db.textos.Find(id);
            if (textoModel == null)
            {
                return HttpNotFound();
            }

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(AesIV128BD);
            aes.Key = Encoding.UTF8.GetBytes(AesKey256);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] src = System.Convert.FromBase64String(textoModel.Textoss);

            using (ICryptoTransform decrypt = aes.CreateDecryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                textoModel.Textoss = Encoding.Unicode.GetString(dest);
            }

            return View(textoModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextoModel textoModel = db.textos.Find(id);
            if (textoModel == null)
            {
                return HttpNotFound();
            }
            return View(textoModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfimed(int id)
        {
            TextoModel textoModel = db.textos.Find(id);
            db.textos.Remove(textoModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}