using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTextPerson : Colliderable
{
    public string[] messages;               private int msgNow = 0;             
    public float showTime;                  public float coolDown = 4.0f;           private float lastShout;


    public bool canLookAtPlayer = false;    private float posDelta;             
    protected override void Start()
    {
                base.Start();
        lastShout = -coolDown;
    }

    protected override void Update()
    {
        base.Update();

                if (canLookAtPlayer)
        {
                        posDelta = GameManager.instance.player.transform.position.x - transform.position.x;
            if (posDelta > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (posDelta < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

        protected override void OnCollide(Collider2D coll)
    {
        if (coll.name != "Player")
            return;

        if (Time.time - lastShout > coolDown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(messages[msgNow++], 30, Color.white, transform.position + new Vector3(0, 0.18f, 0), Vector3.zero, showTime);

            if (msgNow == messages.Length)
                msgNow = 0;
        }
    }
}
