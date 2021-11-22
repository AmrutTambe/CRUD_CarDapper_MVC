using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CRUD_CarDapper_MVC.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string CarName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Model of Car")]
        public string CarModel { get; set; }
        [Required]
       
        [Display(Name = "Company of Car")]
        public string Company { get; set; }

        public int Price { get; set; }

        //public char? Hide { get; set; }
        //public List<Car> std { get; set; }
        //public string SelectedText { get; set; }

    }
}