using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
	public GameObject gameOverUI;
	public GameObject uiObject;

    Animator myAnimator;
    // Use this for initialization
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void ApplyDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        if (currentHealth <= 0)
        {
            Cursor.visible = true;
			gameOverUI.SetActive (true);
			uiObject.SetActive (false);
        }
    }
}