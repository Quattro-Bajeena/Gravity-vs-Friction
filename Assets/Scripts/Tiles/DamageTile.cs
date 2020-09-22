using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Tile", menuName = "Tiles/Damage Tile")]
public class DamageTile : CustomTile
{
	[SerializeField] float damagePerSecond;
	float lastTime = -1;

	public override void OnCollision(Character character)
	{
		float delta = 0;

		if (lastTime > 0)
			delta = Time.time - lastTime;

		lastTime = Time.time;

		character.Health.TakeDamage(damagePerSecond * Time.deltaTime);
	}
}
