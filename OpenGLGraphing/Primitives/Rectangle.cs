using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Vector3 = System.Numerics.Vector3;

namespace OpenGLGraphing.Primitives {
	public class Rectangle : Primitive {

		public Vector3 pos { get; set; }
		public Vector3 size { get; set; }
		
		public override void draw() {
			
			preDraw();

			vertexList = new VertexList() {
				vertices = new List<Vertex> {
					new Vertex {
						point = new Vector3(-0.5f, -0.5f, pos.Z),
						color = color,
						texCoord = new Vector2(0, 1)
					},
					new Vertex {
						point = new Vector3(-0.5f, 0.5f, pos.Z),
						color = color,
						texCoord = new Vector2(0, 0)
					},
					new Vertex {
						point = new Vector3(0.5f, -0.5f, pos.Z),
						color = color,
						texCoord = new Vector2(1, 1)
					},
					new Vertex {
						point = new Vector3(0.5f, 0.5f, pos.Z),
						color = color,
						texCoord = new Vector2(1, 0)
					}
				},
				indices = new List<uint> {
					0, 1, 2,
					2, 3, 1
				}
			};


			transformationMatrix = Matrix4.Identity
				* Matrix4.CreateScale(size.ToOpenTKVector3())
				* Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationDegrees))
				* Matrix4.CreateTranslation(pos.ToOpenTKVector3());


			draw(PrimitiveType.Triangles);
		}


	}
}
