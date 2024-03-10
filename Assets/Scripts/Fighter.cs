using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //player_0 will inherite from this = he is going to be a fighter in the game (he has the ability to fight)

    //Public fields
    public int hitpoint = 10;
    public int maxhitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //Imunity
    protected float imunneTime = 1.0f;
    protected float lastImune;

    //Push
    protected Vector3 pushDirection;

    //All fighters can ReceiveDamage / Die

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImune > imunneTime)
        {
            lastImune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce; //direction towards you should be pushed after being hit
                                                                                          //the vector between you and the enemy which is hitting you

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

         if (hitpoint <=0 )
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}
