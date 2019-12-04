using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExelReader.Transformer.FileTypes
{
    public class ContactInfo
    {
        public DateTime as_Of_Date { get; set; }
        public string alliance_name { get; set; }
        public string contract_id { get; set; }
        public string sub_id { get; set; }
        public string client_name { get; set; }
        public string contract_status { get; set; }
        public string plan_type { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
    }
}
