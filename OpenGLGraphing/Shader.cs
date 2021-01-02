using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing {
	public class Shader
	{
		public int handle;

		public Shader(string vertexPath, string fragmentPath) {
			int vertexShader;
			int fragmentShader;

			using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
			{
				string vertexShaderSource = reader.ReadToEnd();
				vertexShader = GL.CreateShader(ShaderType.VertexShader);
				GL.ShaderSource(vertexShader, vertexShaderSource);
			}

			using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
			{
				string fragmentShaderSource = reader.ReadToEnd();
				fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
				GL.ShaderSource(fragmentShader, fragmentShaderSource);
			}


			GL.CompileShader(vertexShader);

			string infoLogVert = GL.GetShaderInfoLog(vertexShader);
			if (infoLogVert != String.Empty)
				Console.WriteLine(infoLogVert);

			GL.CompileShader(fragmentShader);

			string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);

			if (infoLogFrag != String.Empty)
				Console.WriteLine(infoLogFrag);


			handle = GL.CreateProgram();

			GL.AttachShader(handle, vertexShader);
			GL.AttachShader(handle, fragmentShader);

			GL.LinkProgram(handle);


			GL.DetachShader(handle, vertexShader);
			GL.DetachShader(handle, fragmentShader);
			GL.DeleteShader(fragmentShader);
			GL.DeleteShader(vertexShader);

		}


		public void Use()
		{
			GL.UseProgram(handle);
		}


		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				GL.DeleteProgram(handle);

				disposedValue = true;
			}
		}

		~Shader()
		{
			GL.DeleteProgram(handle);
		}


		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}



	}
}
