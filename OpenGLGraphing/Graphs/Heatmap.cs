using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using OpenGLGraphing.Primitives;
using Rectangle = OpenGLGraphing.Primitives.Rectangle;
using Vector3 = System.Numerics.Vector3;
using Color = System.Drawing.Color;

namespace OpenGLGraphing.Graphs {
	public class Heatmap<T1> : Graph {



		public readonly ObservableCollection<DataItem> dataItems = new ObservableCollection<DataItem>();
		public IEnumerable<Category> xCategories { get; set; } = new List<Category>();
		public IEnumerable<Category> yCategories { get; set; } = new List<Category>();

		private Division[,] division;

		private List<OST> labels = new List<OST>();


		public Heatmap() {
			dataItems.CollectionChanged += (newList, e) => updateSquares((IEnumerable<DataItem>)newList);
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
					pos = getPosForSquare(x, -1),
					anchor = OST.Anchor.Right,
					rotationDegrees = 45f,
					height = 0.05f
				};
				labels.Add(ost);
			}
		}



		private void updateSquares(IEnumerable<DataItem> newList) {

			int yCategoriesCount = yCategories.Count();
			int xCategoriesCount = xCategories.Count();

			division = new Division[yCategoriesCount, xCategoriesCount];

			Vector3 recSize = new Vector3(size.X / xCategoriesCount - 0.002f, size.Y / yCategoriesCount - 0.002f, 0);
			
			for (int y = 0; y < yCategoriesCount; y++) {
				for (int x = 0; x < xCategoriesCount; x++) {

					Category xCategory = xCategories.Skip(x).First(); 
					Category yCategory = yCategories.Skip(y).First();

					int valuesInRect = newList.Count(v =>
						xCategory.categorizer(v.value) && yCategory.categorizer(v.value));

					division[y, x] = new Division {
						rectangle = new Rectangle() {
							size = recSize,
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

					d.rectangle.color = Color.FromArgb(
						255,
						255-(int)(relativeIntensity * 255),
						(int)(relativeIntensity * 255),
						0
					);
				});
			}
			

			drawables = divisionList.Select(d => d.rectangle).Concat<IDrawable>(labels);

		}


		private Vector3 getPosForSquare(int x, int y) =>
			new Vector3(
				size.X / xCategories.Count() * (x + 0.5f),
				size.Y / yCategories.Count() * (y + 0.5f),
				0);


		class Division {
			public Rectangle rectangle { get; set; }
			public int intensity { get; set; }
		}


		public class Category {
			public Func<T1, bool> categorizer{ get; set; }
			public string label { get; set; }

		}
		public class DataItem {
			public T1 value { get; set; }
		}
	}

	

}
