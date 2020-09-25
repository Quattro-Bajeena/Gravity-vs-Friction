using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
	[SerializeField] GameManager gameManager;

	private void Awake()
	{
		if (gameManager == null)
			gameManager = FindObjectOfType<GameManager>();
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		gameManager.CheckpointReached(transform);
	}
}
