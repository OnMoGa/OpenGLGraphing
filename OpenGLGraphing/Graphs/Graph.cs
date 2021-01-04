using System.Numerics;
using System.Threading;
using OpenGLGraphing.Primitives;
using OpenGLGraphing.Structures;
using OpenTK;
using Vector3 = System.Numerics.Vector3;

namespace OpenGLGraphing.Graphs {
	public abstract class Graph : Structure, IDrawable {


		public Graph() {
			size = new Vector3(1, 1, 0);
		}


		public Window showInNewWindow(int width, int height, string title) {
			Window window = Window.NewAsyncWindow(width, height, title);
			window.addDrawable(this);
			return window;
		}

		public bool visible { get; set; } = true;
		
	}
}
