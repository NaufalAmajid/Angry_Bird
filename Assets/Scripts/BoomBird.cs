using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBird : Bird
{

    public float fieldofImpact;

    public float force;

    public LayerMask LayerHit;

    public GameObject ExplosionEffect;

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {

        explode();
        
    }

    public void explode()
    {

        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofImpact, LayerHit);

        foreach (Collider2D obj in objects)
        {
            
            Vector2 direction = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);

        }

        GameObject ExplosionEffectins = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);

        Destroy(ExplosionEffectins, 50);

        Destroy(gameObject);

    }

    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    public void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, fieldofImpact);

    }




}
