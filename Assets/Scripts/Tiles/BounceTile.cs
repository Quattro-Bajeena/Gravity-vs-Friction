﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Bounce Tile", menuName = "Tiles/Bounce Tile")]
public class BounceTile : CustomTile
{
	[SerializeField] float addedVelocity;
	[SerializeField] float velocityReflected; 

	public override void OnCollision(Character character, Tilemap tilemap, Vector3Int tilePos)
	{
		
		//ColliderDistance2D colliderDistance = hit.Distance(boxCollider);
		if( Mathf.Abs(character.Movement.VelocityUp) > 10)
		{
			character.Movement.VelocityUp = -1 * (character.Movement.Velocity.y * velocityReflected) + addedVelocity;
		}

		

	}
}
