using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace OpenGLGraphing {
	public class Window : GameWindow {

		int VertexArrayObject;
		int VertexBufferObject;
		private int ElementBufferObject;
		Shader shader;

		public Window(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) {

		}

		protected override void OnLoad(EventArgs e)
		{
			GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
			
			shader = new Shader("shader.vert", "shader.frag");
			shader.Use();
			
			VertexArrayObject = GL.GenVertexArray();
			GL.BindVertexArray(VertexArrayObject);

			VertexBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

			float[] vertices = {
				0.5f,  0.5f, 0.0f,  // top right
				0.5f, -0.5f, 0.0f,  // bottom right
				-0.5f, -0.5f, 0.0f, // bottom left
				-0.5f,  0.5f, 0.0f  // top left
			};

			uint[] indices = { // note that we start from 0!
				0, 1, 3,       // first triangle
				1, 2, 3        // second triangle
			};

			

			ElementBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
			GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

			
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);
			

			base.OnLoad(e);
		}

		protected override void OnUnload(EventArgs e)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.DeleteBuffer(VertexBufferObject);

			GL.BindVertexArray(VertexArrayObject);

			shader.Dispose();
			base.OnUnload(e);
		}


		protected override void OnUpdateFrame(FrameEventArgs e) {

			KeyboardState input = Keyboard.GetState();

			if (input.IsKeyDown(Key.Escape))
			{
				Exit();
			}

			base.OnUpdateFrame(e);
		}


		protected override void OnRenderFrame(FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);


			//GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
			GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
			
			Context.SwapBuffers();
			base.OnRenderFrame(e);
		}


		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(0, 0, Width, Height);
			base.OnResize(e);
		}




		
	}
}
