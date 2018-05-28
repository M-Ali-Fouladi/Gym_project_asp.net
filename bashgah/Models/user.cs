using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.ComponentModel.DataAnnotations;
namespace bashgah.Models
{
    public class user
    {
     
        public int Id { get; set; }
        public string name { get; set; }

        public string birthdate { get; set; }
        public DateTime birth { get; set; }
    }
}