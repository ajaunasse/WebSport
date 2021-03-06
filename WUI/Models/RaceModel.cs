﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WUI.Models.Attributes;


namespace WUI.Models
{
    /// <summary>
    /// Course
    /// </summary>
    public class RaceModel
    {
        [Display(Name = "Identifiant")]
        public int Id { get; set; }

        [Display(Name = "Titre")]
        [Required(ErrorMessage = "Le {0} est requis")]
        public string Title { get; set; }

        [Display(Name = "Description de la course")]
        [StringLength(200, MinimumLength=20, ErrorMessage="La {0} doit faire au minimum 20 caractères")]
        [Required(ErrorMessage = "La {0} est requise")]
        public string Description { get; set; }

        [Display(Name = "Date de début")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        // Par défaut, une date est requise car le type DateTime n'est pas "Nullable"
        public DateTime DateStart { get; set; }

        [Display(Name = "Date de fin d'inscription")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DateEnd { get; set; }
        
        [Display(Name = "Ville")]
        [Required(ErrorMessage = "La {0} est requise")]
        public string Town { get; set; }

        public int Distance { get; set; }

        public PointModel point { get; set; }

        public List<CompetitorModel> Competitors { get; set; }

        public List<OrganizerModel> Organisers { get; set; }

        public List<PointModel> Points { get; set; }


    }
}
