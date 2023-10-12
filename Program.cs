using System;
using System.Diagnostics;
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
			Stopwatch toCompleteTimer = new Stopwatch();
			toCompleteTimer.Start();

			Image image = new(byteArray, (ushort)width);
			Positions positions = image.GetDices();

			var t = positions.GetClusterAndNumber(positions);
			Console.WriteLine("Start");
			foreach (var item in t)
			{
				Console.WriteLine(item);
			}

			Console.WriteLine($"All: " + toCompleteTimer.ElapsedMilliseconds + "ms");
			return image.GetByteArray();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return new byte[0];
		}
	}
}
