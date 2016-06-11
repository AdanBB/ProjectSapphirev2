using UnityEngine;
using System.Collections;

public class EnemyDetectionSkull : MonoBehaviour {

	public EnemyAI4 enemyAi4;

	void OnTriggerEnter(Collider other)
	{




	}
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") {
			enemyAi4.FollowPlayer ();
			Debug.Log ("dsad");
		}



	}
	void OnTriggerExit(Collider other)
	{

	}
}
