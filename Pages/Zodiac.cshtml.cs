using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zodiacs.Models;

namespace Zodiacs.Pages
{
    public class ZodiacModel : PageModel
    {
        [BindProperty] // Bind the property to capture form input
        [Display(Name = "Year:")]
        public int Years { get; set; }
        
        public string? ZodiacSign { get; set; }

        public void OnGet()
        {
            ViewData["ZodiacImage"] = null; // Reset on GET
            ViewData["YearError"] = null;   // Reset error message
        }

        public void OnPost()
        {
            int currentYear = DateTime.Now.Year;
            if (Years >= 1900 && Years <= currentYear + 1)
            {
                // Get the zodiac sign and set ViewData
                ZodiacSign = Utils.GetZodiac(Years);

                if (!string.IsNullOrEmpty(ZodiacSign))
                {
                    ViewData["ZodiacImage"] = $"/img/{ZodiacSign}.png";
                    ViewData["YearError"] = null; // Clear any error
                }
            }
            else
            {
                // If the year is out of range, set the error message
                ViewData["ZodiacImage"] = null;  // Clear image if invalid input
                ViewData["YearError"] = "Year must be between 1900 and next year. Please try again.";
            }
        }
    }
}
