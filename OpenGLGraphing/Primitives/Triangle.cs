using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Vector3 = System.Numerics.Vector3;

namespace OpenGLGraphing.Primitives {
	class Triangle : Primitive {



		public Vector3 p1 { get; set; }
		public Vector3 p2 { get; set; }
		public Vector3 p3 { get; set; }
		public Vector3 scale { get; set; } = new Vector3(1, 1, 1);

		public Triangle(Vector3 p1, Vector3 p2, Vector3 p3) {
			this.p1=p1;
			this.p2=p2;
			this.p3=p3;
		}

		public override void draw() {
			preDraw();

			vertexList = new VertexList() {
				vertices = new List<Vertex>() {
					new Vertex() {
						point = p1,
						color = color,
						texCoord = new Vector2(0, 0)
					},
					new Vertex() {
						point = p2,
						color = color,
						texCoord = new Vector2(0, 0)
					},
					new Vertex() {
						point = p3,
						color = color,
						texCoord = new Vector2(0, 0)
					},
				},
				indices = new List<uint> {
					1, 2, 3
				}
			};

			transformationMatrix = Matrix4.Identity
				* Matrix4.CreateScale(scale.ToOpenTKVector3())
				* Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationDegrees));

			draw(PrimitiveType.Triangles);
		}


	}
}
