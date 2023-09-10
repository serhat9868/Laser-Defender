using System;
using System.Collections.Generic;

namespace SomeCompanyGames_CodeTest
{

	class Program
	{

		//Shapes Intersection
		//An infinite 2D board contains a number of shapes.These shapes are either a Circle or a
		//Rectangle. Each shape has a unique id. Write a function (see below) that takes a list of Shapes
		//and returns a dictionary of each shapes id mapped to a list of the shape ids it intersects with.

		public class Shape
		{
			protected int _id;
			protected string _type;
			protected byte _typeFlag; //1 = rect 2 = circle
			protected int _sides;

			protected float _x, _y, _width, _height, _radius;

			public virtual int GetID()
			{
				return _id;
			}

			public virtual int GetSideCount()
			{
				return _sides;
			}
			public virtual float GetX()
			{
				return _x;
			}

			public virtual float GetY()
			{
				return _y;
			}

			public virtual float GetWidth()
			{
				return _width;
			}

			public virtual float GetHeight()
			{
				return _height;
			}

			public virtual float GetRadius()
			{
				//The area of a circle is pi times the radius squared
				return _radius;
			}

			public virtual void SetRadius(float radius)
			{
				_radius = radius;
			}

			public virtual string GetTypeStr()
			{
				return _type;
			}

			public virtual byte GetTypeFlag()
			{
				return _typeFlag;
			}
		}

		public class Rectangle : Shape
		{
			public Rectangle(int id, float x, float y, float width, float height)
			{
				_id = id;
				_sides = 4;
				_x = x;
				_y = y;
				_width = width;
				_height = height;
				_type = "Rectangle";
				_typeFlag = 1;
			}
		}

		public class Circle : Shape
		{

			//The area of a circle is pi times the radius squared
			public Circle(int id, float x, float y, float radius)
			{
				_id = id;
				_sides = 1;
				_x = x;
				_y = y;
				_radius = radius;
				_type = "Circle";
				_typeFlag = 2;
			}
		}




		//static public void FindIntersections(List<Shape> shapes)
		static public Dictionary<int, List<int>> FindIntersections(List<Shape> shapes)
		{
			//Objective: Must return dictionary of shapes.
			//			 Each shape ID must be mapped to List of shape ID intersected. 

			Dictionary<int, List<int>> collidedShapes = new Dictionary<int, List<int>>();
			List<int> collidedId = new List<int>();
			//foreach (Shape shape in shapes)
			//{
			//	//if(shape.GetX() + shape.GetWidth())
			//	//if(Collision(shape, shapes.)

			//}

			int max = shapes.Count;
			int id = -1;

			for(int i = 0; i < max; i++)
			{
				for(int j = 0; j < max; j++)
				{
					if(Collision(shapes[i], shapes[j]))
					{
						Console.WriteLine("\nShapes collision= true!");
						collidedId.Add(shapes[j].GetID());
						id = i;
					}
				}
				collidedShapes.Add(shapes[id].GetID(), collidedId);
				collidedId = new List<int>();
				//collidedId.Clear();

			}
			//collidedShapes.Add(shapes[id].GetID(), collidedId);
			return collidedShapes;
			
		}

		//static public bool Collision(Rectangle shape_a, Rectangle shape_b)
		static public bool Collision(Shape shape_a, Shape shape_b)
		{
			byte collisionFlag = 0;					//1=rect vs rect  2=circle vs circle  3=opposite shape types
			byte collisionIncrement = 0;            //Decide on what type of collision we should process.

			#region DetermineCollisionType
			if (shape_a.GetTypeFlag() == 1 && shape_b.GetTypeFlag() == 1)//shape_b.GetTypeStr() == "Rectangle"
			{
				collisionFlag = 1;
			}
			if (shape_a.GetTypeFlag() == 2 && shape_b.GetTypeFlag() == 2)
			{
				collisionFlag = 2;
			}
			if (shape_a.GetTypeFlag() == 1 && shape_b.GetTypeFlag() == 2 ||
				shape_a.GetTypeFlag() == 2 && shape_b.GetTypeFlag() == 1)
			{
				collisionFlag = 3;
			}
			#endregion

			#region CalculateCollision
			switch (collisionFlag)
			{
				case 1:
					#region RectWithRect
					if (shape_a.GetX() + shape_a.GetWidth() > shape_b.GetX() &&
						shape_a.GetX() < shape_b.GetX() + shape_b.GetWidth())
					{
						collisionIncrement++;
					}

					if (shape_a.GetY() + shape_a.GetHeight() > shape_b.GetY() &&
						shape_a.GetY() < shape_b.GetY() + shape_b.GetHeight())
					{
						collisionIncrement++;
					}

					if (collisionIncrement == 2)
					{
						Console.WriteLine("\nInternal Collision(): Two Rectangles have collided!");
						return true;
					}

					#endregion
					break;
				case 2:
					#region CircleWithCircle
					//Circles
					//Take the centre points of the two circles and ensure the distance between their centre points
					//are less than the two radius combined.
					float dx = shape_a.GetX() - shape_b.GetX();
					float dy = shape_a.GetY() - shape_b.GetY();

					double distance = Math.Sqrt(dx * dx + dy * dy); //System.Math.Sqrt() part of C# right? This is allowed?

					if (distance < shape_a.GetRadius() + shape_b.GetRadius())
					{
						Console.WriteLine("\nInternal Collision(): Two Circles have collided!");
						return true;
					}
					#endregion
					break;
				case 3:
					#region RectWithCircle
					//Rectangles
					//Diameter= length * 2 and width * 2 and then add the two together.
					//Radius= divide the diameter by two.

					float diameter = 0f;
					float radius = 0f;

					dx = shape_a.GetX() - shape_b.GetX();
					dy = shape_a.GetY() - shape_b.GetY();

					distance = Math.Sqrt(dx * dx + dy * dy);

					if (shape_a.GetTypeFlag() == 1)
					{
						//Shape is rectangle.
						diameter = (shape_a.GetHeight() * 2) + (shape_a.GetWidth() * 2);
						radius = diameter / 2;
						shape_a.SetRadius(radius);
					}
					if (shape_b.GetTypeFlag() == 1)
					{
						//Shape is rectangle.
						diameter = (shape_b.GetHeight() * 2) + (shape_b.GetWidth() * 2);
						radius = diameter / 2;
						shape_b.SetRadius(radius);
					}

					if (distance < shape_a.GetRadius() + shape_b.GetRadius())
					{
						Console.WriteLine("\nInternal Collision(): A Rectangle & Circle have collided!");
						return true;
					}
					#endregion
					break;
			}
			#endregion




			return false;
		}

		static public void SetupShapeTesting(List<Shape> shapes)
		{
			Console.WriteLine("\nSetupShapeTesting() begin!");
			Console.WriteLine("Shape data displaying...");

			Console.WriteLine("------------------------");
			for (int i = 0; i < shapes.Count; i++)
			{
				Console.WriteLine("\nShape=" + shapes[i].GetTypeStr() + " ID=" + shapes[i].GetID());
				Console.WriteLine("\nx=" + shapes[i].GetX() + " y=" + shapes[i].GetY());
				Console.WriteLine("\nwidth=" + shapes[i].GetWidth() + " height=" + shapes[i].GetHeight());
				Console.WriteLine("\nradius=" + shapes[i].GetRadius());
				Console.WriteLine("------------------------");
			}

			Console.WriteLine("\nShape collision testing...");

			if (Collision(shapes[0], shapes[1]))
			{
				Console.WriteLine("\nThese shapes have collided!");
				Console.WriteLine("\n------------------------");
			}
			else
			{
				Console.WriteLine("\nNo shape collisions detected.");
			}

			Console.WriteLine("\nSetupShapeTesting() end!");
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Initalizing...");

			List<Shape> shapes = new List<Shape>();

			//Rectangle rect = new Rectangle(0, 50.0f, 50.0f, 100.0f, 50.0f);
			//shapes.Add(new Circle(0, 50.0f, 50.0f, 100.0f));
			//shapes.Add(new Circle(1, 156.0f, 156.0f, 49.0f));
			//shapes.Add(new Rectangle(0, 0.0f, 0.0f, 100.0f, 50.0f));
			//shapes.Add(new Rectangle(1, 100.0f, 50.0f, 100.0f, 50.0f));


			shapes.Add(new Circle(0, 50.0f, 50.0f, 100.0f));
			shapes.Add(new Rectangle(1, 25.0f, 25.0f, 25.0f, 25.0f));
			shapes.Add(new Rectangle(2, 75.0f, 75.0f, 50.0f, 50.0f));
			shapes.Add(new Rectangle(3, 0.0f, 0.0f, 100.0f, 100.0f));

			Console.WriteLine("Shapes populated...");

			SetupShapeTesting(shapes);

			Dictionary<int, List<int>> shapesCollided_dict = new Dictionary<int, List<int>>();

			shapesCollided_dict = FindIntersections(shapes);

			//Console.WriteLine("Shapes collided dictionary now printing...");

		}
	}
}