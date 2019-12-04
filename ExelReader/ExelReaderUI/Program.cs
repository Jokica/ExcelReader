using ExelReader.Transformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExelReaderUI
{
    class Program
    {
        static void Main(string[] args)
        {
           

            var DirectoryToReadFrom = @"C:\Users\Jovan Gjorgjiev\Desktop\Source";
            var DirectoryToWriteTo = @"C:\Users\Jovan Gjorgjiev\Desktop\toWrite";

            var transformer = new TransmericaTransform(DirectoryToReadFrom, DirectoryToWriteTo);

            transformer.transform();

        }
    }
}
