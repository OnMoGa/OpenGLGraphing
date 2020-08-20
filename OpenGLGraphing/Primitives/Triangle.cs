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
			preDraw();

			float[] verts = {
				p1.X, p1.Y, p1.Z,
				p2.X, p2.Y, p1.Z,
				p3.X, p3.Y, p1.Z,
			};
			
			uint[] indices = {
				0, 1, 2
			};

			bindVerticies(verts, indices);

			GL.DrawElements(PrimitiveType.Triangles, 3, DrawElementsType.UnsignedInt, 0);

			base.draw();
		}


	}
}
