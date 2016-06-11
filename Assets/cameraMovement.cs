using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {

	public float speed;

	private float image;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + (Vector3.forward.z * Time.deltaTime * speed));
		Application.CaptureScreenshot("path" + image + ".png", 4);
		image++;
	}
}
