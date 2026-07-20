using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using JetBrains.Annotations;
public class shape_spawn : MonoBehaviour
{

    public List<GameObject> shapes;
   

    public BoxCollider2D spawn;


    //pooling
    //requires setup
    private Dictionary<GameObject, IObjectPool<GameObject>> _pools = new();


    //setup
    void Awake()
    {
        //each shape enemy in the list is given these cahristeristics
        //creatfun spawns the shape. actionaget sets to true. aciton onrealse doeos the opisites. actionon destroy destroy
        foreach (GameObject shape in shapes)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>
            (
                createFunc: () => Instantiate(shape),
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj)
            );


            _pools.Add(shape, pool);
        }
    }


        public GameObject GetEnemy(GameObject shape)
        {
        return _pools[shape].Get();
        }

        public void ReturnEnemy(GameObject shape, GameObject obj)
        {
        _pools[shape].Release(obj);
        }

    



  
    void Start()
    {
        StartCoroutine(spawn_time());
    }



    // Update is called once per frame
   
    IEnumerator spawn_time()
    {
        while(true)
        {
            spawn_enemies();
            yield return new WaitForSeconds(3f);
        }
     
    }

    void spawn_enemies()
    {
        Bounds a = spawn.bounds;
        Vector3 randompos = new Vector3(Random.Range(a.min.x, a.max.x), Random.Range(a.min.y, a.max.y), 0f);

        int rand_index = Random.Range(0, shapes.Count);
        GameObject chosenone = shapes[rand_index];

        GameObject prefab = _pools[chosenone].Get();

        prefab.transform.position = randompos;
        prefab.transform.rotation = Quaternion.identity;

        if(prefab != null)
        {
            prefab.GetComponent<enemy_follow>().Initialize(this, chosenone);
        }

     
    
    
    }
}
