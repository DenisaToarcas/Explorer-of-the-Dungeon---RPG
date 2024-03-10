using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    private const float Duration = 1.5f;
    public Sprite emptyChest;
    public int pesosAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.pesos += pesosAmount;
            GameManager.instance.ShowText("+" + pesosAmount + " lei!", 25, Color.green, transform.position, Vector3.up * 25, Duration);
        }
    }
}
