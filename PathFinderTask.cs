using System;
using System.Drawing;


namespace RoutePlanning;

public static class PathFinderTask
{

    private static double bestLength;

    public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
    {
        var bestOrder = MakeTrivialPermutation(checkpoints.Length);
        var permutation = new int[bestOrder.Length];
        bestLength = double.MaxValue;
        MakePermutations(permutation, 0, checkpoints, bestOrder);
        return bestOrder;
    }

    static void Evaluate(int[] permutation, Point[] checkpoints, int[] bestOrder)
    {
        double length = 0;
        for (int i = 1; i < permutation.Length; i++)
        {
            length += checkpoints[permutation[i - 1]].DistanceTo(checkpoints[permutation[i]]);
            if (length >= bestLength)
                return;
        }
        if (bestLength > length)
        {
            bestLength = length;
            for (var i = 0; i < bestOrder.Length; i++)
            {
                bestOrder[i] = permutation[i];
            }
        }
        else return;
        return;
    }

    static void MakePermutations(int[] permutation, int position, Point[] checkpoints, int[] bestOrder)
    {

        if (position == permutation.Length)
        {
            Evaluate(permutation, checkpoints, bestOrder);
            return;
        }

        for (int i = 0; i < permutation.Length; i++)
        {

            var index = Array.IndexOf(permutation, i, 0, position);
            if (index != -1)
                continue;
            permutation[position] = i;
            if (permutation[0] != 0)
                break;
            MakePermutations(permutation, position + 1, checkpoints, bestOrder);
        }
    }

    private static int[] MakeTrivialPermutation(int size)
    {
        var bestOrder = new int[size];
        for (var i = 0; i < bestOrder.Length; i++)
            bestOrder[i] = i;
        return bestOrder;
    }
}


