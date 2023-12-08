using UnityEngine;

public class MapTerrainDisplayer : MonoBehaviour
{
    private HeightMapGenerator mapGenerator;
    private Terrain terrain;
    void Start()
    {
        Generate();
    }
    [ContextMenu("Generate")]
    private void Generate()
    {
        if (!TryGetComponent(out terrain))
        {
            Debug.LogError("This script must be assigned to a terrain asset");
            return;
        }
        if (!TryGetComponent(out mapGenerator))
        {
            Debug.Log("No map generator found. Adding one.");
            mapGenerator = gameObject.AddComponent<HeightMapGenerator>();
        }


        float[,] map = mapGenerator.GenerateMap();
        terrain.terrainData.heightmapResolution = map.GetLength(0);
        terrain.terrainData.SetHeights(0, 0, map);
    }
}
