using RestAspNet5.Hypermedia;
using RestAspNet5.Hypermedia.Abstract;
using RestAspNet5.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5.Data.VO
{

    public class BookVO : ISuportHyperMedia

    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }
        public List<HyperMediaLink> links { get; set; } = new List<HyperMediaLink>();
    }
}


