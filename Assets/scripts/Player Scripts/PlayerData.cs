using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour {

    public static PlayerData instance = null;

    ItemDatabase itemDatabase;
    int totalItemsCount;
    public Dictionary<string, List<Item>> playerItems = new Dictionary<string, List<Item>>();
    //public int[] itemCounts; // a look up array to find out how much of something a character has

    //debugging test inventory
    public static List<int> weapons = new List<int>() { 30, 31, 32 };
    public static List<int> armor = new List<int>() { 17, 18, 19, 20, 21 };
    public static List<int> cons = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
    public static List<int> misc = new List<int>() { 12, 13, 14, 15, 16 };
    public static List<int> keys = new List<int>() { 27, 28, 29 };
    public List<List<int>> allItems = new List<List<int>>() { weapons, armor, cons, misc, keys};
    public int[] itemCounts;

    void Awake ()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //itemCounts = new int[] { 3, 4, 5, 6, 3, 5, 3, 1, 4, 3, 5, 1, 1, 1, 1, 1, 1, 2, 3, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 1, 1, 0, 0 };
        itemCounts = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 3, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 1, 1, 0, 0 };

    }

    void Start()
    {
        itemDatabase = GameObject.Find("Inventory").GetComponent<ItemDatabase>();

        print(cons[7]);
        totalItemsCount = itemDatabase.database.Count;
        //itemCounts = new int[totalItemsCount]; 

        
        //for (int i = 0; i < allItems.Count; i++)
        //{
        //    for (int j = 0; j < allItems[i].Count; j++)
        //    {
        //        itemCounts[allItems[i][j]] = 1; //setting the item counts to 1 for each item
        //    }
        //}
    }
        

 }
