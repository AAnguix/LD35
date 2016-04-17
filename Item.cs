using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public SpriteRenderer m_Dialog;
    private bool m_Touched;
    private bool m_Caught;
    public Player m_Player;

    private float m_Timer;
    private float m_TimeBeforeHide;

    public Weapon m_Weapon;
    public Upgrade m_Upgrade;

    public AudioClip m_DiassembleSound, m_CollectSound;
    private AudioSource m_AudioSource;

    public GameObject m_GUI;

    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

	void Start () 
    {
        m_Touched = false;
        m_Caught = false;
        m_Timer = 0.0f;
        m_TimeBeforeHide = 3.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_Touched)
        {
            m_Timer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.X))
            {
                Diassemble();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Collect();
            }

            if (m_Timer >= m_TimeBeforeHide)
            {
                HideItemDialog();
                m_Timer = 0.0f;
                m_Touched = false;
            }
        }
        else if (m_Caught)
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= 1.0f)
            {
                Destroy(this.gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowItemDialog(this.transform.position);
            m_Touched = true;
        }
    }

    void Diassemble()
    {
        m_AudioSource.PlayOneShot(m_DiassembleSound);

        int l_AmmoInventory = Random.Range (10, 15);
        m_Player.AddWeaponToInventory(m_Weapon, l_AmmoInventory);
        m_GUI.GetComponent<GuiWeaponsController> ().UpdatePlayerWeapons();        
        DestroyObject(this.gameObject);

        m_Caught = true;
        m_Timer = 0.0f;
    }

    void Collect()
    {
        m_AudioSource.PlayOneShot(m_CollectSound);
        m_Player.AddUpgrade(m_Upgrade);
        m_Caught = true;
        m_Timer = 0.0f;
    }

    private void ShowItemDialog(Vector2 Position)
    {
        m_Dialog.enabled = true;
        m_Dialog.transform.position = new Vector2(Position.x,Position.y+0.8f);
    }

    private void HideItemDialog()
    {
        m_Dialog.enabled = false;
    }


    /*void OnCollisionExit2D(Collision2D coll) 
    {
        if (coll.gameObject.tag == "Item")
        {
            coll.gameObject.GetComponent<Item>().Exit();
        }
    }*/
}
