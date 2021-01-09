using System.Collections.Generic;
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


		
	}
}
