using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace EasyZip
{
    public class ZipFile
    {
        private readonly List<KeyValuePair<string, FileInfo>> _singleFiles = new List<KeyValuePair<string, FileInfo>>();

        private readonly List<DirectoryInfo> _directories = new List<DirectoryInfo>();

        public void AddFile(FileInfo file)
        {
            this._singleFiles.Add(new KeyValuePair<string, FileInfo>("\\", file));
        }

        public void AddFile(FileInfo file, string relativePath)
        {
            if (!relativePath.EndsWith("\\"))
            {
                relativePath += "\\";
            }
            this._singleFiles.Add(new KeyValuePair<string, FileInfo>(relativePath, file));
        }

        public void AddDirectory(DirectoryInfo directory)
        {
            this._directories.Add(directory);
        }

        private void CollectDirectory(string root, DirectoryInfo directory, ZipFileInfoCollection zipFiles)
        {
            FileInfo[] files = directory.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fileInfo = files[i];
                this.CollectFile(fileInfo.DirectoryName.Replace(root, "") + "\\", fileInfo, zipFiles);
            }
            DirectoryInfo[] directories = directory.GetDirectories();
            for (int j = 0; j < directories.Length; j++)
            {
                DirectoryInfo directory2 = directories[j];
                this.CollectDirectory(root, directory2, zipFiles);
            }
        }

        private void CollectFile(string root, FileInfo file, ZipFileInfoCollection zipFiles)
        {
            using (FileStream fileStream = file.OpenRead())
            {
                if ((File.GetAttributes(file.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & file.Extension != ".gz")
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        byte[] array = new byte[10240];
                        int count;
                        while ((count = fileStream.Read(array, 0, array.Length)) > 0)
                        {
                            memoryStream.Write(array, 0, count);
                        }
                        zipFiles.Add(new ZipFileInfo
                        {
                            FileBytes = memoryStream.ToArray(),
                            RelativePath = root + file.Name
                        });
                    }
                }
            }
        }

        public void Compress(string path)
        {
            ZipFileInfoCollection zipFileInfoCollection = new ZipFileInfoCollection();
            foreach (DirectoryInfo current in this._directories)
            {
                string root = (current.Parent == null) ? current.FullName : current.Parent.FullName;
                this.CollectDirectory(root, current, zipFileInfoCollection);
            }
            foreach (KeyValuePair<string, FileInfo> current2 in this._singleFiles)
            {
                this.CollectFile(current2.Key, current2.Value, zipFileInfoCollection);
            }
            using (GZipStream gZipStream = new GZipStream(File.Create(path), CompressionMode.Compress))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(gZipStream, zipFileInfoCollection);
            }
        }

        public void Decompress(FileInfo fileToDecompress, string saveDirectory)
        {
            using (FileStream fileStream = fileToDecompress.OpenRead())
            {
                using (GZipStream gZipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    ZipFileInfoCollection zipFileInfoCollection = binaryFormatter.Deserialize(gZipStream) as ZipFileInfoCollection;
                    foreach (ZipFileInfo current in zipFileInfoCollection)
                    {
                        string path = saveDirectory + current.RelativePath;
                        if (!Directory.Exists(Path.GetDirectoryName(path)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(path));
                        }
                        using (FileStream fileStream2 = File.Create(path))
                        {
                            fileStream2.Write(current.FileBytes, 0, current.FileBytes.Length);
                        }
                    }
                }
            }
        }

        public MemoryStream ToMemoryStream()
        {
            ZipFileInfoCollection zipFileInfoCollection = new ZipFileInfoCollection();
            foreach (DirectoryInfo current in this._directories)
            {
                string root = (current.Parent == null) ? current.FullName : current.Parent.FullName;
                this.CollectDirectory(root, current, zipFileInfoCollection);
            }
            foreach (KeyValuePair<string, FileInfo> current2 in this._singleFiles)
            {
                this.CollectFile(current2.Key, current2.Value, zipFileInfoCollection);
            }
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, zipFileInfoCollection);
            memoryStream.Position = 0L;
            return memoryStream;
        }

        public ZipFileInfoCollection ToFileCollection(Stream stream)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            return binaryFormatter.Deserialize(stream) as ZipFileInfoCollection;
        }
    }
}
