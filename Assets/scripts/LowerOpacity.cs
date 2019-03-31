using UnityEngine;
using System.Collections;

public class LowerOpacity : MonoBehaviour {

	private SpriteRenderer spriteRend;

	void Start()
	{
		spriteRend = this.GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{	
		//Had errors from sword entering collider so made it for player only
		if (other.CompareTag("Player") && !other.CompareTag("Weapon"))
			spriteRend.material.SetColor("_Color", new Color(1f,1f,1f,0.5f));
	}

	void OnTriggerExit2D(Collider2D other)
	{
		spriteRend.material.SetColor("_Color", new Color(1f,1f,1f,1f));
	}
}
