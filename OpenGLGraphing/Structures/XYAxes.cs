using System;
using System.Collections.Generic;
using System.Text;
using OpenGLGraphing.Primitives;
using OpenTK;
using Rectangle = OpenGLGraphing.Primitives.Rectangle;

namespace OpenGLGraphing.Structures {
	class XYAxes : Structure {

		public Rectangle xAxes = new Rectangle();
		public Rectangle yAxes = new Rectangle();

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


		private void update() {
			float xAxesHeight = 0.01f * size.Y;
			float yAxesWidth = 0.01f * size.X;

			xAxes.pos = new Vector3(pos.X - yAxesWidth / 4, pos.Y - size.Y / 2, pos.Z);
			xAxes.size = new Vector3(size.X + yAxesWidth / 2, xAxesHeight, 0);
			
			yAxes.pos = new Vector3(pos.X - size.X / 2, pos.Y - xAxesHeight / 4, pos.Z);
			yAxes.size = new Vector3(yAxesWidth, size.Y + xAxesHeight / 2, 0);

			drawables = new List<IDrawable> {
				xAxes, yAxes
			};
		}


	}
}
