using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace GamesMarket.BusinessLogic
{
	public static class Picture
	{
		public static byte[] PicturesToByte(string path)
		{
			Image image = Image.FromFile(path);
			MemoryStream memoryStream = new MemoryStream();
			image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
			byte[] bytesPicture = memoryStream.ToArray();

			return bytesPicture;
		}

		public static string BytesToPicture(byte[] picture, int count)
		{
            if (picture == null)
            {
                return "";
            }
			char from = (char)92;
			char to = (char)47;
			MemoryStream memoryStream1 = new MemoryStream();

			foreach (byte pic in picture)
			{
				memoryStream1.WriteByte(pic);
			}
			Image image = Image.FromStream(memoryStream1);
			string path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath + "img1", "pictureTest" + count + ".png");
			image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
			return to + Path.Combine("img1", "pictureTest" + count + ".png").Replace(from, to);

		}

		public static void DropPictures()
		{
			string[] allFoundFiles = Directory.GetFiles(HostingEnvironment.ApplicationPhysicalPath + "img1", "*.png", SearchOption.TopDirectoryOnly);

			foreach (var file in allFoundFiles)
			{
				File.Delete(file);
			}

		}
	}
}