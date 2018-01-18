using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;



public class PlayerHealth : MonoBehaviour
{
	float timer = 0.0f;
	float deathTimer = 0.0f;


	Animator anim;
	Slider healthSlider;
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;
	PlayerAnimator playerAnimator;
	bool isDead;
	bool damaged;
	int startingHealth = 200;
	int currentHealth;
	float flashSpeed = 5f;
	[SerializeField] Sprite [] hearts;
	[SerializeField] Image heartImage;
	string killer;

    void Awake ()
	{
		healthSlider = GameObject.Find ("HealthSlider").GetComponent<UnityEngine.UI.Slider>();
		playerMovement = GetComponent<PlayerMovement> ();
		playerAnimator = GetComponent<PlayerAnimator> ();
		playerShooting = GetComponentInChildren<PlayerShooting> ();
		anim = GetComponent<Animator> ();
		currentHealth = startingHealth;

    }


    void Update ()
    {
		if (currentHealth >= 0) {
			deathTimer += Time.deltaTime;
		}
    }


	public void TakeDamage (int amount, string killer)
    {
		damaged = true;
		currentHealth -= amount;
		if (currentHealth < 75 && currentHealth >= 50) {
			heartImage.sprite = hearts [1];
		} else if (currentHealth < 50 && currentHealth >= 25) {
			heartImage.sprite = hearts [2];
		} else if (currentHealth < 25) {
			heartImage.sprite = hearts [3];
		}
		
		healthSlider.value = currentHealth;

		if (currentHealth <= 0 && !isDead) {
			this.killer = killer;
			Die ();
			heartImage.enabled = false;
		}
    }

	public int getCurrentHealth()
	{
		return currentHealth;
	}

    void Die ()
    {
		playerShooting.DisableEffects ();
		playerMovement.enabled = false;
		playerShooting.enabled = false;

		timer += Time.deltaTime;

		if (timer >= Time.deltaTime) {
			anim.SetBool ("PlayerDead", false);
			playerAnimator.enabled = false;
			isDead = true;
		} else {
			anim.SetBool ("PlayerDead", true);
		}

		reportDeath ();	
    }

	void reportDeath(){
		
		string path = "Assets/Prototypes/Sidi/Analytics/Death.txt";
		string path1 = "Assets/Prototypes/Sidi/Analytics/Killer.txt";
		string path2 = "Assets/Prototypes/Sidi/Analytics/Score.txt";
		//Report Death time
		StreamWriter writer = new StreamWriter(path, true);
		writer.WriteLine(string.Format("{0:N3}", deathTimer) + ",");
		writer.Close();

		//Report The killer 
		StreamWriter writer1 = new StreamWriter(path1, true);
		writer1.WriteLine(killer + ",");
		writer1.Close();

		//Report The Score
		StreamWriter writer2 = new StreamWriter(path2, true);
		writer2.WriteLine(ScoreManager.score + ",");
		writer2.Close();
		Debug.Log (ScoreManager.score);

	}
		
}
