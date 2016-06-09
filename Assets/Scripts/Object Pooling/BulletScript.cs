using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float speed;

	private PlayerAI _player;
	private GameObject _colorManager;
    internal Color _bulletColor;
    internal Vector3 direction;

    private float step;

	void Awake(){
	
		_player = GameObject.Find ("PlayerAim").GetComponent<PlayerAI> ();
        step = Time.deltaTime * speed;
	}

	void OnEnable()
	{
		_bulletColor = _player.selectedColor;

        direction = _player.instantiatePosition;

		this.GetComponent<Renderer> ().material.color = _bulletColor;

	}
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, step);
    }
    private void OnDisable()
    {
        transform.position = _player.transform.position;
    }
}

