using Microsoft.AspNetCore.Mvc;
using RestAspNet5.Data.VO;
using RestAspNet5.Hypermedia.Contants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAspNet5.Hypermedia.Enricher
{
    public class BookEnricher : ContentResponseEnricher<BookVO>
    {

        private readonly object _lock = new object();
        protected override Task EnrichModel(BookVO content, IUrlHelper urlHelper)
        {
            var path = "api/v1/books";
            string link = getLink(content.Id,  urlHelper ,   path);

            content.links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGET

            });

            content.links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPOST

            });

            content.links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPUT

            });

            content.links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"

            });

            return null;
        }

        private string getLink(long id, IUrlHelper urlHelper, string path)
        {
           lock(_lock)
            {
                var url = new { Controller = path, id = id };
                return new StringBuilder(urlHelper.Link("DefaultAPI", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
