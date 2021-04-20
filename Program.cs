using System;

namespace NeuralNetwork
{
	public class Program
	{
		static void Main(string[] args)
		{
			Network nn = new Network(new int[] { 4, 7, 13, 10 });
			double[] results = nn.Predict(new double[] { 0, 1, 2, 3 });
			Console.WriteLine("\n");
			Console.WriteLine(PrintArray<double>(results));
			Console.ReadLine();
		}

		static string PrintArray<T>(T[] arr)
		{
			string ret = "[";
			if (arr != null && arr.Length > 0)
				ret += arr[0].ToString();
			for (int i = 1; i < arr.Length; i++)
				ret += ", " + arr[i].ToString();
			return ret + "]";
		}
	}
}
