using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace texreplace
{
	class Program
	{
		static void Main(string[] args)
		{
			byte color = args.Length == 0 ? (byte)255 : byte.Parse(args[0]);

			// Current path
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
				Directory.Delete(newTexPath, true);
			}
			Directory.CreateDirectory(newTexPath);
						
			string[] textures = Directory.GetFiles(texPath);
			foreach (string texturePath in textures)
			{
				byte[] bytes = File.ReadAllBytes(texturePath);

				// http://www.dragonwins.com/domains/getteched/bmp/bmpfileformat.htm

				int colorTableStart = 14 + 40; // Skip file header and image header straight to the color table
				int biClrUsed = BitConverter.ToInt32(bytes, colorTableStart - 8); // But read amount of colors in the table
				if (biClrUsed == 0)
				{
					biClrUsed = 256;
				}

				// Set all colors in the table to white!
				for (int i = 0; i < biClrUsed; ++i)
				{
					int colorOffset = colorTableStart + i * 4;
					
					bytes[colorOffset + 0] = color; // B
					bytes[colorOffset + 1] = color; // G
					bytes[colorOffset + 2] = color; // R
				}

				// Write changed bytes
				string fileName = Path.GetFileName(texturePath);
				string copiedFile = newTexPath + "/" + fileName;
				File.WriteAllBytes(copiedFile, bytes);
			}
		}
	}
}
