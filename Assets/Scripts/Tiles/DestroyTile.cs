using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Destroy Tile", menuName = "Tiles/Destroy Tile")]
public class DestroyTile : CustomTile
{
	[SerializeField] float velocityThreshold;

	public override void OnCollision(Character character, Tilemap tilemap, Vector3Int tilePos)
	{
		if(character.Movement.Velocity.magnitude > velocityThreshold)
		{
			tilemap.SetTile(tilePos, null);
		}
		
	}
}
