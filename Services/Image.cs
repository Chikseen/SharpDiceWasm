using System;
using System.Diagnostics;

public class Image
{
	// preCompileConfig
	const int _pixelSkip = 1; // more pixel skiped -> faster -> blured
	const int _maxDiceDotHeight = 10; // more pixel skiped -> faster -> blured

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
		for (int index = 0; index < _size; index += 4 * _pixelSkip)
		{
			int x = index / 4 % _width;
			int y = index / 4 / _width;

			Pixel pixel = new(_data[index], _data[index + 1], _data[index + 2], _data[index + 3]);
			int avg = pixel.GetAvgWthoutAlpha();

			// Check for black Pixel

			if (avg < 25)
			{
				if (y + _maxDiceDotHeight < _height && y - (_maxDiceDotHeight / 4) > 0)
				{
					//Console.WriteLine($"Candidata at {x}:{y}");
					// Check Dimensions Ox pixel Downwards
					int isPixelBaclCounter = 0;
					bool[] isDot = new bool[_maxDiceDotHeight + _maxDiceDotHeight / 4];
					for (int i = -_maxDiceDotHeight / 4; i < _maxDiceDotHeight; i++)
					{
						int pixelPosInArray = ((y + i) * _width * 4) + (x * 4);

						//	Console.WriteLine($"Check bytePos {pixelPosInArray}");

						Pixel cPixel = new(_data[pixelPosInArray], _data[pixelPosInArray + 1], _data[pixelPosInArray + 2], _data[pixelPosInArray + 3]);
						int cAvg = cPixel.GetAvgWthoutAlpha();
						if (cAvg < 100)
						{
							isPixelBaclCounter++;
							isDot[i + (_maxDiceDotHeight / 4)] = false;
						}
						else
						{
							isDot[i + (_maxDiceDotHeight / 4)] = true;
						}
					}

					if (isPixelBaclCounter > 5 && (isDot[0] == true && isDot[(_maxDiceDotHeight + _maxDiceDotHeight / 4) - 1] == true))
					{
						//	Console.WriteLine($"Found at {x}:{y}");
						// DRAW DEBUG LINE
						for (int i = -_maxDiceDotHeight / 4; i < _maxDiceDotHeight; i++)
						{
							int pixelPosInArray = ((y + i) * _width * 4) + (x * 4);
							_data[pixelPosInArray] = 255;
							_data[pixelPosInArray + 1] = 0;
							_data[pixelPosInArray + 2] = 0;
							_data[pixelPosInArray + 3] = 255;
						}
					}

					/*

									Console.WriteLine("s" + index);
									int i = index;
									while (isChecked[i + index] == false)
									{
										isChecked[i + index] = true;
										Console.WriteLine("Check" + i);
										Pixel newPixel = new(_data[i, 0], _data[i, 1], _data[i, 2], _data[i, 3]);
										int newAvg = newPixel.GetAvgWthoutAlpha();

										Console.WriteLine("avg" + newAvg);
										if (newAvg < 10)
											return;

										_data[i, 0] = 255;
										_data[i, 1] = 0;
										_data[i, 2] = 0;
										_data[i, 3] = 255;
										i++;
				}
									}*/
				}
			}
		}
		Console.WriteLine($"Corners: " + toCompleteTimer.ElapsedMilliseconds + "ms");
	}

	public byte[] GetByteArray()
	{
		return _data;
	}
}