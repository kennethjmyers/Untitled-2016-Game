using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance = null;

	private float Speed = 2.5f;

	private float movex = 0f;
	private float movey = 0f;
	private bool facingRight = false;

	private bool slash = false;
	private float slashRate = 0.33f;
	private float nextSlash = 0.0f;
	private bool isAttacking ;

    private GameObject actionBox;

	private Animator anim;
	private string currentPrimaryDir;

	void Awake () 
	{
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        anim = GetComponent<Animator>();
		currentPrimaryDir = "";

		isAttacking = false;

        actionBox = GameObject.Find("actionbox");

        DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		//set direction to down so there is no confusion of a neutral stance
		anim.SetFloat("y",-1f);
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		movex = Input.GetAxisRaw ("Horizontal");
		movey = Input.GetAxisRaw ("Vertical");
		slash = Input.GetButton("Fire1");

		bool isWalking = (Mathf.Abs(movex) + Mathf.Abs(movey)) > 0;
		anim.SetBool("isWalking",isWalking);

		if (slash && Time.time > nextSlash) {
			isAttacking = true;
			StartCoroutine("Slash");
		}

		if (isWalking)
		{
			if ((currentPrimaryDir == "" && movex != 0) || (Mathf.Abs(movex) == 1 && movey == 0 && currentPrimaryDir == "y"))
			{
				currentPrimaryDir = "x";
				if (movex > 0)
					anim.SetFloat("x", -1*movex);
				else
					anim.SetFloat("x", movex);
				if (movey == 0)
					anim.SetFloat("y", movey);
			}
			else if ((currentPrimaryDir == "" && movey != 0) || (movex == 0 && Mathf.Abs(movey) == 1 && currentPrimaryDir == "x"))
			{
				currentPrimaryDir = "y";
				anim.SetFloat("y", movey);
				if (movex == 0) 
					anim.SetFloat("x",movex);
			}


			transform.position += new Vector3(movex, movey, 0).normalized*Time.deltaTime*Speed;

			if (movex > 0 && !facingRight)
			{
				Flip();
			}
			else if (movex < 0 && facingRight)
			{	
				Flip();
			}
		}
		else {
			currentPrimaryDir = "";
		}
	}

	void Flip()
	{
		// Switch the way the player is labelled as facing
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	IEnumerator Slash() {
		nextSlash = Time.time + slashRate;
		anim.SetBool("isAttacking", true);
		Speed *= 0.5f;

		while (isAttacking) {
			yield return null;
		}
	}

	void disableAttack()
	{
		anim.SetBool("isAttacking", false);
		Speed *= 2f;
	}

		
}
