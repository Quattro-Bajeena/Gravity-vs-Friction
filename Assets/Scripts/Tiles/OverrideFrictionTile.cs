using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Override Friction Tile", menuName = "Tiles/Override Friction Tile")]
public class OverrideFrictionTile : CustomTile
{
	[SerializeField] float frictionMultiplier;
	

	public override void OnCollision(Character character, Tilemap tilemap, Vector3Int tilePos)
	{
		
		character.Movement.FrictionMultiplier = frictionMultiplier;
	}
}

