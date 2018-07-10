using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Mailer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Mailer
{
    public class POSFormatter : MailerFormatterAbstract
    {
        private readonly PosRepository posRepository;

        public POSFormatter(ContentFormat format) : base(format)
        {
            posRepository = new PosRepository();
        }

        public override bool saveJSONContent()
        {
            throw new NotImplementedException();
        }

        public override bool saveXMLContent()
        {
            throw new NotImplementedException();
        }

        public override bool saveTXTContent()
        {
            throw new NotImplementedException();
        }

        public override IList<string> getXMLContent(int key = -1)
        {
            throw new NotImplementedException();
        }

        public override IList<string> getJSONContent(int key = -1)
        {
            throw new NotImplementedException();
        }

        public override IList<string> getTXTContent(int key = -1)
        {

            var lines = new List<string>();

            var order = posRepository.GetById(key);

            lines.Add("AI1:" + order.AI1);
            lines.Add("AI2:" + order.AI2);
            lines.Add("AKN:24372");
            lines.Add("KVN:" + order.KVN);
            lines.Add("KNA:" + order.KNA);
            lines.Add("KST:" + order.KST);
            lines.Add("KLA:" + order.KLA);
            lines.Add("KPL:" + order.KPL);
            lines.Add("KOR:" + order.KOR);
            if (order.KLD != null)
            {
                var kld = order.KLD.ToString("ddMMyyyy");
                if (kld != "01010001")
                {
                    lines.Add("KLD:" + kld);
                }
            }
            if (order.KRD != null)
            {
                var krd = order.KRD.ToString("ddMMyyyy");
                if (krd != "01010001")
                {
                    lines.Add("KRD:" + krd);
                }
            }
            if (order.KEM != null)
            {
                lines.Add("KEM:" + order.KEM);
            }
            if (order.KTM != null)
            {
                lines.Add("KTM:" + order.KTM);
            }
            lines.Add("PZA:1");
            lines.Add("PAN:1");
            lines.Add("PMA:" + order.PMA);
            lines.Add("PBA:" + order.PBA);
            lines.Add("PTL:" + order.PTL);
            lines.Add("PSD:" + order.PSD);
            lines.Add("PEN:1");

            return lines;
            // return String.Join(Environment.NewLine, array);
        }
    }
}
