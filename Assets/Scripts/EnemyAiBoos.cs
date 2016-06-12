using UnityEngine;
using System.Collections;

public class EnemyAiBoos : MonoBehaviour {

	public int health;
	private int _health;

	public EnemyAiBos2 enemyAi2;

	public GameObject particles;


	private bool damaged;
	private float countedDamaged;
	private float timeDamaged;

	public Color damagedTexture;
	public Color NornalTexture;
	public Renderer rend;

	// Use this for initialization
	void Start () {
		damaged = false;
		countedDamaged = 0;
		timeDamaged = 0.25f;

	}
	void Update(){

		if (damaged) {

			countedDamaged += Time.deltaTime;

			Debug.Log ("dsa");
		
		}
		if(countedDamaged >= timeDamaged){

			rend.material.color = NornalTexture;
			Debug.Log("lel");
			countedDamaged = 0;
			damaged = false;
		}


	}


	public void AdDamage(int Damage){

		health -= Damage;

	
		rend.material.SetColor("_Color", Color.red);

		damaged = true;

		if (health <= 0) {
			
			particles.SetActive (true);
			enemyAi2.SetLife ();
			enemyAi2.active = true;
			Destroy (this.gameObject);

		}

	}
}
