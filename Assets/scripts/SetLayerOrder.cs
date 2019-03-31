using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SetLayerOrder : MonoBehaviour {



    void Update()
    {
        GetComponent<Renderer>().sortingOrder = (int)(transform.position.y * -10);
    }

	//private SpriteRenderer sr;
	//[SerializeField]
	//private int layerOrder;

	// Use this for initialization
	//void Start () {
	//	sr = GetComponent<SpriteRenderer>();
	//	adjustLayerOrder();
	//}
	
	// Update is called once per frame
	//void LateUpdate () {
	//	adjustLayerOrder();
	//}

	//void adjustLayerOrder()
	//{
	//	if (transform.tag == "Player")
	//		layerOrder = -1*(int)Mathf.Floor(transform.root.position.y);
	//	else 
	//		layerOrder = -1*(int)Mathf.Floor(transform.position.y);
	//	sr.sortingOrder = layerOrder;
	//}
}
