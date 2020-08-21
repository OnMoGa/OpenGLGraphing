using System.Threading;

namespace OpenGLGraphing.Graphs {
	public abstract class Graph {

		public abstract Thread showInNewWindow(int width, int height, string title);
	}
}
