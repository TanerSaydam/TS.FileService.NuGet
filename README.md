# Dependency

This library was created by .Net 8.0

## Install

```bash
dotnet add package TS.FileService
```

## Usage
If you want to save file in server
```Csharp
string filePath = "./Files/";

string fileName = FileService.FileSaveToServer(file,filePath);
```

If you want to save file in Database (byte[])
```Csharp
byte[] fileByeArray = FileService.FileConvertByteArrayToDatabase(file);
```

If you want to delete file in server
```Csharp
string path = "./Files/" + "FileName";

FileService.FileDeleteToServer(path);
```