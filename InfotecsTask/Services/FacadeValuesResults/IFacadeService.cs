namespace InfotecsTask.Services.FacadeValuesResults
{
    public interface IFacadeService
    {
        public Task<List<string>> CreateValuesResults(StreamReader reader, string file_name);
    }
}
