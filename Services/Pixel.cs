using System;

public class Pixel : IDisposable
{
	public byte R = 0;
	public byte G = 0;
	public byte B = 0;
	public byte A = 0;

	public Pixel(byte r, byte g, byte b, byte a)
	{
		R = r;
		G = g;
		B = b;
		A = a;
	}

	public byte GetAvgWthoutAlpha()
	{
		ushort avg = (ushort)(R + G + B);
		return (byte)(avg / 3);
	}

	public void Dispose()
	{ }
}