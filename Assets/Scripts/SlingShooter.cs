using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShooter : MonoBehaviour
{   

    public CircleCollider2D Collider;
    public LineRenderer Trajectory;
    private Vector2 _startPos;

    [SerializeField] private float _radius = 0.75f;

    [SerializeField] private float _throwSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {   
        _startPos = transform.position;
    }   

    /// <summary>
    /// OnMouseUp is called when the user has released the mouse button.
    /// </summary>

    private Bird _bird;

    void OnMouseUp()
    {

        Collider.enabled = false;

        Vector2 velocity = _startPos - (Vector2)transform.position;

        float distance = Vector2.Distance(_startPos, transform.position);

        _bird.Shoot(velocity, distance, _throwSpeed);

        //mengembalikan ketapel keposisi awal
        gameObject.transform.position = _startPos;

        Trajectory.enabled = false;

    }

    public void InitiateBird(Bird bird){

        _bird = bird;

        _bird.MoveTo(gameObject.transform.position, gameObject);

        Collider.enabled = true;

    }

    /// <summary>
    /// OnMouseDrag is called when the user has clicked on a GUIElement or Collider
    /// and is still holding down the mouse.
    /// </summary>
    void OnMouseDrag()
    {
        
        //mengubah posisi mouse ke world position
        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //supaya 'karet' ketapel berada dalam radius yang ditentukan
        Vector2 dir = p - _startPos;
        
        if(dir.sqrMagnitude > _radius)

            dir = dir.normalized * _radius;

        transform.position = _startPos + dir;

        float distance = Vector2.Distance(_startPos, transform.position);

        if (!Trajectory.enabled)
        {
            Trajectory.enabled = true;
        }

        DisplayTrajectory(distance);

    }

    void DisplayTrajectory(float distance)
    {

        if (_bird == null)
        {

            return;

        }

        Vector2 velocity = _startPos - (Vector2)transform.position;

        int segmentCount = 5;

        Vector2[] segment = new Vector2[segmentCount];

        //posisi awal trajectory sama dengan posisi mouse dari player
        segment[0] = transform.position;

        //velocity awal
        Vector2 segVelocity = velocity * _throwSpeed * distance;

        for (int i = 1; i < segmentCount; i++)
        {
            
            float elapsedTime = i * Time.fixedDeltaTime * 5;

            segment[i] = segment[0] + segVelocity * elapsedTime + 0.5f * Physics2D.gravity * Mathf.Pow(elapsedTime, 2);

        }

        Trajectory.positionCount = segmentCount;

        for (int i = 0; i < segmentCount ; i++)
        {
            
            Trajectory.SetPosition(i, segment[i]);

        }

    }


}
