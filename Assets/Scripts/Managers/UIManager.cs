using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject startText;
    [SerializeField] GameObject restartText;
    [SerializeField] GameObject nextLevelText;
    [SerializeField] GameObject timeEndedText;
    [SerializeField] GameObject checkpointText;
    [SerializeField] TextMeshProUGUI timerText;
    

    void Awake()
    {
        if (startText == null)
            startText = GameObject.Find("StartText");
        if (restartText == null)
            restartText = GameObject.Find("RestartText");
        if (nextLevelText == null)
            nextLevelText = GameObject.Find("NextLevelText");
        if (timeEndedText == null)
            timeEndedText = GameObject.Find("TimeEndedText");
        if (checkpointText == null)
            checkpointText = GameObject.Find("CheckpointText");


        startText.SetActive(false);
        restartText.SetActive(false);
        nextLevelText.SetActive(false);
        timeEndedText.SetActive(false);
        checkpointText.SetActive(false);
    }


    public void StartLevel()
	{
        //startText.SetActive(false);
        restartText.SetActive(false);
        nextLevelText.SetActive(false);
    }

    public void CharacterDied()
	{
        //startText.SetActive(false);
        restartText.SetActive(true);
        nextLevelText.SetActive(false);
    }


    public void CompleteLevel()
	{
        //startText.SetActive(false);
        restartText.SetActive(false);
        nextLevelText.SetActive(true);
    }


    public void TimeEnded()
	{
        timeEndedText.SetActive(true);
	}

    public void UpdateTimer(float timer)
	{
        timerText.text = Mathf.Round(timer).ToString();
	}

    
    public void CheckpointReached()
	{
        StartCoroutine(CheckpointReachedCoroutine());
	}

    IEnumerator CheckpointReachedCoroutine()
	{
        Vector3 orgPosition = checkpointText.transform.position;
        float timeShown = 3f;
        float timer = 0;
        checkpointText.SetActive(true);
        while(timer < timeShown)
		{
            checkpointText.transform.Translate( new Vector2(0, 20 * Time.deltaTime));
            timer += Time.deltaTime;
            yield return null;
		}
        checkpointText.SetActive(false);
        checkpointText.transform.position = orgPosition;
    }

    void Update()
    {
        
    }


}
