using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExelReader.Transformer.FileTypes
{
    public class FundInfo
    {
        public string contract_id { get; set; }
        public string sub_id { get; set; }
        public string ticker { get; set; }
        public string cusip { get; set; }
        public string fund_name { get; set; }
        public float amount { get; set; }
        public string secid { get; set; }
    }
}
