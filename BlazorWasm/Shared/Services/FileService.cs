using Microsoft.AspNetCore.Components.Forms;

namespace BlazorWasm.Shared.Services
{
    public interface IFileService
    {
        Task<string?> ProcessImageAsync(IBrowserFile file, int maxWidth = 400, int maxHeight = 400, long maxSizeBytes = 2 * 1024 * 1024);
    }

    public class FileService : IFileService
    {
        public async Task<string?> ProcessImageAsync(IBrowserFile file, int maxWidth = 400, int maxHeight = 400, long maxSizeBytes = 2 * 1024 * 1024)
        {
            if (file == null) return null;

            if (!file.ContentType.StartsWith("image/"))
                throw new ArgumentException("El archivo debe ser una imagen.");

            if (file.Size > maxSizeBytes)
                throw new ArgumentException($"La imagen no debe superar los {maxSizeBytes / (1024 * 1024)}MB.");

            try
            {
                var resizedFile = await file.RequestImageFileAsync(file.ContentType, maxWidth, maxHeight);
                var buffer = new byte[resizedFile.Size];
                using (var stream = resizedFile.OpenReadStream(maxSizeBytes))
                {
                    await stream.ReadAsync(buffer);
                }
                return $"data:{resizedFile.ContentType};base64,{Convert.ToBase64String(buffer)}";
            }
            catch (Exception)
            {
                throw new Exception("Error al procesar la imagen.");
            }
        }
    }
}
