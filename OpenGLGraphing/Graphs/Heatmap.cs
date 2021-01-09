using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using OpenGLGraphing.Primitives;
using Rectangle = OpenGLGraphing.Primitives.Rectangle;
using Vector3 = System.Numerics.Vector3;
using Color = System.Drawing.Color;

namespace OpenGLGraphing.Graphs {
	public class Heatmap<T1, T2, T3> : Graph {



		public readonly SmartCollection<T1> dataItems = new SmartCollection<T1>();
		public IEnumerable<XCategory> xCategories { get; set; } = new List<XCategory>();
		public IEnumerable<YCategory> yCategories { get; set; } = new List<YCategory>();

		public Func<T1, XCategory, YCategory, bool> categorizer { get; set; }

		private Division[,] division;

		private List<OST> labels = new List<OST>();


		public Color colorSmall { get; set; } = Color.FromArgb(255, 0, 0);
		public Color colorLarge { get; set; } = Color.FromArgb(0, 255, 0);


		public Heatmap() {
			dataItems.CollectionChanged += (newList, e) => updateSquares((IEnumerable<T1>)newList);
		}


		protected override void sizePosUpdated() {
			updateSquares(dataItems);
			updateLabels();
		}



		private void updateLabels() {
			labels = new List<OST>();
			for (int x = 0; x < xCategories.Count(); x++) {
				OST ost = new OST() {
					text = xCategories.Skip(x).First().label,
					pos = getPosForSquare(x, 0) - new Vector3(0, squareSize.Y/2, 0),
					anchor = OST.Anchor.Right,
					rotationDegrees = 45f,
					height = 0.05f
				};
				labels.Add(ost);
			}

			for (int y = 0; y < yCategories.Count(); y++) {
				OST ost = new OST() {
					text = yCategories.Skip(y).First().label,
					pos = getPosForSquare(0, y) - new Vector3(squareSize.X/2, 0, 0),
					anchor = OST.Anchor.Right,
					height = Math.Min(Math.Max(size.Y/yCategories.Count(), 0.01f), 0.1f)
				};
				labels.Add(ost);
			}

		}



		private void updateSquares(IEnumerable<T1> newList) {

			int yCategoriesCount = yCategories.Count();
			int xCategoriesCount = xCategories.Count();

			division = new Division[yCategoriesCount, xCategoriesCount];

			for (int y = 0; y < yCategoriesCount; y++) {
				for (int x = 0; x < xCategoriesCount; x++) {

					XCategory xCategory = xCategories.Skip(x).First(); 
					YCategory yCategory = yCategories.Skip(y).First();
					
					int valuesInRect = newList.Count(v => categorizer(v, xCategory, yCategory));
					
					division[y, x] = new Division {
						rectangle = new Rectangle() {
							size = squareSize,
							pos = getPosForSquare(x, y)
						},
						intensity = valuesInRect
					};
				}
			}
			List<Division> divisionList = division.Cast<Division>().ToList();

			int maxIntensity = divisionList.Any() ? divisionList.Max(d => d.intensity) : 0;
			int minIntensity = divisionList.Any() ? divisionList.Min(d => d.intensity) : 0;
			int intensityRange = maxIntensity - minIntensity;

			if (intensityRange != 0) {
				divisionList.ForEach(d => {
					float relativeIntensity = (d.intensity - minIntensity) / (float)intensityRange;

					d.rectangle.color = Tools.mixColors(colorSmall, colorLarge, relativeIntensity);
				});
			}
			

			drawables = divisionList.Select(d => d.rectangle).Concat<IDrawable>(labels);

		}


		private Vector3 getPosForSquare(int x, int y) =>
			new Vector3(
				size.X / xCategories.Count() * (x + 0.5f) - size.X/2,
				size.Y / yCategories.Count() * (y + 0.5f) - size.Y/2,
				0);

		private Vector3 squareSize => new Vector3(size.X / xCategories.Count() - 0.002f, size.Y / yCategories.Count() - 0.002f, 0);
		


		class Division {
			public Rectangle rectangle { get; set; }
			public int intensity { get; set; }
		}


		public class XCategory {
			public string label { get; set; }
			public T2 value { get; set; }
		}

		public class YCategory {
			public string label { get; set; }
			public T3 value { get; set; }
		}

	}

	

}
