using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="Death Tile", menuName ="Tiles/Death Tile")]
public class DeathTile : CustomTile
{
	public override void OnCollision(Character character, Tilemap tilemap, Vector3Int tilePos)
	{
		character.Health.Die();
	}
}

