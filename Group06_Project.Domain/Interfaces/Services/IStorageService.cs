using Microsoft.AspNetCore.Http;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IStorageService
{
    Task<string> UploadFileAsync(IFormFile file);
    Task<string> GetFileUrlAsync(string fileId);
    Task DeleteFileAsync(string fileId);
}