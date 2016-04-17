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
    public Text m_AmmoText;
    public int m_AmmoValue;

    public float m_WeaponsVisible;

    public Player m_Player;
    List<Slot> m_Weapons;

    void Start () 
    {
        m_Next.gameObject.SetActive (false);
        m_Previous.gameObject.SetActive (false);
        m_ActualWeapon = 0;
        m_PreviousWeapon = 0;
        m_NextWeapon = 0;
        m_AmmoValue = 0;

        m_Weapons = m_Player.GetInventory();
    }
        
    void Update () 
    {
        if (m_Weapons!= null && m_Weapons.Count != 0)
        {
            //m_AmmoText.text = ""+m_AmmoValue;
            //m_AmmoText.text = ""+m_Weapons [m_ActualWeapon].m_Ammunation;
            m_AmmoText.text = ""+m_Player.GetCurrentWeapon().m_Ammunation;

            //m_AmmoText.text = ""+
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
                m_AmmoText.gameObject.SetActive (false);    
                m_Previous.gameObject.SetActive (true);
                m_Next.gameObject.SetActive (true);

                m_Actual.sprite = m_Weapons[m_ActualWeapon].m_Weapon.m_Sprite;
                m_Next.overrideSprite = m_Weapons[m_NextWeapon].m_Weapon.m_Sprite; 
                m_Previous.overrideSprite = m_Weapons [m_PreviousWeapon].m_Weapon.m_Sprite; 
                m_Player.ChangeWeapon (m_Weapons[m_ActualWeapon]);

                Invoke ("ChangeState", m_WeaponsVisible);
            }
        }
    }

    public void UpdatePlayerWeapons()
    {
        m_Weapons = m_Player.GetInventory();
    }

    void ChangeState()
    {
        m_AmmoText.gameObject.SetActive (true);
        m_Previous.gameObject.SetActive (false);
        m_Next.gameObject.SetActive (false);
    }
}
