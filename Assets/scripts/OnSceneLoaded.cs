using UnityEngine;
using System.Collections;

public class OnSceneLoaded : MonoBehaviour {

    private SceneFader sceneFader;

    IEnumerator Start ()
    {
        sceneFader = GameObject.Find("SceneFade").GetComponent<SceneFader>();

        Time.timeScale = 1.0f;
        yield return StartCoroutine(sceneFader.FadeToClear(Time.time));
        sceneFader.gameObject.SetActive(false);
    }
	
}
