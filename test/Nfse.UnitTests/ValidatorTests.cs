using Nfse.Domain;
using System;
using System.IO;
using Xunit;

namespace Nfse.UnitTests
{
    public class ValidatorTests
    {
        [Fact]
        public void ValidateXml()
        {
            var xml = File.ReadAllText(@"D:\staff\Downloads\PL_NFSe_0.10A\test-send.xml");

            Validator.Validate(xml);
        }
    }
}
