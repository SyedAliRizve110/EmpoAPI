namespace Empo.EmployeeService.Infrastructure.Data.Services;

public interface IFileStorageService
{
    Task<StoredFile> SaveAsync(Stream fileStream, string fileName, string contentType, string container);
    Task DeleteAsync(string storageKey);
    Task<Stream> OpenReadAsync(string storageKey);
}

public record StoredFile(string StorageKey, string PublicUrl, long SizeInBytes, string ContentType);
