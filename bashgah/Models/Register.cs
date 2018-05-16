using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace bashgah.Models
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        public string date { get; set; }
        public string time { get; set; }


       
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual customer Customer { get; set; }

    }
}