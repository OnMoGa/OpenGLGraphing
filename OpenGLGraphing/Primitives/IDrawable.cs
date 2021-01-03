using System;

namespace OpenGLGraphing.Primitives {
	public interface IDrawable {

		public void draw();
		public bool visible { get; set; }
	}
}
