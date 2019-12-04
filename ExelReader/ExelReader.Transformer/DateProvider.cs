using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ExelReader.Transformer
{
    [Serializable]
    public class DateProvider
    {
        public List<Advisor> Advisors { get; set; } = new List<Advisor>();

        public class Advisor
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string City { get; set; }
        
            public string ZipCode { get; set; }
            public string PhoneNumber { get; set; }
           
          
            public Firm Firm { get; set; }
            [XmlArrayItemAttribute(IsNullable = false)]
            public List<Account> Accounts { get; set; } = new List<Account>();

        }

        public class Firm
        {
            public string Name { get; set; }
        }

        public class State
        {
            public string Abbrevation { get; set; }
        }
        public class Account
        {
            public string AccountUid { get; set; }
            public string Name { get; set; }
            public string CompanyName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

        }

    }


}