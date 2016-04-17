using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

   
    AudioManager m_AudioManager;

	// Use this for initialization
	void Start () 
    {
        GameObject g = GameObject.FindGameObjectWithTag("AudioManager");
        if (g != null)
        {
            m_AudioManager = g.GetComponent<AudioManager>();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void FinishGame()
    {
        m_AudioManager.FinishGame();
    }

    void DisableFireEmitters()
    {
        
    }
}
