using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject m_Player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector2 l_PlayerPos = m_Player.transform.position;
        float l_Z = this.transform.position.z;
        this.transform.position = new Vector3(l_PlayerPos.x, l_PlayerPos.y, l_Z);
	}
}
