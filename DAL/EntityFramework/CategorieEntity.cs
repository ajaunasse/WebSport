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
    
    public partial class CategorieEntity
    {
        public CategorieEntity()
        {
            this.Points = new HashSet<PointEntity>();
        }
    
        public int Id { get; set; }
        public string Titre { get; set; }
    
        public virtual ICollection<PointEntity> Points { get; set; }
    }
}
