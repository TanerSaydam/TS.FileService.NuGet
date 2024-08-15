using Microsoft.AspNetCore.Http;

namespace GenericFileService.Files;

public static class FileService
{
    /// <summary>
    /// The FileSaveToServer method save your file in a folder your computer
    /// </summary>
    public static string FileSaveToServer(IFormFile file, string filePath)
    {
        string fileName = string.Join(".", DateTime.Now.ToFileTime().ToString(), file.FileName);
        string path = filePath + fileName;
        using (var stream = File.Create(path))
        {
            file.CopyTo(stream);
        }
        return fileName;
    }

    /// <summary>
    /// The FileConvertByteArrayToDatabase method convert file to byte[] and return
    /// </summary>
    public static byte[] FileConvertByteArrayToDatabase(IFormFile file)
    {
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            var fileBytes = memoryStream.ToArray();
            string fileString = Convert.ToBase64String(fileBytes);
            return fileBytes;
        }
    }

    /// <summary>
    /// The FileDeleteToServer method delete file in your computer
    /// </summary>
    public static void FileDeleteToServer(string path)
    {
        try
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        catch (Exception)
        {
        }
    }
}
