using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using OpenGLGraphing.Graphs;
using OpenGLGraphing.Primitives;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenGLGraphing {
	public class Window : GameWindow {

		int VertexArrayObject;
		int VertexBufferObject;
		public static Shader shader;


		protected List<IDrawable> drawables = new List<IDrawable>();


		public Window(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) {

		}

		protected override void OnLoad(EventArgs e)
		{
			GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
			
			shader = new Shader("shader.vert", "shader.frag");
			shader.Use();
			
			VertexArrayObject = GL.GenVertexArray();
			GL.BindVertexArray(VertexArrayObject);

			int VertexBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

			int ElementBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
			

			base.OnLoad(e);
		}


		protected override void OnUnload(EventArgs e)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.DeleteBuffer(VertexBufferObject);

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

			GL.BindVertexArray(VertexArrayObject);

			foreach(IDrawable drawable in drawables) {
				drawable.draw();
			}
			
			Context.SwapBuffers();
			base.OnRenderFrame(e);
		}


		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(0, 0, Width, Height);
			base.OnResize(e);
		}



		public void addDrawable(IDrawable drawable) {
			drawables.Add(drawable);
		}


		public void removeDrawable(IDrawable drawable) {
			drawables.Remove(drawable);
		}



		public static Window NewAsyncWindow(int width, int height, string title, double? updatesPerSecond = null, double? framesPerSecond = null) {
			EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

			Window window = null;
			Thread thread = new Thread(() => {

				window = new Window(width, height, title);
				waitHandle.Set();


				if (framesPerSecond != null) {
					if (updatesPerSecond != null) {
						window.Run(updatesPerSecond.Value, framesPerSecond.Value);
					} else {
						window.Run(framesPerSecond.Value);
					}
				} else {
					window.Run();
				}

				
			});

			thread.Start();

			waitHandle.WaitOne();
			return window;
		}





	}
}
