using UnityEngine;

public static class TerrainGenerator
{
    public static BlocksType[,,] GenerateTerrain(int offsetX, int offsetZ)
    {
        BlocksType[,,] result = new BlocksType[ChunkRenderer.ChunkWidth, ChunkRenderer.ChunkHeight, ChunkRenderer.ChunkWidth];

        for (int x = 0; x < ChunkRenderer.ChunkWidth; x++)
        {
            for (int z = 0; z < ChunkRenderer.ChunkWidth; z++)
            {
                float height = Mathf.PerlinNoise((x + offsetX) * 0.2f, (z + offsetZ) * 0.2f) * 5 + 10;

                for (int y = 0; y < height; y++)
                {
                    result[x, y, z] = BlocksType.Grass;
                }
            }
        }

        return result;
    }
}