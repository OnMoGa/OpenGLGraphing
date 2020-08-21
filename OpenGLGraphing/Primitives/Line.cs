using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing.Primitives {
	public class Line : Primitive {


		public List<Vector3> points { get; set; } = new List<Vector3>();


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
			
			bindVerticies(verts, indices);

			GL.DrawElements(PrimitiveType.LineStrip, points.Count, DrawElementsType.UnsignedInt, 0);

			base.draw();
		}



	}
}
