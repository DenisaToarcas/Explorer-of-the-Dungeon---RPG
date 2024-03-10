using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_0 : Mover
{

    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;

        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }
    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.DeathMenuAnim.SetTrigger("ShowMenu");
        GameManager.instance.Respawn();
    }
   
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (isAlive)
           UpdateMotor(new Vector3(x, y, 0));

    }

    public void SwapSprite(int skinID)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinID];
    }
    public void OnLevelUp()
    {
        maxhitpoint++;
        hitpoint = maxhitpoint;

    }
    public void SetLevel(int level) //once we start the game, we give ourselves a chance to actually set the level
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();

    }
    public void Heal(int healingAmount)
    {
        if (hitpoint == maxhitpoint)
            return;

            hitpoint += healingAmount;
        if (hitpoint > maxhitpoint)
            hitpoint = maxhitpoint;
            GameManager.instance.ShowText("+" + healingAmount.ToString() + " HP", 25, Color.cyan, transform.position, Vector3.up * 30, 1.0f);
            GameManager.instance.OnHitpointChange();
    }
    public void Respawn()
    {
        Heal(maxhitpoint);
        isAlive = true;
        lastImune = Time.time;
        pushDirection = Vector3.zero;
        
    }
}
