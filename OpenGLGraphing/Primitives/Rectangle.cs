using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing.Primitives {
	public class Rectangle : Primitive {


		public Vector3 pos { get; set; }
		public Vector3 size { get; set; }
		
		public override void draw() {
			if(pos == null || size == null) return;

			preDraw();

			float left = pos.X - size.X / 2;
			float right = pos.X + size.X / 2;
			float top = pos.Y - size.Y / 2;
			float bottom = pos.Y + size.Y / 2;

			verticies = new List<Vertex> {
				new Vertex {point = new Vector3(left, bottom, pos.Z), color = color},
				new Vertex {point = new Vector3(left, top, pos.Z), color = color},
				new Vertex {point = new Vector3(right, bottom, pos.Z), color = color},
				new Vertex {point = new Vector3(right, top, pos.Z), color = color},
			}.SelectMany(v => v.toFloatArray()).ToArray();
			
			indices = new uint[]{
				0, 1, 2,
				2, 3, 1
			};

			draw(PrimitiveType.Triangles);
		}


	}
}
