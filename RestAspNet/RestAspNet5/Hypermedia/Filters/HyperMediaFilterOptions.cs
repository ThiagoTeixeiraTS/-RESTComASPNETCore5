using RestAspNet5.Hypermedia.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IReponseEnricher> ContentResponseEnricherList { get; set; } = new List<IReponseEnricher>();
    }
}
