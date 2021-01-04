using System.Threading;
using OpenGLGraphing.Primitives;
using OpenGLGraphing.Structures;
using OpenTK;

namespace OpenGLGraphing.Graphs {
	public abstract class Graph : Structure, IDrawable {

		public Window showInNewWindow(int width, int height, string title) {
			Window window = Window.NewAsyncWindow(width, height, title);
			window.addDrawable(this);
			return window;
		}

		public bool visible { get; set; } = true;
		
	}
}
