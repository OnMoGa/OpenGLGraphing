using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using BitmapGenerators;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Bitmap = System.Drawing.Bitmap;

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


		public void draw() {
			if(pos == null || size == null) return;

			preDraw();


			List<byte> pixels = new List<byte>();
			
			for(int y = 0; y < bitmap.Height; y++) {
				for(int x = 0; x < bitmap.Width; x++) {
					System.Drawing.Color pixel = bitmap.GetPixel(x, y);
					pixels.Add(pixel.R);
					pixels.Add(pixel.G);
					pixels.Add(pixel.B);
					pixels.Add(pixel.A);
				}
			}

			int textureID;
			GL.GenTextures(1, out textureID);
			GL.BindTexture(TextureTarget.Texture2D, textureID);

			BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
				ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
				OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			bitmap.UnlockBits(data);


			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			
			GL.BindTexture(TextureTarget.Texture2D, textureID);

			GL.Begin(BeginMode.Quads);
			GL.TexCoord2(0, 0);
			GL.Vertex2(-1 * size.X / 2, 1 * size.Y / 2);

			GL.TexCoord2(1, 0);
			GL.Vertex2(1 * size.X / 2, 1 * size.Y / 2);

			GL.TexCoord2(1, 1);
			GL.Vertex2(1 * size.X / 2, -1 * size.Y / 2);

			GL.TexCoord2(0, 1);
			GL.Vertex2(-1 * size.X / 2, -1 * size.Y / 2);
			GL.End();

			base.draw();
		}




	}
}
