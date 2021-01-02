using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BitmapGenerators;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Bitmap = System.Drawing.Bitmap;
using Color = OpenTK.Color;
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
			texture ??= new Texture(bitmap);
			preDraw();


			float left = pos.X - size.X / 2;
			float right = pos.X + size.X / 2;
			float top = pos.Y - size.Y / 2;
			float bottom = pos.Y + size.Y / 2;

			vertexList = new VertexList() {
				vertices = new List<Vertex> {
					new Vertex {
						point = new Vector3(left, bottom, pos.Z),
						color = color,
						texCoord = new Vector2(0, 0)
					},
					new Vertex {
						point = new Vector3(left, top, pos.Z),
						color = color,
						texCoord = new Vector2(0, 1)
					},
					new Vertex {
						point = new Vector3(right, bottom, pos.Z),
						color = color,
						texCoord = new Vector2(1, 0)
					},
					new Vertex {
						point = new Vector3(right, top, pos.Z),
						color = color,
						texCoord = new Vector2(1, 1)
					}
				},
				indices = new List<uint>{
					0, 1, 2,
					2, 3, 1
				}
			};

			draw(PrimitiveType.Triangles);
		}

	}
}
