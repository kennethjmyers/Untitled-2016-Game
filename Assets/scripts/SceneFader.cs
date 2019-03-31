using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class SceneFader : MonoBehaviour {

    public float fadeTime = 1.0f;

    void Start ()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator FadeToBlack (float startTime)
    {
        while (Time.time < startTime + fadeTime)
        {
            gameObject.GetComponent<Image>().color = Color.Lerp(gameObject.GetComponent<Image>().color, 
                Color.black, (Time.time - startTime) / fadeTime);
            yield return null;
        }
        gameObject.GetComponent<Image>().color = Color.black;
    }

    public IEnumerator FadeToClear(float startTime)
    {
        while (Time.time < startTime + fadeTime)
        {
            gameObject.GetComponent<Image>().color = Color.Lerp(gameObject.GetComponent<Image>().color,
                Color.clear, (Time.time - startTime) / fadeTime);
            yield return null;
        }
        gameObject.GetComponent<Image>().color = Color.clear;
    }

}
