using System;
using System.Numerics;

namespace Shapes.Classes
{
	public abstract class Shape
	{
		public abstract Vector3 Center { get; }
		public abstract float Area { get; }

		// We need this in the class, because if we create the Random instance inside GenerateShape(), we will be getting the same values over and over again
		// That is due to the base seed which uses system clock, and if we create them too often, we will end up with the same values and shapes
		private static Random RNG = new Random();

		private static float GetRandomValue() => (float)RNG.NextDouble() * 50;

		public static Shape GenerateShape()
		{
			// Returned shape
			Shape shape = null;

			var num = RNG.Next(0, 7);

			switch (num)
			{
				case 0: // Circle
					{
						var radius = GetRandomValue();
						var cen = new Vector2()
						{
							X = GetRandomValue(),
							Y = GetRandomValue()
						};

						shape = new Circle(cen, radius);

						break;
					}
				case 1: // Rectangle
					{
						var size = new Vector2()
						{
							X = GetRandomValue(),
							Y = GetRandomValue()
						};
						var cen = new Vector2()
						{
							X = GetRandomValue(),
							Y = GetRandomValue()
						};

						shape = new Rectangle(cen, size);

						break;
					}
				case 2: // Square
					{
						var width = GetRandomValue();
						var cen = new Vector2()
						{
							X = GetRandomValue(),
							Y = GetRandomValue()
						};

						shape = new Rectangle(cen, width);

						break;
					}
				case 3: // Triangle
					{
						var p1 = new Vector2
						{
							X = GetRandomValue(),
							Y = GetRandomValue()
						};
						var p2 = new Vector2
						{
							X = GetRandomValue(),
							Y = GetRandomValue()
						};
						var p3 = new Vector2
						{
							X = GetRandomValue(),
							Y = GetRandomValue()
						};

						shape = new Triangle(p1, p2, p3);

						break;
					}
				case 4: //Cuboid
					{
						var size = new Vector3()
						{
							X = GetRandomValue(),
							Y = GetRandomValue(),
							Z = GetRandomValue()
						};
						var cen = new Vector3()
						{
							X = GetRandomValue(),
							Y = GetRandomValue(),
							Z = GetRandomValue()
						};

						shape = new Cuboid(cen, size);

						break;
					}
				case 5: // Cube
					{
						var width = GetRandomValue();
						var cen = new Vector3()
						{
							X = GetRandomValue(),
							Y = GetRandomValue(),
							Z = GetRandomValue()
						};

						shape = new Cuboid(cen, width);

						break;
					}
				case 6: // Sphere
					{
						var radius = GetRandomValue();
						var cen = new Vector3()
						{
							X = GetRandomValue(),
							Y = GetRandomValue(),
							Z = GetRandomValue()
						};

						shape = new Sphere(cen, radius);

						break;
					}
			}

			return shape;
		}
	}

	public abstract class Shape2D : Shape
	{
		public abstract float Circumference { get; }
	}
	public abstract class Shape3D : Shape
	{
		public abstract float Volume { get; }
	}

	public class Circle : Shape2D
	{
		public override Vector3 Center { get; }
		public float Radius { get; }
		public override float Circumference => 2f * ((float)Math.PI) * Radius;
		public override float Area => ((float)Math.PI) * Radius * Radius;

		public Circle(Vector2 center, float radius)
		{
			Center = new Vector3()
			{
				X = center.X,
				Y = center.Y,
				// There's no Z axis in 2D shapes
				Z = 0
			};
			Radius = radius;
		}

		public override string ToString() => $"Circle @ ({Center.X:0.00}, {Center.Y:0.00}): r = {Radius:0.00}";
	}
	public class Rectangle : Shape2D
	{
		public override Vector3 Center { get; }
		public float Width { get; }
		public float Height { get; }
		public override float Circumference => 2 * Width + 2 * Height;
		public override float Area => Width * Height;

		public Rectangle(Vector2 center, Vector2 size)
		{
			// size.X - width; size.Y - height
			Center = new Vector3()
			{
				X = center.X,
				Y = center.Y,
				// There's no Z axis in 2D shapes
				Z = 0
			};

			Width = size.X;
			Height = size.Y;
		}

		// Square
		public Rectangle(Vector2 center, float width)
		{
			Center = new Vector3()
			{
				X = center.X,
				Y = center.Y,
				// There's no Z axis in 2D shapes
				Z = 0
			};

			Width = width;
			Height = width;
		}

		// Returns true if Width and Height are equal
		public bool IsSqare() => Width == Height;

		public override string ToString() => IsSqare() ? $"Square @ ({Center.X:0.00}, {Center.Y:0.00}): w = {Width:0.00}" : $"Rectangle @ ({ Center.X:0.00} ,  { Center.Y:0.00} ): w =  {Width:0.00}";
	}
	public class Triangle : Shape2D
	{
		public Vector2 P1 { get; }
		public Vector2 P2 { get; }
		public Vector2 P3 { get; }
		public override Vector3 Center { get; }
		public override float Circumference { get; }
		public override float Area { get; }

		public Triangle(Vector2 p1, Vector2 p2, Vector2 p3)
		{
			P1 = p1;
			P2 = p2;
			P3 = p3;

			//Center
			Center = new Vector3()
			{
				X = (P1.X + P2.X + P3.X) / 3f,
				Y = (P1.Y + P2.Y + P3.Y) / 3f,
				// There's no Z axis in 2D shapes
				Z = 0
			};

			// Sides - each side is the length between two of three points of the triangle
			float s1 = (float)Math.Sqrt((P2.X - P1.X) * (P2.X - P1.X) + (P2.Y - P1.Y) * (P2.Y - P1.Y));
			float s2 = (float)Math.Sqrt((P3.X - P2.X) * (P3.X - P2.X) + (P3.Y - P2.Y) * (P3.Y - P2.Y));
			float s3 = (float)Math.Sqrt((P1.X - P3.X) * (P1.X - P3.X) + (P1.Y - P3.Y) * (P1.Y - P3.Y));

			// Circumference and Area
			Circumference = s1 + s2 + s3;

			var p = Circumference / 2f;
			Area = (float)Math.Sqrt(p * (p - s1) * (p - s2) * (p - s3));
		}

		public override string ToString() => $"Triangle @ ({Center.X:0.00}, {Center.Y:0.00}): P1 ({P1.X:0.00}, {P1.Y:0.00}), P2 ({P2.X:0.00}, {P2.Y:0.00}), P3 ({P3.X:0.00}, {P3.Y:0.00})";
	}
	public class Sphere : Shape3D
	{
		public override Vector3 Center { get; }
		public float Radius { get; }
		public override float Volume => 4 * ((float)Math.PI) * Radius * Radius * Radius / 3f;
		public override float Area => 4 * ((float)Math.PI) * Radius * Radius;

		public Sphere(Vector3 center, float radius)
		{
			Center = center;
			Radius = radius;
		}

		public override string ToString() => $"Sphere @ ({Center.X:0.00}, {Center.Y:0.00}): r = {Radius:0.00}";
	}
	public class Cuboid : Shape3D
	{
		public float Width { get; }
		public float Height { get; }
		public float Depth { get; }
		public override Vector3 Center { get; }
		public override float Volume => Width * Height * Depth;
		public override float Area => 2 * (Width * Height + Height * Depth + Width * Depth);

		public Cuboid(Vector3 center, Vector3 size)
		{
			Center = center;

			// size.X - width; size.Y - height; size.Z - depth
			Width = size.X;
			Height = size.Y;
			Depth = size.Z;
		}

		// Cube
		public Cuboid(Vector3 center, float width)
		{
			Center = center;
			Width = width;
			Height = width;
			Depth = width;
		}

		// Returns true if Width, Height and Depth are equal
		public bool IsCube() => Width == Height && Height == Depth;

		public override string ToString() => IsCube() ? $"Cube @ ({Center.X:0.00}, {Center.Y:0.00}, {Center.Z:0.00}): w = {Width:0.00}" : $"Cuboid @ ({Center.X:0.00}, {Center.Y:0.00}, {Center.Z:0.00}): w = {Width:0.00}, h = {Height:0.00}, l = {Depth:0.00}";
	}
}
