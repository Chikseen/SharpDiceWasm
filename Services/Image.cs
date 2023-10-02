using System;
using System.Diagnostics;

public class Image
{
	private byte[,] _Data;
	private readonly uint Size = 0;
	private ushort Width = 0;
	private ushort Height = 0;
	private uint PixelAmount = 0;

	private int PixelSkip = 4; // more pixel skiped -> faster -> blured

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

		for (int i = 0; i < PixelAmount;  i += PixelSkip)
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