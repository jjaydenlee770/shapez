using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class player_control : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;



    public GameObject bulletfab;
    public Transform point_where_mouse;

    public float bullet_speed = 12f;
    [SerializeField] private pool_bullet bulletpool;

    private Vector2 mov;
    private Camera cam;



    public int hp = 5;

    public int stamina = 5;


    public List<GameObject> hpbars;

  


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
      
    }

    // Update is called once per frame
    void Update()
    {

        //movement 

        mov = new Vector2(Input.GetAxis("Horizontal"),
        Input.GetAxis("Vertical"));

      

        //projectile finding

        if(Input.GetMouseButtonDown(0))
        {
            shoot();
        }
       


        if(hp <= 0)
        {
            Application.Quit();
        }


      

        
    }
    void FixedUpdate()
    {
        rb.linearVelocity = mov * speed;
    }


//mouseposition find the mouse position in the screen.
//direction subtracts the mousepsoition from the the shooting point to find direction of where the bullet should go.

    void shoot()
    {
        
        GameObject bullet = bulletpool.usebullet();
        bullet.transform.position = point_where_mouse.position;

       
        Vector3 mouseposition = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseposition.z = 0f;
        Vector2 direction = (Vector2)(mouseposition - point_where_mouse.position).normalized;




        Rigidbody2D rb2 = bullet.GetComponent<Rigidbody2D>();

        if(rb2 !=null)
        {
            rb2.linearVelocity = direction * bullet_speed;
            
        }

        StartCoroutine(Returndelay(bullet, 1f));



    }

    IEnumerator Returndelay(GameObject bullet, float  delay)
    {
        yield return new WaitForSeconds(delay);

        if(bullet !=null && bulletpool != null)
        {
             bulletpool.ReturnBullet(bullet);
        }
        
    }
    public void lost_hp()
    {
        if(hpbars.Count > 0)
        {
            GameObject last_jp = hpbars[hpbars.Count - 1];
            hpbars.RemoveAt(hpbars.Count - 1);
            Destroy(last_jp);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        
        if(collision.gameObject.CompareTag("Enemy"))
        {
           
            hp -= 1;
            lost_hp();
            Destroy(collision.gameObject);
           
        }
    }

    
}
