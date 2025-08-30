using OpenProfileAPI.Application.Common.Models;

namespace OpenProfileAPI.Application.Common.Interfaces;
public interface IFileUploadRepository
{
    Task<FileDto> UploadAndStoreFileAsync(Stream fileStream, string fileName, string contentType, long fileSize, int userId);
}
