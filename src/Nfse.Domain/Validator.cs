using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Nfse.Domain
{
    public static class Validator
    {
        private static XmlSchemaSet _schemaSet;
        private static string DefaultSchemaUri = @"..\..\..\..\..\assets\PL_NFSe_1.00A\";

        static Validator()
        {
            LoadSchema(schemaUri: DefaultSchemaUri);
        }

        public static void LoadSchema(string schemaUri)
        {
            _schemaSet = new XmlSchemaSet();

            _schemaSet.Add("http://www.w3.org/2000/09/xmldsig#", $"{schemaUri}xmldsig-core-schema_v1.00.xsd");
            _schemaSet.Add("http://www.sped.fazenda.gov.br/nfse", $"{schemaUri}tiposSimples_v1.00.xsd");
            _schemaSet.Add("http://www.sped.fazenda.gov.br/nfse", $"{schemaUri}tiposComplexos_v1.00.xsd");
            _schemaSet.Add("http://www.sped.fazenda.gov.br/nfse", $"{schemaUri}DPS_v1.00.xsd");
            _schemaSet.Add("http://www.sped.fazenda.gov.br/nfse", $"{schemaUri}NFSe_v1.00.xsd");
            _schemaSet.Compile();
        }

        public static void Validate(string xml)
        {
            var tr = new StringReader(xml);
            var doc = XDocument.Load(tr);
            doc.Validate(_schemaSet, ValidationEventHandler);
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlSeverityType type = XmlSeverityType.Warning;
            if (Enum.TryParse<XmlSeverityType>("Error", out type))
            {
                if (type == XmlSeverityType.Error) throw new Exception(e.Message);
            }
        }
    }
}
