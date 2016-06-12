using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class EnemyAI4 : MonoBehaviour {

	public int health;
	private int _health;

	public float damage;
	private SphereCollider detectionCollider;
	public float detectionRange;

	public float velocity;

	public GameObject deathParticles;
	public GameObject explosionParticles;

	public GameObject player;
	private float range;
	private float _ramge;

	private Animator anim;

	public AudioClip enemyAttack;
	public AudioClip lastHitSound;
	private AudioSource enemySound;

	private Rigidbody rb;

	public Transform pointA;
	public Transform pointB;

	void Awake(){


		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody> ();
		detectionCollider = GetComponentInChildren<SphereCollider> ();
		anim = GetComponent<Animator> ();
		enemySound = GetComponent<AudioSource> ();

	}

	// Use this for initialization
	void Start () {
		
		detectionCollider.radius = detectionRange;

		range = 2;


		transform.LookAt (player.transform);

		//rb.MovePosition (pointA.position);


	}
		
	// Update is called once per frame
	void Update () {

		transform.LookAt (player.transform);

	
	}
	public void FollowPlayer()
	{
		Debug.Log ("dsadasdsadasdas");
		//transform.LookAt(new Vector3 (player.transform.position.x, transform.position.y , player.transform.position.z));
		anim.SetBool("IsMoving",true);
		//rb.position = player.transform.position;

		rb.velocity =(player.transform.position -transform.position).normalized*10;
		//rb.MovePosition (transform.position + transform.forward * Time.deltaTime);

		anim.SetBool ("IsMoving", true);
		//Debug.Log (agent.stoppingDistance);

		if (Vector3.Distance (transform.position, player.transform.position) < detectionCollider.radius /1.4) {

			//rb.MovePosition (player.transform.position);
		}

		if (Vector3.Distance (transform.position, player.transform.position) < range + 1) {
		
			//Debug.Log(Vector2.Distance (player.transform.position, this.transform.position));
			player.GetComponent<CharacterStats> ().ApplyDamage (damage);

            Instantiate(explosionParticles, transform.position, transform.localRotation);

			Destroy (this.gameObject);
		
		}

	}


	public void AdDamage(int Damage, int color){


		health = health - Damage;

		if (health <= 0) {
			if (SceneManager.GetActiveScene().buildIndex == 6) {

				if (!GameObject.FindGameObjectWithTag ("Boss2").GetComponent<EnemyAiBos2> ().dead1) {

					GameObject.FindGameObjectWithTag ("Boss2").GetComponent<EnemyAiBos2> ().dead1 = true;
				}
				if (!GameObject.FindGameObjectWithTag ("Boss2").GetComponent<EnemyAiBos2> ().dead2) {

					GameObject.FindGameObjectWithTag ("Boss2").GetComponent<EnemyAiBos2> ().dead2 = true;

				}

			}


            Instantiate(explosionParticles, transform.position, transform.localRotation);

            Destroy (this.gameObject);
		
		}

	}
	public void DamagePlayer(){

		player.GetComponent<CharacterStats> ().ApplyDamage ((float)damage);
		enemySound.PlayOneShot (enemyAttack);

	}
	public void GotoPosition(int where){

		/*
		if (where == 1) {
			rb.MovePosition (pointA.position);


		}
		if (where == 2) {

			rb.MovePosition (pointB.position);

		}*/

	}
}
