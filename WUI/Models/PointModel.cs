using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WUI.Models
{
    /// <summary>
    /// Classe mère pour sauvegarder une position 
    /// </summary>
    public class PointModel : PositionModel
    {
        public int Id { get; set; }

        public List<Point> Points { get; set; }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the altitude in meters relative to sea level.
        /// </summary>
        /// <value>The altitude.</value>
        public double Altitude { get; set; }

        public List<Poi> pois { get; set; }

    }
}
