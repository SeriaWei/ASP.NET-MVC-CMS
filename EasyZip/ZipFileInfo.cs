using System;

namespace EasyZip
{
    [Serializable]
    public class ZipFileInfo
    {
        public string RelativePath
        {
            get;
            set;
        }

        public byte[] FileBytes
        {
            get;
            set;
        }
    }
}
