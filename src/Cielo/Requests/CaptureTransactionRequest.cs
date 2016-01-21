using System;
using Cielo.Configuration;
using DynamicBuilder;
using Cielo.Extensions;

namespace Cielo.Requests
{
    public class CaptureTransactionRequest : CieloRequest
    {
        public CaptureTransactionRequest(string tid, decimal? valor = null, IConfiguration configuration = null)
            : base(configuration)
        {
            UniqueKey = Guid.NewGuid();
            Tid = tid;
            Valor = valor;
        }

        public Guid UniqueKey { get; set; }

        public string Tid { get; private set; }

        public decimal? Valor { get; set; }

        public override string ToXml(bool indent)
        {
            dynamic xml = new Xml { UseDashInsteadUnderscore = true };
            xml.Declaration(encoding: "ISO-8859-1");

            xml.requisicao_captura(new { id = UniqueKey, versao = CieloVersion.Version }, Xml.Fragment(req =>
            {
                req.tid(Tid);
                Affiliate.ToXml(req, Configuration);
                if (Valor.HasValue && Valor.Value > 0)
                {
                    req.valor(Valor.Value.ToCieloFormatValue());
                }
            }));

            return xml.ToString(indent);

        }
    }
}
