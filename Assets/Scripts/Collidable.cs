using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private readonly Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // collision work
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            NewMethod(i);

            //The array is not cleaned up, so we do it ourselves
            hits[i] = null;
        }

        void NewMethod(int i)
        {
            OnCollide(hits[i]);
        }
    }

        protected virtual void OnCollide(Collider2D coll)
       {
        Debug.Log("OnCollide was not implemented in" + this.name);
       }
    }

