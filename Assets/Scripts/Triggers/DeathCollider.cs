using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
	

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Character character = collision.gameObject.GetComponent<Character>();
		if(character != null)
		{
			character.Health.Die();
		}
	}
}
