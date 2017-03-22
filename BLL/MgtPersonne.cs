using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL.EntityFramework;
using Repository;

namespace BLL
{
    public class MgtPersonne
    {

        private static MgtPersonne _instance;

        public static MgtPersonne GetInstance()
        {
            if (_instance == null)
                _instance = new MgtPersonne();
            return _instance;
        }
        private UnitOfWork _uow { get; set; }

        public MgtPersonne()
        {
            _uow = new UnitOfWork();
        }
        public Personne ValidateConnection(Personne pers)
        {
            WebSportEntities context = new WebSportEntities();

            var test = context.PersonEntities.Where(p => p.Mail == pers.Email);

            PersonEntity entity = context.PersonEntities.Single(p =>p.Mail == pers.Email);

            if (entity.Password == pers.Password)
            {
                pers = EntityToBO(entity);
                return pers;
            }
            else
            {
                return null;
            }


        }

        public Personne EntityToBO(PersonEntity personne)
        {
            Personne bo = new Personne(personne.Mail);
            bo.Id = personne.Id;
            bo.Nom = personne.Lastname;
            bo.Prenom = personne.Firstname;
            bo.DateNaissance = personne.BirthDate;
            bo.Password = personne.Password;
            bo.Phone = personne.Phone;
            bo.Role = personne.webpages_Roles.First().RoleId;

            return bo;
        }

        public PersonEntity BoToEntity(Personne personne)
        {

            WebSportEntities context = new WebSportEntities();

            PersonEntity bo = new PersonEntity();
            bo.Mail = personne.Email;
            bo.Lastname = personne.Nom;
            bo.Firstname = personne.Prenom;
            if (personne.DateNaissance == DateTime.MinValue)
            {
                bo.BirthDate = null;
            }
            else
            {
                bo.BirthDate = personne.DateNaissance;
            }
            
            bo.Password = personne.Password;
            bo.Phone = personne.Phone;
            
            return bo;
        }

        public Personne Addpersonne(Personne personne)
        {
            try
            {
                PersonEntity entity = BoToEntity(personne);
                WebSportEntities context = new WebSportEntities();
                webpages_Roles role = context.webpages_Roles.FirstOrDefault(r => r.RoleId == personne.Role);
                entity.webpages_Roles.Add(role);
                role.Personnes.Add(entity);
                context.PersonEntities.Add(entity);
                context.SaveChanges();

                return EntityToBO(entity);

            }
            catch (Exception ex)
            {
                string t = ex.ToString();
                return null;
            }
        }

        public List<Personne> GetAllItems() {
            return this._uow.PersonneRepo.GetAllItems();
        }

        public Personne UpdatePersonne(Personne personne)
        {
            try
            {
                PersonEntity entity = BoToEntity(personne);
                WebSportEntities context = new WebSportEntities();
                entity = context.PersonEntities.Single(p => p.Id == personne.Id);
                entity.Firstname = personne.Prenom;
                entity.Lastname = personne.Nom;
                entity.Mail = personne.Email;
                entity.Phone = personne.Phone;
                if (personne.DateNaissance == DateTime.MinValue)
                {
                    entity.BirthDate = null;
                }
                else
                {
                    entity.BirthDate = personne.DateNaissance;
                }
                entity.Password = personne.Password;
                
                context.SaveChanges();

                return EntityToBO(entity);

            }
            catch (Exception ex)
            {
                string t = ex.ToString();
                return null;
            }


        }

        // CREATE

        // UPDATE

        // DELETE
    }
}
