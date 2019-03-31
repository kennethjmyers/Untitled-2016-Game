using UnityEngine;
using System.Collections;

public class InventoryDisplayManager : MonoBehaviour {

    public GameObject inventoryDisplay;
    public bool isPaused;
    public bool lastState;

    InventoryController inventoryController;

	// Use this for initialization
	void Start () {
        inventoryDisplay = GameObject.Find("Inventory Menu");
        isPaused = false;
        lastState = false;
        inventoryController = GameObject.Find("Inventory").GetComponent<InventoryController>();
	}
	
	// Update is called once per frame
	void Update () {
        PauseGame(isPaused);

        if (Input.GetButtonDown("Cancel") || Input.GetKeyDown("e"))
        {
            isPaused = !isPaused; //switch if game is paused or not
        }
    }

    void PauseGame(bool state)
    {
        inventoryDisplay.SetActive(state);
        if (state)
        {
            Time.timeScale = 0.0f; // Paused
            if (state != lastState) // only called when menu first active, not on every frame
            {
                inventoryController.displayItemCategory(InventoryController.lastCat);
            }
        }
        else
        {
            Time.timeScale = 1.0f; // Unpaused  
        }
        lastState = state;
        
    }
}
