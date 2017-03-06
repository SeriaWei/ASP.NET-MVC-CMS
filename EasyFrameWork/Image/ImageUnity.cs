/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Easy
{
    public static class ImageUnity
    {
        private static string GetAbsuPath(string Path)
        {
            bool isAbsu = Path.Contains(@":\");
            if (!isAbsu)
            {
                Path = AppDomain.CurrentDomain.BaseDirectory + Path;
            }
            return Path;
        }
        /// <summary>
        /// 缩放到新大小，裁剪原图
        /// </summary>
        /// <param name="img"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static Bitmap ScaleTrim(Bitmap img, int Width, int Height)
        {
            //缩放裁剪
            int tempWidth = 0;
            int tempHeight = 0;
            Point p = new Point();
            int detX = 0;
            int detY = 0;
            if ((double)img.Width / (double)img.Height > (double)Width / (double)Height)
            {//以高为准
                tempHeight = Height;
                tempWidth = (int)((double)Height / (double)img.Height * (double)img.Width);
                detX = tempWidth - Width;
                p.X = detX / 2;
            }
            else
            {
                tempWidth = Width;
                tempHeight = (int)((double)Width / (double)img.Width * (double)img.Height);
                detY = tempHeight - Height;
                p.Y = detY / 2;
            }
            Bitmap Thumbtemp = new Bitmap(img, tempWidth, tempHeight);
            Bitmap imgThumb = new Bitmap(Width, Height);
            Graphics gh = Graphics.FromImage(imgThumb);
            gh.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gh.DrawImage(Thumbtemp, new Rectangle(new Point(0, 0), new Size() { Width = tempWidth, Height = tempHeight }), new Rectangle(p, new Size() { Width = Width + detX, Height = Height + detY }), GraphicsUnit.Pixel);
            gh.Dispose();
            Thumbtemp.Dispose();
            img.Dispose();
            return imgThumb;
        }
        /// <summary>
        /// 缩放到新大小，宽或高为0时，按比例缩放
        /// </summary>
        /// <param name="img"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static Bitmap Scale(Bitmap img, int? Width, int? Height)
        {
            bool reset = false;
            if (!Width.HasValue)
            {
                reset = true;
                Width = (int)((double)img.Width / (double)img.Height * (double)Height);
            }
            else if (!Height.HasValue)
            {
                reset = true;
                Height = (int)((double)img.Height / (double)img.Width * (double)Width);
            }
            Bitmap imgThumb = null;
            if (reset)
            {
                imgThumb = new Bitmap(img, Width.Value, Height.Value);
            }
            else
            {
                imgThumb = ScaleTrim(img, Width.Value, Height.Value);
            }
            return imgThumb;
        }

        /// <summary>
        /// 按新大小保存图片（自动删除原图）
        /// </summary>
        /// <param name="SavePath">原图路径</param>
        /// <param name="NewWidth">新图宽度,0表示不强制宽度</param>
        /// <param name="NewHeight">新图高度,0表示不强制高度</param>
        public static void SizeTo(string Path, int? Width, int? Height)
        {
            Path = GetAbsuPath(Path);
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(Path);
            Bitmap imgThumb = Scale(img, Width, Height);
            img.Dispose();
            System.IO.File.Delete(Path);
            imgThumb.Save(Path, System.Drawing.Imaging.ImageFormat.Jpeg);
            imgThumb.Dispose();
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="Path">原图路径-可相对，可绝对</param>
        /// <param name="Width">缩略图宽度,</param>
        /// <param name="Height">缩略图高度,</param>
        /// <returns>返回缩略图名称</returns>
        public static string SetThumb(string Path, int? Width, int? Height)
        {
            Path = GetAbsuPath(Path);
            Bitmap img = new Bitmap(Path);
            Bitmap imgThumb = Scale(img, Width, Height);
            string fileName = System.IO.Path.GetFileName(Path);
            string fileExt = System.IO.Path.GetExtension(Path);
            string thumbName = Path.Replace(fileExt, string.Format("_Thumb_{0}x{1}_{2}", Width, Height, fileExt));
            imgThumb.Save(thumbName);
            img.Dispose();
            imgThumb.Dispose();
            return System.IO.Path.GetFileName(thumbName);
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="Captcha"></param>
        /// <returns></returns>
        public static Bitmap GetCaptcha(out string Captcha)
        {
            int width = 120;
            int height = 40;
            Bitmap code = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(code);
            graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));
            StringBuilder builder = new StringBuilder();
            Random ran = new Random(DateTime.Now.Millisecond);
            FontFamily family;
            List<string> exceptFont = new List<string>() { "Marlett", "MS Outlook", "MS Reference Specialty", "MT Extra", "Webdings", "Wingdings 2", "Wingdings 3", "Wingdings", "Bookshelf Symbol 7" };
            while (true)
            {
                family = FontFamily.Families[ran.Next(0, FontFamily.Families.Length)];
                if (!exceptFont.Contains(family.Name))
                    break;
            }
            Font f = new Font(family, 28);
            PointF p = new PointF() { X = ran.Next(0, 15) };
            for (int i = 0; i < 4; i++)
            {
                int chose = ran.Next(0, 2);
                string chr = "0";
                if (chose == 0)
                {
                    int c = ran.Next(65, 91);
                    chr = Convert.ToChar(c).ToString();
                    builder.Append(chr);
                }
                else
                {
                    int c = ran.Next(0, 10);
                    chr = c.ToString();
                    builder.Append(c);
                }
                graphics.DrawString(chr, f, Brushes.DodgerBlue, p);
                for (int j = 0; j < 3; j++)
                {
                    graphics.DrawLine(new Pen(Brushes.CadetBlue, ran.Next(0, 3)), ran.Next(0, width), ran.Next(0, height), ran.Next(0, width), ran.Next(0, height));
                }
                p.X += 10 + ran.Next(0, 25);
                p.Y = ran.Next(-10, 10);
            }
            graphics.Dispose();
            Captcha = builder.ToString();
            return code;
        }
        public static Bitmap DrawName(string Name, int size)
        {
            Random ran = new Random();
            Color co = Color.FromArgb(ran.Next(200), ran.Next(200), ran.Next(200));

            Bitmap map = new Bitmap(size, size);
            Graphics gh = Graphics.FromImage(map);
            gh.FillRectangle(new SolidBrush(co), 0, 0, map.Width, map.Height);
            float fnotSize = map.Width;
            SizeF nameSize = new SizeF();
            Font fo = new Font("楷体", fnotSize, FontStyle.Bold);
            for (; fnotSize >= 0; fnotSize--)
            {
                fo = new Font("楷体", fnotSize);
                nameSize = gh.MeasureString(Name, fo);
                if (nameSize.Width <= map.Width && nameSize.Height <= map.Height)
                {
                    break;
                }
            }
            float y = map.Height - nameSize.Height;
            gh.DrawString(Name, fo, new SolidBrush(Color.White), new PointF() { X = (map.Width - nameSize.Width) / 2, Y = y });
            return map;
        }
    }
}
