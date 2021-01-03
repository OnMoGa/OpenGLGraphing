using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Bitmap = System.Drawing.Bitmap;

namespace OpenGLGraphing.Primitives {
	public abstract class Primitive : IDrawable, IDisposable {
		
		int vertexArrayObject; // stores all the settings
		int vertexBufferObject; //stores all the vertex data
		int elementBufferObject; //stores the order of the vertexes need to draw triangles

		public virtual Color color { get; set; } = Color.White;
		protected Texture texture { get; set; }

		public bool visible { get; set; } = true;

		public float[] vertices;
		public uint[] indices;

		public VertexList vertexList { get; set; }

		private void init() {
			vertexArrayObject = GL.GenVertexArray();
			vertexBufferObject = GL.GenBuffer();
			elementBufferObject = GL.GenBuffer();

			bindBuffers();

			int posLength = 3;
			int colorLength = 4;
			int texLength = 2;
			int stride = (posLength + colorLength + texLength) * sizeof(float);

			GL.VertexAttribPointer(0, posLength, VertexAttribPointerType.Float, false, stride, 0 * sizeof(float));
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(1, colorLength, VertexAttribPointerType.Float, false, stride, posLength * sizeof(float));
			GL.EnableVertexAttribArray(1);
			GL.VertexAttribPointer(2, texLength, VertexAttribPointerType.Float, false, stride, (posLength + colorLength) * sizeof(float));
			GL.EnableVertexAttribArray(2);
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
			if (!visible) return;

			if (texture == null) {
				Bitmap bitmap = new Bitmap(1, 1);
				bitmap.SetPixel(0, 0, System.Drawing.Color.White);
				texture = new Texture(bitmap);
			}
			texture.Use();
			
			GL.BufferData(BufferTarget.ArrayBuffer, vertexList.vertexSize, vertexList.getFloats().ToArray(), BufferUsageHint.StaticDraw);
			GL.BufferData(BufferTarget.ElementArrayBuffer, vertexList.indiciesSize, vertexList.indices.ToArray(), BufferUsageHint.StaticDraw);
			GL.DrawElements(primitiveType, vertexList.indices.Count(), DrawElementsType.UnsignedInt, 0);
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

	public class Vertex {
		public Vector3 point { get; set; } = Vector3.Zero;
		public Vector2 texCoord { get; set; }
		public Color color { get; set; }

		public int size => 9 * sizeof(float);

		public IEnumerable<float> getFloats() {

			List<float> floats = new List<float>();
			floats.AddRange(new []{point.X, point.Y, point.Z});

			floats.AddRange(new [] {
				(float)color.R / 255,
				(float)color.G / 255,
				(float)color.B / 255,
				(float)color.A / 255,
			});

			floats.AddRange(new [] {
				texCoord.X,
				texCoord.Y
			});

			return floats.ToArray();
		}
	}

	public class VertexList {
		public IEnumerable<Vertex> vertices { get; set; } = new List<Vertex>();
		public IEnumerable<uint> indices { get; set; } = new List<uint>();
		public int vertexSize => vertices.Sum(v => v.size);
		public int indiciesSize => indices.Count() * sizeof(uint);

		public IEnumerable<float> getFloats() {
			return vertices.SelectMany(v => v.getFloats());
		}
	}



}
