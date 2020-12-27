using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing.Primitives {
	public abstract class Primitive : IDrawable {

		public List<float> verticies { get; set; } = new List<float>();


		public virtual Color color { get; set; } = Color.White;


		public void preDraw() {
			GL.Color4(color);
		}


		public virtual void draw() {
			
		}


		public void bindVerticies(float[] vertices, uint[] indices) {
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);
			GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
		}


	}
}
