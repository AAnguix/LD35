using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GuiWeaponsController : MonoBehaviour {

	public Image m_Previous;
	public Image m_Actual;
	public Image m_Next;
	//public Sprite[] m_Weapons;
	public int m_PreviousWeapon;
	public int m_ActualWeapon;
	public int m_NextWeapon;

	public float m_WeaponsVisible;

    public Player m_Player;
    List<Slot> m_Weapons;

	// Use this for initialization
	void Start () 
	{
		m_Next.gameObject.SetActive (false);
		m_Previous.gameObject.SetActive (false);
		m_ActualWeapon = 0;
		m_PreviousWeapon = 0;
		m_NextWeapon = 0;

        m_Weapons = m_Player.GetInventory();
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetAxis("Mouse ScrollWheel") != 0f ) // forward
		{
			//m_ActualWeapon += Input.GetAxis("Mouse ScrollWheel");
			//m_ActualWeapon += 1;
			if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
			{
				m_ActualWeapon++;
				m_ActualWeapon = m_ActualWeapon % m_Weapons.Count;
				//m_PreviousWeapon = (m_ActualWeapon -1)% m_Weapons.Length;
                m_PreviousWeapon = ((m_ActualWeapon-1) % m_Weapons.Count + m_Weapons.Count) % m_Weapons.Count;
                m_NextWeapon = (m_ActualWeapon +1) % m_Weapons.Count;
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
			{
				m_ActualWeapon--;
                m_ActualWeapon = (m_ActualWeapon % m_Weapons.Count + m_Weapons.Count) % m_Weapons.Count;

                m_PreviousWeapon = (m_ActualWeapon +1)% m_Weapons.Count;
				//m_NextWeapon = (m_ActualWeapon -1) % m_Weapons.Length;
                m_NextWeapon = ((m_ActualWeapon-1) % m_Weapons.Count + m_Weapons.Count) % m_Weapons.Count;
			}
				
			m_Previous.gameObject.SetActive (true);
			m_Next.gameObject.SetActive (true);

            m_Actual.sprite = m_Weapons[m_ActualWeapon].m_Weapon.m_Sprite;
            m_Next.overrideSprite = m_Weapons[m_NextWeapon].m_Weapon.m_Sprite; 
            m_Previous.overrideSprite = m_Weapons [m_PreviousWeapon].m_Weapon.m_Sprite; 
			Invoke ("ChangeState", m_WeaponsVisible);
		}
		/*
		if (Input.anyKeyDown) 
		{
			m_ActualWeapon += 1;



			int l_ActualWeapon = m_ActualWeapon % m_Weapons.Length;
			int l_PreviousWeapon = (l_ActualWeapon -1)% m_Weapons.Length;
			int l_NextWeapon = (l_ActualWeapon +1) % m_Weapons.Length;

			m_Previous.gameObject.SetActive (true);
			m_Next.gameObject.SetActive (true);

			m_Actual.sprite = m_Weapons[l_ActualWeapon];
			m_Next.overrideSprite = m_Weapons[l_NextWeapon]; 
			m_Previous.overrideSprite = m_Weapons [l_PreviousWeapon]; 
			Invoke ("ChangeState", m_WeaponsVisible);
		}
		*/
	}

    public void AddWeaponToInventory(Weapon Weapon, int Ammunation)
    {
        m_Player.AddWeaponToInventory(Weapon, Ammunation);
        m_Weapons = m_Player.GetInventory();
    }

	void ChangeState()
	{
		m_Previous.gameObject.SetActive (false);
		m_Next.gameObject.SetActive (false);
	}

	void Module()
	{


	}
}
