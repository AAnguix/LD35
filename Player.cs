using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DamageType{Normal = 0, Wind = 1, Roc = 2, Fire = 3};

public struct Slot
{
    public Weapon m_Weapon;
    public int m_Ammunation;

    public Slot(Weapon Weapon, int Ammunation)
    {
        m_Weapon = Weapon;
        m_Ammunation = Ammunation;
    }
};

public class Player : MonoBehaviour 
{
    public bool m_Locked;

    public Vector2 m_UpDisplacementForce, m_RightDisplacementForce;
    private Vector2 m_DownDisplacementForce, m_LeftDisplacementForce;

    [HideInInspector]
    public Vector2 m_DisplacementForce;

    /*Inventory*/
    private Slot m_CurrentWeapon;
    private List<Slot> m_Inventory;
    private List<Upgrade> m_Upgrades;

    /*Components*/
    private Rigidbody2D m_RigidBody2D;
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;
    private AudioSource m_AudioSource;

    public PlayerHealth m_PlayerHealth;
    public PlayerSound m_PlayerSound;

    public List<Slot> GetInventory()
    {
        return m_Inventory;   
    }
    public List<Upgrade> GetUpgrades()
    {
        return m_Upgrades;   
    }

    void Awake()
    {
        m_Locked = false;
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();

        m_PlayerHealth = GetComponent<PlayerHealth>();
        m_PlayerSound = GetComponent<PlayerSound>();
    }

	void Start () 
    {
        m_DisplacementForce = Vector2.zero;
        m_DownDisplacementForce = m_UpDisplacementForce * (-1);
        m_LeftDisplacementForce = m_RightDisplacementForce * (-1);

        m_Inventory = new List<Slot>();
	}

    /*Weapons*/
    public Slot GetCurrentWeapon()
    {
        return m_CurrentWeapon;
    }

    public void AddWeaponToInventory(Weapon Weapon, int Ammunation)
    {
        Slot l_Slot = new Slot(Weapon, Ammunation);
        m_Inventory.Add(l_Slot);
    }

    public void AddUpgrade(Upgrade Upgrade)
    {
        m_Upgrades.Add(Upgrade);
    }

    public void ChangeWeapon(Slot NewWeapon)
    {
        m_CurrentWeapon = NewWeapon;
    }

    void FixedUpdate()
    {
        m_RigidBody2D.velocity = Vector2.zero;
        Move(m_DisplacementForce);
    }

    void Update () 
    {
        m_DisplacementForce = Vector2.zero;
        this.transform.rotation = Quaternion.identity;

        if (!m_Locked)
        {
            if (Input.GetKey(KeyCode.W))
            {
                AddForce(m_UpDisplacementForce);
                //m_PlayerSound.PlayWalkClicp();
            }
            if (Input.GetKey(KeyCode.A))
            {
                AddForce(m_LeftDisplacementForce);
            }
            if (Input.GetKey(KeyCode.S))
            {
                AddForce(m_DownDisplacementForce);
            }
            if (Input.GetKey(KeyCode.D))
            {
                AddForce(m_RightDisplacementForce);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }
	}
        
    private void AddForce(Vector2 Force)
    {
        m_DisplacementForce += Force;
    }

    private void Move(Vector2 Movement)
    {
        m_RigidBody2D.AddForce(Movement);
    }

    private void Attack()
    {
        if (m_CurrentWeapon.m_Ammunation > 0)
        {
            m_CurrentWeapon.m_Ammunation -= 1;
            // m_AudioSource.PlayOneShot(m_CurrentWeapon.m_Weapon.m_Sound);
            // m_Animator.SetInteger("state", m_CurrentWeapon.m_Weapon.m_AnimmationID);
        }  

       // m_AudioSource.PlayOneShot(m_CurrentWeapon.m_Weapon.m_Sound);
       // m_Animator.SetInteger("state", m_CurrentWeapon.m_Weapon.m_AnimmationID);
    }

    void TakeDamage(EnemyProjectile Projectile)
    {
        m_PlayerHealth.TakeDamage(Projectile.m_Damage - Projectile.m_Damage*DamageFactorReduction(Projectile));
    }
        
    private float DamageFactorReduction(EnemyProjectile Projectile)
    {
        for (int i = 0; i < m_Upgrades.Count; ++i)
        {
            if (m_Upgrades[i].m_DamageType == Projectile.m_DamageType)
            {
                int ta;
                return m_Upgrades[i].m_Factor;
            }
        }

        return 0.0f;
    }

    void OnCollisionEnter2D(Collision2D coll) 
    {
        if (coll.gameObject.tag == "EnemyProjectile")
        {
            EnemyProjectile l_Obj = coll.gameObject.GetComponent<EnemyProjectile>();

            if (l_Obj != null)
            {
                TakeDamage(l_Obj);
                Destroy(coll.gameObject);
            }
        }
    }

    public void Lock()
    {
        m_Locked = !m_Locked;
    }
}
