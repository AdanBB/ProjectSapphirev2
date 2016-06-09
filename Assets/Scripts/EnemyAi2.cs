using UnityEngine;
using System.Collections;

public class EnemyAi2 : MonoBehaviour {
	
	public float health;
	private float _health;

	internal bool lastHit;
	private float lastHitCounter;
	public float velocity;


	public Color normal;

	private GameObject treeBody;

    private GameObject player;
	private NavMeshAgent agent;

	public float damage;
	private float counter;
	private float counter2;
	private bool attack;

	public  float range;
	public float detectionRange;
	public float distanceAttack;
	private SphereCollider detectionCollider;

	internal bool isAttacking;
	private int AttackNum;
	private float counterAnim;
	private float _counterAnim;

	public GameObject[] deathParticles;

	public ColorManager colorManager;
	private Animator anim;

	public AudioClip enemyAttack;
	public AudioClip lastHitSound;
	private AudioSource enemySound;

	public Transform pointA;
	public Transform pointB;

	private PlayerAI _playerAI;

	private bool hit;
	private float hitcounter;

	private bool damaged;
	private float countedDamaged;
	private float timeDamaged;

	public Color damagedTexture;
	public Color NornalTexture;
	public Renderer rend;

	// Use this for initialization
    void Awake () {

        player = GameObject.FindGameObjectWithTag("Player");
		_playerAI = player.GetComponent<PlayerAI> ();

		agent = GetComponent<NavMeshAgent> ();
		detectionCollider = GetComponentInChildren<SphereCollider> ();
		treeBody = transform.GetChild (2).gameObject;
		anim = GetComponent<Animator> ();
		enemySound = GetComponent<AudioSource> ();

		colorManager = GameObject.FindGameObjectWithTag ("ColorManager").GetComponent<ColorManager> ();
    }

	void Start () {
		
		_health = health;
		attack = false;
		counter2 = 1.2f;
		counter = counter2;
		detectionCollider.radius = detectionRange;

		isAttacking = false;
		agent.SetDestination (pointA.position);
		anim.SetBool ("IsMoving", true );

		damaged = false;
		countedDamaged = 0;
		timeDamaged = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {




		if (damaged) {

			countedDamaged += Time.deltaTime;

			Debug.Log ("dsa");

		}
		if(countedDamaged >= timeDamaged){

			rend.materials[1].color = NornalTexture;
			Debug.Log("lel");
			countedDamaged = 0;
			damaged = false;
		}


		if (hit) 
		{
			hitcounter += Time.deltaTime;
		}

		if (isAttacking) {

			_counterAnim += Time.deltaTime;

			if (_counterAnim >= counterAnim) {
			
				anim.SetBool ("IsAttack", false);
				isAttacking = false;
				_counterAnim = 0;
			}
		}

		if (attack) {

			counter -= Time.deltaTime;
			if (counter <= 0) {
			
				attack = false;
				counter = counter2;
			}
		}

		if (lastHit) {

			anim.SetBool ("IsLastHit", true);
			lastHitCounter += Time.deltaTime;

			if (lastHitCounter >= 3) {
				treeBody.GetComponent<Renderer> ().materials [1].color = normal;
				health = _health;
				lastHitCounter = 0;
				lastHit = !lastHit;
			}

		} else {
			anim.SetBool ("IsLastHit", false);
			anim.speed = 1;
			agent.speed = 1;
		}
		velocity = agent.velocity.magnitude;


		if (Vector3.Distance (agent.destination, this.transform.position) <= 1.40f) 
		{
			anim.SetBool ("IsMoving", false);
		}

	}

	public void GotoPosition(int where)
	{
		if (where == 1) {
			agent.SetDestination (pointA.position);
		}
		if (where == 2) {
			agent.SetDestination (pointB.position);
		}
	}

    public void FollowPlayer()
    {
		if (!lastHit) {
	
			anim.speed = 2;
			agent.speed = 2.5f;
			//transform.LookAt (new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z));
			agent.SetDestination (player.transform.position);
			anim.SetBool ("IsMoving", true);
		} else if(lastHit) {
		
			agent.Stop ();
		
		}


		range = Vector3.Distance (this.transform.position, player.transform.position);

		if ((range <= distanceAttack) && (!attack) && (!lastHit)&&(!isAttacking)) 
		{
			Attack ();
		}

		if (range >= distanceAttack)
		{
			CancelInvoke ("DamagePlayer");
		}

    }

	#region SelfDamage

	public void AdDamage(int Damage, Color color){

		hit = true;

		if ((health >= 0)) {

			health = health - Damage;
			rend.materials[1].SetColor("_Color",color);

			damaged = true;
		}

		if ((health <= 10) && (!lastHit)) {
			rend.materials[1].SetColor("_Color", color);

			damaged = true;
			lastHit = !lastHit;
		}

        else if (lastHit)
		{
			Instantiate (deathParticles[0], new Vector3 (transform.position.x,transform.position.y + 1, transform.position.z), transform.rotation);
			Destroy (this.gameObject);
		}
	}

	#endregion

	#region Attack

	public void Attack(){

		Invoke ("DamagePlayer", 0.8f);

		attack = true;
		AttackNum = Random.Range (1, 4);
		anim.SetInteger ("Attack", AttackNum);
		anim.SetBool ("IsAttack", true);
		isAttacking = true;
		counterAnim = 1.3f;

	}

	public void DamagePlayer(){
	
		player.GetComponent<CharacterStats> ().ApplyDamage (damage);
		enemySound.PlayOneShot (enemyAttack);

	}

	#endregion
}
