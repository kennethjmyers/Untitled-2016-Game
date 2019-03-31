using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Consider not making doors clickable and instead just use the exit controller for all entry/exits

public class DoorwayController : MonoBehaviour {

    public string sceneName;
    public Vector3 playerEnterPosition;
    public Vector3 cameraEnterPosition;

    private bool action;
    private GameObject player;
    private GameObject mainCamera;
    private GameObject sceneFade;
    private SceneFader sceneFader;

    void Awake ()
    {
        sceneFader = GameObject.Find("SceneFade").GetComponent<SceneFader>();
    }

    void Start ()
    {
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");
        

        cameraEnterPosition = playerEnterPosition;
        cameraEnterPosition.z = -10;
    }
	
    IEnumerator OnTriggerStay2D(Collider2D other)
    {
        action = Input.GetButtonUp("Fire2");

        if (action && other.CompareTag("PlayerActionBox"))
        {
            //stop time, fadetoblack,  move character and camera, load scene, fadetoclear resume time
            sceneFader.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            yield return StartCoroutine(sceneFader.FadeToBlack(Time.time));
            print("should be black");
            player.transform.position = playerEnterPosition;
            mainCamera.transform.position = cameraEnterPosition;
            print("moved player and camera");
            SceneManager.LoadScene(sceneName);
            print("scene loaded");
        }
    }

}
