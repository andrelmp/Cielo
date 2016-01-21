using System;
using Cielo.Requests;
using CieloTests.Configuration;
using CieloTests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace CieloTests
{
    [TestFixture]
    public class CaptureTransactionRequestTests
    {
        private const string ExpectedXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                                <requisicao-captura id=""4c38f150-b67d-4059-88d1-b53b13e54a8e"" versao=""1.3.0"">
                                                    <tid>10069930690864271001</tid>
                                                    <dados-ec>
                                                        <numero>1001734898</numero>
                                                        <chave>e84827130b9837473681c2787007da5914d6359947015a5cdb2b8843db0fa832</chave>
                                                    </dados-ec>
                                                </requisicao-captura>";

        private const string ExpectedXml2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                                <requisicao-captura id=""4c38f150-b67d-4059-88d1-b53b13e54a8e"" versao=""1.3.0"">
                                                    <tid>10069930690864271001</tid>
                                                    <dados-ec>
                                                        <numero>1001734898</numero>
                                                        <chave>e84827130b9837473681c2787007da5914d6359947015a5cdb2b8843db0fa832</chave>
                                                    </dados-ec>
                                                    <valor>10000</valor>
                                                </requisicao-captura>";

        [Test]
        public void ToXml_GivenACaptureTransactionRequest_ShouldGenerateAXmlAsExpected()
        {
            var checkTransactionRequest = new CaptureTransactionRequest("10069930690864271001", configuration: new FakeConfiguration())
            {
                UniqueKey = Guid.Parse("4c38f150-b67d-4059-88d1-b53b13e54a8e")
            };

            checkTransactionRequest
                .ToXml(false)
                .RemoveNewLinesAndSpaces()
                .Should()
                .Be(ExpectedXml.RemoveNewLinesAndSpaces());
        }

        [Test]
        public void ToXml_GivenACaptureTransactionRequestWithValor_ShouldGenerateAXmlAsExpected()
        {
            var checkTransactionRequest = new CaptureTransactionRequest("10069930690864271001", valor: 100M, configuration: new FakeConfiguration())
            {
                UniqueKey = Guid.Parse("4c38f150-b67d-4059-88d1-b53b13e54a8e")
            };

            checkTransactionRequest
                .ToXml(false)
                .RemoveNewLinesAndSpaces()
                .Should()
                .Be(ExpectedXml2.RemoveNewLinesAndSpaces());
        }
    }
}
