using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HyperSlackers.Localization.Demo.Models
{
    public class DemoOneModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Range(10, 20)]
        public int RangedNumber { get; set; }
        [Required]
        [MinValue(0)]
        public int PositiveNumber { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 4)]
        [Display(Name = "StringOne (from Model)")]
        public string StringOne { get; set; }
        [Display(Name = "StringTwo (from Model)")]
        public string StringTwo { get; set; }
        [Display(Name = "StringThree (from Model)")]
        public string StringThree { get; set; }
        public string StringFour { get; set; }
    }
}