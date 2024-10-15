using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Group06_Project.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Group06_Project.Infrastructure.Storage;

public class CloudinaryService : IStorageService
{
    private readonly Cloudinary _cloudinary;
    private readonly ILogger<CloudinaryService> _logger;

    public CloudinaryService(IConfiguration configuration, ILogger<CloudinaryService> logger)
    {
        _logger = logger;
        _cloudinary = new Cloudinary(configuration["Cloudinary:CloudUrl"])
        {
            Api =
            {
                Secure = true
            }
        };
    }

    public Task DeleteFile(string fileId)
    {
        _cloudinary.Destroy(new DeletionParams(fileId));
        return Task.CompletedTask;
    }

    public async Task<string> UploadImage(IFormFile file)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            UseFilename = false,
            Overwrite = false,
            PublicId = Guid.NewGuid().ToString()
        };
        await _cloudinary.UploadAsync(uploadParams);
        return await Task.FromResult(uploadParams.PublicId);
    }

    public async Task<string> UploadVideo(IFormFile file)
    {
        var uploadParams = new VideoUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            UseFilename = false,
            Overwrite = false,
            PublicId = Guid.NewGuid().ToString()
        };
        var result = await _cloudinary.UploadLargeAsync(uploadParams);
        return result.PublicId;
    }

    public Task<string> GetImageUrl(string fileId)
    {
        var url = _cloudinary.Api.UrlImgUp.BuildUrl(fileId);
        return Task.FromResult(url);
    }
    public Task<string> GetVideoUrl(string? fileId)
    {
        var url = _cloudinary.Api.UrlVideoUp.BuildUrl(fileId);
        return Task.FromResult(url);
    }
}