using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjective : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

	private void Awake()
	{
		if (gameManager == null)
			gameManager = FindObjectOfType<GameManager>();
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("TRigger");
		gameManager.CompleteLevel();
	}
}
