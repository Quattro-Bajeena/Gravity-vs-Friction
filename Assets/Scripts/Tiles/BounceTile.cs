using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bounce Tile", menuName = "Tiles/Bounce Tile")]
public class BounceTile : CustomTile
{
	[SerializeField] float addedVelocity;
	[SerializeField] float velocityReflected; 

	public override void OnCollision(Character character)
	{

		character.Movement.AddVelocityUp(Mathf.Abs(character.Movement.Velocity.y) * (1f + velocityReflected) + addedVelocity);
		
	}
}
