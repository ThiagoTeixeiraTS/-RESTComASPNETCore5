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
  
    public class PersonVO : ISuportHyperMedia
    {

        public long Id { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string Gender { get; set; }
        public List<HyperMediaLink> links { get ; set ; } = new List<HyperMediaLink>();
    }
}
