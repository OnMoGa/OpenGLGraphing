using System;
using System.Collections.Generic;
using System.Text;
using OpenGLGraphing.Primitives;

namespace OpenGLGraphing {
	class Structure : IDrawable {

		List<Primitive> primitives = new List<Primitive>();


		public void draw() {
			foreach (Primitive primitive in primitives) {
				primitive.draw();
			}
		}


	}
}
