//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class PointEntity
    {
        public PointEntity()
        {
            this.POIs = new HashSet<POIEntity>();
        }
    
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public Nullable<int> CourseId { get; set; }
    
        public virtual ICollection<POIEntity> POIs { get; set; }
        public virtual RaceEntity Course { get; set; }
    }
}
