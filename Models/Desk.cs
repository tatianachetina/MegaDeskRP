using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskRP.Models
{
    public class Desk
    {

        public int DeskId { get; set; }

        [Range(24, 96)]
        [Required]
        public int Width { get; set; }

        [Range(12, 48)]
        [Required]
        public int Depth { get; set; }

        [Display(Name = "Number of Drawers")]
        [Range(0, 7)]
        [Required]
        public int NumberOfDrawers { get; set; }

        [Display(Name = "Surface Material")]
        public string SurfaceMaterialId { get; set; }
        public SurfaceMaterial SurfaceMaterial { get; set; }

    }
}

