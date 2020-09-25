using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public abstract class CustomTile : Tile
{

    public abstract void OnCollision(Character character, Tilemap tilemap, Vector3Int tilePos);
}



