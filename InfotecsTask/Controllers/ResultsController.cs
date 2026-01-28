using InfotecsTask.Mappers;
using InfotecsTask.Queryies;
using InfotecsTask.Services.ResultsService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InfotecsTask.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IResultsService _resultsService;

        public ResultsController(IResultsService resultsService) => _resultsService = resultsService;

        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredList([FromQuery] ResultsQuery query)
        {
           List<Models.Results> results = await _resultsService.GetFilteredResults(query);
            results.Select(r => r.ToDtoFromReuslts());
           return Ok(results);
        }
    }
}
