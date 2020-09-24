using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadingProgressBar : MonoBehaviour
{
	Slider slider;

	private void Awake()
	{
		slider = GetComponent<Slider>();
	}

	private void Update()
	{
		slider.value = SceneLoader.GetLoadingProgress();
	}

}
