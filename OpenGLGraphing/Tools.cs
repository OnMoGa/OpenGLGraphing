using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OpenGLGraphing {
	public static class Tools {



		public static System.Drawing.Color SystemColor(this OpenTK.Color color) {
			return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
		}
		public static OpenTK.Color OpenTKColor(this System.Drawing.Color color) {
			return OpenTK.Color.FromArgb(color.A, color.R, color.G, color.B);
		}


	}
}
