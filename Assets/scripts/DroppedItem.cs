using UnityEngine;
using System.Collections;
using System;

public class DroppedItem : MonoBehaviour, IFollowPlayer {

    public Item ThisItem { get; set; }

    private PlayerData playerData;

    //Player following variables
    public float moveSpeed;
    public float minDist;

    private Transform target;
    private Vector3 dir;
    private FloatBehavior floatBehavior;
    private bool isFollowing;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        minDist = 1.0f;
        moveSpeed = 1.3f;
        floatBehavior = gameObject.GetComponent<FloatBehavior>();
        isFollowing = false;

        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        FollowPlayer();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //player picks up item
        if (other.gameObject.transform.root.CompareTag("Player") && !other.CompareTag("Weapon"))
        {
            playerData.itemCounts[ThisItem.ID] += 1;
            gameObject.SetActive(false);
        }
    }

    public void SetSprite()
    {
        //Grab sprite, put this in a namespace because its also used in ItemSelect
        string imgSource = "";
        switch (ThisItem.ItemType)
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
            if (sprites[i].name == ThisItem.Slug)
            {
                sprite = sprites[i];
                break;
            }
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void FollowPlayer()
    {
        dir = target.position - transform.position;
        dir.z = 0.0f;

        //Move Towards Target
        if (dir.magnitude < minDist)
        {
            floatBehavior.enabled = false;
            transform.position += (dir).normalized * moveSpeed * Time.deltaTime;
            isFollowing = true;
        }
        else if (dir.magnitude >= minDist && isFollowing == true)
        {
            isFollowing = false;
            floatBehavior.originalY = transform.position.y;
            floatBehavior.enabled = true;
        }
        
    }
}
