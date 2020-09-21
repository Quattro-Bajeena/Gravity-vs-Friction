﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject characterPrefab;
    [SerializeField] Transform startingPoint;
    [SerializeField] CameraController cameraController;
    [SerializeField] UIManager uiManager;

    [SerializeField] Color gravityModeColor;
    [SerializeField] Color frictionModeColor;
    Color defaultColor;

    [SerializeField] SpriteRenderer background;

    Character character;

    public enum GameState
	{
        Starting,
        Playing,
        Dead,
        LevelCompleted
	}

    GameState state;

	private void Awake()
	{
        if (cameraController == null)
            cameraController = FindObjectOfType<CameraController>();
        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
        if (background == null)
            background = GameObject.Find("Background").GetComponent<SpriteRenderer>();

        defaultColor = background.color;
        state = GameState.Starting;
	}

	void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
		switch (state)
		{
			case GameState.Starting:
                if (Input.GetKeyDown(KeyCode.F))
                    StartLevel();
				break;
			case GameState.Playing:
				break;
			case GameState.Dead:
                if (Input.GetKeyDown(KeyCode.R))
                    RestartLevel();
				break;
			case GameState.LevelCompleted:
                if (Input.GetKeyDown(KeyCode.N))
                    NextLevel();
				break;
			default:
				break;
		}
		if (character)
		{
            if (character.Movement.frictionMode == true && character.Movement.gravityMode == false)
            {
                background.color = frictionModeColor;
            }
            else if (character.Movement.frictionMode == false && character.Movement.gravityMode == true)
            {
                background.color = gravityModeColor;
            }
            else
            {
                background.color = defaultColor;
            }

			if (character.IsDead)
			{
                CharacterDied();
			}
        }
        

    }

    void StartLevel()
	{
        GameObject _character = Instantiate(characterPrefab);
        _character.transform.position = startingPoint.position;
        character = _character.GetComponent<Character>();
        cameraController.AssignCharacter(character.transform);
        state = GameState.Playing;
        uiManager.StartLevel();
    }


    public void CompleteLevel()
	{
        Debug.Log("level completed");
        state = GameState.LevelCompleted;
        uiManager.CompleteLevel();
        
	}

    public void CharacterDied()
	{
        Debug.Log("Character died");
        state = GameState.Dead;
        uiManager.CharacterDied();
    }

    void RestartLevel()
	{
        character.GetComponent<CharacterHealth>().RestoreHealth(100);
        character.transform.position = startingPoint.position;
        state = GameState.Playing;
        uiManager.StartLevel();
	}

    void NextLevel()
	{

	}
}