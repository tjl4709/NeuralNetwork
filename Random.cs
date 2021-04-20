using System;

namespace NeuralNetwork
{
	public class RandomExt : Random
	{
		private double gauss2;

		public RandomExt() : base() { gauss2 = double.NaN; }
		public RandomExt(int i) : base(i) { gauss2 = double.NaN; }

		public double NextDouble(double max) { return NextDouble(0, max); }
		public double NextDouble(double min, double max)
		{ return NextDouble() * (max - min) + min; }

		public double NextNormal()
		{
			double s;
			if (!double.IsNaN(s = gauss2)) {
				gauss2 = double.NaN;
				return s;
			}
			double u,v;
			do {
				u = NextDouble(-1, 1);
				v = NextDouble(-1, 1);
				s = u * u + v * v;
			} while (s == 0 || s >= 1);
			s = Math.Sqrt(-2 * Math.Log(s) / s);
			gauss2 = u * s;
			return v * s;
		}
	}
}
