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

    void Awake()
    {
        if (startText == null)
            startText = transform.Find("StartText").gameObject;
        if (restartText == null)
            restartText = transform.Find("RestartText").gameObject;
        if (nextLevelText == null)
            nextLevelText = transform.Find("NextLevelText").gameObject;


        startText.SetActive(true);
        restartText.SetActive(false);
        nextLevelText.SetActive(false);
    }


    public void StartLevel()
	{
        startText.SetActive(false);
        restartText.SetActive(false);
        nextLevelText.SetActive(false);
    }

    public void CharacterDied()
	{
        startText.SetActive(false);
        restartText.SetActive(true);
        nextLevelText.SetActive(false);
    }


    public void CompleteLevel()
	{
        startText.SetActive(false);
        restartText.SetActive(false);
        nextLevelText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
