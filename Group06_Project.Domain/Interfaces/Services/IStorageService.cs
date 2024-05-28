using Microsoft.AspNetCore.Http;

namespace Group06_Project.Domain.Interfaces.Services;

public interface IStorageService
{
    Task<string> UploadImage(IFormFile file);
    Task<string> UploadVideo(IFormFile file);
    Task<string> GetFileUrl(string fileId);
    Task DeleteFile(string fileId);
}