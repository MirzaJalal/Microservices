using Microservices.Api.Search.Interfaces;
using Microservices.Api.Search.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microservices.Api.Search.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;
        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var sResult = await searchService.SearchAsync(term.CustomerId);
            if (sResult.IsSuccess)
            {
                return Ok(sResult.SearchResults);
            }
            return NotFound();
        }
    }
}
