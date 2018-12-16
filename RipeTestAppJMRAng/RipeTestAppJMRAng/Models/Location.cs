using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RipeTestAppJMRAng.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        
        public string Longitude { get; set; }
        
        public string Lattitude { get; set; }
    }
}