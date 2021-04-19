using System;

public class Network
{
	private Layer[] layers;

	public Network(int[] layerSizes)
	{
		layers = new Layer[layerSizes.Length - 1];
		for (int i = 0; i < layers.Length; i++)
			layers[i] = new Layer(layerSizes[i], layerSizes[i + 1]);
	}
}

public class Layer
{
	private double[,] weights;
	public double[,] Weights { get { return weights; } }

	private double[] biases;
	public double[] Biases { get { return biases; } }

	public Layer(int preSize, int size)
	{
		weights = new double[size, preSize];
		biases = new double[size];
		for (int r = 0; r < weights.GetLength(0); r++)
		{
			biases[r] = 1;//random number (0,1]
			for (int c = 0; c < weights.GetLength(1); c++)
				weights[r,c] = 1;//generate random numbers on normal distribution
		}
	}
}
