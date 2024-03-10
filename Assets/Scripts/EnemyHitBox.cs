using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : Collidable
{
    //Damage
    public int damage = 1;
    public float pushForce = 5;

    protected override void OnCollide(Collider2D coll)
    {
       if (coll.tag == "Fighter" && coll.name == "player_0")
        {
            // Create a new damage object, before sending it to the player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

}
