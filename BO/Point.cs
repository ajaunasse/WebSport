using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Classe mère pour sauvegarder une position 
    /// </summary>
    public class Point
    {
        public int Id { get; set; }

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

        public bool isPoi { get; set; }

        public string titre { get; set; }

        public Category categorie { get; set;}


    }
}
