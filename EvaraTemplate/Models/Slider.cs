﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaraTemplate.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "boş qala bilməz!")]
        public string Offer { get; set; } 
        public string? Title1 { get; set; }
        public string? Title2 { get; set; }
        public string? Description { get; set; }
        public string? ButtonTitle { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
