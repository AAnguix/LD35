using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

    public float m_Damage;
    public float m_Force;
    public DamageType m_DamageType;

    [HideInInspector]
    public Rigidbody2D m_RigidBody2D;

    void Awake()
    {
        m_RigidBody2D = GetComponent<Rigidbody2D>();
    }

	void Start () 
    {
       
	
	}
	
	void Update () 
    {
       
	
	}
}
