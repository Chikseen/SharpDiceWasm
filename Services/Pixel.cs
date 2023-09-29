using System;
using System.Reflection.Metadata;

public class Pixel
{
	public byte R = 0;
	public byte G = 0;
	public byte B = 0;
	public byte A = 0;

	public Pixel(byte[] chunk)
	{
		if (chunk.Length == 4)
		{
			R = chunk[0];
			G = chunk[1];
			B = chunk[2];
			A = chunk[3];
		}
		else
		{
			Console.WriteLine("WARNING: Chunk fault");
		}
	}

	public byte[] GetByte()
	{
		byte[] byteArray = new byte[4];
		byteArray[0] = R;
		byteArray[1] = G;
		byteArray[2] = B;
		byteArray[3] = A;
		return byteArray;
	}

	public byte GetAvgWthoutAlpha()
	{
		ushort avg = (ushort)(R + G + B);
		return (byte)(avg / 3);
	}

	public void SetColor(byte r, byte g, byte b, byte a = 255)
	{
		R = r;
		G = g;
		B = b;
		if (A != 255)
			A = a;
	}

	public string Print()
	{
		return $"R{R} G{G} B{B} A{A}";
	}
}