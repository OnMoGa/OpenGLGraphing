using System.Threading;
using OpenGLGraphing.Structures;

namespace OpenGLGraphing.Graphs {
	public abstract class Graph {
		protected Structure structure { get; set; } = new Structure();


		public abstract Window showInNewWindow(int width, int height, string title);
	}
}
