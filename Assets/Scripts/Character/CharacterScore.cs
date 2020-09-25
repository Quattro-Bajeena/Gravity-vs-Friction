using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScore : MonoBehaviour
{
    Character character;

    [SerializeField] int _score = 0;
    public int Score
    {
		get { return _score; }
    }

    void Awake()
    {
        character = GetComponent<Character>();
    }

   
    public void AddPoints(int points)
	{
        _score += points;
	}

    public void ResetPoints()
	{
        _score = 0;
	}
}
