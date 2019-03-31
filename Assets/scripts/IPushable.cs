using UnityEngine;
using System.Collections;

public interface IPushable {

	IEnumerator PushBack (float startTime, Vector3 destination);
}
