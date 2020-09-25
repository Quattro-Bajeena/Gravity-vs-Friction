using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject UIPrefab;

    [SerializeField] Transform startingPoint;
    

    UIManager uiManager;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Color gravityModeColor;
    [SerializeField] Color frictionModeColor;
    Color defaultColor;

    [SerializeField] SpriteRenderer background;
    [SerializeField] float targetTime;

    Transform _lastCheckpoint;
    Transform LastCheckpoint
	{
		get { return _lastCheckpoint; }
		set
		{
            _lastCheckpoint = value;
            visitedCheckpoints.Add(value);
		}
	}
    List<Transform> visitedCheckpoints = new List<Transform>();

    Character character;

    int levelNumber;
    float timer;
    bool timerActive = false;
    


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
        

        levelNumber = int.Parse(SceneManager.GetActiveScene().name.Split(' ')[1]);
        Debug.Log("Current Level number: " + levelNumber);

        timer = targetTime;

        LastCheckpoint = startingPoint;
	}

	void Start()
    {
        QualitySettings.vSyncCount = 1;
        //Application.targetFrameRate = 60;

        

        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0 && timerActive == true)
            timer -= Time.deltaTime;
        if(timer <= 0)
            TimeEnded();

        uiManager.UpdateTimer(timer);


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
        Time.timeScale = 0;
        StartCoroutine(StartLevelCoroutine());
    }

    IEnumerator StartLevelCoroutine()
	{
        yield return new WaitForSecondsRealtime(1);
        state = GameState.Playing;
        virtualCamera.Follow = character.transform;
        timerActive = true;
        uiManager.StartLevel();
        Time.timeScale = 1;
    }


    public void CompleteLevel()
	{
        
        state = GameState.LevelCompleted;
        timerActive = false;
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
        character.transform.position = LastCheckpoint.position;
        state = GameState.Playing;
        uiManager.StartLevel();
	}

    void NextLevel()
	{
        SceneLoader.LoadLevel(levelNumber + 1);
	}

    void TimeEnded()
	{
        timer = 0;
        uiManager.TimeEnded();

	}

    public void CheckpointReached(Transform newCheckpoint)
	{
        if(visitedCheckpoints.Contains(newCheckpoint) == false )
		{
            LastCheckpoint = newCheckpoint;
            uiManager.CheckpointReached();
        }
        
	}
}
