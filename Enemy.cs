using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject m_Player;
    public GameObject m_Projectile;
    public float m_Health;
    public float m_VisionRadius;

    public float m_ShootRate;
    private float m_Timer;

    public DamageType m_DamageType;

	void Start () 
    {
	
	}
	
    bool DetectPlayer()
    {
        float l_PlayerX = m_Player.transform.position.x;
        float l_PlayerY = m_Player.transform.position.y;
        float l_MyX = transform.position.x;
        float l_MyY = transform.position.y;

        float l_X = Mathf.Pow((l_MyX - l_PlayerX), 2);
        float l_Y = Mathf.Pow((l_MyY - l_PlayerY), 2);

        float l_Distance = Mathf.Sqrt(l_X + l_Y);

        if (l_Distance <= m_VisionRadius)
        {
            return true;
        }

        return false;
    }

	// Update is called once per frame
	void Update () 
    {
        if (DetectPlayer())
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= m_ShootRate)
            {
                m_Timer = 0.0f;
                Shoot(GetShootDirection());
            }
        }
	}

    private Vector2 GetShootDirection()
    {
        Vector2 m_PlayerPos = m_Player.transform.position;
        Vector2 m_Direction = m_PlayerPos - new Vector2(transform.position.x,transform.position.y);
        m_Direction.Normalize();
        return m_Direction;
    }

    public void Shoot(Vector2 Direction)
    {
        Vector2 m_EnemyPos = new Vector2(transform.position.x,transform.position.y);
        GameObject l_Object = Instantiate(m_Projectile,m_EnemyPos + Direction, Quaternion.identity) as GameObject;
        EnemyProjectile l_Projectile = l_Object.GetComponent<EnemyProjectile>();

        AddForceToProjectile(l_Projectile,Direction);
    }

    public void AddForceToProjectile(EnemyProjectile Projectile, Vector2 Force)
    {
        Projectile.m_RigidBody2D.AddForce(Force * Projectile.m_Force);
    }
}
