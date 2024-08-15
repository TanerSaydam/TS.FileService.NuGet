namespace GenericFileService.Files;

public sealed record FileSaveToFtpModel(
    string Host,
    string UserName,
    string Password);
