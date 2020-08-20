using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace OpenGLGraphing {
	public class Window : GameWindow {

		int VertexBufferObject;
		int VertexArrayObject;
		Shader shader;

		public Window(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) {

		}

		protected override void OnLoad(EventArgs e)
		{
			shader = new Shader("shader.vert", "shader.frag");
			GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

			VertexArrayObject = GL.GenVertexArray();
			VertexBufferObject = GL.GenBuffer();


			float[] vertices = {
				-0.5f, -0.5f, 0.0f, //Bottom-left vertex
				0.5f, -0.5f, 0.0f,  //Bottom-right vertex
				0.0f,  0.5f, 0.0f   //Top vertex
			};
			shader.Use();


			GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
			GL.BindVertexArray(VertexArrayObject);

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


			GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

			
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
