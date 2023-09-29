using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

public class Image
{
	private Pixel[,] _Data;
	private readonly uint Size = 0;

	public ushort Width = 0;
	public ushort Height = 0;

	public Image(byte[] byteArray, ushort width)
	{

		Stopwatch toCompleteTimer = new Stopwatch();
		toCompleteTimer.Start();

		Size = (uint)byteArray.Length;
		Width = width;
		Height = (ushort)(byteArray.Length / 4 / Width);
		Console.WriteLine("Image Size: " + byteArray.Length + "byte");
		Console.WriteLine("Width: " + Width + "px");
		Console.WriteLine("Height: " + Height + "px");

		_Data = new Pixel[Width, Height];

		ushort widthCounter = 0;
		ushort heightCounter = 0;

		for (int i = 0; i < byteArray.Length; i += 4)
		{

			byte[] subChunk = new byte[4];
			subChunk[0] = byteArray[i];
			subChunk[1] = byteArray[i + 1];
			subChunk[2] = byteArray[i + 2];
			subChunk[3] = byteArray[i + 3];
			Pixel pixel = new(subChunk);

			//Console.WriteLine($"Add Pixel At: {widthCounter},{heightCounter} : {pixel.Print()}");
			//Console.WriteLine($"PixelLeft: {byteArray.Length - i}");

			_Data[heightCounter, widthCounter] = pixel;

			widthCounter++;
			if (widthCounter == Width)
			{
				widthCounter = 0;
				heightCounter++;
			}
		};

		Console.WriteLine($"D: " + toCompleteTimer.ElapsedMilliseconds + "ms");
	}

	public void GetContrast()
	{
		Stopwatch toCompleteTimer = new Stopwatch();
		toCompleteTimer.Start();

		for (int i = 0; i < Width; i++)
		{
			for (int j = 0; j < Height; j++)
			{
				if (_Data[i, j] != null)
				{
					byte avgColor = _Data[i, j].GetAvgWthoutAlpha();
					if (avgColor > 127)
						_Data[i, j].SetColor(255, 255, 255, 255);
					else
						_Data[i, j].SetColor(0, 0, 0, 255);
				}
			}
		}

		Console.WriteLine($"GC: " + toCompleteTimer.ElapsedMilliseconds + "ms");
	}

	public void Print()
	{
		for (int i = 0; i < Width; i++)
		{
			for (int j = 0; j < Height; j++)
			{
				if (_Data[i, j] != null)
				{
					Console.WriteLine($"PIXEL R{_Data[i, j].R} G{_Data[i, j].G} B{_Data[i, j].B} A{_Data[i, j].A}");
				}
			}
		}
	}

	public byte[] GetByteArray()
	{
		Stopwatch toCompleteTimer = new Stopwatch();
		toCompleteTimer.Start();

		byte[] byteArray = new byte[Size];
		int byteIndex = 0;

		for (int i = 0; i < Width; i++)
		{
			for (int j = 0; j < Height; j++)
			{
				if (_Data[i, j] != null)
				{
					byteArray[byteIndex] = _Data[i, j].R;
					byteArray[byteIndex + 1] = _Data[i, j].G;
					byteArray[byteIndex + 2] = _Data[i, j].B;
					byteArray[byteIndex + 3] = _Data[i, j].A;
				}
				else
				{
					byteArray[byteIndex] = 255;
					byteArray[byteIndex + 1] = 255;
					byteArray[byteIndex + 2] = 255;
					byteArray[byteIndex + 3] = 255;
				}
				byteIndex += 4;
			}
		}

		Console.WriteLine($"BA: " + toCompleteTimer.ElapsedMilliseconds + "ms");
		return byteArray;

	}

	/// <summary>
	/// //////////////////////////////////////////////////////////////////////////////////
	/// </summary>


	// You can define other methods, fields, classes and namespaces here
}