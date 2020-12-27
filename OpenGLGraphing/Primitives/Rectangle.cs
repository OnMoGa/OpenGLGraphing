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
			
			float[] verts = {
				left, bottom, pos.Z,
				left,  top, pos.Z,
				right, bottom, pos.Z,
				right,  top, pos.Z
			};
			
			uint[] indices = {
				0, 1, 3,
				0, 2, 3
			};

			bindVerticies(verts, indices);

			GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

			base.draw();
		}


	}
}
