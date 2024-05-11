using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottonRandomNumberGeneratorV2.Extensions
{
    public static class ListExtensions
    {
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static List<int[][]> ChunkArray(this int[][] array, int chunkSize)
        {
            List<int[][]> chunks = new List<int[][]>();

            for (int i = 0; i < array.Length; i += chunkSize)
            {
                int remainingLength = array.Length - i;
                int actualChunkSize = Math.Min(chunkSize, remainingLength);

                int[][] chunk = new int[actualChunkSize][];
                Array.Copy(array, i, chunk, 0, actualChunkSize);
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}
