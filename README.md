# Dependency

This library was created by .Net 9.0

## Install

```bash
dotnet add package TS.FileService
```

## Usage
If you want to save file in server
```csharp
string filePath = "./Files/";

string fileName = FileService.FileSaveToServer(file,filePath);
```

If you want to save file in Database (byte[])
```csharp
byte[] fileByeArray = FileService.FileConvertByteArrayToDatabase(file);
```

If you want to delete file in server
```csharp
string path = "./Files/" + "FileName";

FileService.FileDeleteToServer(path);
```