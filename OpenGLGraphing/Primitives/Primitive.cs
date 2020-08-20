using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing.Primitives {
	abstract class Primitive : IDrawable {

		public List<float> verticies { get; set; } = new List<float>();


		public Color color { get; set; } = Color.White;


		public void preDraw() {
			GL.Color4(color);
		}


		public virtual void draw() {
			
		}



	}
}
