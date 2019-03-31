using UnityEngine;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour, IDamagable, IPushable {

	public int health;
	public float timeBetweenDmg = 0.5f;
	public float pushBackDistance;
    public LayerMask blockingLayer;

    // variables controlling player pushback when hit
    private BoxCollider2D moveBoxCollider;
    private float lastAttackedTime = 0.0f;
	private Vector3 dir;
	private Vector3 bumpTarget;
	private float startTime = 0.0f;
	private float overTime = 0.25f; //slightly less than time between attacks so player can recover


	void Awake()
	{
        //set moveBoxCollider and blockingLayer of that collider
        moveBoxCollider = gameObject.transform.FindChild("movebox").GetComponent<BoxCollider2D>();
        //blockingLayer = gameObject.transform.FindChild("movebox").gameObject.layer;


        health = 100;
		// how far to push the player away from the enemy.
		pushBackDistance = 1.0f;

	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IDamagable implementation
	public void Damage (int dmg, Vector3 dir)
	{
        if (Time.time >= lastAttackedTime + timeBetweenDmg)
        {
            health -= dmg;
            lastAttackedTime = Time.time;
            startTime = Time.time;

            //get the position that the vector should go to
            dir = dir.normalized * pushBackDistance;
            bumpTarget = transform.position + dir;

            //Vector2 curPosition2D = new Vector2(transform.position.x, transform.position.y);
            //Vector2 bumpTarget2D = new Vector2(bumpTarget.x, bumpTarget.y);

            moveBoxCollider.enabled = false; //disable move box so it isn't hit by raycast
            RaycastHit2D hit = Physics2D.Linecast(gameObject.transform.position, bumpTarget, blockingLayer.value);
            moveBoxCollider.enabled = true; //re enable

            //readjust the bumptarget based on the Linecasts hit distance
            if (hit && hit.distance < pushBackDistance)
            {
                //added division by 2 to decrease gap because cur position is from center of player
                Vector3 adjDir = dir.normalized * hit.distance/2; 
                bumpTarget = transform.position + adjDir;
            }

            StartCoroutine(PushBack(startTime,bumpTarget));
		}

	}
	#endregion

	#region IPushable implementation
	public IEnumerator PushBack (float startTime, Vector3 destination)
	{
		while (Time.time < startTime + overTime)
		{
			transform.position = Vector3.Lerp(transform.position, destination, Mathf.Sqrt((Time.time - startTime) / overTime));
			yield return null;
		}
		transform.position = destination;
	}
    #endregion
}
