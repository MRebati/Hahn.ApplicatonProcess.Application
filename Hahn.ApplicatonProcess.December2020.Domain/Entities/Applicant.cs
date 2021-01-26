using System;
using System.Collections.Generic;
using System.Text;
using Hahn.ApplicationProcess.December2020.Domain.Infrastructure;

namespace Hahn.ApplicationProcess.December2020.Domain.Entities
{
    public class Applicant : Entity<int>
    {
        public string Name { get; set; }

        public string FamilyName { get; set; }

        public string EmailAddress { get; set; }

        public string CountryOfOrigin { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }

        public bool Hired { get; set; } = false;
        

        /*
         *
         * ID ( int )
         * Name ( string )
         * FamilyName ( string )
         * Address ( string )
         * CountryOfOrigin ( string )
         * EMailAdress ( string )
         * Age (int)
         * Hired (bool) – false if not provided.
         *
         */
    }
}
