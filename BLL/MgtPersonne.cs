﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL.EntityFramework;

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
        public Personne ValidateConnection(Personne pers)
        {
            WebSportEntities context = new WebSportEntities();

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
            bo.webpages_Roles = new List<webpages_Roles>();
            bo.webpages_Roles.Add(new webpages_Roles() { RoleId = personne.Role });

            return bo;
        }

        public Personne Addpersonne(Personne personne)
        {
            try
            {
                PersonEntity entity = BoToEntity(personne);
                WebSportEntities context = new WebSportEntities();
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

        // CREATE

        // UPDATE

        // DELETE
    }
}
