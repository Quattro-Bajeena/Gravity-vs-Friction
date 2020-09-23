using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject characterPrefab;
    [SerializeField] Transform startingPoint;

    [SerializeField] UIManager uiManager;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
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
        if (virtualCamera == null)
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
        if (background == null)
            background = GameObject.Find("Background").GetComponent<SpriteRenderer>();

        character = FindObjectOfType<Character>();

        defaultColor = background.color;
        state = GameState.Playing;
	}

	void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        StartLevel();
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
			switch (character.Movement.mode)
			{
				case CharacterController.MovementMode.Normal:
                    background.color = defaultColor;
                    break;
				case CharacterController.MovementMode.NoGravity:
                    background.color = gravityModeColor;
                    break;
				case CharacterController.MovementMode.NoFriction:
                    background.color = frictionModeColor;
                    break;
				default:
					break;
			}

			if (state != GameState.Dead && character.IsDead)
			{
                CharacterDied();
			}
        }
        

    }

    void StartLevel()
	{
        if(character == null)
		{
            GameObject _character = Instantiate(characterPrefab);
            _character.transform.position = startingPoint.position;
            character = _character.GetComponent<Character>();
        }
        
        
        state = GameState.Playing;
        uiManager.StartLevel();
    }


    public void CompleteLevel()
	{
        
        state = GameState.LevelCompleted;
        uiManager.CompleteLevel();
        
	}

    public void CharacterDied()
	{
        
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
