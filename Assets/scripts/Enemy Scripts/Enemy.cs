using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour, IDamagable, IKillable, IPushable, IDroppable
{

    public int health;
    public int dmgToPlayer;
    //slightly less than players slash rate for continuous attack
    public float timeBetweenDmg = 0.32f;
    //distance this object can be pushed back
    public float pushBackDistance = 0.0f;
    public GameObject droppedItemPrefab;

    [SerializeField]
    private float lastAttackTime = 0.0f;
    private Vector3 dir;
    private Vector3 bumpTarget;
    private float startTime = 0.0f;
    private float overTime = 0.450f;

    private ItemDatabase itemDatabase;

    // Use this for initialization
    void Awake()
    {
        dmgToPlayer = 3;
        health = 5;
        // force is how forcefully we will push the player away from the enemy.
        pushBackDistance = 2.5f;
    }

    void Start()
    {
        itemDatabase = GameObject.Find("Inventory").GetComponent<ItemDatabase>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //attack player on contact
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitBox"))
        {
            other.gameObject.transform.root.GetComponent<PlayerHealth>().Damage(dmgToPlayer, other.transform.position - transform.position);
        }
    }

    //continue to attack the player if colliders are overlapped
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitBox"))
        {
            other.gameObject.transform.root.GetComponent<PlayerHealth>().Damage(dmgToPlayer, other.transform.position - transform.position);
        }
    }


    #region IDamagable implementation
    public void Damage(int dmg, Vector3 dir)
    {
        if (Time.time >= lastAttackTime + timeBetweenDmg)
        {
            health -= dmg;
            lastAttackTime = Time.time;
            dir = dir.normalized * pushBackDistance;
            bumpTarget = transform.position + dir;
            startTime = Time.time;
            StartCoroutine(PushBack(startTime, bumpTarget));
        }
    }
    #endregion

    #region IPushable implementation
    public IEnumerator PushBack(float startTime, Vector3 destination)
    {
        if (health <= 0)
            DropItems(); //drop items before being knocked back

        while (Time.time < startTime + overTime)
        {
            //sqrt in interpolation to speed up initial movement, then slow down
            transform.position = Vector3.Lerp(transform.position, destination, Mathf.Sqrt((Time.time - startTime) / overTime));
            yield return null;
        }
        transform.position = destination;
        if (health <= 0)
            Kill();
    }
    #endregion

    #region IKillable implementation
    public void Kill()
    {
        dmgToPlayer = 0;
        gameObject.SetActive(false);
    }
    #endregion

    #region IDroppable implementation
    public void DropItems()
    {
        int[] droppables = { 0, 1, 2, 3, 4 };
        int chosen = UnityEngine.Random.Range(0, droppables.Length);
        GameObject droppedItem = (GameObject)Instantiate(droppedItemPrefab, transform.position, transform.rotation);
        droppedItem.GetComponent<DroppedItem>().ThisItem = itemDatabase.FetchItemByID(chosen);

        droppedItem.GetComponent<DroppedItem>().SetSprite();
    }
    #endregion

}
