using InfotecsTask.Data;
using InfotecsTask.Models;
using InfotecsTask.Services.ResultsService;
using InfotecsTask.Services.ValuesService;

namespace InfotecsTask.Services.FacadeValuesResults
{
    public class FacadeService : IFacadeService
    {
        private readonly IValuesService _valuesService;
        private readonly IResultsService _resultsService;
        private readonly AppDBContext _dbContext;

        public FacadeService(IValuesService valuesService, IResultsService resultsServise, AppDBContext context)
        {
            _valuesService = valuesService;
            _resultsService = resultsServise;
            _dbContext = context;
        }

        public async Task<List<string>> CreateValuesResults(StreamReader reader, string file_name)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                List<string> errors = await _valuesService.CreateValues(reader, file_name);
                IReadOnlyList<Values> values = _valuesService.GetValues();

                if (errors.Any())
                {
                    transaction.Rollback();
                    return errors;
                } 

                await _resultsService.CreateResult(values);

                _dbContext.SaveChanges(); 
                transaction.Commit();

                return errors;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
