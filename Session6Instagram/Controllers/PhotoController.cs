using Session6Instagram.Models;
using System;
using System.Collections.Generic;
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
    }
}