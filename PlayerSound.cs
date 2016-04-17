using UnityEngine;
using System.Collections;

public class PlayerSound : MonoBehaviour {

    public AudioClip[] m_WalkClips;

    AudioSource m_AudioSource;

	// Use this for initialization
	void Start () {
	
        m_AudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayWalkClicp()
    {
        int l_ID = Random.Range(0, m_WalkClips.Length);
        m_AudioSource.PlayOneShot(m_WalkClips[l_ID]);
    }
}
