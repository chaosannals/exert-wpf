using System;
using System.IO;
using System.Linq;
using System.Resources;
using Microsoft.EntityFrameworkCore;
using HaloKid.Models;
using HaloKid.Properties;

namespace HaloKid
{
    public class HkDbc : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AccountModel>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite(@"Data Source=HaloKid.db");
        }

        public static void Initialize()
        {
            if (!File.Exists("HaloKid.db"))
            {
                File.WriteAllBytes("HaloKid.db", Resources.ResourceManager.GetObject("HaloKid") as byte[]);
            }
            using (HkDbc dbc = new HkDbc())
            {
                if (dbc.Set<AccountModel>().Count() < 1)
                {
                    dbc.Set<AccountModel>().Add(new AccountModel
                    {
                        Id = 1,
                        Account = "admin",
                        Password = "123456".ToSha256(),
                        Nickname = "管理员",
                        CreatorId = 1,
                    });
                    dbc.SaveChanges();
                }
            }
        }
    }
}
