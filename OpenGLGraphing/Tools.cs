using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLGraphing {
	public static class Tools {



		public static System.Drawing.Color SystemColor(this OpenTK.Color color) {
			return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
		}
		public static OpenTK.Color OpenTKColor(this System.Drawing.Color color) {
			return OpenTK.Color.FromArgb(color.A, color.R, color.G, color.B);
		}





		/// <summary>
		/// Takes points between with values between 0-1 and fits them in the size at the position provided
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




	}
}
