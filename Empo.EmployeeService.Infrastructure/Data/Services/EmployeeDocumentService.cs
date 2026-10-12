using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.EmployeeDocument;
using Empo.EmployeeService.Application.EmployeeDocument.UploadDocument;
using Empo.EmployeeService.Application.Enums;
using Empo.EmployeeService.Infrastructure.Data.Entities.Document;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Services;

public class EmployeeDocumentService : IEmployeeDocumentService
{
    private readonly EmployeeContext _dbContext;
    private readonly IFileStorageService _storage;

    private const string DocumentContainer = "employee-documents";
    private const long MaxFileSizeBytes = 10 * 1024 * 1024;

    private static readonly string[] AllowedTypes =
    {
        "application/pdf", "image/jpeg", "image/png",
        "application/msword",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
    };
    public EmployeeDocumentService(EmployeeContext dbContext, IFileStorageService storage)
    {
        _dbContext = dbContext;
        _storage = storage;
    }

    public async Task<DocumentModel> UploadAsync(Guid employeeId, UploadEmployeeDocumentRequest request, Guid uploadedByUserId)
    {
        if (request.File is null || request.File.Length == 0)
            throw new BadRequestException("A file is required.");
        if (request.File.Length > MaxFileSizeBytes)
            throw new BadRequestException("File exceeds the 10 MB limit.");
        if (!AllowedTypes.Contains(request.File.ContentType))
            throw new BadRequestException($"File type '{request.File.ContentType}' is not allowed for documents.");
        if (!Enum.TryParse<DocumentType>(request.DocumentType, ignoreCase: true, out var documentType))
            throw new BadRequestException($"'{request.DocumentType}' is not a valid document type.");

        await using var stream = request.File.OpenReadStream();
        var stored = await _storage.SaveAsync(stream, request.File.FileName, request.File.ContentType, DocumentContainer);

        var document = new EmployeeDocumentEntity
        {
            EmployeeId = employeeId,
            DocumentType = documentType,
            FileName = request.File.FileName,
            StorageKey = stored.StorageKey,
            ContentType = stored.ContentType,
            SizeInBytes = stored.SizeInBytes,
            UploadedByUserId = uploadedByUserId,
            UploadedAt = DateTime.UtcNow,
        };

        _dbContext.EmployeeDocument.Add(document);
        await _dbContext.SaveChangesAsync();

        return MapToDto(document);
    }

    public async Task DeleteAsync(Guid employeeId, Guid documentId)
    {
        var document = await _dbContext.EmployeeDocument
    .FirstOrDefaultAsync(d => d.Id == documentId && d.EmployeeId == employeeId);
        if (document is null)
            throw new NotFoundException("Document", documentId);

        await _storage.DeleteAsync(document.StorageKey);
        _dbContext.EmployeeDocument.Remove(document);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<DownloadedFile> DownloadAsync(Guid employeeId, Guid documentId)
    {
        var document = await _dbContext.EmployeeDocument
    .FirstOrDefaultAsync(d => d.Id == documentId && d.EmployeeId == employeeId);
        if (document is null)
            throw new NotFoundException("Document", documentId);

        var stream = await _storage.OpenReadAsync(document.StorageKey);
        if (stream is null)
            throw new NotFoundException("The document file could not be found in storage.");

        return new DownloadedFile(stream, document.ContentType, document.FileName);
    }

    public async Task<List<DocumentModel>> ListAsync(Guid employeeId)
    {
        return await _dbContext.EmployeeDocument
            .Where(d => d.EmployeeId == employeeId)
            .OrderByDescending(d => d.UploadedAt)
            .Select(d => new DocumentModel
            {
                Id = d.Id,
                EmployeeId = d.EmployeeId,
                DocumentType = d.DocumentType.ToString(),
                FileName = d.FileName,
                SizeInBytes = d.SizeInBytes,
                UploadedAt = d.UploadedAt,
                IsVerified = d.IsVerified,
            }).ToListAsync();
    }

    public async Task<DocumentModel> VerifyAsync(Guid employeeId, Guid documentId, Guid verifiedByUserId)
    {
        var document = await _dbContext.EmployeeDocument
    .FirstOrDefaultAsync(d => d.Id == documentId && d.EmployeeId == employeeId);
        if (document is null)
            throw new NotFoundException("Document", documentId);

        document.IsVerified = true;
        document.VerifiedByUserId = verifiedByUserId;
        document.VerifiedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        return MapToDto(document);
    }
    private static DocumentModel MapToDto(EmployeeDocumentEntity d) => new()
    {
        Id = d.Id,
        EmployeeId = d.EmployeeId,
        DocumentType = d.DocumentType.ToString(),
        FileName = d.FileName,
        SizeInBytes = d.SizeInBytes,
        UploadedAt = d.UploadedAt,
        IsVerified = d.IsVerified,
    };
}
