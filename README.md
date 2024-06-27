# Dependency

This library was created by .Net 8.0

## Install

```bash
dotnet add package TS.FileService
```

## Integration
IoC Configuration and Usage of FileService
This WebAPI project manages static files (wwwroot directory) using the TS.FileService package. To utilize this package, you need to perform dependency injection (DI) and provide the necessary configuration.

Step 1: Adding Dependencies to IoC Container
In your Program.cs file, use the AddFileService method to configure the IFileHostEnvironment interface. This method ensures that the necessary dependencies are registered correctly in the IoC container:

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
```csharp
builder.Services.AddFileService(builder.Environment.WebRootPath);
If you are using layered architecture
IoC Code
builder.Services.AddInfrastructure(builder.Environment.WebRootPath);
The logical thing to do is to install the package in the Domain layer and use it in the Infrastructure Layer as follows.

public static class DependencyInjection
{
 public static IServiceCollection AddInfrastructure
 (
 this IServiceCollection services,
 string webRootPath
 )
 {
 services.AddFileService(webRootPath);
 return services;
 }
}
```

## Usage
If you want to save file in server
```Csharp
string filePath = "./Files/";

string fileName = FileService.FileSaveToServer(file,filePath);
```

If you want to save file in Ftp
```Csharp
FileSaveToFtpModel fileSaveToFtpModel = new("ftp.ftpadresiniz.com","userName", "password");

string fileName = FileService.FileSaveToFtp(file,fileSaveToFtpModel);
```

If you want to save file in Database (byte[])
```Csharp
byte[] fileByeArray = FileService.FileConvertByteArrayToDatabase(file);
```

If you want to delete file in server
```Csharp
//string path = "./Files/" + "FileName";

If you want to use the package using layered architecture
Example Handler Code:
internal sealed class MyCommandHandler(IFileHostEnvironment fileHostEnvironment) : IRequestHandler
bla bla bla
this handle code

var fullPath = Path.Combine(fileHostEnvironment.WebRootPath, "Files", file.jpeg);

FileService.FileDeleteToServer(fullPath);
```

If you want to delete file in ftp
```Csharp
FileSaveToFtpModel fileSaveToFtpModel = new("ftp.ftpadresiniz.com","userName", "password");
string path = "./Files/" + "FileName";

FileService.FileDeleteToFtp(path,fileSaveToFtpModel);
```

## Method and Class
```Csharp
public class FileSaveToFtpModel
    {
        public FileSaveToFtpModel(string ftpAddress, string userName, string password)
        {
            FtpAddress = ftpAddress;
            UserName = userName;
            Password = password;
        }
        public string FtpAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
```


```Csharp
public static class FileService
    {
        public static string FileSaveToServer(IFormFile file, string filePath)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            fileFormat = fileFormat.ToLower();
            string fileName = Guid.NewGuid().ToString() + fileFormat;
            string path = filePath + fileName;
            using (var stream = System.IO.File.Create(path))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public static string FileSaveToFtp(IFormFile file, FileSaveToFtpModel fileSaveToFtpModel)
        {
            var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            fileFormat = fileFormat.ToLower();
            string fileName = Guid.NewGuid().ToString() + fileFormat;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(
                $"{fileSaveToFtpModel.FtpAddress}{fileName}");
            request.Credentials = new NetworkCredential(
                fileSaveToFtpModel.UserName,
                fileSaveToFtpModel.Password);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream ftpStream = request.GetRequestStream())
            {
                file.CopyTo(ftpStream);
            }

            return fileName;
        }

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

        public static void FileDeleteToFtp(string path, FileSaveToFtpModel fileSaveToFtpModel)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(
                    $"{fileSaveToFtpModel}{path}");
                request.Credentials = new NetworkCredential(
                    fileSaveToFtpModel.UserName,
                    fileSaveToFtpModel.Password);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
            }
        }
    }
```
