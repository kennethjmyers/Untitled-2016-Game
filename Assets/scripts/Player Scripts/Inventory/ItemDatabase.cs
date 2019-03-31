using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> database = new List<Item>();
    private JsonData itemData;
	
    void Awake()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
    }

    public Item FetchItemByID(int id)
    {
        return database[id];  //json items must have same id as order they appear
    }

    void ConstructItemDatabase()
    {
        print(itemData.Count);
        for(var i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["name"].ToString(), itemData[i]["slug"].ToString(), 
                itemData[i]["itemType"].ToString(), (bool)itemData[i]["isStackable"], (bool)itemData[i]["isConsumable"], 
                itemData[i]["description"].ToString(), (int)itemData[i]["value"]));
        }    
    }
	
}

public class Item 
{

    public int ID { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string ItemType { get; set; }
    public bool IsStackable { get; set; }
    public bool IsConsumable { get; set; }
    public string Description { get; set; }
    public int Value { get; set; }

    public Item(int id, string name, string slug, string itemType, bool isStackable, bool isConsumable,
        string description, int value)
    {
        this.ID = id;
        this.Name = name;
        this.Slug = slug;
        this.ItemType = itemType;
        this.IsStackable = isStackable;
        this.IsConsumable = isConsumable;
        this.Description = description;
        this.Value = value;
        
    }

    public Item()
    {
        this.ID = -1;
    }

}
