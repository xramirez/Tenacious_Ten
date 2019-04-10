using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundManager : MonoBehaviour
{
    public Tilemap darkMap;
    public Tilemap blurredMap;
    public Tilemap backgroundMap;

    public Tile darkTile;
    public Tile blurredTile;
    // Start is called before the first frame update
    void Start()
    {
        darkMap.origin = blurredMap.origin = backgroundMap.origin;
        darkMap.size = blurredMap.size = backgroundMap.size;

        foreach(Vector3Int p in darkMap.cellBounds.allPositionsWithin)
        {
            darkMap.SetTile(p, darkTile);
        }

        foreach (Vector3Int p in blurredMap.cellBounds.allPositionsWithin)
        {
            blurredMap.SetTile(p, blurredTile);
        }
    }
}
