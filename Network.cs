using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
	public class Network
	{
		private readonly Layer[] layers;

		public Network(int[] layerSizes)
		{
			layers = new Layer[layerSizes.Length - 1];
			for (int i = 0; i < layers.Length; i++)
				layers[i] = new Layer(layerSizes[i], layerSizes[i + 1]);
		}

		public double[] Predict(double[] input)
		{
			foreach (Layer layer in layers)
				input = layer.Predict(input);
			return input;
		}

		public void Train(List<double[]> inputs, List<double[]> outputs)
		{
			foreach (Layer layer in layers)
				layer.BeginTraining();
			for (int i = 0; i < inputs.Count; i++) {
				double[] da = Cost(Predict(inputs[i]), outputs[i]);
				foreach (Layer layer in layers)
					da = layer.Train(da);
			}
			foreach (Layer layer in layers)
				layer.EndTraining(inputs.Count);
		}
		private double[] Cost(double[] results, double[] output)
		{
			for (int i = 0; i < results.Length; i++)
				results[i] = 2 * (results[i] - output[i]);
			return results;
		}

		public override string ToString()
		{
			string ret = "";
			for (int i = 0; i < layers.Length; i++)
				ret += layers[i].ToString() + "\n";
			return ret;
		}
		
		
		private class Layer
		{
			private double[,] weights, dw;
			public double[,] Weights { get { return weights; } }

			private double[] biases, db;
			public double[] Biases { get { return biases; } }

			private bool training;
			private double[] input, result;

			public Layer(int preSize, int size)
			{
				RandomExt rand = new RandomExt();
				training = false;
				weights = new double[size, preSize];
				biases = new double[size];
				for (int r = 0; r < weights.GetLength(0); r++) {
					biases[r] = rand.NextDouble();
					for (int c = 0; c < weights.GetLength(1); c++)
						weights[r, c] = rand.NextNormal() / size;
				}
				//Console.WriteLine(Program.PrintArray<double>(weights));
			}

			public double[] Predict(double[] input)
			{
				if (input.Length != weights.GetLength(1))
					throw new ArgumentException("Requires an input with length of " + weights.GetLength(1) + ", but got a length of " + input.Length);
				double[] result = new double[biases.Length];
				for (int r = 0; r < weights.GetLength(0); r++) {
					for (int c = 0; c < weights.GetLength(1); c++)
						result[r] += weights[r,c] * input[c];
					result[r] = Math.Tanh(result[r] + biases[r]);
				}
				if (training) {
					this.input = input;
					this.result = result;
				}
				//Console.WriteLine(Program.PrintArray<double>(result));
				return result;
			}

			public void BeginTraining()
			{
				training = true;
				db = new double[biases.Length];
				dw = new double[weights.GetLength(0),weights.GetLength(1)];
			}
			public double[] Train(double[] cost)
			{
				double[] da = new double[weights.GetLength(1)],
						 dz = new double[biases.Length];
				for (int r = 0; r < dz.Length; r++) {
					dz[r] = (1 - Math.Pow(result[r], 2)) * cost[r];
					db[r] += dz[r];
					for (int c = 0; c < dw.GetLength(1); c++) {
						dw[r,c] += input[c] * dz[r];
						da[c] += weights[r, c] * dz[r];
					}
				}
				return da;
			}
			public void EndTraining(int cases)
			{
				for (int r = 0; r < dw.GetLength(0); r++) {
					biases[r] += db[r]/cases;
					for (int c = 0; c < dw.GetLength(1); c++)
						weights[r,c] += dw[r,c]/cases;
				}
				db = input = result = null;
				dw = null;
				training = false;
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
}
