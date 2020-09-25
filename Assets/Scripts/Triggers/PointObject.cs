using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PointObject : MonoBehaviour
{
    [SerializeField] [Range(1, 100)]int addedPoints = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
		if (character)
		{
            character.Score.AddPoints(addedPoints);
            Destroy(gameObject);
		}
    }
}
