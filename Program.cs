using System;

namespace NeuralNetwork
{
	public class Program
	{
		static void Main(string[] args)
		{
			Network nn = new Network(new int[] { 4, 7, 13, 10 });
			double[] results = nn.Predict(new double[] { 0, 1, 2, 3 });
			Console.WriteLine();
			Console.WriteLine(PrintArray<double>(results));
			Console.ReadLine();
		}

		public static string PrintArray<T>(T[] arr)
		{
			string ret = "[";
			if (arr != null && arr.Length > 0)
				ret += arr[0].ToString();
			for (int i = 1; i < arr.Length; i++)
				ret += ", " + arr[i].ToString();
			return ret + "]";
		}
		public static string PrintArray<T>(T[,] arr)
		{
			string ret = "[";
			if (arr.GetLength(1) > 0)
				for (int r = 0; r < arr.GetLength(0); r++) {
					ret += "[" + arr[r,0].ToString();
					for (int c = 0; c < arr.GetLength(1); c++)
						ret += ", " + arr[r,c].ToString();
					ret += "]\n";
				}
			return ret + "]";
		}
	}
}
