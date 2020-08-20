using OpenTK;

namespace OpenGLGraphing.Primitives {
	class Line : Primitive {


		public Vector3 p1 { get; set; }
		public Vector3 p2 { get; set; }

		public Line(Vector3 p1, Vector3 p2) {

		}


		public new void draw() {


			base.draw();
		}


	}
}
