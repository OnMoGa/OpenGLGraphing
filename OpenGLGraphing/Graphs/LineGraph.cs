using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenGLGraphing.Primitives;
using OpenGLGraphing.Structures;
using OpenTK;

namespace OpenGLGraphing.Graphs {
	public class LineGraph : Graph {

		
		public ObservableCollection<DataPoint> dataPoints = new ObservableCollection<DataPoint>();


		private Vector3 _size = new Vector3(1.6f, 1.6f, 0);
		public Vector3 size {
			get => _size;
			set {
				_size = value;
				axes.size = value;
				updateDataPoints(dataPoints);
			}
		}

		private Vector3 _pos = new Vector3(0, 0, 0);
		public Vector3 pos {
			get => _pos;
			set {
				_pos = value;
				axes.pos = value;
				updateDataPoints(dataPoints);
			}
		}


		private Line line;
		private XYAxes axes;
	

		public LineGraph() {
			dataPoints.CollectionChanged += (newList, e) => updateDataPoints((IEnumerable<DataPoint>)newList);
			line = new Line();
			axes = new XYAxes() {
				pos = pos,
				size = size
			};

		}


		public override Thread showInNewWindow(int width, int height, string title) {


			Thread thread = new Thread(() => {

				Window window = new Window(width, height, title);
				
				line.color = Color.Red;

				window.drawables.Add(line);
				window.drawables.Add(axes);
				window.Run();

			});

			thread.Start();

			return thread;
		}


		private void updateDataPoints(IEnumerable<DataPoint> dataPoints) {
			if(!dataPoints.Any()) return;
			
			var points = dataPoints
						 .normalize(size, new Vector3(
							 dataPoints.Max(d => d.x),
							 dataPoints.Max(d => d.y),
							 dataPoints.Max(d => d.z)))
						 .Select(d => d.ToVector3())
						 .Select(v => new Vector3(v.X + pos.X - size.X/2, v.Y + pos.Y - size.Y/2, v.Z + pos.Z - size.Z/2))
						 .OrderBy((v) => v.X)
						 .ToList();
			line.points = points;
		}






	}

	public class DataPoint {

		public float x { get; set; }
		public float y { get; set; }
		public float z { get; set; }

		public DataPoint(float x, float y, float z) {
			this.x=x;
			this.y=y;
			this.z=z;
		}

		public DataPoint(float x, float y) {
			this.x=x;
			this.y=y;
			this.z = 0;
		}
		
	}
	



	public static class DataPointTools {

		public static IEnumerable<DataPoint> normalize(this IEnumerable<DataPoint> dataPoints, Vector3 graphSize, Vector3 dataRange) {
			return dataPoints.Select(p => new DataPoint(
				(p.x/(dataRange.X == 0 ? 1:dataRange.X)) * graphSize.X,
				(p.y/(dataRange.Y == 0 ? 1:dataRange.Y)) * graphSize.Y,
				(p.z/(dataRange.Z == 0 ? 1:dataRange.Z)) * graphSize.Z
			)).ToList();
		}

		public static Vector3 ToVector3(this DataPoint datapoint) => new Vector3(datapoint.x, datapoint.y, datapoint.z);

	}


}
