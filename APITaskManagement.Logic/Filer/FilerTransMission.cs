using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Filer.Formatters;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Schedulers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace APITaskManagement.Logic.Filer
{
    public class FilerTransMission : FilerAbstract
    {
        private readonly QueueRepository queueRepository = new QueueRepository();
        private readonly TaskRepository taskRepository = new TaskRepository();

        public FilerTransMission(IList<ContentFormat> formats) : base(formats)
        {

        }

        public override void SaveDocuments(Share share, Schedulers.Task task)
        {
            var requests = new List<Request>();
            var items = queueRepository.ListByTask(task.Id, task.TotalProcessedItems);

            var response = new Response();
            var UNC = share.UNCPath + DateTime.Now.ToString("-yyyyMMddHHmmss") + ".xml";

            var formatter = new TransmissionFormatter(ContentFormat.XML);

            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement opdrachten = doc.CreateElement("oOpdrachten");
            doc.AppendChild(opdrachten);

            IList<string> ids = new List<string>();
            foreach (var item in items)
            {
                var xml = formatter.getXMLContent(doc, item.Key);
                if (xml != null)
                {
                    opdrachten.AppendChild(xml);
                    ids.Add(item.Key.ToString());
                }
            }

            if (ids.Count > 0)
            {
                try
                {
                    doc.Save(UNC);

                    response.Ids = string.Join(",", ids.ToArray());
                    response.Code = 201;
                    response.Description = "Created";
                    response.Detail = UNC + " was saved succesfully";
                }
                catch (Exception)
                {
                    
                    response.Code = 400;
                    response.Description = "Bad Request";
                    response.Detail = "There was an error when saving " + UNC;
                }
            }
            else
            {
                response.Code = 200;
                response.Description = "OK";
                response.Detail = "There where no items ";
            }

            Responses.Add(response);
            
        }
    }
}
