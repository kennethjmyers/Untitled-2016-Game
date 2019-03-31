using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSelect : MonoBehaviour, ISelectHandler
{
    //Consider adding tags to these elements in the ui because going to be Finding them a lot
    //GameObject itemDisplay;
    static GameObject itemImage;
    static GameObject itemName;
    static GameObject itemDescription;
    GameObject itemScrollbar;
    InventoryController inventory;

    void Awake()
    {
        //itemDisplay = GameObject.Find("ItemDisplay");
        
        
        inventory = GameObject.Find("Inventory").GetComponent<InventoryController>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        //I am not sure why I have to put these references here but I was getting 
        //A nullreferenceexception if they were placed in the awake
        //I guess when the instances of the objects changed, it was having trouble
        //linking the old reference to the new
        itemImage = GameObject.Find("ItemImage");
        itemName = GameObject.Find("ItemName");
        itemDescription = GameObject.Find("ItemDescription");
        itemScrollbar = GameObject.Find("ItemScrollbar");

        Item thisItem = inventory.items[int.Parse(this.gameObject.name)]; //name of each item should be the number in the items list
        string imgSource = "";
        switch (thisItem.ItemType)
        {
            case "armor":
                imgSource = "sprites/roguelikeitems";
                break;
            case "misc":
                imgSource = "sprites/roguelikeitems";
                break;
            case "key":
                imgSource = "sprites/roguelikeitems";
                break;
            case "cons":
                imgSource = "sprites/foodfromcts1a";
                break;
            case "weapon":
                imgSource = "sprites/swords";
                break;
        }
       
        Sprite[] sprites = Resources.LoadAll<Sprite>(imgSource);
        Sprite sprite = null;
        for (var i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == thisItem.Slug)
            {
                sprite = sprites[i];
                break;
            }
        }

        itemImage.GetComponent<Image>().sprite = sprite;
        itemName.GetComponent<Text>().text = thisItem.Name;
        itemDescription.GetComponent<Text>().text = thisItem.Description;

        //set the scrollbar position by selected item
        float itemTotal = (float)inventory.items.Count;
        itemScrollbar.GetComponent<Scrollbar>().value = 1.0f-(float.Parse(gameObject.name) / (itemTotal-1));


    }

    public static void ResetDisplay ()
    {
        itemImage = GameObject.Find("ItemImage");
        itemName = GameObject.Find("ItemName");
        itemDescription = GameObject.Find("ItemDescription");

        itemImage.GetComponent<Image>().sprite = null;
        itemName.GetComponent<Text>().text = "";
        itemDescription.GetComponent<Text>().text = "";
    }
}