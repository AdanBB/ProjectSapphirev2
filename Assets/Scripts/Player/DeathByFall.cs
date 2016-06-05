using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathByFall : MonoBehaviour {

	public GameObject gameOverUI;
	public GameObject uiObject;
	private Vector3 enterPosition;
	private GameObject _player;
	public GameObject mainCamera;

	private bool dead;

	void Start()
	{
		dead = false;
	}

	void FixedUpdate()
	{
		if (dead) {
			_player.transform.position = enterPosition;
		}
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
			mainCamera.GetComponent<UnityStandardAssets.Utility.SimpleMouseRotator>().enabled = false;
			_player = other.gameObject;
			enterPosition = other.gameObject.transform.position;
			other.gameObject.SetActive (false);

            Cursor.visible = true;
            dead = true;

			gameOverUI.SetActive (true);
			uiObject.SetActive (false);
        }
    }
}
