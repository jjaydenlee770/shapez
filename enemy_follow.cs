using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
public class enemy_follow : MonoBehaviour
{

    public Transform player;

    private GameObject _prefabReference;

    private shape_spawn _spawner;

    public ParticleSystem ps;




  
    

    private float speed = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
        ps = GetComponentInChildren<ParticleSystem>();

        if(player == null)
        {
            GameObject playerbackup = GameObject.Find("Player");
            player = playerbackup.transform;
        }
       
        
    }

    // Update is called once per frame
    void Update()
    {
        //finds player through an empty gameobject called waypoint and walks towards them
      transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
      
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        string tag = collision2D.gameObject.tag;
        if(tag == "Bullet")
        {

          
          
            
            ps.gameObject.SetActive(true);
            ps.Play();
            collision2D.gameObject.SetActive(false);
            

            if(_spawner != null)
            {
                 StartCoroutine(ReturnToPoolAfterDelay(ps.main.duration));

            }
           
          

           
            
      
            
        }

        IEnumerator ReturnToPoolAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            _spawner.ReturnEnemy(_prefabReference, this.gameObject);
        }
    }
        //recyclles player back to list
        public void Initialize(shape_spawn spawner, GameObject prefab)
        {
            _spawner = spawner;
            _prefabReference = prefab;
        }   
    }

