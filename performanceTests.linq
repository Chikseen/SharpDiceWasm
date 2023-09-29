<Query Kind="Program" />

void Main()
{
	int size = 960000; // Real ByteArray example

	//ArrayVsList(size);
	//ArrayElemntAccessTime(size);
}

public void ArrayElemntAccessTime(int size) 
{
	DateTime d1 = DateTime.Now;
	int[] l = new int[size];
	for (int index = 0; index < size; index++)
	{
		l[index] = index;
	}
	Console.WriteLine("Gen And Pupulate Array: " + (DateTime.Now.Ticks - d1.Ticks));
	Console.WriteLine("Gen And Pupulate Array: " + new TimeSpan(DateTime.Now.Ticks - d1.Ticks).Milliseconds);
	
	DateTime d2 = DateTime.Now;
	int t1 = l[1];
	Console.WriteLine("GetLowElement: " + (DateTime.Now.Ticks - d2.Ticks));
	
	DateTime d3 = DateTime.Now;
	int t2 = l[size - 10];
	Console.WriteLine("GetHighElement: " + (DateTime.Now.Ticks - d3.Ticks));
	
	DateTime d4 = DateTime.Now;
	l[1] = t2;
	Console.WriteLine("SetLowElement: " + (DateTime.Now.Ticks - d4.Ticks));
	
	DateTime d5 = DateTime.Now;
	l[size - 10] = t1;
	Console.WriteLine("SetHighElement: " + (DateTime.Now.Ticks - d5.Ticks));
}

public void ArrayVsList(int size) 
{
	DateTime d2 = DateTime.Now;
	int[] i2 = new int[size];
	Console.WriteLine("New Array: " + (DateTime.Now.Ticks - d2.Ticks));

	DateTime d4 = DateTime.Now;
	for (int index = 0; index < size; index++)
	{
		i2[index] = index;
	}
	Console.WriteLine("Puplate Array: " + (DateTime.Now.Ticks - d4.Ticks));

	DateTime d1 = DateTime.Now;
	List<int> i1 = new();
	Console.WriteLine("New List: " + (DateTime.Now.Ticks - d1.Ticks));

	DateTime d5 = DateTime.Now;
	for (int index = 0; index < size; index++)
	{
		i1.Add(index);
	}
	Console.WriteLine("Add to List: " + (DateTime.Now.Ticks - d5.Ticks));

	DateTime d3 = DateTime.Now;
	for (int index = 0; index < size; index++)
	{
		i1[index] = index;
	}
	Console.WriteLine("Puplate List: " + (DateTime.Now.Ticks - d3.Ticks));
}

// You can define other methods, fields, classes and namespaces here