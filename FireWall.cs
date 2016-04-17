using UnityEngine;
using System.Collections;

public class FireWall : MonoBehaviour {

    public float m_DelayTime;
    public float m_TimeEmitting;
    private float m_Time;
    private bool m_Emitting;
    Animator m_Animator;
    SpriteRenderer m_SpriteRenderer;

	// Use this for initialization
	void Start () 
    {
        m_Time = 0.0f;
        m_Emitting = false;
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update () 
    {
        m_Time += Time.deltaTime;

        if (!m_Emitting)
        {
            if (m_Time >= m_DelayTime)
            {
                m_Emitting = true;
                m_Time = 0.0f;
            }
        }
        else
        {
            if (m_Time >= m_TimeEmitting)
            {
                m_Emitting = false;
                m_Time = 0.0f;
            }
        }

        if (m_Emitting)
        {
            m_SpriteRenderer.enabled = true;
        }
        else
        {
            m_SpriteRenderer.enabled = false;
        }
    }
}
