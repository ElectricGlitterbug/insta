using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Session6Instagram.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
    }

    public class Photo
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public DateTime Date { get; set; }
        public string Caption { get; set; }

        public virtual User PhotoUser { get; set; }
    }

    public class InstagramDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}