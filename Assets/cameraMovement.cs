using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {

	public float speed;


	public bool adelante;
	public bool atras;
	public bool derecha;
	public bool izquierda;
	public bool arriba;
	public bool abajo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		PremadeMovements ();
		Controls ();
	}

	public void PremadeMovements()
	{
		if (adelante) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + (Vector3.forward.z * Time.deltaTime * speed));
		}
		if (derecha) {
			transform.position = new Vector3 (transform.position.x + (Vector3.forward.z * Time.deltaTime * speed), transform.position.y, transform.position.z);
		}
		if (izquierda) {
			transform.position = new Vector3 (transform.position.x + (Vector3.forward.z * Time.deltaTime * speed * -1), transform.position.y, transform.position.z);
		}
		if (atras) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + (Vector3.forward.z * Time.deltaTime * speed * -1));
		}
		if (arriba) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + (Vector3.forward.z * Time.deltaTime * speed), transform.position.z);
		}
		if (abajo) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + (Vector3.forward.z * Time.deltaTime * speed * -1), transform.position.z);
		}
	}

	public void Controls()
	{
		if (Input.GetKey (KeyCode.A)) {

			transform.position = new Vector3 (transform.position.x + (Vector3.forward.z * Time.deltaTime * speed * -1), transform.position.y, transform.position.z);
			
		} else if (Input.GetKey (KeyCode.D)) {

			transform.position = new Vector3 (transform.position.x + (Vector3.forward.z * Time.deltaTime * speed), transform.position.y, transform.position.z);			
		}

		if (Input.GetKey (KeyCode.W)) {

			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + (Vector3.forward.z * Time.deltaTime * speed));

		} else if (Input.GetKey (KeyCode.S)) {

			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + (Vector3.forward.z * Time.deltaTime * speed * -1));			
		}

		if (Input.GetKey (KeyCode.Space)) {
			
			transform.position = new Vector3 (transform.position.x, transform.position.y + (Vector3.forward.z * Time.deltaTime * speed), transform.position.z);
		} else if (Input.GetKey (KeyCode.LeftShift)) {
		
			transform.position = new Vector3 (transform.position.x, transform.position.y + (Vector3.forward.z * Time.deltaTime * speed * -1), transform.position.z);
		}

	}
}
