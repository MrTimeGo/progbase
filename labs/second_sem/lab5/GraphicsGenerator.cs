using System;
using ScottPlot;

namespace lab5
{
    static class GraphicsGenerator
    {
        public static void CreateGraphics(string filePath, string[] labels, float[] values)
        {
            Plot plot = new Plot(600, 400);

            double[] ys = ConvertToDoubleArray(values);
            double[] xs = new double[labels.Length];
            FillNatural(xs);

            plot.PlotBar(xs, ys, horizontal: true);

            plot.YTicks(xs, labels);

            plot.SaveFig(filePath);
        }
        private static void FillNatural(double[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }
        }
        private static double[] ConvertToDoubleArray(float[] array)
        {
            double[] doubleArray = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                doubleArray[i] = array[i];
            }
            return doubleArray;
        }
    }
}
