using System;
using UnityEngine;

public class MapDisplayer : MonoBehaviour
{


    private HeightMapGenerator mapGenerator;

    void Start()
    {
        Generate();
    }
    [ContextMenu("Generate")]
    private void Generate()
    {
        if (!TryGetComponent(out mapGenerator))
        {
            Debug.Log("No map generator found. Adding one.");
            mapGenerator = gameObject.AddComponent<HeightMapGenerator>();
        }

        float[,] map = mapGenerator.GenerateMap();
        Texture2D mapTexture = new Texture2D(map.GetLength(0), map.GetLength(1));

        for (int x = 0; x < map.GetLength(0); x++)
            for (int y = 0; y < map.GetLength(1); y++)
                mapTexture.SetPixel(x, y, Color.white * map[x, y]);
        mapTexture.Apply();

        GetComponent<MeshRenderer>().sharedMaterial.mainTexture = mapTexture;
    }

}
