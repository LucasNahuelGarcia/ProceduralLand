using UnityEngine;

internal class HeightMapGenerator : MonoBehaviour
{
    [SerializeField] private float zoom = 100;
    [Range(0.01f, 0.99f)]
    [SerializeField] private float persistance = .7f;
    [SerializeField] private Vector2Int MapSize = Vector2Int.one * 100;

    [Space]
    [SerializeField] private Octave[] Octaves;

    public float[,] GenerateMap()
    {
        float peak = 0;
        float[,] Map = new float[MapSize.x, MapSize.y];
        int middleX = Mathf.RoundToInt(MapSize.x / 2);
        int middleY = Mathf.RoundToInt(MapSize.y / 2);
        float Zoom = zoom * MapSize.x;
        for (int x = 0; x < MapSize.x; x++)
            for (int y = 0; y < MapSize.y; y++)
            {
                float edgeClosenessX = GetEdgeCloseness(middleX, x);
                float edgeClosenessY = GetEdgeCloseness(middleY, y);
                float edgeCloseness = edgeClosenessX * edgeClosenessY;
                float res = GetNoiseAtPoint((x / Zoom), (y / Zoom));
                res *= edgeCloseness;
                Map[x, y] = res;
                if (Map[x, y] > peak) peak = Map[x, y];
            }

        for (int x = 0; x < MapSize.x; x++)
            for (int y = 0; y < MapSize.y; y++)
                Map[x, y] /= peak;


        return Map;
    }

    private float GetNoiseAtPoint(float x, float y)
    {
        float noise = 0;
        float lacunarity = 1;
        for (int i = 0; i < Octaves.Length; i++)
        {
            Octave octave = Octaves[i];
            float octaveNoise = Mathf.Abs(Mathf.PerlinNoise(x * octave.Detail, y * octave.Detail)) * lacunarity * octave.Strength;

            if (i != 0)
                octaveNoise *= noise;

            noise += octaveNoise;

            lacunarity *= persistance;
        }

        return noise;
    }
    private static float GetEdgeCloseness(int middleN, int x)
    {
        float n = middleN * 2;
        float edgeCloseness = 1 - (4 / (n * n)) * Mathf.Pow(x - n / 2, 2);
        return (edgeCloseness);
    }
}