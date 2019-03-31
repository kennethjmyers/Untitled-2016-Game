using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using System;


public class InventoryController : MonoBehaviour
{

    public static int lastCat = 0; //used for reloading the screen 

    InventoryDisplayManager inventoryDisplayManager;
    GameObject inventoryPanel;
    GameObject itemList;
    GameObject inventoryDisplay;
    ItemDatabase database;
    GameObject eventSystem;
    public GameObject inventorySlot;

    string[] buttonNames = new string[] { "Weapons", "Armor", "Cons", "Misc", "Key" };
    Navigation buttonNav;
    //Navigation slotNav;

    PlayerData playerData;

    public List<GameObject> slots = new List<GameObject>();
    public List<Item> items = new List<Item>();
    //going to duplicate the items of whatever the player has in this list
    //this list is what the ItemSelect script will look up connecting the panel name
    //to the index in the list

    public List<List<int>> allItems;
    public int[] itemCounts;


    void Awake()
    {
        inventoryDisplayManager = GameObject.Find("Inventory").GetComponent<InventoryDisplayManager>();
        database = GetComponent<ItemDatabase>();
        inventoryPanel = GameObject.Find("Inventory Panel");
        itemList = inventoryPanel.transform.FindChild("Item List").gameObject;
        inventoryDisplay = GameObject.Find("Inventory Panel");
        eventSystem = GameObject.Find("EventSystem");
        
    }

    void Start()
    {
        playerData = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();

        allItems = playerData.allItems;
        itemCounts = playerData.itemCounts;

        displayItemCategory(0);

        //attach the onclick listeners for the item button tabs
        int i = 0;
        foreach (string btn in buttonNames)
        {
            int param = i; //this is needed because if i is used then the reference changes with each iteration
            GameObject.Find(btn).GetComponent<Button>().onClick.AddListener(() => { displayItemCategory(param); });
            i++;
        }
    }

    void LateUpdate()
    {
        //Set buttons ondownselect to first item
        // had an error with finding buttons on first frame, thus the need to find button first
        if (inventoryDisplayManager.isPaused && GameObject.Find(buttonNames[0])) 
        {
            foreach (string btn in buttonNames)
            {
                buttonNav = GameObject.Find(btn).GetComponent<Button>().navigation;
                if (GameObject.Find("0"))
                    buttonNav.selectOnDown = GameObject.Find("0").GetComponent<Selectable>();
                else
                    buttonNav.selectOnDown = null;
                GameObject.Find(btn).GetComponent<Button>().navigation = buttonNav;
            }
        }
    }

    public void displayItemCategory(int cat)
    {
        itemCounts = playerData.itemCounts;
        //destroy objects in slots
        foreach (Transform child in itemList.transform)
        {
            DestroyObject(child.gameObject);
        }
        slots.Clear();
        items.Clear(); //clear current items list

        int itemsInst = 0; //number of items instantiated
        for (int i = 0; i < allItems[cat].Count; i++)
        {
            if (itemCounts[allItems[cat][i]] > 0) //only proceed if item counts > 0
            {
                int freshObjCount = 1;
                if (itemCounts[allItems[cat][i]] > 1 && !database.FetchItemByID(allItems[cat][i]).IsConsumable)
                    freshObjCount = itemCounts[allItems[cat][i]];
                for (int j = 0; j < freshObjCount; j++)
                {
                    //add item to a list of items, this includes duplicates for all counts in the player inventory
                    //this seems like it could be a lot of data so come back to this some day and make it better
                    Item itemToAdd = database.FetchItemByID(allItems[cat][i]);
                    items.Add(itemToAdd);

                    //instantiate objects
                    slots.Add(Instantiate(inventorySlot));
                    slots[itemsInst].transform.SetParent(itemList.transform, false);
                    slots[itemsInst].gameObject.name = itemsInst.ToString();

                    //set ui values
                    slots[itemsInst].transform.FindChild("Item Text").GetComponent<Text>().text = items[itemsInst].Name;
                    if (items[itemsInst].IsStackable) //if stackable keep track of how many there are
                        slots[itemsInst].transform.FindChild("Item Count").GetComponent<Text>().text = itemCounts[items[itemsInst].ID].ToString();
                    else
                        slots[itemsInst].transform.FindChild("Item Count").GetComponent<Text>().text = "";

                    //increment amout of items instantiated
                    itemsInst++;
                }
            }
        }

        /*

        //Explicitly set the navigation of the items
        for (int i = 0; i < slots.Count; i++)
        {
            slotNav = itemList.transform.GetChild(i).GetComponent<Selectable>().navigation;
            //up/down navigation
            if (i == 0)
            {
                switch (cat)
                {
                    case 0:
                        slotNav.selectOnUp = GameObject.Find("Weapons").GetComponent<Button>();
                        break;
                    case 1:
                        slotNav.selectOnUp = GameObject.Find("Armor").GetComponent<Button>();
                        break;
                    case 2:
                        slotNav.selectOnUp = GameObject.Find("Cons").GetComponent<Button>();
                        break;
                    case 3:
                        slotNav.selectOnUp = GameObject.Find("Misc").GetComponent<Button>();
                        break;
                    case 4:
                        slotNav.selectOnUp = GameObject.Find("Key").GetComponent<Button>();
                        break;
                }
                slotNav.selectOnDown = itemList.transform.GetChild(i+1).GetComponent<Selectable>();
            }
            else if (i == (slots.Count - 1))
            {
                slotNav.selectOnUp = itemList.transform.GetChild(i-1).GetComponent<Selectable>();
            }
            else
            {
                slotNav.selectOnUp = itemList.transform.GetChild(i-1).GetComponent<Selectable>();
                slotNav.selectOnDown = itemList.transform.GetChild(i+1).GetComponent<Selectable>();
            }

            //left/right navigation
            switch (cat)
            {
                case 0:
                    slotNav.selectOnRight = GameObject.Find("Armor").GetComponent<Button>();
                    break;
                case 1:
                    slotNav.selectOnLeft = GameObject.Find("Weapons").GetComponent<Button>();
                    slotNav.selectOnRight = GameObject.Find("Cons").GetComponent<Button>();
                    break;
                case 2:
                    slotNav.selectOnLeft = GameObject.Find("Armor").GetComponent<Button>();
                    slotNav.selectOnRight = GameObject.Find("Misc").GetComponent<Button>();
                    break;
                case 3:
                    slotNav.selectOnLeft = GameObject.Find("Cons").GetComponent<Button>();
                    slotNav.selectOnRight = GameObject.Find("Key").GetComponent<Button>();
                    break;
                case 4:
                    slotNav.selectOnLeft = GameObject.Find("Misc").GetComponent<Button>();
                    break;
            }


            itemList.transform.GetChild(i).GetComponent<Selectable>().navigation = slotNav;   
        }

        */

        //sets selected item to first item, will need to change this for when inventory empty (tryexcept?)
        if (slots.Count > 0)
        {
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(slots[0]);
        }
        else
        {
            ItemSelect.ResetDisplay();
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find(buttonNames[cat]));
        }

        lastCat = cat; //set the lastCat to this cat
    }


}
