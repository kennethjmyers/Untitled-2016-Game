using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour {

    public string sceneName;
    public Vector3 playerExitPosition;
    public Vector3 cameraExitPosition;

    private GameObject player;
    private GameObject mainCamera;
    private SceneFader sceneFader;

    void Awake ()
    {
        sceneFader = GameObject.Find("SceneFade").GetComponent<SceneFader>();
    }

    void Start ()
    {
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");

        cameraExitPosition = playerExitPosition;
        cameraExitPosition.z = -10;
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //stop time, fadetoblack,  move character and camera, load scene, fadetoclear resume time
            sceneFader.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            yield return StartCoroutine(sceneFader.FadeToBlack(Time.time));
            print("should be black");
            player.transform.position = playerExitPosition;
            mainCamera.transform.position = cameraExitPosition;
            print("moved player and camera");
            SceneManager.LoadScene(sceneName);
            print("scene loaded");
        }
    }
}
