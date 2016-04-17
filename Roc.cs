using UnityEngine;
using System.Collections;

public class Roc : MonoBehaviour {

    private Collider2D m_Collider2D;
    private Rigidbody2D m_Rigidbody2D;
    private Vector2 m_Displacement;

	void Start () 
    {
        m_Displacement = Vector2.zero;
        m_Collider2D = GetComponent<Collider2D>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        //if(m_Displacement != Vector2.zero)
        m_Rigidbody2D.AddForce(m_Displacement);
        m_Rigidbody2D.velocity = Vector2.zero;
    }

	void Update () 
    {
        m_Displacement = Vector2.zero;
        this.transform.rotation = Quaternion.identity;    
	}

    void OnCollisionEnter2D(Collision2D coll) 
    {
        if (coll.gameObject.tag == "Player")
        {
            Player l_Obj = coll.gameObject.GetComponent<Player>();

            if (l_Obj != null)
            {
                m_Displacement = l_Obj.m_DisplacementForce;
            }
        }
    } 
}
