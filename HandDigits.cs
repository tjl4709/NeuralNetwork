using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NeuralNetwork
{
    class HandDigits
    {
        private BinaryReader images, labels;
        private int[] idim, ldim;
        private Type itype, ltype;
        private Network network;

        public HandDigits()
        {
            images = new BinaryReader(File.OpenRead("..\\..\\train-images"));
            idim = ReadHeader(images, out itype);
            labels = new BinaryReader(File.OpenRead("..\\..\\train-labels"));
            ldim = ReadHeader(labels, out ltype);
            network = new Network(new int[] { 784, 800, 10});
        }

        private int[] ReadHeader(BinaryReader sr, out Type t)
        {
            sr.ReadBytes(2);
            switch (sr.ReadByte())
            {
                case 0x08:
                    t = typeof(byte);
                    break;
                case 0x09:
                    t = typeof(sbyte);
                    break;
                case 0x0B:
                    t = typeof(short);
                    break;
                case 0x0C:
                    t = typeof(int);
                    break;
                case 0x0D:
                    t = typeof(float);
                    break;
                default:
                    t = typeof(double);
                    break;
            }
            int[] dim = new int[sr.ReadByte()];
            for (int i = 0; i < dim.Length; i++)
                dim[i] = sr.ReadInt32();
            return dim;
        }
        private void ReadCase(out double[] input, out int r)
        {
            input = new double[784];
            for (int j = 0; j < 784; j++)
                input[j] = images.ReadByte() / 255.0;
            r = labels.ReadByte();
        }

        public void Train(int num)
        {
            List<double[]> inputs = new List<double[]>(), results = new List<double[]>();
            for (int i = 0; i < num && images.BaseStream.Position < images.BaseStream.Length && labels.BaseStream.Position < labels.BaseStream.Length; i++) {
                double[] result = new double[10];
                ReadCase(out double[] input, out int r);
                result[r] = 1;
                inputs.Add(input);
                results.Add(result);
            }
            network.Train(inputs, results);
        }
        public void Test(out int expected, out double[] actual)
        {
            if (images.BaseStream.Position >= images.BaseStream.Length || labels.BaseStream.Position >= labels.BaseStream.Length) {
                expected = -1;
                actual = null;
                return;
            }
            ReadCase(out double[] input, out expected);
            actual = network.Predict(input);
        }
    }
}
