using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public SlingShooter SlingShooter;

    public TrailController TrailController;

    public List<Bird> Birds;

    public List<Enemy> Enemies;

    private Bird _shotBird;

    public BoxCollider2D TapCollider;

    private bool _isGameEnded = false;

    public float delay = 2f;

    float countdown;

    public GameObject next;

    [SerializeField] private Text status;

    [SerializeField] private GameObject panel;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {

        countdown = delay;

        for (int i = 0; i < Birds.Count ; i++)
        {
            
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShoot += AssignTrail;

        }

        for(int i = 0; i < Enemies.Count ; i++)
        {

           Enemies[i].OnEnemyDestroyed += CheckGameEnd;

        }  

        TapCollider.enabled = false;

        SlingShooter.InitiateBird(Birds[0]);

        _shotBird = Birds[0];
    }

    public void ChangeBird()
    {

        TapCollider.enabled = false;

        if(_isGameEnded)
        {
            return;
        }
        
        Birds.RemoveAt(0);

        if(Birds.Count > 0)
        {
         
            SlingShooter.InitiateBird(Birds[0]);

            _shotBird = Birds[0];

        }

        if (Enemies.Count > 0)
        {
            if (Birds.Count == 0)
            {
                countdown -= Time.deltaTime;

                _isGameEnded = true;

                status.text = "You Kalah";

                panel.gameObject.SetActive(true);

                next.gameObject.SetActive(false);

            }

        }

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>

    public void CheckGameEnd(GameObject destroyobject)
    {

        for(int i = 0; i < Enemies.Count ; i++)
        {

            if(Enemies[i].gameObject == destroyobject)
            {
                
                Enemies.RemoveAt(i);
                break;

            }
            
        }

        if (Enemies.Count == 0)
        {

            countdown -= Time.deltaTime;

            _isGameEnded = true;

            status.text = "You Menang";

            panel.gameObject.SetActive(true);

            next.gameObject.SetActive(true);

        }
        
    }

    public void AssignTrail(Bird bird)
    {
        
        TrailController.SetBird(bird);

        StartCoroutine(TrailController.SpawnTrail());

        TapCollider.enabled = true;

    }

    /// <summary>
    /// OnMouseUp is called when the user has released the mouse button.
    /// </summary>
    void OnMouseUp()
    {
        
        if(_shotBird != null)
        {
            
            _shotBird.OnTap();

        }

    }

    



}
