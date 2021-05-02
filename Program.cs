using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
	public class Program
	{
		static void Main(string[] args)
		{
			HandDigits hd = new HandDigits();
			List<string> accepted = new List<string> { "test", "train", "end" };
			int left = 60000;
			string[] next;
			do {
				Console.Write("Enter a command: ");
				next = Console.ReadLine().Split();
				while (!accepted.Contains(next[0])) {
					Console.Write("Sorry, that command is not recognized. Please try again: ");
					next = Console.ReadLine().Split();
				}
				if (next[0] == "test") {
					left--;
					hd.Test(out int expected, out double[] actual);
					Console.WriteLine("Expected output: " + expected);
					Console.WriteLine("Actual output: " + PrintArray(actual));
				} else if (next[0] == "train") {
					int num;
					if (next.Length == 1 || !int.TryParse(next[1], out num)) {
						Console.Write("How many test cases would you like to run? ");
						string snum = Console.ReadLine();
						while (!int.TryParse(snum, out num))
						{
							Console.Write("Please enter a number: ");
							snum = Console.ReadLine();
						}
					}
					left -= num;
					hd.Train(num);
				}
				if (left < 0) left = 0;
				Console.WriteLine(left + " cases remaining\n");
			} while (next[0] != "end");
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
