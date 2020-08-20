using System.Collections.Generic;
using OpenGLGraphing.Primitives;
using OpenTK;
using Rectangle = OpenGLGraphing.Primitives.Rectangle;

namespace OpenGLGraphing.Graphs {
	public class LineGraph : Graph {

		List<XYPair> dataPoints = new List<XYPair>();

		public LineGraph() {

		}

		public override void showInNewWindow(int x, int y, string title) {
			Window window = new Window(x, y, title);

			window.drawables.Add(new Rectangle(new Vector3(0, -0.25f, 0), new Vector3(0.5f, 0.5f, 0)));

			window.drawables.Add(new Triangle(
				new Vector3(-0.3f, 0, 0),
				new Vector3(0.3f, 0, 0),
				new Vector3(0, 0.3f, 0)
			));

			window.Run(60.0);
		}




	}



	class XYPair {
		public decimal x { get; set; }
		public decimal y { get; set; }

		public XYPair(decimal x, decimal y) {
			this.x = x;
			this.y = y;
		}
	}

}
