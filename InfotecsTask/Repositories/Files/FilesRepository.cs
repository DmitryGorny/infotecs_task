using InfotecsTask.Data;
using Microsoft.EntityFrameworkCore;

namespace InfotecsTask.Repositories.Files
{
    public class FilesRepository : IFilesRepository
    {
        private readonly AppDBContext _db_context;

        public FilesRepository(AppDBContext db_context)
        {
            _db_context = db_context;
        }

        public async Task<Models.Files> SaveFile(Models.Files file)
        {
            await _db_context.Files.AddAsync(file);
            await _db_context.SaveChangesAsync();
            return file;
        }

        public async Task DeleteFile(Models.Files file)
        {
             await _db_context.Files.Where(f => f.FileName == file.FileName)
                                    .ExecuteDeleteAsync();
        }
    }
}
