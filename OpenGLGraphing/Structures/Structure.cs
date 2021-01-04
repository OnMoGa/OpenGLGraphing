using System;
using System.Collections.Generic;
using System.Numerics;
using OpenGLGraphing.Primitives;

namespace OpenGLGraphing.Structures {
	public abstract class Structure : IDrawable {

		public bool visible { get; set; } = true;
		public IEnumerable<IDrawable> drawables = new List<IDrawable>();


		private Vector3 _pos;
		public Vector3 pos {
			get => _pos;
			set {
				_pos = value;
				sizePosUpdated();
			}
		}

		private Vector3 _size;
		public Vector3 size {
			get => _size;
			set {
				_size = value;
				sizePosUpdated();
			}
		}

		protected abstract void sizePosUpdated();

		public void draw() {
			if(!visible) return;
			foreach (IDrawable drawable in drawables) {
				drawable.draw();
			}
		}

		
	}
}
