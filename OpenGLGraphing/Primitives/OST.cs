using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using BitmapGenerators;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Bitmap = System.Drawing.Bitmap;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace OpenGLGraphing.Primitives {
	public class OST : Primitive, IDrawable {

		private string _text;
		public string text {
			get {
				return _text;
			}
			set {
				_text = value;
				generateBitmap();
			}
		}

		private Font _font = new Font("Arial", 100);
		public Font font {
			get {
				return _font;
			}
			set {
				_font = new Font(value.FontFamily, 100);
				generateBitmap();
			}
		}

		public override OpenTK.Color color {
			get {
				return base.color;
			}
			set {
				base.color = value;
				generateBitmap();
			}
		}


		public Vector3 pos { get; set; } = new Vector3(0, 0, 0);
		public Vector3 size { get; set; } = new Vector3(0.1f, 0.1f, 0);

		private Bitmap bitmap;


		private void generateBitmap() {
			bitmap = BitmapGenerators.TextToBitmap.GenerateBitmap(text, new TextOptions() {
				backgroundColor = System.Drawing.Color.FromArgb(0, 0, 0, 0),
				font = font,
				textColor = color.SystemColor()
			});
		}


		public override void draw() {
			//if(pos == null || size == null) return;
			//preDraw();

			//List<byte> pixels = new List<byte>();
			//for(int y = 0; y < bitmap.Height; y++) {
			//	for(int x = 0; x < bitmap.Width; x++) {
			//		System.Drawing.Color pixel = bitmap.GetPixel(x, y);
			//		pixels.Add(pixel.R);
			//		pixels.Add(pixel.G);
			//		pixels.Add(pixel.B);
			//		pixels.Add(pixel.A);
			//	}
			//}


			//float[] texCoords = {
			//	0.0f, 0.0f, // lower-left corner  
			//	1.0f, 0.0f, // lower-right corner
			//	0.0f, 1.0f,  // top-left corner
			//	1.0f, 1.0f  // top-right corner
			//};

			////wrapping
			//GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
			//GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

			////filtering
			//GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
			//GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);


			//GL.GenTextures(1, out int texture);
			//GL.ActiveTexture(TextureUnit.Texture0); // activate the texture unit first before binding texture
			//GL.BindTexture(TextureTarget.Texture2D, texture);
			//GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
			//	bitmap.Width, bitmap.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());

			//GL.BindVertexArray(Window.VertexArrayObject);

			//float left = pos.X - size.X / 2;
			//float right = pos.X + size.X / 2;
			//float top = pos.Y - size.Y / 2;
			//float bottom = pos.Y + size.Y / 2;
			
			//float[] verts = {
			//	left,  bottom, pos.Z, color.R, color.G, color.B,
			//	left,  top,    pos.Z, color.R, color.G, color.B,
			//	right, bottom, pos.Z, color.R, color.G, color.B,
			//	right, top,    pos.Z, color.R, color.G, color.B,
			//};
			
			//uint[] indices = {
			//	0, 1, 3,
			//	0, 2, 3
			//};

			//bindVerticies(verts, indices);

			//GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);


			//base.draw();
		}

	}
}
