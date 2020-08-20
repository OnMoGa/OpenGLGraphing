using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGLGraphing {
	class LineGraph : Graph {

		List<XYPair> dataPoints = new List<XYPair>();

		public LineGraph() {

		}

		public override void showInNewWindow(int x, int y, string title) {
			Window window = new Window(x, y, title);



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
