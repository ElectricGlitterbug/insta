using Session6Instagram.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Session6Instagram.Controllers
{
    public class PhotoController : Controller
    {
        public ActionResult UploadPhoto()
        {
            InstagramDbContext database = new InstagramDbContext();
            var users = database.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public ActionResult SavePhoto(HttpPostedFileBase photo, int userId, string caption)
        {
            InstagramDbContext database = new InstagramDbContext();
            Photo temp_photo = new Photo();

            
            

            temp_photo.Picture = new byte[photo.ContentLength];
            photo.InputStream.Read(temp_photo.Picture, 0, photo.ContentLength);
            temp_photo.Caption = caption;
            temp_photo.PhotoUser = database.Users.Find(userId);
            temp_photo.Date = DateTime.Now;
            database.Photos.Add(temp_photo);
            database.SaveChanges();

            return RedirectToAction("Feed");
        }

        public ActionResult Feed()
        {
            var database = new InstagramDbContext();
            return View(database.Photos.OrderByDescending(p => p.Date).ToList());
        }

        public ActionResult GetImage(int id)
        {
            var database = new InstagramDbContext();
            var photo = database.Photos.Find(id);
            using (var ms = new MemoryStream(photo.Picture))
            {
                var image = System.Drawing.Image.FromStream(ms);

                var ratioX = (double)300 / image.Width;
                var ratioY = (double)300 / image.Height;
                var ratio = Math.Min(ratioX, ratioY);

                var width = (int)(image.Width * ratio);
                var height = (int)(image.Height * ratio);

                var newImage = new System.Drawing.Bitmap(width, height);
                System.Drawing.Graphics.FromImage(newImage).DrawImage(image, 0, 0, width, height);
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(newImage);

                System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();

                photo.Picture = (byte[])converter.ConvertTo(bmp, typeof(byte[]));

                var stream = new MemoryStream(photo.Picture);

                var bitmap = new Bitmap(stream);

                var newstream = new MemoryStream();
                bitmap.Save(newstream, ImageFormat.Jpeg);
                newstream.Position = 0;

                return new FileStreamResult(newstream, "image/jpg");


                //  return "data:image/*;base64," + Convert.ToBase64String(photo.Picture);

            }
        }
    }
}