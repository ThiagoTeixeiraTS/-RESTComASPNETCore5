using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5.Hypermedia.Abstract
{
    public interface ISuportHyperMedia
    {
        List<HyperMediaLink> links { get; set; }
    }
}
