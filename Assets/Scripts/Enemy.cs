using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Coding the movement of the enemy on the map

    //Experience
    public int xpValue = 1;

    //Logic
    public float triggerLenght = 1; //if the distance between the enemy and the player <= 1, he starts chasing him
    public float chaseLenght = 5;
    private bool chasing; //to know wheter the player is being chased/not rn
    public bool collidingWithPlayer; //a way to know if the enenmy is colliding with the player
    private Transform playerTransform;
    private Vector3 startingPosition;

    //HitBox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //is the player in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
                chasing = true; //we are checking wheter the player is between the triggerLenght and the chaseLenght

            if (chasing)
            {
                if (!collidingWithPlayer) //if the player is close enough to the enemy, but they aren't touching (colliding with) eachother
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized); //the enemy is going to go in the direction of the playerSSS
                }

                else
                {
                    UpdateMotor(startingPosition - transform.position); //the enemy is going back to where he belongs
                }
            }

            else
            {
                UpdateMotor(startingPosition - transform.position);
                chasing = false;
            }

            //checking for overlaps
            collidingWithPlayer = false;
            // collision work
            boxCollider.OverlapCollider(filter, hits);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i] == null)
                    continue;

                if (hits[i].name == "player_0" && hits[i].tag == "Fighter") //wherever he writes "Player", mine will be "player_0"
                {
                    collidingWithPlayer = true;
                }

                //The array is not cleaned up, so we do it ourselves
                hits[i] = null;
            }

        }

    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }

}
