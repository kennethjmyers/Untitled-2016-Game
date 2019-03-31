using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

	public int dmg ;

	// Use this for initialization
	void Awake () {
		dmg = 4;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.isTrigger == true && other.CompareTag("Enemy"))
		{
			//call damage function on the enemy that was attacked, used the weapons root position
			// (the player) as the position of the weapon so that the enemy will be sent away from
			// player rather than the sword
			other.gameObject.GetComponent<Enemy>().Damage(dmg, other.transform.position - transform.root.position);
		}
	}
}
