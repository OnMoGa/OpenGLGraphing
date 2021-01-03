using System.Threading;
using OpenGLGraphing.Primitives;
using OpenGLGraphing.Structures;

namespace OpenGLGraphing.Graphs {
	public abstract class Graph : IDrawable {
		protected Structure structure { get; set; } = new Structure();


		public Window showInNewWindow(int width, int height, string title) {
			Window window = Window.NewAsyncWindow(width, height, title);
			window.addDrawable(this);
			return window;
		}
		public void draw() {
			if(!visible) return;
			structure.draw();
		}

		public bool visible { get; set; } = true;
	}
}
