using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuManager : MonoBehaviour {

    public PlayerAI playerAI;
    public GameObject player;
    public GameObject mainCamera;

    public GameObject uiObject;
    public GameObject pauseObject;
    public GameObject optionsObject;
	public GameObject gameOverUI;

    public string nextLevel;
    public string menuLevel;

	private bool isPaused;
	private bool gameOver;

	// Use this for initialization
	void Start () {
	
		isPaused = false;
		gameOver = false;
	}

    // Update is called once per frame
    void Update() {

		if (gameOverUI.activeInHierarchy) {
			
			gameOver = true;
		}

		if (gameOver == false) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (!isPaused) {

					SetPause ();
				}
			}
			if (Input.GetKeyDown (KeyCode.O)) {
				Application.LoadLevel (6);
			}
			if (Input.GetKeyDown (KeyCode.I)) {
				Application.LoadLevel (3);
			}
			if (Input.GetKeyDown (KeyCode.K)) {
				Application.LoadLevel (4);
			}
			if (Input.GetKeyDown (KeyCode.L)) {
				Application.LoadLevel (5);
			}
		}
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (isPaused)
            {
                SetGame();
                SceneManager.LoadScene(nextLevel);
            }

        }
    }

    #region Pause Behavior

    public void ResumeLevel()
    {
        SetGame();
    }

    public void MainMenu()
    {
        if (isPaused)
        {
            SetGame();
            SceneManager.LoadScene(menuLevel);
        }
    }

    public void Exit()
    {
        if (isPaused)
        {
            SetGame();
            Application.Quit();
        }
    }

    public void Options()
    {
        pauseObject.SetActive(false);
        optionsObject.SetActive(true);
    }

	public void RstartLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

    #endregion

    #region Options Behavior

    public void BackButton()
    {
        pauseObject.SetActive(true);
        optionsObject.SetActive(false);
    }

    public void AudioVolume(int value)
    {
        
    }

    public void QualityDropdown(int value)
    {
        if (value == 0)
        {
            QualitySettings.SetQualityLevel(5, true);
        }
        if (value == 1)
        {
            QualitySettings.SetQualityLevel(2, true);
        }
        if (value == 2)
        {
            QualitySettings.SetQualityLevel(0, true);
        }
    }

    public void AADropdown(int value)
    {
        if (value == 0)
        {
            QualitySettings.antiAliasing = 0;
        }
        if (value == 1)
        {
            QualitySettings.antiAliasing = 2;
        }
        if (value == 2)
        {
            QualitySettings.antiAliasing = 4;
        }
        if (value == 3)
        {
            QualitySettings.antiAliasing = 8;
        }
    }

    #endregion

    void SetPause(){

        uiObject.SetActive(false);
        pauseObject.SetActive(true);
        mainCamera.GetComponent<UnityStandardAssets.Utility.SimpleMouseRotator>().enabled = false;
        Cursor.visible = true;
        isPaused = true;
		Time.timeScale = 0;
	}

	void SetGame(){

        uiObject.SetActive(true);
        mainCamera.GetComponent<UnityStandardAssets.Utility.SimpleMouseRotator>().enabled = true;
        Cursor.visible = false;
        pauseObject.SetActive(false);
		isPaused = false;
		Time.timeScale = 1;
	}
}
