using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Override Friction Tile", menuName = "Tiles/Override Friction Tile")]
public class OverrideFrictionTile : CustomTile
{
	[SerializeField] float frictionMultiplier;
	

	public override void OnCollision(Character character)
	{
		
		character.Movement.FrictionMultiplier = frictionMultiplier;
	}
}

