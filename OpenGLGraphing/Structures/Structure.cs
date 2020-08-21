using System.Collections.Generic;
using OpenGLGraphing.Primitives;

namespace OpenGLGraphing.Structures {
	public class Structure : IDrawable {

		public IEnumerable<IDrawable> drawables = new List<IDrawable>();


		public void draw() {
			foreach (IDrawable drawable in drawables) {
				drawable.draw();
			}
		}


	}
}
