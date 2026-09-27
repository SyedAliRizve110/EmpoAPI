using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Empo.EmployeeService.Infrastructure.Data.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _rootPath;
    private readonly string _publicBaseUrl;
    public LocalFileStorageService(IConfiguration config, IWebHostEnvironment env)
    {
        _rootPath = Path.Combine(env.ContentRootPath, config["Storage:LocalRootPath"] ?? "storage");
        _publicBaseUrl = config["Storage:PublicBaseUrl"] ?? "http://localhost:7208/files";
        Directory.CreateDirectory(_rootPath);
    }

    public Task DeleteAsync(string storageKey)
    {
        var fullPath = Path.Combine(_rootPath, storageKey.Replace('/', Path.DirectorySeparatorChar));
        if (File.Exists(fullPath))
            File.Delete(fullPath);
        return Task.CompletedTask;
    }

    public Task<Stream> OpenReadAsync(string storageKey)
    {
        var fullPath = Path.Combine(_rootPath, storageKey.Replace('/', Path.DirectorySeparatorChar));
        if (!File.Exists(fullPath))
            return Task.FromResult<Stream?>(null);

        Stream stream = File.OpenRead(fullPath);
        return Task.FromResult<Stream?>(stream);
    }

    public async Task<StoredFile> SaveAsync(Stream fileStream, string fileName, string contentType, string container)
    {
        var extension = Path.GetExtension(fileName);
        var storageKey = $"{container}/{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(_rootPath, storageKey.Replace('/', Path.DirectorySeparatorChar));

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        await using (var output = File.Create(fullPath)!)
        {
            await fileStream.CopyToAsync(output);
        }

        var sizeInBytes = new FileInfo(fullPath).Length;
        var publicUrl = $"{_publicBaseUrl}/{storageKey}";

        return new StoredFile(storageKey, publicUrl,
            sizeInBytes, contentType);

    }
}
