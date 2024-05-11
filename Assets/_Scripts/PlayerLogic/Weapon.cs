using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Colliderable
{
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };                         
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3.0f, 3.3f, 3.6f, 4.0f };    
    public int weaponLevel = 0;                 private SpriteRenderer SpriteRenderer;      

    public Animator animator;                  private  float swingCoolDown = 0.4f;       private float lastSwing;

    public GameObject flamingSword;             public GameObject rageState;                public bool CanRageSkill = false;           public bool raging = false;                 public float ragingTime = 4f;               

    private void Awake()
    {
                                                SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        rageState.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

                if (GameManager.instance.player.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (Time.time - lastSwing > swingCoolDown)
                {
                    lastSwing = Time.time;

                                        Swing();

                                        if (raging)
                    {                     
                        CreateFlamingSword();
                    }
                    else
                        rageState.SetActive(false);                        
                }
            }

                        if(Input.GetKeyDown(KeyCode.R) && (!raging))
            {
                if (CanRageSkill)
                {
                    raging = true;
                    rageState.SetActive(true);
                    StartCoroutine("WaitingForRestRageSkill");
                }
            }
        }

    }

            protected override void OnCollide(Collider2D coll)
    {
                if (coll.CompareTag("Fighter"))
        {
                        if (coll.name == "Player")
                return;

                        Damag dmg = new Damag
            {
                                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

                        coll.SendMessage("ReceiveDamage", dmg);
        }
    }

        private void Swing()
    {

        animator.SetTrigger("Swing");
    }

        private void CreateFlamingSword()
    {
        GameObject go = Instantiate(flamingSword);
    }

        public void UpgradeWeapon()
    {
                weaponLevel++;
        SpriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

        public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        SpriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    IEnumerator WaitingForRestRageSkill()
    {
        yield return new WaitForSeconds(ragingTime);
        raging = false;
        CanRageSkill = false;
        GameManager.instance.player.rage = 0;
        GameManager.instance.OnUIChange();
    }
}