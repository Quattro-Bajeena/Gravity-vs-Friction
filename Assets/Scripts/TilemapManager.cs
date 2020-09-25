using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    Tilemap tilemap;

    // Start is called before the first frame update
    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
		//BoundsInt bounds = tilemap.cellBounds;

		//for (int x = bounds.xMin; x < bounds.xMax; x++)
		//{
		//	for (int y = bounds.yMin; y < bounds.yMax; y++)
		//	{
		//		Vector3Int localPlace = (new Vector3Int(x, y, (int)tilemap.transform.position.y));
		//		Vector3 place = tilemap.CellToWorld(localPlace);
		//		if (tilemap.HasTile(localPlace))
		//		{
		//			CustomTile tile = tilemap.GetTile(localPlace) as CustomTile;
		//			if (tile)
		//			{

		//				if (tile.ToBeDestroyed == true)
		//				{
		//					Debug.Log("tile destroyed");
		//					tilemap.SetTile(localPlace, null);
		//				}
		//			}

		//		}
		//		else
		//		{
		//			//No tile at "place"
		//		}
		//	}
		//}
	}
}
