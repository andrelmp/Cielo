using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Cielo.Configuration;
using Cielo.Requests.Entities;
using DynamicBuilder;

namespace Cielo.Requests
{
    public class CreateTransactionRequest : CieloRequest
    {
        public CreateTransactionRequest(
            Order order,
            PaymentMethod paymentMethod,
            CreateTransactionOptions options,
            CreditCardData creditCardData = null,
            IConfiguration configuration = null) : base(configuration)
        {
            PaymentMethod = paymentMethod;
            Options = options;
            CreditCardData = creditCardData;
            Order = order;

            UniqueKey = Guid.NewGuid();
        }

        public Order Order { get; private set; }

        public CreditCardData CreditCardData { get; private set; }

        public PaymentMethod PaymentMethod { get; private set; }

        public CreateTransactionOptions Options { get; private set; }

        public Guid UniqueKey { get; set; }

        public override string ToXml(bool indent)
        {
            dynamic xml = new Xml {UseDashInsteadUnderscore = true};
            xml.Declaration(encoding: "ISO-8859-1");
            xml.requisicao_transacao(new {id = UniqueKey, versao = CieloVersion.Version}, Xml.Fragment(req =>
            {
                Affiliate.ToXml(req, Configuration);

                if (CreditCardData != null)
                {
                    CreditCardData.ToXml(req);
                }

                Order.ToXml(req, Configuration);

                PaymentMethod.ToXml(req);

                req.url_retorno(Configuration.ReturnUrl);
                req.autorizar((int) Options.AuthorizationType);
                req.capturar(Options.Capture.ToString(CultureInfo.InvariantCulture).ToLower());
                req.gerar_token(Options.GenerateToken.ToString(CultureInfo.InvariantCulture).ToLower());
            }));

            return xml.ToString(indent);
        }

        public string ToXmlWithoutSensitiveData(bool indent)
        {
            var replaceSensitiveDataPattern = new Regex("<dados-portador>(.*)</dados-portador>");
            return replaceSensitiveDataPattern.Replace(ToXml(indent), "");
        }
    }
}