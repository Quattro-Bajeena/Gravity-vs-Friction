using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject UIPrefab;

    [SerializeField] GameObject characterPrefab;
    [SerializeField] Transform startingPoint;

    UIManager uiManager;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Color gravityModeColor;
    [SerializeField] Color frictionModeColor;
    Color defaultColor;

    [SerializeField] SpriteRenderer background;

    Character character;

    int levelNumber;


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
        GameObject ui = Instantiate(UIPrefab);
        uiManager = ui.GetComponent<UIManager>();

        if (virtualCamera == null)
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        if (background == null)
            background = GameObject.Find("Background").GetComponent<SpriteRenderer>();

        if (startingPoint == null)
            startingPoint = GameObject.Find("Starting Point").transform;


        character = FindObjectOfType<Character>();

        defaultColor = background.color;
        state = GameState.Playing;

        levelNumber = int.Parse(SceneManager.GetActiveScene().name.Split(' ')[1]);
        Debug.Log("Current Level number: " + levelNumber);
	}

	void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        virtualCamera.Follow = character.transform;

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
        character.Movement.Restart();
        character.Health.RestoreHealth(100);
        character.transform.position = startingPoint.position;
        state = GameState.Playing;
        uiManager.StartLevel();
	}

    void NextLevel()
	{
        SceneLoader.LoadLevel(levelNumber + 1);
	}
}
