using InfotecsTask.Dtos.ValuesDtos;
using InfotecsTask.Mappers;
using InfotecsTask.Models;
using InfotecsTask.Repositories.ValuesRepository;
using InfotecsTask.Services.FacadeValuesResults;
using InfotecsTask.Services.ResultsService;
using InfotecsTask.Services.ValuesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InfotecsTask.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IValuesService _valuesService;
        private readonly IResultsService _resultsService;
        private readonly IFacadeService _facadeService;

        public ValuesController(IValuesService valuesService, IFacadeService facadeService, IResultsService resultsService)
        {
            _valuesService = valuesService;
            _facadeService = facadeService;
            _resultsService = resultsService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> FileReader(IFormFile csv_file)
        {
            var reader = new StreamReader(csv_file.OpenReadStream(), Encoding.UTF8);
            List<string> errors = new List<string>();
            string fileName = csv_file.FileName;
            errors = await _facadeService.CreateValuesResults(reader, fileName);
               
            if (errors.Any())
            {
                return BadRequest(errors);
            }
            return Ok("Файл записан в базу");
        }

        [HttpGet("sorted")]
        public async Task<IActionResult> GetSortedList([FromQuery] string fileName)
        {
            List<Values> results = await _valuesService.GetSortedValues(fileName);
            results.Select(v => v.ToDtoFromValues());
            return Ok(results);
        }

    }
}
