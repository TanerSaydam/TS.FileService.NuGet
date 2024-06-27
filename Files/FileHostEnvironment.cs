namespace GenericFileService.Files;
internal sealed class FileHostEnvironment : IFileHostEnvironment
{
    public string WebRootPath { get; set; } = default!;
}