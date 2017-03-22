using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Poi : Point d'intérêt = Localisation GPS associée à une catégorie : Départ, Arrivée, Ravitaillement...
    /// </summary>
    
    public class Poi : Point
    {

        public int id { get; set; } 
        public string Title { get; set; }

        private Category _category;
        public Category Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public int idCategory { get; set; }

        public int idPoint { get; set; }

        public Poi()
        {
        }
    }
}