using InfotecsTask.Data;
using InfotecsTask.Dtos.FilesDto;
using InfotecsTask.Mappers;
using InfotecsTask.Models;
using InfotecsTask.Repositories.Files;
using InfotecsTask.Services.ResultsService;
using InfotecsTask.Services.ValuesService;
using SQLitePCL;
using System.Linq.Expressions;

namespace InfotecsTask.Services.FacadeValuesResults
{
    public class FacadeService : IFacadeService
    {
        private readonly IValuesService _valuesService;
        private readonly IResultsService _resultsService;
        private readonly IFilesRepository _fileRepository;
        private readonly AppDBContext _dbContext;

        public FacadeService(IValuesService valuesService,
            IResultsService resultsServise,
            AppDBContext context,
            IFilesRepository filesRepository)
        {
            _valuesService = valuesService;
            _resultsService = resultsServise;
            _dbContext = context;
            _fileRepository = filesRepository;
        }

        public async Task<List<string>> CreateValuesResults(StreamReader reader, string file_name)
        {
            if (string.IsNullOrWhiteSpace(file_name)) 
                return new List<string> { "Ошибка: Название файла не может быть пустым" };


            string firstLine = reader.ReadLine();

            if (firstLine == null || string.IsNullOrWhiteSpace(firstLine))
            {
                return new List<string> { "Ошибка: файл пуст" };
            }

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                Files file = await CreateFile(file_name);
                List<string> errors = await _valuesService.CreateValues(reader, file.Id);
                IReadOnlyList<Values> values = _valuesService.GetValues();

                if (errors.Any())
                {
                    await transaction.RollbackAsync();
                    await RemoveFile(file);
                    return errors;
                } 

                await _resultsService.CreateResult(values);

                await _dbContext.SaveChangesAsync(); 
                await transaction.CommitAsync();

                return errors;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<Files> CreateFile(string file_name)
        {
            FilesCreateDto file_dto = new FilesCreateDto { FileName = file_name };
            Files file = file_dto.ToFilesFromCreateDto();
            await _fileRepository.DeleteFile(file);
            file = await _fileRepository.SaveFile(file);
            return file;
        }

        private async Task RemoveFile(Files file)
        {
            await _fileRepository.DeleteFile(file);
        }
    }
}
