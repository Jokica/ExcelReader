using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ExelReader.Transformer
{
    public abstract class Serilization : BaseTransform
    {
        public DateProvider dateProvider { get; set; }

        public Serilization(string from , string to)
            :base(from,to)
        {
            init();
        }

        private void init()
        {
            dateProvider = new DateProvider();
        }

        public void Serilize()
        {
            string fileName = Path.Combine(this.DirectoryTo, "data.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(DateProvider));
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            var namspaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty});
            using (var writer = new StringWriter())
            using(var xmlWriter = XmlWriter.Create(writer,settings))
            {
               
                serializer.Serialize(xmlWriter, dateProvider,namspaces);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(writer.ToString().Trim());

                doc.Save(fileName);

            };
        }
        public abstract override void transform();
  
    }
}
