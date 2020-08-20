using OpenTK;
using OpenTK.Graphics.OpenGL4;

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
			
			float[] verts = {
				p1.X, p1.Y, p1.Z,
				p2.X, p2.Y, p1.Z,
				p3.X, p3.Y, p1.Z,
			};
			
			uint[] indices = {
				0, 1, 2
			};

			GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);
			
			GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);


			GL.DrawElements(PrimitiveType.Triangles, 3, DrawElementsType.UnsignedInt, 0);

			base.draw();
		}


	}
}
