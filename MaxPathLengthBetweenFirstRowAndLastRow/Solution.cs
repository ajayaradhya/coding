using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    class Program
    {
        private static HashSet<Tuple<int,int>> visited = new HashSet<Tuple<int, int>>();

        public static void Main(string[] args)
        {
            int m=4, n=5;
            int[][] matrix = new int[m][];
            matrix[0] = new int[] {0, 1, 1, 0, 0};
            matrix[1] = new int[] {0, 1, 0, 0, 0};
            matrix[2] = new int[] {0, 1, 1, 1, 1};
            matrix[3] = new int[] {1, 1, 1, 1, 1};

            int maxPath = Int32.MinValue;
            for(int i=0; i<n; i++)
            {
                if(matrix[0][i] == 1)
                {
                    visited.Add(Tuple.Create(0,i));
                    var currentPath = FindMaxPath(matrix, 0, i);
                    maxPath = Math.Max(currentPath, maxPath);
                    Console.WriteLine("[{0},{1}] has {2} path", 0, i, currentPath);
                    visited.RemoveWhere((tuple) => tuple.Item1 == 0 && tuple.Item2 == i);
                }
            }

            maxPath = Math.Max(maxPath, 0);

            Console.WriteLine("maxPath : {0}", maxPath);
        }

        private static int FindMaxPath(int[][] matrix, int row, int col)
        {
            var children = GetChildren(matrix, row, col);

            //checking if we have reached the last row
            if(row == matrix.Length-1 && children.Count == 0)
            {
                return 1;
            }
            
            //checking if we have reached end of traversal and no remaining elements
            if(row < matrix.Length && children.Count == 0)
            {
                //using minvalue to track the paths which has not reached the last row
                return Int32.MinValue;
            }

            var maxPath = Int32.MinValue;
            foreach(var item in children)
            {
                visited.Add(item);
                var currentPathLength = FindMaxPath(matrix, item.Item1, item.Item2);
                var currentpath = 1 + (currentPathLength == Int32.MinValue ? Int32.MinValue : currentPathLength);
                maxPath = Math.Max(currentpath, maxPath);
                visited.RemoveWhere((tuple) => tuple.Item1 == item.Item1 && tuple.Item2 == item.Item2);
            }

            return maxPath;
        }

        private static List<Tuple<int,int>> GetChildren(int[][] image, int i, int j)
        {
            var children = new List<Tuple<int,int>>();
            
            //left
            if(i >= 0 && j-1>=0 && image[i][j-1] == 1 && !visited.Any((tuple) => tuple.Item1 == i && tuple.Item2 == j-1))
            {
                children.Add(Tuple.Create(i, j-1));
            }
            
            //bottom
            if(i+1 < image.Count() && j < image[0].Count() && image[i+1][j] == 1 && !visited.Any((tuple) => tuple.Item1 == i+1 && tuple.Item2 == j))
            {
                children.Add(Tuple.Create(i+1, j));
            }
            
            //right
            if(i < image.Count() && j+1 < image[0].Count() && image[i][j+1] == 1 && !visited.Any((tuple) => tuple.Item1 == i && tuple.Item2 == j+1))
            {
                children.Add(Tuple.Create(i, j+1));
            }
            
            return children;
        }
    }
}
