using InfotecsTask.Dtos.ValuesDtos;
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
            Console.WriteLine(csv_file.Length);
            if (csv_file == null || csv_file.Length == 0) return BadRequest("Файл не выбран");

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

    }
}
