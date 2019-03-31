using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

		// The target we are following
		[SerializeField]
		private Transform target;

		[SerializeField]
		private float spaceDifference;
		[SerializeField]
		private float movementThreshold = 3;
		[SerializeField]
		private float speed = 2;
		private Vector3 moveTemp;

		void Awake()
		{
			
		}

		// Use this for initialization
		void Start()
        {
            target = GameObject.FindWithTag("Player").transform;
        }

		// Update is called once per frame
		void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target)
				return;

			//Calculate 2d magnitude of distance between character and camera
			spaceDifference = magnitude2d(target.position-transform.position);

			if (spaceDifference > movementThreshold)
			{
				// Set the position of the camera on the x-z plane to:
				// distance meters behind the target
				moveTemp = target.position;
				moveTemp.z = -10;
				transform.position = Vector3.MoveTowards(transform.position, moveTemp, speed*Time.deltaTime);
				
			}
		}

		public float magnitude2d(Vector3 v)
		{
			return Mathf.Sqrt(Mathf.Pow(v.x,2) + Mathf.Pow(v.y,2));
		}
	}
}