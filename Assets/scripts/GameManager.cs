using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    public GameObject playerData;
    public GameObject canvas;
    public GameObject eventSystem;
	
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);


        //Create necessary game objects
        if (!GameObject.FindGameObjectWithTag("EventSystem")) //if statement not necessary but removes warning
        {
            GameObject eventSystemObject = Instantiate(eventSystem);
            eventSystemObject.name = "EventSystem";
        }

        if (!GameObject.FindGameObjectWithTag("PlayerData"))
        {
            GameObject playerDataObject = Instantiate(playerData);
            playerDataObject.name = "PlayerData";
        }

        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject playerObject = Instantiate(player);
            playerObject.name = "Player";
        }

        if (!GameObject.FindGameObjectWithTag("Canvas"))
        {
            GameObject canvasObject = Instantiate(canvas);
            canvasObject.name = "Canvas";
        }

        DontDestroyOnLoad(gameObject);

    }


	void Start () {
	
	}
	
	
	void Update () {
	
	}
}
