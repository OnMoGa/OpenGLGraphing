using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGLGraphing.Primitives;
using OpenTK;
using Rectangle = OpenGLGraphing.Primitives.Rectangle;
using Color = System.Drawing.Color;
using Vector3 = System.Numerics.Vector3;

namespace OpenGLGraphing.Structures {
	class XYAxes : Structure {

		public Rectangle xAxes = new Rectangle() {
			color = Color.FromArgb(255, 255, 255, 255)
		};
		public Rectangle yAxes = new Rectangle() {
			color = Color.FromArgb(255, 255, 255, 255)
		};
		public Rectangle cornerBlock = new Rectangle() {
			color = Color.FromArgb(255, 255, 255, 255)
		};


		public List<AxesTick> yAxesTicks { get; set; } = new List<AxesTick>();
		public List<AxesTick> xAxesTicks { get; set; } = new List<AxesTick>();
		

		private float _maxYValue;
		public float maxYValue {
			get {
				return _maxYValue;
			}
			set {
				_maxYValue = value;
				update();
			}
		}


		private float _minYValue;
		public float minYValue {
			get {
				return _minYValue;
			}
			set {
				_minYValue = value;
				update();
			}
		}


		protected override void update() {
			float xAxesHeight = 0.01f * size.Y;
			float yAxesWidth = 0.01f * size.X;
			Vector3 tickSize = new Vector3(0.04f, 0.01f, 0);

			xAxes.pos = new Vector3(
				pos.X,
				pos.Y - size.Y/2 - xAxesHeight/2,
				pos.Z);
			xAxes.size = new Vector3(size.X, xAxesHeight, 0);
			
			yAxes.pos = new Vector3(
				pos.X - size.X/2 - yAxesWidth/2,
				pos.Y,
				pos.Z);
			yAxes.size = new Vector3(yAxesWidth, size.Y, 0);

			cornerBlock.pos = new Vector3(
				pos.X - size.X/2 - yAxesWidth/2,
				pos.Y - size.Y/2 - xAxesHeight/2,
				pos.Z);
			cornerBlock.size = new Vector3(yAxesWidth, xAxesHeight, 0);


			yAxesTicks = new List<AxesTick>();

			AxesTick maxTick = new AxesTick();
			maxTick.tick.pos = yAxes.pos + new Vector3(-yAxesWidth/2, yAxes.size.Y/2, 0) - tickSize/2;
			maxTick.tick.size = tickSize;
			maxTick.label.text = $"{maxYValue:F2}";
			maxTick.label.pos = maxTick.tick.pos + new Vector3(-0.01f, 0, 0);
			maxTick.label.anchor = OST.Anchor.Right;
			yAxesTicks.Add(maxTick);

			AxesTick minTick = new AxesTick();
			minTick.tick.pos = yAxes.pos + new Vector3(-yAxesWidth/2, -yAxes.size.Y/2, 0) + new Vector3(-tickSize.X/2, 0, 0);
			minTick.tick.size = tickSize;
			minTick.label.text = $"{minYValue:F2}";
			minTick.label.pos = minTick.tick.pos + new Vector3(-0.01f, 0, 0);
			minTick.label.anchor = OST.Anchor.Right;
			yAxesTicks.Add(minTick);


			drawables = new List<IDrawable> {
				xAxes, yAxes, cornerBlock
			}
			.Concat(xAxesTicks)
			.Concat(yAxesTicks);
		}

	}

	public class AxesTick : Structure {

		public Rectangle tick = new Rectangle() {
			color = Color.FromArgb(255, 255, 255, 255)
		};
		public OST label = new OST();

		public AxesTick() {
			drawables = new List<IDrawable>() {
				tick,
				label
			};
		}

		protected override void update() {
			throw new NotImplementedException();
		}
	}



}
