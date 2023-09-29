using System;
using System.Runtime.InteropServices.JavaScript;

// I dont know why but this is the MAIN -> without it programm wont work DAFUQ
Console.WriteLine("I am your cool image inspector");

public partial class Main
{
	[JSExport]
	internal static byte[] LookAtMe(byte[] byteArray, int width)
	{
		try
		{
			Image image = new(byteArray, (ushort)width);
			//image.Print();
			image.GetContrast();
			return image.GetByteArray();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return new byte[0];
		}
	}
}
