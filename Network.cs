using System;

namespace NeuralNetwork
{
	public class Network
	{
		private Layer[] layers;
		private RandomExt rand;

		public Network(int[] layerSizes)
		{
			rand = new RandomExt();
			layers = new Layer[layerSizes.Length - 1];
			for (int i = 0; i < layers.Length; i++)
				layers[i] = new Layer(layerSizes[i], layerSizes[i + 1], rand);
		}

		public double[] Predict(double[] input)
		{
			foreach (Layer layer in layers)
				input = layer.Predict(input);
			return input;
		}

		public override string ToString()
		{
			string ret = "";
			for (int i = 0; i < layers.Length; i++)
				ret += layers[i].ToString() + "\n";
			return ret;
		}
	}

	public class Layer
	{
		private RandomExt rand;

		private double[,] weights;
		public double[,] Weights { get { return weights; } }

		private double[] biases;
		public double[] Biases { get { return biases; } }

		public Layer(int preSize, int size, RandomExt rand)
		{
			this.rand = rand;
			weights = new double[size, preSize];
			biases = new double[size];
			for (int r = 0; r < weights.GetLength(0); r++) {
				biases[r] = rand.NextDouble();
				for (int c = 0; c < weights.GetLength(1); c++)
					weights[r, c] = rand.NextNormal();
			}
		}

		public double[] Predict(double[] input)
		{
			if (input.Length != weights.GetLength(1))
				throw new ArgumentException("Requires an input with length of " + weights.GetLength(1) + ", but got a length of " + input.Length);
			double[] result = new double[biases.Length];
			for (int r = 0; r < weights.GetLength(0); r++)
				for (int c = 0; c < weights.GetLength(1); c++)
					result[r] = weights[r,c] * input[c] + biases[r];
			return result;
		}

		public override string ToString()
		{
			string ret = "";
			for (int i = 0; i < weights.GetLength(0); i++) {
				ret += "[" + weights[i,0].ToString();
				for (int j = 1; j < weights.GetLength(1); j++)
					ret += ", " + weights[i,j].ToString();
				ret += "] | " + biases[i].ToString() + "\n";
			}
			return ret;
		}
	}
}
