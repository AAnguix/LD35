using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyFollow : MonoBehaviour {
	public float floatHeight;
	public float liftForce;
	public float damping;
	Rigidbody2D rb2D;
	GameObject m_Player;
	public float m_Speed;
	bool m_Caminando;
	Vector2 m_PointToGo;



	// Use this for initialization
	void Start () 
	{
		m_Player = GameObject.FindGameObjectWithTag ("Player");
		rb2D = GetComponent<Rigidbody2D>();
		m_Caminando = false;
		m_PointToGo = new Vector2 (m_Player.transform.position.x, m_Player.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void FixedUpdate()
	{
		Vector2 l_Dir = new Vector2(m_Player.transform.position.x, m_Player.transform.position.y);
		l_Dir.x = l_Dir.x - transform.position.x;
		l_Dir.y = l_Dir.y - transform.position.y;


		Debug.DrawRay(transform.position, new Vector3(l_Dir.x, l_Dir.y, 0));
		RaycastHit2D hit = Physics2D.Raycast(transform.position, l_Dir);
		if (hit.collider != null) 
		{
			if (hit.collider.tag == "Player") 
			{
				m_PointToGo = new Vector2 (m_Player.transform.position.x, m_Player.transform.position.y);
			}
			else if(hit.collider.tag == "Pared")
			{
				m_Caminando = true;
				BoxCollider2D l_BoxCollider2D = hit.collider.GetComponent<BoxCollider2D> ();
				//l_BoxCollider2D.transform.position 
				m_PointToGo.x = 7;
				m_PointToGo.y = 5;

				//m_PointToGo.x = l_BoxCollider2D.gameObject.transform.position.x + l_BoxCollider2D.size.x*6;
				//m_PointToGo.y = l_BoxCollider2D.gameObject.transform.position.y + l_BoxCollider2D.size.y*6;

			}


		}
		if(m_Caminando)
		{
			Vector2 l_AuxDist = new Vector2 (m_PointToGo.x - transform.position.x, m_PointToGo.y - transform.position.y);
			if(l_AuxDist.x + l_AuxDist.y < 0.8)
			{
				m_Caminando = false;
				m_PointToGo = new Vector2 (m_Player.transform.position.x, m_Player.transform.position.y);
			}
		}

		l_Dir = m_PointToGo;
		l_Dir.x = l_Dir.x - transform.position.x;
		l_Dir.y = l_Dir.y - transform.position.y;
		l_Dir.Normalize ();

		transform.position = new Vector3(transform.position.x + l_Dir.x*Time.deltaTime,transform.position.y + l_Dir.y*Time.deltaTime,0);

	}
}
