using System;
using System.Drawing;
using System.IO;

namespace texreplace
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

			string texPath = path + "/TEXTURE";
			if (!Directory.Exists(texPath))
			{
				Console.WriteLine("TEXTURE folder not found");
				return;
			}

			string newTexPath = path + "/TEXTURE_";
			
			if (Directory.Exists(newTexPath))
			{
				Directory.Delete(newTexPath);
			}
			Directory.CreateDirectory(newTexPath);

			int size = 1;
			Bitmap whiteImage = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			for (int i = 0; i < size; ++i)
			{
				for (int j = 0; j < size; ++j)
				{
					whiteImage.SetPixel(i, j, Color.White);
				}
			}
			string templatePath = newTexPath + "/template.bmp";
			whiteImage.Save(templatePath);

			string[] textures = Directory.GetFiles(texPath);
			foreach (string texturePath in textures)
			{
				string fileName = Path.GetFileName(texturePath);
				string copiedFile = newTexPath + "/" + fileName;
				File.Copy(templatePath, copiedFile);
			}
			File.Delete(templatePath);
		}
	}
}
