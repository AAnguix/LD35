using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{         
    /*Sounds*/
    public AudioClip m_TakeDamage, m_Dead;

	public float currentHealth;                               
	public Slider healthSlider;                             
	public Image damageImage;                              
	//public AudioClip deathClip;                           
	public float flashSpeed = 5f;                           
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);                                      
                                                   
	bool isDead;                                               
	bool damaged;                                              

	private float xVibe = 0.5f;
	private float yVibe = 0.5f;
	private float zVibe = 0.5f;
	private float xRot = 0.05f;
	private float yRot = -0.05f;
	private float zRot = -0.05f;
	private float speed = 60.0f;
	private float diminish = 0.5f;
	private int numberOfShakes = 8;

	void Awake ()
	{
	
	}

	void Update ()
	{
		if (Input.anyKeyDown) 
		{
			TakeDamage (10);
		}

		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
	}


	public void TakeDamage (float amount )
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;

		// Reduce the current health by the damage amount.
		currentHealth -= amount;

		// Set the health bar's value to the current health.
		healthSlider.value = currentHealth;

		// Play the hurt sound effect.
		//playerAudio.Play ();

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead)
		{
			// ... it should die.
			Death ();
		}
	}


	void Death ()
	{
		// Set the death flag so this function won't be called again.
		//isDead = true;

		// Turn off any remaining shooting effects.
		//playerShooting.DisableEffects ();

		// Tell the animator that the player is dead.
		//anim.SetTrigger ("Die");

		// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();

		// Turn off the movement and shooting scripts.
		//playerMovement.enabled = false;
		//playerShooting.enabled = false;
		Vibration vibration = Camera.main.GetComponent<Vibration>();
		vibration.StartShaking(new Vector3(xVibe, yVibe, zVibe), new Quaternion(xRot, yRot, zRot, 1), speed, diminish, numberOfShakes);
	}       
}
