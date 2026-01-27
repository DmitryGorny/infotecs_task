namespace InfotecsTask.Repositories.Files
{
    public interface IFilesRepository
    {
        public Task<Models.Files> SaveFile(Models.Files file);

        public Task DeleteFile(Models.Files file);
    }
}
