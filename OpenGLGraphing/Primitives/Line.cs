using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing.Primitives {
	public class Line : Primitive {


		public List<Vector3> points { get; set; } = new List<Vector3>();

		public override void draw() {
			preDraw();
			verticies = points.Select(p => new Vertex {point = p, color = color})
				.SelectMany(v => v.toFloatArray())
				.ToArray();
			indices = points.Select((p, i) => (uint)i).ToArray();
			draw(PrimitiveType.LineStrip);
		}



	}
}
