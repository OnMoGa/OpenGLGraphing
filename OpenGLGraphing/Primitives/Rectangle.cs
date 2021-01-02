using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing.Primitives {
	public class Rectangle : Primitive {


		public Vector3 pos { get; set; }
		public Vector3 size { get; set; }
		
		public override void draw() {
			
			preDraw();

			float left = pos.X - size.X / 2;
			float right = pos.X + size.X / 2;
			float top = pos.Y - size.Y / 2;
			float bottom = pos.Y + size.Y / 2;

			vertexList = new VertexList() {
				vertices = new List<Vertex> {
					new Vertex {
						point = new Vector3(left, bottom, pos.Z),
						color = color,
						texCoord = new Vector2(0, 0)
					},
					new Vertex {
						point = new Vector3(left, top, pos.Z),
						color = color,
						texCoord = new Vector2(0, 1)
					},
					new Vertex {
						point = new Vector3(right, bottom, pos.Z),
						color = color,
						texCoord = new Vector2(1, 0)
					},
					new Vertex {
						point = new Vector3(right, top, pos.Z),
						color = color,
						texCoord = new Vector2(1, 1)
					}
				},
				indices = new List<uint> {
					0, 1, 2,
					2, 3, 1
				}
			};


			draw(PrimitiveType.Triangles);
		}


	}
}
