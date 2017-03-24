using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WUI.Models
{
    public class ResultatModel
    {

        public PersonneModel Personne { get; set; }

        public TimeSpan TempsDeCourse { get; set; }

        public TimeSpan HeureDebut { get; set; }

        public TimeSpan HeureArrivee { get; set; }

        
        public int Classement { get; set; }

        public RaceModel Race { get; set; } 


    }
}