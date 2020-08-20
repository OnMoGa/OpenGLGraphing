using System.Collections.Generic;

namespace OpenGLGraphing.Primitives {
	abstract class Primitive : IDrawable {

		public List<float> verticies { get; set; } = new List<float>();


		public virtual void draw() {

		}



	}
}
