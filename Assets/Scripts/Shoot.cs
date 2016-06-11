using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {


	public GameObject shoot;

	private float counter;

	public float shootRatio;


	public laserScript laserscript;

	// Use this for initialization
	void Start () {
	
		counter = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (!laserscript.spawning) {
			counter += Time.deltaTime;
			if (counter >= shootRatio) {
				Instantiate (shoot, transform.position, transform.rotation);
				counter = 0;
			}
		}

	}
}
