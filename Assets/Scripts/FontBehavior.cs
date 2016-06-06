using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FontBehavior : MonoBehaviour {
	
	public AudioClip getColor;
	public string colorSelected;

	private ColorController _colorManager;

	internal int chargesToAdd;

	internal Color _fontColor;
    internal PlayerAI _player;
	internal bool isInside;
	internal CopsuleTint _uiColor;
	internal CopsuleTint _uiColor2;

	internal Image _splash;

    void Awake()
    {
        _player = GameObject.Find("PlayerAim").GetComponent<PlayerAI>();
		_colorManager = GameObject.Find("ColorManager").GetComponent<ColorController>();
		_uiColor = GameObject.Find ("Splash").GetComponent<CopsuleTint> ();
		_uiColor2 = GameObject.Find ("Fill").GetComponent<CopsuleTint> ();

		_splash = GameObject.Find ("Splash").GetComponent<Image> ();

		chargesToAdd = 20;

		for (int i = 0; i < _colorManager.colorList.Count; i++) 
		{
			if (colorSelected == _colorManager.colorList[i].colorName) 
			{
				_fontColor = _colorManager.colorList [i].color;

				break;
			}
		}

		isInside = false;
	}
	void Update(){

		if (isInside && Input.GetKey (KeyCode.E)) {
			_uiColor.ChangeColor (_fontColor);
			_uiColor2.ChangeColor (_fontColor);

			if (!_player.playerColors.Contains(_fontColor) || _player.selectedColor != _fontColor) {

				_player.paintCharges = 20f;
				_player.playerColors.Add (_fontColor);
				_player.selectedColor = _fontColor;
                _splash.enabled = true;
                gameObject.GetComponent<AudioSource> ().PlayOneShot (getColor);
			}

			if (_player.paintCharges <= 20) {

				_player.paintCharges += (Time.deltaTime * 4);
			}
		}

	}

    void OnTriggerEnter(Collider other)
    {
		if (other.tag == ("Player") )
        {
			isInside = true;
        }
    }

	void OnTriggerExit(Collider other)
	{
		if (other.tag == ("Player") )
		{
			isInside = false;
		}
	}

	void colorSet()
    {
		gameObject.GetComponent<AudioSource> ().PlayOneShot (getColor);
	}
}
