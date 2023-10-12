using System;
using System.Diagnostics;

public class Image
{
	// preCompileConfig
	const int _pixelSkip = 1; // more pixel skiped -> faster -> blured
	const int _maxDiceDotHeight = 10; // more pixel skiped -> faster -> blured
	const bool _isDebugMode = true;

	// RunTimeVar
	private byte[] _data;
	private readonly uint _size = 0;
	private int _width = 0;
	private int _height = 0;

	public Image(byte[] byteArray, int width)
	{
		Stopwatch toCompleteTimer = new Stopwatch();
		toCompleteTimer.Start();

		_size = (uint)byteArray.Length;
		_width = width;
		_height = (ushort)(byteArray.Length / 4 / _width);
		Console.WriteLine("Image Size: " + byteArray.Length + "byte");
		Console.WriteLine("Width: " + _width + "px");
		Console.WriteLine("Height: " + _height + "px");

		_data = byteArray;
		Console.WriteLine($"SetUp: " + toCompleteTimer.ElapsedMilliseconds + "ms");
	}

	public void GetDices()
	{
		Stopwatch toCompleteTimer = new Stopwatch();
		toCompleteTimer.Start();

		// Loop Every Pixel
		int negativeDiceDotHeight = _maxDiceDotHeight / 4;
		int pixelCheckHeight = _maxDiceDotHeight + negativeDiceDotHeight;

		for (int index = 0; index < _size; index += 4 * _pixelSkip)
		{
			int pixelSzie = index / 4;
			int x = pixelSzie % _width;
			int y = pixelSzie / _width;

			Pixel pixel = new(_data[index], _data[index + 1], _data[index + 2], _data[index + 3]);
			int avg = pixel.GetAvgWthoutAlpha();

			// Check for black Pixel
			if (avg < 25)
			{
				// Check bounds
				if (y + _maxDiceDotHeight < _height && y - negativeDiceDotHeight > 0)
				{
					// Check Dimensions x pixel Downwards
					int plackPixelCounter = 0;
					bool[] isDot = new bool[pixelCheckHeight];

					// get negtaive Y Pixels and some below it to create line to check 
					for (int i = -negativeDiceDotHeight; i < _maxDiceDotHeight; i++)
					{
						int pixelPosInArray = ((y + i) * _width * 4) + (x * 4);

						Pixel cPixel = new(_data[pixelPosInArray], _data[pixelPosInArray + 1], _data[pixelPosInArray + 2], _data[pixelPosInArray + 3]);
						int cAvg = cPixel.GetAvgWthoutAlpha();
						if (cAvg < 100)
						{
							plackPixelCounter++;
							isDot[i + negativeDiceDotHeight] = false;
						}
						else
						{
							isDot[i + negativeDiceDotHeight] = true;
						}
					}

					if (_isDebugMode)
					{
						if (plackPixelCounter > 5 && isDot[0] == true && isDot[pixelCheckHeight - 1] == true)
						{
							// DRAW DEBUG LINE
							for (int i = -negativeDiceDotHeight; i < _maxDiceDotHeight; i++)
							{
								int pixelPosInArray = ((y + i) * _width * 4) + (x * 4);
								_data[pixelPosInArray] = 255;
								_data[pixelPosInArray + 1] = 0;
								_data[pixelPosInArray + 2] = 0;
								_data[pixelPosInArray + 3] = 255;
							}
						}
					}
				}
			}
		}
		Console.WriteLine($"DiceLogic: " + toCompleteTimer.ElapsedMilliseconds + "ms");
	}

	public byte[] GetByteArray()
	{
		return _data;
	}
}