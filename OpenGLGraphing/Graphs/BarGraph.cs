﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using OpenGLGraphing.Primitives;
using OpenGLGraphing.Structures;
using Rectangle = OpenGLGraphing.Primitives.Rectangle;

namespace OpenGLGraphing.Graphs {
	public class BarGraph : Graph {



		public SmartCollection<Bar> bars { get; set; } = new SmartCollection<Bar>();

		private XYAxes axes = new XYAxes() {
			size = new Vector3(1, 1, 0)
		};

		public BarGraph() {
			bars.CollectionChanged += (newList, e) => updateBars((IEnumerable<Bar>)newList); 

			drawables = new List<IDrawable>() {
				axes
			};
		}



		protected override void sizePosUpdated() {


		}

		

		private void updateBars(IEnumerable<Bar> data) {

			List<Bar> bars = data.ToList();

			List<Rectangle> rectangles = new List<Rectangle>();
			List<OST> labels = new List<OST>();

			float maxHeight = bars.Max(b => b.height);

			axes.maxYValue = maxHeight;

			float barSpacing = size.X/bars.Count;
			float barWidth = barSpacing - 0.01f;
			
			for (int barIndex = 0; barIndex < bars.Count; barIndex++) {
				Bar bar = bars[barIndex];
				float barX = (-bars.Count/2.0f*barSpacing) + (barIndex*barSpacing) + (barSpacing*0.5f);
				float scaledHeight = bar.height / maxHeight * size.Y;

				Rectangle rectangle = new Rectangle() {
					pos = new Vector3(barX, -size.Y/2 + scaledHeight/2, 0),
					size = new Vector3(barWidth, scaledHeight, 0.1f)
				};
				rectangles.Add(rectangle);


				OST label = new OST() {
					text = bar.label,
					rotationDegrees = -45,
					anchor = OST.Anchor.Left,
					pos = new Vector3(barX, -size.Y/2 - 0.02f, 0),
					height = 0.05f
				};

				labels.Add(label);

			}


			drawables = new List<IDrawable> {
				axes
			}.Concat(rectangles).Concat(labels);
		}



		public class Bar {
			public string label { get; set; }
			public Color color { get; set; }
			public float height { get; set; }

		}


	}
}
