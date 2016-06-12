using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class EnemyAiBos2 : MonoBehaviour {

	public int health;
	private int _health;
	public GameObject particles;
	public GameObject hoguera;
	public bool active;
	private bool damaged;
	private float countedDamaged;
	private float timeDamaged;

	public Color damagedTexture;
	public Color NornalTexture;
	public Renderer rend;
	public Shoot shoot;

	public Transform spawn1;
	public Transform spawn2;

	public GameObject enemy1;
	public GameObject enemy2;

	public bool dead1;
	public bool dead2;

	public laserScript laserscript;

	// Use this for initialization
	void Start () {
		active = false;
		damaged = false;
		countedDamaged = 0;
		timeDamaged = 1f;
		enemy1.SetActive (false);

		enemy2.SetActive (false);
		enemy1.SetActive (false);
	
	}
	void Update(){




		if (damaged) {
		
			countedDamaged += Time.deltaTime;

		
		}
		if(countedDamaged >= timeDamaged){

			rend.material.color = NornalTexture;
			countedDamaged = 0;
			damaged = false;
		}
	
	
	}

	public void AdDamage(int Damage){
		if (active) {



			health -= Damage;
	
			rend.material.SetColor("_Color", Color.red);
				
			damaged = true;

		}
		if (health <= 0) {

			if (SceneManager.GetActiveScene().buildIndex == 6) {
			
				if (!GameObject.FindGameObjectWithTag ("Boss2").GetComponent<EnemyAiBos2> ().dead1) {
				
					dead1 = true;
				
				}
				if (!GameObject.FindGameObjectWithTag ("Boss2").GetComponent<EnemyAiBos2> ().dead2) {

					dead2 = true;

				}
			
			}
			particles.SetActive (true);

			hoguera.SetActive (true);
			Debug.Log ("adeee");
			Destroy (this.gameObject);

		}
	}
	public void SetLife(){


		Debug.Log ("dsa");

		//laserscript.spawning = false;
		//Instantiate(enemy1, spawn1.position, spawn1.rotation);
		//enemy1.SetActive (true);
		enemy2.SetActive (true);

		active = true;
	
	}
	public void setDead(){
	
		//enemy2.SetActive (true);
	
	}
}
