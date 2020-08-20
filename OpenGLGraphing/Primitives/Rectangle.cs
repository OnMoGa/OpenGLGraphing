﻿using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing.Primitives {
	class Rectangle : Primitive {


		public Vector3 pos { get; set; }
		public Vector3 size { get; set; }

		public Rectangle(Vector3 pos, Vector3 size) {
			this.pos=pos;
			this.size=size;
		}

		public override void draw() {
			preDraw();

			float left = pos.X - size.X / 2;
			float right = pos.X + size.X / 2;
			float top = pos.Y - size.Y / 2;
			float bottom = pos.Y + size.Y / 2;
			
			float[] verts = {
				left, bottom, pos.Z,
				left,  top, pos.Z,
				right, bottom, pos.Z,
				right,  top, pos.Z
			};
			
			uint[] indices = {
				0, 1, 3,
				0, 2, 3
			};

			GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);
			
			GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

			GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

			base.draw();
		}


	}
}