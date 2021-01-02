using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing.Primitives {
	class Triangle : Primitive {



		public Vector3 p1 { get; set; }
		public Vector3 p2 { get; set; }
		public Vector3 p3 { get; set; }

		public Triangle(Vector3 p1, Vector3 p2, Vector3 p3) {
			this.p1=p1;
			this.p2=p2;
			this.p3=p3;
		}

		public override void draw() {
			if(p1 == null || p2 == null || p3 == null) return;
			preDraw();

			verticies = new []{
				p1.X, p1.Y, p1.Z, color.R, color.G, color.B,
				p2.X, p2.Y, p1.Z, color.R, color.G, color.B,
				p3.X, p3.Y, p1.Z, color.R, color.G, color.B,
			};
			verticies = new List<Vertex> {
				new Vertex {point = p1, color = color},
				new Vertex {point = p2, color = color},
				new Vertex {point = p3, color = color}
			}.SelectMany(v => v.toFloatArray()).ToArray();
			
			indices = new uint[]{
				0, 1, 2
			};

			draw(PrimitiveType.Triangles);
		}


	}
}
