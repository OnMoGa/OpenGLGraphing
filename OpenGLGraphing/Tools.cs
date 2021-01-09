using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace OpenGLGraphing {
	public static class Tools {
		
		/// <summary>
		/// Takes points with values between 0-1 and fits them in the size at the position provided
		/// </summary>
		/// <param name="points"></param>
		/// <param name="pos"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public static IEnumerable<Vector3> ScaleToFit(this IEnumerable<Vector3> points, Vector3 pos, Vector3 size) {
			return points.Select(v => new Vector3(
				v.X * size.X + pos.X - size.X/2,
				v.Y * size.Y + pos.Y - size.Y/2,
				v.Z * size.Z + pos.Z - size.Z/2
			));
		}


		public static Vector3 ToSystemVector3(this OpenTK.Vector3 vec3) {
			return new Vector3(vec3.X, vec3.Y, vec3.Z);
		}

		public static OpenTK.Vector3 ToOpenTKVector3(this Vector3 vec3) {
			return new OpenTK.Vector3(vec3.X, vec3.Y, vec3.Z);
		}


		public static Color mixColors(Color color1, Color color2, double ratio) {

			int aDiff = color2.A - color1.A;
			double aDelta = aDiff * ratio;
			int newA = (int)(color1.A + aDelta);

			int rDiff = color2.R - color1.R;
			double rDelta = rDiff * ratio;
			int newR = (int)(color1.R + rDelta);

			int gDiff = color2.G - color1.G;
			double gDelta = gDiff * ratio;
			int newG = (int)(color1.G + gDelta);

			int bDiff = color2.B - color1.B;
			double bDelta = bDiff * ratio;
			int newB = (int)(color1.B + bDelta);

			return Color.FromArgb(newA, newR, newG, newB);
		}
		
	}
}
