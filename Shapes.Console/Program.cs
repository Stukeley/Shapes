using Shapes.Classes;
using System.Collections.Generic;
using System.Linq;

namespace Shapes.Console
{
	internal class Program
	{
		private static void Main()
		{
			// Any shape can be saved here
			var list = new List<Shape>();

			for (int i = 0; i < 20; i++)
			{
				var shape = Shape.GenerateShape();
				list.Add(shape);
			}

			for (int i = 0; i < list.Count; i++)
			{
				System.Console.WriteLine($"{i + 1}: " + list[i].ToString() + "\n");
			}

			System.Console.WriteLine("\n");

			// Calculate the sum of perimeters of all triangles
			var triangles = list.Where(x => x is Triangle).ToList();
			var sum = triangles.Select(x => (x as Triangle).Circumference).Sum();
			System.Console.WriteLine($"The sum of perimeters of all triangles is: {sum:0.00}\n");

			// Calculate the average area of all shapes
			var average = list.Select(x => x.Area).Average();
			System.Console.WriteLine($"The average area of all shapes is: {average:0.00}\n");

			// Find the Shape3D that has the largest volume
			var shapes3D = list.Where(x => x is Shape3D);
			var highestV = shapes3D.OrderByDescending(x => (x as Shape3D).Volume).FirstOrDefault();
			if (highestV == default)
			{
				// No 3D shapes have been generated
				System.Console.WriteLine("No 3D shapes have been generated! Cannot find the one with the highest volume.");
			}
			else
			{
				System.Console.Write($"The highest volume of all Shape3Ds is:\n");
				System.Console.WriteLine(highestV.ToString());
			}

			System.Console.ReadKey();
		}
	}
}
