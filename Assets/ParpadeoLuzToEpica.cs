using UnityEngine;
using System.Collections;

public class ParpadeoLuzToEpica : MonoBehaviour {

	public Light lightBoos;

	private bool active;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (lightBoos.intensity >= 8  ) {



			active = false;
		
		}
		if (lightBoos.intensity <= 4.25 ) {




			active = true;


		}

		if(active)lightBoos.intensity += Time.deltaTime% 10;

		if(!active)lightBoos.intensity -= Time.deltaTime% 10;
		
	

	}



}
