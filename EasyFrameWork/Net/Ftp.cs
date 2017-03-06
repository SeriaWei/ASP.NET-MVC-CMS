/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Easy.Net
{
    public class Ftp
    {
        FtpWebRequest _ftp;
        readonly string _ftpUrl;
        readonly string _userName;
        readonly string _passWord;
        public Ftp(string ftpUrl, string userName, string passWord)
        {
            this._ftpUrl = ftpUrl;
            this._userName = userName;
            this._passWord = passWord;
            Connect();
        }
        void Connect()
        {
            _ftp = (FtpWebRequest)WebRequest.Create(new Uri(this._ftpUrl));
            _ftp.UseBinary = true;
            _ftp.Credentials = new NetworkCredential(this._userName, this._passWord);
        }
        public bool UpLoad(string file)
        {
            FileInfo copyfile = new FileInfo(file);
            _ftp.Method = WebRequestMethods.Ftp.UploadFile;
            const int bufflength = 2048;
            byte[] buff = new byte[bufflength];
            FileStream upstream = copyfile.OpenRead();
            try
            {
                Stream ftpstream = _ftp.GetRequestStream();
                int readLength;
                while ((readLength = upstream.Read(buff, 0, bufflength)) > 0)
                {
                    ftpstream.Write(buff, 0, readLength);
                }
                upstream.Close();
                ftpstream.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
