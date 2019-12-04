using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExelReader.Transformer
{
    public abstract class BaseTransform:ITransform
    {
        protected string DirectoryFrom { get; set; }
        protected string DirectoryTo { get; set; }

        public BaseTransform(string directoryFrom,string directoryTo)
        {
            this.DirectoryFrom = directoryFrom;
            this.DirectoryTo = directoryTo;
        }

        public abstract void transform();
     
    }
}
