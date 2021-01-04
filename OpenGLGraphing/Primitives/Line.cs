using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing.Primitives {
	public class Line : Primitive {


		public List<Vector3> points { get; set; } = new List<Vector3>();

		public override void draw() {
			preDraw();
			vertexList = new VertexList() {
				vertices = points.Select(p => new Vertex { point = p, color = color }),
				indices = points.Select((p, i) => (uint)i)
			};
			draw(PrimitiveType.LineStrip);
		}



	}
}
