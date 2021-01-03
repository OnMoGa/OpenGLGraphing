using System.Collections.Generic;
using OpenGLGraphing.Primitives;

namespace OpenGLGraphing.Structures {
	public class Structure : IDrawable {

		public bool visible { get; set; } = true;
		public IEnumerable<IDrawable> drawables = new List<IDrawable>();


		public void draw() {
			if(!visible) return;
			foreach (IDrawable drawable in drawables) {
				drawable.draw();
			}
		}

		
	}
}
