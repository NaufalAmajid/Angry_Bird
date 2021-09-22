using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    
    public float Health = 50f;

    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };

    private bool _isHit = false;

    void OnDestroy()
    {

        if(_isHit)
        {
            
            OnEnemyDestroyed(gameObject);

        }

    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if(col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        if(col.gameObject.tag == "Bird")
        {

            _isHit = true;

            Destroy(gameObject);

        }
        else if(col.gameObject.tag == "Obstacle")
        {
            
            //Hitung damage yang diperoleh
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;

            Health -= damage;

            if(Health <= 0)
            {
                
                _isHit = true;

                Destroy(gameObject);

            }

        }
    

    }

    public void DestroyBoom()
    {

        

    }

}
