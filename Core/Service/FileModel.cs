using System.IO.Compression;

namespace Core.Service;

public class FileModel
{
    public FileModel(string root, string dir, string fileName)
    {
        Dir = dir;
        FileName = fileName;
        Root = root;
    }

    public FileModel(string root, string file)
    {
        Root = root;
        var parms = file.Split(Path.DirectorySeparatorChar);
        Dir = string.Join(Path.DirectorySeparatorChar, parms[new Range(0, parms.Length - 2)]);
        FileName = parms[^1];
    }
    
    public string Root { get; }
    public string Dir { get; }
    public string FileName { get;  }

    public string FilePath => Path.Join(Dir, FileName);

    public string FullFilePath => Path.Join(Root, Dir, FileName);

    public string DownloadFileName { get; set; }
    
    public string ReadAllText()
    {
        return File.ReadAllText(FullFilePath);
    }

    public byte[] ReadAllBytes()
    {
        return File.ReadAllBytes(FullFilePath);
    }

    public string ReadAllAsBase64()
    {
        if (!File.Exists(FullFilePath))
            return "";
        
        return Convert.ToBase64String(ReadAllBytes());
    }
    
    public void WriteAllText(string data)
    {
        File.WriteAllText(FullFilePath, data);
    }

    public void WriteAllBytes(byte[] data)
    {
        File.WriteAllBytes(FullFilePath, data);
    }

    public void WriteBase64ToFile(string base64Data)
    {
        File.WriteAllBytes(FullFilePath, Convert.FromBase64String(base64Data));
    }

    public void FileCopy(FileModel target, bool overwrite = false)
    {
        File.Copy(FullFilePath, target.FullFilePath, overwrite);
    }

    public void Unzip(DirectoryModel dir)
    {
        ZipFile.ExtractToDirectory(FullFilePath, dir.FullDirPath, true);
    }

    public void Delete()
    {
        File.Delete(FullFilePath);
    }
}