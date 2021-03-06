﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenGLGraphing.Primitives;
using OpenGLGraphing.Structures;
using Color = System.Drawing.Color;
using Vector3 = System.Numerics.Vector3;

namespace OpenGLGraphing.Graphs {
	public class LineGraph : Graph {

		
		public readonly SmartCollection<DataPoint> dataPoints = new SmartCollection<DataPoint>();
		private Line line;
		private XYAxes axes;


		protected override void sizePosUpdated() {
			axes.size = size;
			axes.pos = pos;
			updateDataPoints(dataPoints);
		}

		public Color lineColor {
			get => line.color;
			set => line.color = value;
		}

		public float? frameSize = null;
		public bool waitForOverflowBeforeScrolling = false;


		public bool showDataLabels { get; set; }
		private IEnumerable<OST> dataLabels { get; set; } = new List<OST>();
	

		public LineGraph() {
			dataPoints.CollectionChanged += (newList, e) => updateDataPoints((IEnumerable<DataPoint>)newList);
			line = new Line();
			axes = new XYAxes() {
				pos = pos,
				size = pos
			};

			drawables = new List<IDrawable> {
				line, axes
			}.Concat(dataLabels);
		}




		private void updateDataPoints(IEnumerable<DataPoint> dataPoints) {
			if(!dataPoints.Any()) return;

			IEnumerable<Vector3> vectors = dataPoints.Select(d => d.ToVector3());


			if (frameSize == null) {
				List<Vector3> points = vectors
				   .Normalize(
					   new Vector3(
						   Math.Min(vectors.Min(d => d.X), 0),
						   Math.Min(vectors.Min(d => d.Y), 0),
						   Math.Min(vectors.Min(d => d.Z), 0)
					   ),
					   new Vector3(
						   vectors.Max(d => d.X),
						   vectors.Max(d => d.Y),
						   vectors.Max(d => d.Z)
					   )
				   )
				   .Select(v => v.NaNToZero())
				   .ScaleToFit(pos, size)
				   .OrderBy((v) => v.X)
				   .ToList();
				line.points = points;


			} else {
				float rightMostX = vectors.Max(d => d.X);
				float minX = rightMostX - frameSize.Value;

				List<Vector3> allPoints = vectors.Reverse().ToList();
				List<Vector3> relevantPoints = allPoints.TakeWhile(p => p.X > minX).ToList();



				//add cut off point at y-intercept
				if(allPoints.Count > relevantPoints.Count) {
					Vector3 halfPoint = allPoints.Skip(relevantPoints.Count).FirstOrDefault();
					Vector3 lastFullPoint = relevantPoints.Last();


					float gradient = (lastFullPoint.Y - halfPoint.Y) / (lastFullPoint.X - halfPoint.X);
					float negDistance = minX - halfPoint.X;

					float yCompensation = negDistance * gradient;

					halfPoint.X = minX;
					halfPoint.Y += yCompensation;
					relevantPoints.Add(halfPoint);
				}


				List<Vector3> pointsScaled = relevantPoints
					.Normalize(
						new Vector3(
							 minX,
							 Math.Max(relevantPoints.Min(d => d.Y), 0),
							 Math.Max(relevantPoints.Min(d => d.Z), 0)
						 ),
						 new Vector3(
							 frameSize.Value + minX,
							 relevantPoints.Max(d => d.Y),
							 relevantPoints.Max(d => d.Z)
						 )
					)
					.Select(d => d.NaNToZero())
					.ScaleToFit(pos, size)
					.OrderBy(v => v.X)
					.ToList();

				IEnumerable<(Vector3 coordinatePoint, Vector3 dataPoint)> zippedPoints = pointsScaled.Zip(relevantPoints.AsEnumerable().Reverse().ToList(),
					(coordinatePoint, dataPoint) => (coordinatePoint, dataPoint));

				float distanceFromYAxis = pointsScaled.First().X - pos.X + size.X/2;

				if (waitForOverflowBeforeScrolling) {
					pointsScaled = pointsScaled.Select(p => new Vector3(p.X-distanceFromYAxis, p.Y, p.Z)).ToList();
				}

				line.points = pointsScaled;
				axes.minYValue = relevantPoints.Min(p => p.Y);
				axes.maxYValue = relevantPoints.Max(p => p.Y);

				dataLabels = zippedPoints.Select(zp => new OST() {
					text = $"{zp.dataPoint.X:F2}, {zp.dataPoint.Y:F2}",
					pos = zp.coordinatePoint,
					height = 0.05f
				});

				drawables = new List<IDrawable> {
					line, axes
				}.Concat(dataLabels);
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

	}


	


	public static class DataPointTools {

		/// <summary>
		/// Looks at a series of points and scales them to all their values to fit between 0 and 1
		/// </summary>
		/// <param name="vectors"></param>
		/// <param name="dataMin"></param>
		/// <param name="dataMax"></param>
		/// <returns></returns>
		public static IEnumerable<Vector3> Normalize(this IEnumerable<Vector3> vectors, Vector3 dataMin, Vector3 dataMax) {
			return vectors.Select(p => new Vector3(
				(p.X-dataMin.X) / (dataMax.X-dataMin.X),
				(p.Y-dataMin.Y) / (dataMax.Y-dataMin.Y),
				(p.Z-dataMin.Z) / (dataMax.Z-dataMin.Z)
			));
		}

		public static Vector3 NaNToZero(this Vector3 vector) {
			return new Vector3(
				Single.IsNaN(vector.X) ? 0: vector.X,
				Single.IsNaN(vector.Y) ? 0: vector.Y,
				Single.IsNaN(vector.Z) ? 0: vector.Z
			);
		}



		public static Vector3 ToVector3(this LineGraph.DataPoint datapoint) => new Vector3(datapoint.x, datapoint.y, datapoint.z);

	}


}
