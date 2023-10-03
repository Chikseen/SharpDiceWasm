using System;
using System.Diagnostics;

public class Image
{
	// preCompileConfig
	const int PixelSkip = 1; // more pixel skiped -> faster -> blured

	const int CornerCheckRadius = 8; // Padding to borders -> more pixel -> more accuracy -> more time


	// RunTimeVar
	private byte[,] _Data;
	private readonly uint Size = 0;
	private ushort Width = 0;
	private ushort Height = 0;
	private uint PixelAmount = 0;

	public Image(byte[] byteArray, ushort width)
	{
		Stopwatch toCompleteTimer = new Stopwatch();
		toCompleteTimer.Start();

		Size = (uint)byteArray.Length;
		PixelAmount = Size / 4;
		Width = width;
		Height = (ushort)(byteArray.Length / 4 / Width);
		Console.WriteLine("Image Size: " + byteArray.Length + "byte");
		Console.WriteLine("Width: " + Width + "px");
		Console.WriteLine("Height: " + Height + "px");

		_Data = new byte[PixelAmount, 4];
		for (int i = 0; i < PixelAmount; i += PixelSkip)
		{
			_Data[i, 0] = byteArray[i * 4];
			_Data[i, 1] = byteArray[(i * 4) + 1];
			_Data[i, 2] = byteArray[(i * 4) + 2];
			_Data[i, 3] = byteArray[(i * 4) + 3];
		}
		Console.WriteLine($"SetUp: " + toCompleteTimer.ElapsedMilliseconds + "ms");
	}

	public void GetContrast()
	{
		Stopwatch toCompleteTimer = new Stopwatch();
		toCompleteTimer.Start();

		for (int i = 0; i < PixelAmount; i += PixelSkip)
		{
			Pixel pixel = new(_Data[i, 0], _Data[i, 1], _Data[i, 2], _Data[i, 3]);
			byte avg = pixel.GetAvgWthoutAlpha();
			if (avg > 127)
			{
				_Data[i, 0] = 255;
				_Data[i, 1] = 255;
				_Data[i, 2] = 255;
				_Data[i, 3] = 255;
			}
			else
			{
				_Data[i, 0] = 0;
				_Data[i, 1] = 0;
				_Data[i, 2] = 0;
				_Data[i, 3] = 255;
			}
		}
		Console.WriteLine($"GC: " + toCompleteTimer.ElapsedMilliseconds + "ms");
	}

	public void GetCorners()
	{
		Stopwatch toCompleteTimer = new Stopwatch();
		toCompleteTimer.Start();

		// Loop Every Pixel
		for (int index = 0; index < PixelAmount; index += PixelSkip + CornerCheckRadius)
		{
			int x = index / Width;
			int y = index % Width;

			// Check if Pixel is in X Bounds
			if (x > CornerCheckRadius && x < (Width - CornerCheckRadius))
			{
				// Check if Pixel is in Y Bounds
				if (y > CornerCheckRadius && y < (Width - CornerCheckRadius))
				{
					int darkCounter = 0;
					int lightCounter = 0;
					// Get Every Pixel in certain Radius
					for (int i = 0 - CornerCheckRadius; i < CornerCheckRadius; i++)
					{
						for (int j = 0 - CornerCheckRadius; j < CornerCheckRadius; j++)
						{
							int xOffset = x + i;
							int yOffset = (Width * y) + (Width * j);
							int byteArrayOffset = xOffset + yOffset;

							Pixel pixel = new(_Data[byteArrayOffset, 0], _Data[byteArrayOffset, 1], _Data[byteArrayOffset, 2], _Data[byteArrayOffset, 3]);
							byte avg = pixel.GetAvgWthoutAlpha();
							if (avg > 127)
							{
								darkCounter++;
							}
							else
							{
								lightCounter++;
							}
						}
					}

					double darkPrecentage = darkCounter / (double)(darkCounter + lightCounter) * 100d;


					if (darkPrecentage > 0d && darkPrecentage < 1d)
					{
						Console.WriteLine(darkPrecentage);
						for (int i = 0 - CornerCheckRadius; i < CornerCheckRadius; i++)
						{
							for (int j = 0 - CornerCheckRadius; j < CornerCheckRadius; j++)
							{
								int xOffset = x + i;
								int yOffset = (Width * y) + (Width * j);
								int byteArrayOffset = xOffset + yOffset;

								_Data[byteArrayOffset, 0] = 255;
								_Data[byteArrayOffset, 1] = 0;
								_Data[byteArrayOffset, 2] = 0;
								_Data[byteArrayOffset, 3] = 255;
							}
						}
					}
				}
			}

		}
		Console.WriteLine($"Corners: " + toCompleteTimer.ElapsedMilliseconds + "ms");
	}

	public byte[] GetByteArray()
	{
		Stopwatch toCompleteTimer = new Stopwatch();
		toCompleteTimer.Start();

		byte[] byteArray = new byte[Size];

		for (int i = 0; i < PixelAmount; i += PixelSkip)
		{
			byteArray[i * 4] = _Data[i, 0];
			byteArray[(i * 4) + 1] = _Data[i, 1];
			byteArray[(i * 4) + 2] = _Data[i, 2];
			byteArray[(i * 4) + 3] = _Data[i, 3];
		}
		Console.WriteLine($"GC: " + toCompleteTimer.ElapsedMilliseconds + "ms");
		return byteArray;
	}
}