using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing.Primitives {
	public abstract class Primitive : IDrawable, IDisposable {
		
		int vertexArrayObject; // stores all the settings
		int vertexBufferObject; //stores all the vertex data
		int elementBufferObject; //stores the order of the vertexes need to draw triangles

		public virtual Color color { get; set; } = Color.White;

		public float[] verticies;
		public uint[] indices;

		private void init() {
			vertexArrayObject = GL.GenVertexArray();
			vertexBufferObject = GL.GenBuffer();
			elementBufferObject = GL.GenBuffer();

			bindBuffers();
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0 * sizeof(float));
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));
			GL.EnableVertexAttribArray(1);
		}


		public void preDraw() {
			if(vertexArrayObject == 0) init();
			bindBuffers();
		}

		public void bindBuffers() {
			GL.BindVertexArray(vertexArrayObject);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
		}

		
		public abstract void draw();
		public void draw(PrimitiveType primitiveType) {
			GL.BufferData(BufferTarget.ArrayBuffer, verticies.Length * sizeof(float), verticies, BufferUsageHint.StaticDraw);
			GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
			GL.DrawElements(primitiveType, indices.Length, DrawElementsType.UnsignedInt, 0);
		}
		


		private bool _disposed = false;
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Dispose(bool disposing) {
			if(_disposed) return;

			if (disposing) {
				GL.DeleteBuffer(vertexBufferObject);
				GL.DeleteBuffer(elementBufferObject);
				_disposed = true;
			}
		}

	}

	class Vertex {
		public Vector3 point { get; set; } = Vector3.Zero;
		public Color color { get; set; } = Color.White;

		public IEnumerable<float> toFloatArray() {
			return new[] {point.X, point.Y, point.Z, (float)color.R / 255, (float)color.G / 255, (float)color.B / 255, (float)color.A / 255};
		}

	}

}
