using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Damage Tile", menuName = "Tiles/Damage Tile")]
public class DamageTile : CustomTile
{
	[SerializeField] float damagePerSecond;
	float lastTime = -1;

	public override void OnCollision(Character character, Tilemap tilemap, Vector3Int tilePos)
	{
		float delta = 0;

		if (lastTime > 0)
			delta = Time.time - lastTime;

		lastTime = Time.time;

		character.Health.TakeDamage(damagePerSecond * Time.deltaTime);
	}
}
