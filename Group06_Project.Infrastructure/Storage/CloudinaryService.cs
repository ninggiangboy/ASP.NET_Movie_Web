using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Group06_Project.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Group06_Project.Infrastructure.Storage;

public class CloudinaryService : IStorageService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration configuration)
    {
        _cloudinary = new Cloudinary(configuration["Cloudinary:CloudUrl"])
        {
            Api =
            {
                Secure = true
            }
        };
    }

    public Task<string> UploadFileAsync(IFormFile file)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.Name, file.OpenReadStream()),
            UseFilename = false,
            UniqueFilename = true,
            Overwrite = true
        };
        var uploadResult = _cloudinary.Upload(uploadParams);
        return Task.FromResult(uploadResult.PublicId);
    }

    public Task<string> GetFileUrlAsync(string fileId)
    {
        var url = _cloudinary.Api.UrlImgUp.BuildUrl(fileId);
        return Task.FromResult(url);
    }

    public Task DeleteFileAsync(string fileId)
    {
        _cloudinary.Destroy(new DeletionParams(fileId));
        return Task.CompletedTask;
    }
}