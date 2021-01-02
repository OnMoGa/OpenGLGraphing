using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenGLGraphing.Primitives;
using OpenTK;
using Rectangle = OpenGLGraphing.Primitives.Rectangle;

namespace OpenGLGraphing.Structures {
	class XYAxes : Structure {

		public Rectangle xAxes = new Rectangle() {
			color = new Color(255, 255, 255, 50)
		};
		public Rectangle yAxes = new Rectangle() {
			color = new Color(255, 255, 255, 50)
		};


		public List<AxesTick> yAxesTicks { get; set; } = new List<AxesTick>();
		public List<AxesTick> xAxesTicks { get; set; } = new List<AxesTick>();


		private Vector3 _pos;
		public Vector3 pos {
			get => _pos;
			set {
				_pos = value;
				update();
			}
		}


		private Vector3 _size;
		public Vector3 size {
			get => _size;
			set {
				_size = value;
				update();
			}
		}


		private float _maxValue;
		public float maxValue {
			get {
				return _maxValue;
			}
			set {
				_maxValue = value;
				update();
			}
		}


		private float _minValue;
		public float minValue {
			get {
				return _minValue;
			}
			set {
				_minValue = value;
				update();
			}
		}



		private void update() {
			float xAxesHeight = 0.01f * size.Y;
			float yAxesWidth = 0.01f * size.X;

			xAxes.pos = new Vector3(pos.X - yAxesWidth / 4, pos.Y - size.Y / 2, pos.Z);
			xAxes.size = new Vector3(size.X + yAxesWidth / 2, xAxesHeight, 0);
			
			yAxes.pos = new Vector3(pos.X - size.X / 2, pos.Y - xAxesHeight / 4, pos.Z);
			yAxes.size = new Vector3(yAxesWidth, size.Y + xAxesHeight / 2, 0);


			yAxesTicks = new List<AxesTick>();

			AxesTick maxTick = new AxesTick();
			maxTick.tick.pos = yAxes.pos + new Vector3(-0.02f, yAxes.size.Y/2, 0);
			maxTick.label.text = $"{maxValue:F2}";
			maxTick.label.pos = maxTick.tick.pos + new Vector3(-0.1f, 0, 0);
			yAxesTicks.Add(maxTick);

			AxesTick minTick = new AxesTick();
			minTick.tick.pos = yAxes.pos + new Vector3(-0.02f, -yAxes.size.Y/2, 0);
			minTick.label.text = $"{minValue:F2}";
			yAxesTicks.Add(minTick);
			

			drawables = new List<IDrawable> {
				xAxes, yAxes
			}
			.Concat(xAxesTicks)
			.Concat(yAxesTicks);
		}

	}

	public class AxesTick : Structure {

		public Rectangle tick = new Rectangle() {
			size = new Vector3(0.04f, 0.01f, 0)
		};

		public OST label = new OST();

		public AxesTick() {

			drawables = new List<IDrawable>() {
				tick,
				label
			};
		}
	}



}
