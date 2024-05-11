using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portal_Door : Colliderable
{
    public BoxCollider2D boxOUT;           

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnCollide(Collider2D coll)
    {      
        if (coll.name == "Player")
        {
            Vector3 vector = boxOUT.transform.position;

            vector.z = 0;
            
            if (boxOUT.gameObject.transform.rotation == Quaternion.Euler(0,0,0))
                vector.y += 0.2f;
            if(boxOUT.gameObject.transform.rotation == Quaternion.Euler(0, 0, 180))
                vector.y -= 0.2f;

            if (boxOUT.gameObject.transform.rotation == Quaternion.Euler(0, 0, -90))
                vector.x += 0.2f;
            if (boxOUT.gameObject.transform.rotation == Quaternion.Euler(0, 0, 90))
                vector.x -= 0.2f;

            GameManager.instance.player.transform.position = vector;
        }
    }
}
