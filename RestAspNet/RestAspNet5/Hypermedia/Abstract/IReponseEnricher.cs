using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace RestAspNet5.Hypermedia.Abstract
{
    public interface IReponseEnricher
    {
        bool CanEnrich(ResultExecutingContext context);

        Task Enrich(ResultExecutingContext context);
    }
}
