using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WUI.Models
{
    /// <summary>
    /// Poi : Point d'intérêt = Localisation GPS associée à une catégorie : Départ, Arrivée, Ravitaillement...
    /// </summary>
    public class PoiModel : PointModel
    {
        public string Title { get; set; }

        public int idCategory { get; set; }

        public int idPoint { get; set; }

        public IEnumerable<CategoryModel> Category { get; set; }
        
        public List<SelectListItem> SelectlistCategory { get; set; }
    }
}