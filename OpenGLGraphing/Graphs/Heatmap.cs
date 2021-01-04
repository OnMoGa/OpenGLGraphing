using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Rectangle = OpenGLGraphing.Primitives.Rectangle;
using Vector3 = System.Numerics.Vector3;
using Color = System.Drawing.Color;

namespace OpenGLGraphing.Graphs {
	public class Heatmap<T1> : Graph {



		public readonly ObservableCollection<DataItem> dataItems = new ObservableCollection<DataItem>();
		public IEnumerable<Category> xCategories { get; set; } = new List<Category>();
		public IEnumerable<Category> yCategories { get; set; } = new List<Category>();

		private Division[,] division;


		protected override void sizePosUpdated() {
			updateSquares(dataItems);
		}

		public Heatmap() {
			dataItems.CollectionChanged += (newList, e) => updateSquares((IEnumerable<DataItem>)newList);


		}

		private void updateSquares(IEnumerable<DataItem> newList) {

			int yCategoriesCount = yCategories.Count();
			int xCategoriesCount = xCategories.Count();

			division = new Division[yCategoriesCount, xCategoriesCount];

			Vector3 recSize = new Vector3(size.X / xCategoriesCount - 0.005f, size.Y / yCategoriesCount - 0.005f, 0);
			
			for (int y = 0; y < yCategoriesCount; y++) {
				for (int x = 0; x < xCategoriesCount; x++) {

					Vector3 recPos = new Vector3();
					recPos.X = -size.X/2 + size.X/xCategoriesCount * x + recSize.X/2;
					recPos.Y = -size.Y/2 + size.Y/yCategoriesCount * y + recSize.Y/2;

					Category xCategory = xCategories.Skip(x).First(); 
					Category yCategory = yCategories.Skip(y).First();


					int valuesInRect = newList.Count(v =>
						xCategory.categorizer(v.value) && yCategory.categorizer(v.value));

					division[y, x] = new Division {
						rectangle = new Rectangle() {
							size = recSize,
							pos = recPos
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
			
				




			drawables = divisionList.Select(d => d.rectangle);

		}


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
