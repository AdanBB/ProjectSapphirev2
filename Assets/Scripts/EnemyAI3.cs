﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class EnemyAI3 : MonoBehaviour {

	public int health;
	private int _health;

	public float damage;
	private SphereCollider detectionCollider;
	public float detectionRange;

	public float velocity;

	public GameObject deathParticles;
	public GameObject explosionParticles;

	private GameObject player;
	private float range;
	private float _ramge;

	private Animator anim;
	private NavMeshAgent agent;

	public AudioClip enemyAttack;
	public AudioClip lastHitSound;
	private AudioSource enemySound;

	public Transform pointA;
	public Transform pointB;

	void Awake(){


		player = GameObject.FindGameObjectWithTag("Player");
		agent = GetComponent<NavMeshAgent> ();
		detectionCollider = GetComponentInChildren<SphereCollider> ();
		anim = GetComponent<Animator> ();
		enemySound = GetComponent<AudioSource> ();

	}

	// Use this for initialization
	void Start () {
		
		detectionCollider.radius = detectionRange;

		range = agent.stoppingDistance;
		agent.SetDestination (pointA.position);

	}
		
	// Update is called once per frame
	void Update () {

	

	
	}
	public void FollowPlayer()
	{

		transform.LookAt(new Vector3 (player.transform.position.x, transform.position.y , player.transform.position.z));
		agent.SetDestination (player.transform.position);
		anim.SetBool ("IsMoving", true);
		//Debug.Log (agent.stoppingDistance);

		if (Vector3.Distance (transform.position, player.transform.position) < detectionCollider.radius /1.4) {

			if (agent.baseOffset > 0.29) {
				agent.baseOffset -= Time.deltaTime * 2.2f;
			}
		}

		if (Vector3.Distance (transform.position, player.transform.position) < range) {
		
			//Debug.Log(Vector2.Distance (player.transform.position, this.transform.position));
			player.GetComponent<CharacterStats> ().ApplyDamage (damage);

            Instantiate(explosionParticles, transform.position, transform.localRotation);

			Destroy (gameObject);
		
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
			agent.enabled = false;

            Instantiate(explosionParticles, transform.position, transform.localRotation);

            Destroy (this.gameObject);
		
		}

	}
	public void DamagePlayer(){

		player.GetComponent<CharacterStats> ().ApplyDamage ((float)damage);
		enemySound.PlayOneShot (enemyAttack);

	}
	public void GotoPosition(int where){


		if (where == 1) {

			agent.SetDestination (pointA.position);

		}
		if (where == 2) {

			agent.SetDestination (pointB.position);

		}

	}
}
