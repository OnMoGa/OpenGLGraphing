using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BitmapGenerators;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Bitmap = System.Drawing.Bitmap;
using Color = System.Drawing.Color;
using Vector3 = System.Numerics.Vector3;

namespace OpenGLGraphing.Primitives {
	public class OST : Primitive {

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

		public override Color color {
			get {
				return base.color;
			}
			set {
				base.color = value;
				generateBitmap();
			}
		}


		public Vector3 pos { get; set; } = new Vector3(0, 0, 0);
		public float height { get; set; } = 0.1f;

		public enum Anchor {
			Left,
			Right,
			Center
		}

		public Anchor anchor { get; set; } = Anchor.Center;

		private Bitmap bitmap;

		private void generateBitmap() {
			bitmap = BitmapGenerators.TextToBitmap.GenerateBitmap(text, new TextOptions() {
				backgroundColor = Color.FromArgb(0, 0, 0, 0),
				font = font,
				textColor = color
			});
		}


		public override void draw() {
			texture ??= new Texture(bitmap);
			preDraw();

			float aspectRatio = (float)bitmap.Width / bitmap.Height;
			Vector3 size = new Vector3(height * aspectRatio, height, 1);
			
			vertexList = new VertexList() {
				vertices = new List<Vertex> {
					new Vertex {
						point = new Vector3(-0.5f, -0.5f, pos.Z),
						color = color,
						texCoord = new Vector2(0, 1)
					},
					new Vertex {
						point = new Vector3(-0.5f, 0.5f, pos.Z),
						color = color,
						texCoord = new Vector2(0, 0)
					},
					new Vertex {
						point = new Vector3(0.5f, -0.5f, pos.Z),
						color = color,
						texCoord = new Vector2(1, 1)
					},
					new Vertex {
						point = new Vector3(0.5f, 0.5f, pos.Z),
						color = color,
						texCoord = new Vector2(1, 0)
					}
				},
				indices = new List<uint>{
					0, 1, 2,
					2, 3, 1
				}
			};

			transformationMatrix = Matrix4.Identity
				* Matrix4.CreateScale(size.ToOpenTKVector3());

			if(anchor == Anchor.Left) {
				transformationMatrix *= Matrix4.CreateTranslation(size.X/2, 0, 0);
			} else if(anchor == Anchor.Right) {
				transformationMatrix *= Matrix4.CreateTranslation(-size.X/2, 0, 0);
			}

			transformationMatrix *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(45))
				* Matrix4.CreateTranslation(pos.ToOpenTKVector3());
			
			draw(PrimitiveType.Triangles);
		}

	}
}
