using OpenProfileAPI.Application.Common.Models;

namespace OpenProfileAPI.Application.Common.Interfaces;
public interface IS3FileRepository
{
    Task<(FileDto file, string url)> UploadFileAsync(Stream fileStream, FileDto fileDto);
    Task<string> GetFileUrlAsync(string filepath, int expiresInMin = 15);
}
