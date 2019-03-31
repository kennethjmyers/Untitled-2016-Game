using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour, IFollowPlayer {

	public float moveSpeed;
    public float minDist;

    private Transform target;
	private Vector3 dir;
	

	void Awake()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		minDist = 5.0f;
        moveSpeed = 1.5f;
	}

	void Start () {}
	
	void Update () {
        FollowPlayer();
	}

    public void FollowPlayer()
    {
        dir = target.position - transform.position;
        dir.z = 0.0f;

        //Move Towards Target
        if (dir.magnitude < minDist)
            transform.position += (dir).normalized * moveSpeed * Time.deltaTime;
    }
}
