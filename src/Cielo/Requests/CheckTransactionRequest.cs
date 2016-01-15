using System;
using Cielo.Configuration;
using DynamicBuilder;

namespace Cielo.Requests
{
    public class CheckTransactionRequest : CieloRequest
    {
        public CheckTransactionRequest(string tid, IConfiguration configuration = null)
            : base(configuration)
        {
            Tid = tid;
            UniqueKey = Guid.NewGuid();
        }

        public Guid UniqueKey { get; set; }

        public string Tid { get; private set; }

        public override string ToXml(bool indent)
        {
            dynamic xml = new Xml {UseDashInsteadUnderscore = true};
            xml.Declaration(encoding: "ISO-8859-1");

            xml.requisicao_consulta(new {id = UniqueKey, versao = CieloVersion.Version}, Xml.Fragment(req =>
            {
                req.tid(Tid);
                Affiliate.ToXml(req, Configuration);
            }));

            return xml.ToString(indent);
        }
    }
}