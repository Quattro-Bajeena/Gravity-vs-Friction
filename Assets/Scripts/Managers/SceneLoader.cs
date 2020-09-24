using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
   
	private class LoadingMonoBehaviour : MonoBehaviour { }
	static AsyncOperation loadingAsyncOperation;

	public static void LoadLevel(int level)
	{
		string sceneName = "Level " + level;
		Debug.Log("Loading level " + sceneName);
		if (SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + sceneName + ".unity") >= 0)
		{
			
			GameObject loadingMonoBehaviourObject = new GameObject("Loading Scene Object");
			loadingMonoBehaviourObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(sceneName));
		}
		else Debug.LogWarning("There is no scene: " + sceneName);
		

	}

	public static float GetLoadingProgress()
	{
		if (loadingAsyncOperation != null)
		{
			return loadingAsyncOperation.progress;
		}
		else return 1f;
	}

	static IEnumerator LoadSceneAsync(string sceneName)
	{
		yield return null;
		loadingAsyncOperation = SceneManager.LoadSceneAsync(sceneName);
		if(loadingAsyncOperation == null)
		{
			Debug.Log("Tere is no scene: " + sceneName);
			yield break; 

		}


		while(loadingAsyncOperation.isDone == false)
		{
			yield return null;
		}

		loadingAsyncOperation = null;
	}
}
