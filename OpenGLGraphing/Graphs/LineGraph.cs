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

			window.drawables.Add(new Rectangle(
				new Vector3(0, -0.25f, 0),
				new Vector3(0.5f, 0.5f, 0)) {
				color = Color.Blue
			});

			window.drawables.Add(new Triangle(
				new Vector3(-0.3f, 0, 0),
				new Vector3(0.3f, 0, 0),
				new Vector3(0, 0.3f, 0)
			) {
				color = Color.Pink
			});

			window.drawables.Add(new Line {
				points = new List<Vector3> {
					new Vector3(-0.7f, 0, 0),
					new Vector3(0, 0.4f, 0),
					new Vector3(0.3f, 0.9f, 0),
					new Vector3(-0.3f, 0.9f, 0)
				},
				color = Color.Red
			});

			window.Run();
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
