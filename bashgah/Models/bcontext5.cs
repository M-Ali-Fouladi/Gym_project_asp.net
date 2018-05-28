using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;
namespace bashgah.Models
{
    public class bcontext5:DbContext
    {
        public bcontext5()
        : base("bcontext5")
        {
        }
        public DbSet<customer> customers { get; set; }
        public DbSet<Register> registers { get; set; }
      
        public DbSet<TodoItem> Todoitems { get; set; }
        public class selectModel
        {
            public int Id { get; set; }
            public string name { get; set; }
            public string family { get; set; }

            public int Idd { get; set; }
            public string date { get; set; }
            public string time { get; set; }

        }

        public System.Data.Entity.DbSet<bashgah.Models.viewmodel> viewmodels { get; set; }

        public System.Data.Entity.DbSet<bashgah.Models.user> users { get; set; }
    }
}