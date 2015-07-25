using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Drawing;

namespace Easy
{
    public static class Common
    {
        public static string GetLinqExpressionText(Expression ex)
        {
            switch (ex.NodeType)
            {
                case ExpressionType.Lambda:
                    {
                        return ex.ToString().Split('.')[1].Replace(")", "");
                    }
                default: return "NoName";
            }
        }
        public static Dictionary<string, string> GetEmum<T>()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Type dataType = typeof(T);
            if (!dataType.IsEnum)
            {
                throw new Exception(dataType.FullName + ",不是枚举类型。");
            }
            string[] text = Enum.GetNames(dataType);
            for (int i = 0; i < text.Length; i++)
            {
                result.Add(Enum.Format(dataType, Enum.Parse(dataType, text[i], true), "d"), text[i]);
            }
            return result;
        }
        public static string GetContentType(string extend)
        {
            string contentType;
            switch (extend.ToLower())
            {
                case ".*": contentType = "application/octet-stream"; break;
                case ".aif": contentType = "audio/aiff"; break;
                case ".aifc": contentType = "audio/aiff"; break;
                case ".aiff": contentType = "audio/aiff"; break;
                case ".anv": contentType = "application/x-anv"; break;
                case ".asa": contentType = "text/asa"; break;
                case ".asf": contentType = "video/x-ms-asf"; break;
                case ".asp": contentType = "text/asp"; break;
                case ".asx": contentType = "video/x-ms-asf"; break;
                case ".au": contentType = "audio/basic"; break;
                case ".avi": contentType = "video/avi"; break;
                case ".awf": contentType = "application/vnd.adobe.workflow"; break;
                case ".biz": contentType = "text/xml"; break;
                case ".bmp": contentType = "application/x-bmp"; break;
                case ".bot": contentType = "application/x-bot"; break;
                case ".c4t": contentType = "application/x-c4t"; break;
                case ".c90": contentType = "application/x-c90"; break;
                case ".cal": contentType = "application/x-cals"; break;
                case ".cat": contentType = "application/vnd.ms-pki.seccat"; break;
                case ".cdf": contentType = "application/x-netcdf"; break;
                case ".cdr": contentType = "application/x-cdr"; break;
                case ".cel": contentType = "application/x-cel"; break;
                case ".cer": contentType = "application/x-x509-ca-cert"; break;
                case ".cg4": contentType = "application/x-g4"; break;
                case ".cgm": contentType = "application/x-cgm"; break;
                case ".cit": contentType = "application/x-cit"; break;
                case ".class": contentType = "java/*"; break;
                case ".cml": contentType = "text/xml"; break;
                case ".cmp": contentType = "application/x-cmp"; break;
                case ".cmx": contentType = "application/x-cmx"; break;
                case ".cot": contentType = "application/x-cot"; break;
                case ".crl": contentType = "application/pkix-crl"; break;
                case ".crt": contentType = "application/x-x509-ca-cert"; break;
                case ".csi": contentType = "application/x-csi"; break;
                case ".css": contentType = "text/css"; break;
                case ".cut": contentType = "application/x-cut"; break;
                case ".dbf": contentType = "application/x-dbf"; break;
                case ".dbm": contentType = "application/x-dbm"; break;
                case ".dbx": contentType = "application/x-dbx"; break;
                case ".dcd": contentType = "text/xml"; break;
                case ".dcx": contentType = "application/x-dcx"; break;
                case ".der": contentType = "application/x-x509-ca-cert"; break;
                case ".dgn": contentType = "application/x-dgn"; break;
                case ".dib": contentType = "application/x-dib"; break;
                case ".dll": contentType = "application/x-msdownload"; break;
                case ".doc": contentType = "application/msword"; break;
                case ".docx": contentType = "application/msword"; break;
                case ".dot": contentType = "application/msword"; break;
                case ".drw": contentType = "application/x-drw"; break;
                case ".dtd": contentType = "text/xml"; break;
                case ".dwf": contentType = "Model/vnd.dwf"; break;
                case ".dwg": contentType = "application/x-dwg"; break;
                case ".dxb": contentType = "application/x-dxb"; break;
                case ".dxf": contentType = "application/x-dxf"; break;
                case ".edn": contentType = "application/vnd.adobe.edn"; break;
                case ".emf": contentType = "application/x-emf"; break;
                case ".eml": contentType = "message/rfc822"; break;
                case ".ent": contentType = "text/xml"; break;
                case ".epi": contentType = "application/x-epi"; break;
                case ".eps": contentType = "application/x-ps"; break;
                case ".etd": contentType = "application/x-ebx"; break;
                case ".exe": contentType = "application/x-msdownload"; break;
                case ".fax": contentType = "image/fax"; break;
                case ".fdf": contentType = "application/vnd.fdf"; break;
                case ".fif": contentType = "application/fractals"; break;
                case ".fo": contentType = "text/xml"; break;
                case ".frm": contentType = "application/x-frm"; break;
                case ".g4": contentType = "application/x-g4"; break;
                case ".gbr": contentType = "application/x-gbr"; break;
                case ".gcd": contentType = "application/x-gcd"; break;
                case ".gif": contentType = "image/gif"; break;
                case ".gl2": contentType = "application/x-gl2"; break;
                case ".gp4": contentType = "application/x-gp4"; break;
                case ".hgl": contentType = "application/x-hgl"; break;
                case ".hmr": contentType = "application/x-hmr"; break;
                case ".hpg": contentType = "application/x-hpgl"; break;
                case ".hpl": contentType = "application/x-hpl"; break;
                case ".hqx": contentType = "application/mac-binhex40"; break;
                case ".hrf": contentType = "application/x-hrf"; break;
                case ".hta": contentType = "application/hta"; break;
                case ".htc": contentType = "text/x-component"; break;
                case ".htm": contentType = "text/html"; break;
                case ".html": contentType = "text/html"; break;
                case ".htt": contentType = "text/webviewhtml"; break;
                case ".htx": contentType = "text/html"; break;
                case ".icb": contentType = "application/x-icb"; break;
                case ".ico": contentType = "image/x-icon"; break;
                case ".iff": contentType = "application/x-iff"; break;
                case ".ig4": contentType = "application/x-g4"; break;
                case ".igs": contentType = "application/x-igs"; break;
                case ".iii": contentType = "application/x-iphone"; break;
                case ".img": contentType = "application/x-img"; break;
                case ".ins": contentType = "application/x-internet-signup"; break;
                case ".isp": contentType = "application/x-internet-signup"; break;
                case ".IVF": contentType = "video/x-ivf"; break;
                case ".java": contentType = "java/*"; break;
                case ".jfif": contentType = "image/jpeg"; break;
                case ".jpe": contentType = "image/jpeg"; break;
                case ".jpeg": contentType = "image/jpeg"; break;
                case ".jpg": contentType = "image/jpeg"; break;
                case ".js": contentType = "application/x-javascript"; break;
                case ".jsp": contentType = "text/html"; break;
                case ".la1": contentType = "audio/x-liquid-file"; break;
                case ".lar": contentType = "application/x-laplayer-reg"; break;
                case ".latex": contentType = "application/x-latex"; break;
                case ".lavs": contentType = "audio/x-liquid-secure"; break;
                case ".lbm": contentType = "application/x-lbm"; break;
                case ".lmsff": contentType = "audio/x-la-lms"; break;
                case ".ls": contentType = "application/x-javascript"; break;
                case ".ltr": contentType = "application/x-ltr"; break;
                case ".m1v": contentType = "video/x-mpeg"; break;
                case ".m2v": contentType = "video/x-mpeg"; break;
                case ".m3u": contentType = "audio/mpegurl"; break;
                case ".m4e": contentType = "video/mpeg4"; break;
                case ".mac": contentType = "application/x-mac"; break;
                case ".man": contentType = "application/x-troff-man"; break;
                case ".math": contentType = "text/xml"; break;
                case ".mdb": contentType = "application/msaccess"; break;
                case ".mfp": contentType = "application/x-shockwave-flash"; break;
                case ".mht": contentType = "message/rfc822"; break;
                case ".mhtml": contentType = "message/rfc822"; break;
                case ".mi": contentType = "application/x-mi"; break;
                case ".mid": contentType = "audio/mid"; break;
                case ".midi": contentType = "audio/mid"; break;
                case ".mil": contentType = "application/x-mil"; break;
                case ".mml": contentType = "text/xml"; break;
                case ".mnd": contentType = "audio/x-musicnet-download"; break;
                case ".mns": contentType = "audio/x-musicnet-stream"; break;
                case ".mocha": contentType = "application/x-javascript"; break;
                case ".movie": contentType = "video/x-sgi-movie"; break;
                case ".mp1": contentType = "audio/mp1"; break;
                case ".mp2": contentType = "audio/mp2"; break;
                case ".mp2v": contentType = "video/mpeg"; break;
                case ".mp3": contentType = "audio/mp3"; break;
                case ".mp4": contentType = "video/mpeg4"; break;
                case ".mpa": contentType = "video/x-mpg"; break;
                case ".mpd": contentType = "application/vnd.ms-project"; break;
                case ".mpe": contentType = "video/x-mpeg"; break;
                case ".mpeg": contentType = "video/mpg"; break;
                case ".mpg": contentType = "video/mpg"; break;
                case ".mpga": contentType = "audio/rn-mpeg"; break;
                case ".mpp": contentType = "application/vnd.ms-project"; break;
                case ".mps": contentType = "video/x-mpeg"; break;
                case ".mpt": contentType = "application/vnd.ms-project"; break;
                case ".mpv": contentType = "video/mpg"; break;
                case ".mpv2": contentType = "video/mpeg"; break;
                case ".mpw": contentType = "application/vnd.ms-project"; break;
                case ".mpx": contentType = "application/vnd.ms-project"; break;
                case ".mtx": contentType = "text/xml"; break;
                case ".mxp": contentType = "application/x-mmxp"; break;
                case ".net": contentType = "image/pnetvue"; break;
                case ".nrf": contentType = "application/x-nrf"; break;
                case ".nws": contentType = "message/rfc822"; break;
                case ".odc": contentType = "text/x-ms-odc"; break;
                case ".out": contentType = "application/x-out"; break;
                case ".p10": contentType = "application/pkcs10"; break;
                case ".p12": contentType = "application/x-pkcs12"; break;
                case ".p7b": contentType = "application/x-pkcs7-certificates"; break;
                case ".p7c": contentType = "application/pkcs7-mime"; break;
                case ".p7m": contentType = "application/pkcs7-mime"; break;
                case ".p7r": contentType = "application/x-pkcs7-certreqresp"; break;
                case ".p7s": contentType = "application/pkcs7-signature"; break;
                case ".pc5": contentType = "application/x-pc5"; break;
                case ".pci": contentType = "application/x-pci"; break;
                case ".pcl": contentType = "application/x-pcl"; break;
                case ".pcx": contentType = "application/x-pcx"; break;
                case ".pdf": contentType = "application/pdf"; break;
                case ".pdx": contentType = "application/vnd.adobe.pdx"; break;
                case ".pfx": contentType = "application/x-pkcs12"; break;
                case ".pgl": contentType = "application/x-pgl"; break;
                case ".pic": contentType = "application/x-pic"; break;
                case ".pko": contentType = "application/vnd.ms-pki.pko"; break;
                case ".pl": contentType = "application/x-perl"; break;
                case ".plg": contentType = "text/html"; break;
                case ".pls": contentType = "audio/scpls"; break;
                case ".plt": contentType = "application/x-plt"; break;
                case ".png": contentType = "image/png"; break;
                case ".pot": contentType = "application/vnd.ms-powerpoint"; break;
                case ".ppa": contentType = "application/vnd.ms-powerpoint"; break;
                case ".ppm": contentType = "application/x-ppm"; break;
                case ".pps": contentType = "application/vnd.ms-powerpoint"; break;
                case ".ppt": contentType = "application/vnd.ms-powerpoint"; break;
                case ".pr": contentType = "application/x-pr"; break;
                case ".prf": contentType = "application/pics-rules"; break;
                case ".prn": contentType = "application/x-prn"; break;
                case ".prt": contentType = "application/x-prt"; break;
                case ".ps": contentType = "application/x-ps"; break;
                case ".ptn": contentType = "application/x-ptn"; break;
                case ".pwz": contentType = "application/vnd.ms-powerpoint"; break;
                case ".r3t": contentType = "text/vnd.rn-realtext3d"; break;
                case ".ra": contentType = "audio/vnd.rn-realaudio"; break;
                case ".ram": contentType = "audio/x-pn-realaudio"; break;
                case ".rar": contentType = "application/x-msdownload"; break;
                case ".ras": contentType = "application/x-ras"; break;
                case ".rat": contentType = "application/rat-file"; break;
                case ".rdf": contentType = "text/xml"; break;
                case ".rec": contentType = "application/vnd.rn-recording"; break;
                case ".red": contentType = "application/x-red"; break;
                case ".rgb": contentType = "application/x-rgb"; break;
                case ".rjs": contentType = "application/vnd.rn-realsystem-rjs"; break;
                case ".rjt": contentType = "application/vnd.rn-realsystem-rjt"; break;
                case ".rlc": contentType = "application/x-rlc"; break;
                case ".rle": contentType = "application/x-rle"; break;
                case ".rm": contentType = "application/vnd.rn-realmedia"; break;
                case ".rmf": contentType = "application/vnd.adobe.rmf"; break;
                case ".rmi": contentType = "audio/mid"; break;
                case ".rmj": contentType = "application/vnd.rn-realsystem-rmj"; break;
                case ".rmm": contentType = "audio/x-pn-realaudio"; break;
                case ".rmp": contentType = "application/vnd.rn-rn_music_package"; break;
                case ".rms": contentType = "application/vnd.rn-realmedia-secure"; break;
                case ".rmvb": contentType = "application/vnd.rn-realmedia-vbr"; break;
                case ".rmx": contentType = "application/vnd.rn-realsystem-rmx"; break;
                case ".rnx": contentType = "application/vnd.rn-realplayer"; break;
                case ".rp": contentType = "image/vnd.rn-realpix"; break;
                case ".rpm": contentType = "audio/x-pn-realaudio-plugin"; break;
                case ".rsml": contentType = "application/vnd.rn-rsml"; break;
                case ".rt": contentType = "text/vnd.rn-realtext"; break;
                case ".rtf": contentType = "application/msword"; break;
                case ".rv": contentType = "video/vnd.rn-realvideo"; break;
                case ".sam": contentType = "application/x-sam"; break;
                case ".sat": contentType = "application/x-sat"; break;
                case ".sdp": contentType = "application/sdp"; break;
                case ".sdw": contentType = "application/x-sdw"; break;
                case ".sit": contentType = "application/x-stuffit"; break;
                case ".slb": contentType = "application/x-slb"; break;
                case ".sld": contentType = "application/x-sld"; break;
                case ".slk": contentType = "drawing/x-slk"; break;
                case ".smi": contentType = "application/smil"; break;
                case ".smil": contentType = "application/smil"; break;
                case ".smk": contentType = "application/x-smk"; break;
                case ".snd": contentType = "audio/basic"; break;
                case ".sol": contentType = "text/plain"; break;
                case ".sor": contentType = "text/plain"; break;
                case ".spc": contentType = "application/x-pkcs7-certificates"; break;
                case ".spl": contentType = "application/futuresplash"; break;
                case ".spp": contentType = "text/xml"; break;
                case ".ssm": contentType = "application/streamingmedia"; break;
                case ".sst": contentType = "application/vnd.ms-pki.certstore"; break;
                case ".stl": contentType = "application/vnd.ms-pki.stl"; break;
                case ".stm": contentType = "text/html"; break;
                case ".sty": contentType = "application/x-sty"; break;
                case ".svg": contentType = "text/xml"; break;
                case ".swf": contentType = "application/x-shockwave-flash"; break;
                case ".tdf": contentType = "application/x-tdf"; break;
                case ".tg4": contentType = "application/x-tg4"; break;
                case ".tga": contentType = "application/x-tga"; break;
                case ".tif": contentType = "image/tiff"; break;
                case ".tiff": contentType = "image/tiff"; break;
                case ".tld": contentType = "text/xml"; break;
                case ".top": contentType = "drawing/x-top"; break;
                case ".torrent": contentType = "application/x-bittorrent"; break;
                case ".tsd": contentType = "text/xml"; break;
                case ".txt": contentType = "text/plain"; break;
                case ".uin": contentType = "application/x-icq"; break;
                case ".uls": contentType = "text/iuls"; break;
                case ".vcf": contentType = "text/x-vcard"; break;
                case ".vda": contentType = "application/x-vda"; break;
                case ".vdx": contentType = "application/vnd.visio"; break;
                case ".vml": contentType = "text/xml"; break;
                case ".vpg": contentType = "application/x-vpeg005"; break;
                case ".vsd": contentType = "application/vnd.visio"; break;
                case ".vss": contentType = "application/vnd.visio"; break;
                case ".vst": contentType = "application/vnd.visio"; break;
                case ".vsw": contentType = "application/vnd.visio"; break;
                case ".vsx": contentType = "application/vnd.visio"; break;
                case ".vtx": contentType = "application/vnd.visio"; break;
                case ".vxml": contentType = "text/xml"; break;
                case ".wav": contentType = "audio/wav"; break;
                case ".wax": contentType = "audio/x-ms-wax"; break;
                case ".wb1": contentType = "application/x-wb1"; break;
                case ".wb2": contentType = "application/x-wb2"; break;
                case ".wb3": contentType = "application/x-wb3"; break;
                case ".wbmp": contentType = "image/vnd.wap.wbmp"; break;
                case ".wiz": contentType = "application/msword"; break;
                case ".wk3": contentType = "application/x-wk3"; break;
                case ".wk4": contentType = "application/x-wk4"; break;
                case ".wkq": contentType = "application/x-wkq"; break;
                case ".wks": contentType = "application/x-wks"; break;
                case ".wm": contentType = "video/x-ms-wm"; break;
                case ".wma": contentType = "audio/x-ms-wma"; break;
                case ".wmd": contentType = "application/x-ms-wmd"; break;
                case ".wmf": contentType = "application/x-wmf"; break;
                case ".wml": contentType = "text/vnd.wap.wml"; break;
                case ".wmv": contentType = "video/x-ms-wmv"; break;
                case ".wmx": contentType = "video/x-ms-wmx"; break;
                case ".wmz": contentType = "application/x-ms-wmz"; break;
                case ".wp6": contentType = "application/x-wp6"; break;
                case ".wpd": contentType = "application/x-wpd"; break;
                case ".wpg": contentType = "application/x-wpg"; break;
                case ".wpl": contentType = "application/vnd.ms-wpl"; break;
                case ".wq1": contentType = "application/x-wq1"; break;
                case ".wr1": contentType = "application/x-wr1"; break;
                case ".wri": contentType = "application/x-wri"; break;
                case ".wrk": contentType = "application/x-wrk"; break;
                case ".ws": contentType = "application/x-ws"; break;
                case ".ws2": contentType = "application/x-ws"; break;
                case ".wsc": contentType = "text/scriptlet"; break;
                case ".wsdl": contentType = "text/xml"; break;
                case ".wvx": contentType = "video/x-ms-wvx"; break;
                case ".xdp": contentType = "application/vnd.adobe.xdp"; break;
                case ".xdr": contentType = "text/xml"; break;
                case ".xfd": contentType = "application/vnd.adobe.xfd"; break;
                case ".xfdf": contentType = "application/vnd.adobe.xfdf"; break;
                case ".xhtml": contentType = "text/html"; break;
                case ".xls": contentType = "application/vnd.ms-excel"; break;
                case ".xlsx": contentType = "application/vnd.ms-excel"; break;
                case ".xlw": contentType = "application/x-xlw"; break;
                case ".xml": contentType = "text/xml"; break;
                case ".xpl": contentType = "audio/scpls"; break;
                case ".xq": contentType = "text/xml"; break;
                case ".xql": contentType = "text/xml"; break;
                case ".xquery": contentType = "text/xml"; break;
                case ".xsd": contentType = "text/xml"; break;
                case ".xsl": contentType = "text/xml"; break;
                case ".xslt": contentType = "text/xml"; break;
                case ".xwd": contentType = "application/x-xwd"; break;
                case ".x_b": contentType = "application/x-x_b"; break;
                case ".x_t": contentType = "application/x-x_t"; break;
                case ".zip": contentType = "application/x-msdownload"; break;

                default:
                    contentType = "text/html"; break;
            }
            return contentType;

        }

        public static DbType ConvertToDbType(TypeCode code)
        {
            switch (code)
            {
                case TypeCode.Boolean:return DbType.Boolean;
                case TypeCode.Byte: return DbType.Byte;
                case TypeCode.Char: 
                case TypeCode.DBNull: 
                case TypeCode.Empty: 
                case TypeCode.String: return DbType.String;
                case TypeCode.DateTime: return DbType.DateTime;
                case TypeCode.Decimal: return DbType.Currency;
                case TypeCode.Double: return DbType.Double;
                case TypeCode.Int16: return DbType.Int16;
                case TypeCode.Int32: return DbType.Int32;
                case TypeCode.Int64: return DbType.Int64;
                case TypeCode.Object: return DbType.Object;
                case TypeCode.SByte: return DbType.SByte;
                case TypeCode.Single: return DbType.Single;
                case TypeCode.UInt16: return DbType.UInt16;
                case TypeCode.UInt32: return DbType.UInt32;
                case TypeCode.UInt64: return DbType.UInt64;
                default: return DbType.String;
            }
        }
    }
}
