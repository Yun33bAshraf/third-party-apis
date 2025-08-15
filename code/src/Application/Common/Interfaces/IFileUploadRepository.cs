using ThirdPartyAPIs.Application.Common.Models;

namespace ThirdPartyAPIs.Application.Common.Interfaces;
public interface IFileUploadRepository
{
    Task<FileDto> UploadAndStoreFileAsync(Stream fileStream, string fileName, string contentType, long fileSize, int userId);
}
