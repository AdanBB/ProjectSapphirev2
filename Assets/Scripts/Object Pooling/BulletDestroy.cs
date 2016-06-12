using UnityEngine;
using System.Collections;


public class BulletDestroy : MonoBehaviour {

	public GameObject bulletExplosion;

	private ParticleSystem bulleExplosionParticle;

	public GameObject splashColor1;
	public GameObject splashColor2;

	private PlayerAI _player;
	private ColorController colorController;
	private GameObject instantiateObject;

	public int damage;

	private Color _thisColor;

	private int privateColor;

    internal Vector3 positioninsta;
    internal Quaternion rotateinsta;

    void Awake()
	{
		colorController = GameObject.Find ("ColorManager").GetComponent<ColorController> ();
		_thisColor = GetComponent<Renderer> ().material.color;
		_player = GameObject.Find ("PlayerAim").GetComponent<PlayerAI> ();
	}

	// Use this for initialization
    private void OnEnable()
    {
        rotateinsta = _player.lookDirection;
        positioninsta = _player.instantiatePosition;

        Invoke("Destroy", 3f);
		_thisColor = _player.selectedColor;
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Info" || other.tag != "Detection")
        {
            if (other.tag == "Boss2")
            {
				Instantiate (bulletExplosion, transform.position, transform.rotation);
                other.GetComponent<EnemyAiBos2>().AdDamage(damage);
                Invoke("Destroy", 0.05f);
            }
            else if (other.gameObject.tag == "Platform")
            {
                Invoke("Destroy", 0.05f);
            }
            else if (other.tag == "Boss")
            {
				Instantiate (bulletExplosion, transform.position, transform.rotation);
                other.GetComponent<EnemyAiBoos>().AdDamage(damage);
                Invoke("Destroy", 0.05f);
            }
            else if (other.tag == "Enemy")
            {
                other.GetComponent<EnemyAi2>().AdDamage(damage, _thisColor);
				Instantiate (bulletExplosion, transform.position, transform.rotation);
                Invoke("Destroy", 0.05f);
            }
            else if (other.tag == "EnemySkull")
            {
				Instantiate (bulletExplosion, transform.position, transform.rotation);
                other.GetComponent<EnemyAI3>().AdDamage(damage, 0);
                Invoke("Destroy", 0.05f);
            }
			else if (other.tag == "EnemySkullBoos")
			{
				Instantiate (bulletExplosion, transform.position, transform.rotation);
				other.GetComponent<EnemyAI4>().AdDamage(damage, 0);
				Invoke("Destroy", 0.05f);
			}

            else if (other.tag == "Floor")
            {

                for (int i = 0; i < colorController.colorList.Count; i++)
                {
                    if (_thisColor == colorController.colorList[i].color)
                    {

                        if (colorController.colorList[i].colorName == "Green")
                        {
                            Instantiate(GameObject.Find("paintSplash_Green"), positioninsta, rotateinsta);
                            Invoke("Destroy", 0.05f);
                            break;
                        }
                        else if (colorController.colorList[i].colorName == "Blue")
                        {
                            Instantiate(GameObject.Find("paintSplash_Blue"), positioninsta, rotateinsta);
                            Invoke("Destroy", 0.05f);
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            Invoke("Destroy", 0.05f);
        }
    }

	void Destroy ()
    {
		//Instantiate (bulletExplosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
	}
}
