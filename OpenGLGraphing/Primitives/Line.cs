using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing.Primitives {
	class Line : Primitive {


		public List<Vector3> points { get; set; }


		public override void draw() {
			preDraw();

			List<float> vertList = new List<float>();
			points.ForEach(p => vertList.AddRange(
				new List<float>
					{p.X, p.Y, p.Z}
				)
			);
			float[] verts = vertList.ToArray();
			
			uint[] indices = points.Select((p, i) => (uint)i).ToArray();
			
			GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);
			
			GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

			GL.DrawElements(PrimitiveType.LineStrip, points.Count, DrawElementsType.UnsignedInt, 0);

			base.draw();
		}



	}
}
