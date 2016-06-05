using UnityEngine;
using System.Collections;

public class GodMode : MonoBehaviour {
   
	public bool isInvulnerable;
	private Rigidbody rb;
	private playerController pc;
	private float pastGravity;

	void Awake()
	{
		rb = GetComponent<Rigidbody> ();
		pc = GameObject.Find ("Direction").GetComponent<playerController> ();
	}


	void Start () {
		
        isInvulnerable = false;
		pastGravity = pc.gravity;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       
		if (Input.GetKeyDown(KeyCode.G))
        {
            isInvulnerable = !isInvulnerable;
        }

		if (isInvulnerable) {
			
			pc.gravity = 0;
			rb.useGravity = false;
			GodCommands ();

		} else if (!isInvulnerable) {

			pc.gravity = pastGravity;
			rb.useGravity = true;
		}

	}

	public void GodCommands()
	{
		if (Input.GetKey(KeyCode.LeftShift)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
		}
		else if (Input.GetKey(KeyCode.Space)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);
		}
        
		if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * 30;
        }
		else if (Input.GetKey(KeyCode.S))
		{
			transform.position += Vector3.back * Time.deltaTime * 30;
		}

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * 30;
        }
		else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * 30;
        }
    }
}
