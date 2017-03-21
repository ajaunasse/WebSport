using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Resultat
    {

        public int PersonneID { get; set; }

        public Race Race { get; set; }

        public TimeSpan HeureDebut { get; set; }

        public TimeSpan HeureArrivee { get; set; }

        public int Classement { get; set; }

        private TimeSpan _tempsDeCourse;

        public TimeSpan TempsDeCourse
        {
            get { return _tempsDeCourse; }
            set { _tempsDeCourse = HeureArrivee - HeureDebut; }
        }

    }
}
