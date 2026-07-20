using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class level_change : MonoBehaviour
{


    public bullet targets;
   
    public TextMeshProUGUI text;
    public int quota = 10;
    public int lvl = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
        
            if(targets.hits >= quota)
              {
                 targets.hits = 0;  
                 lvl++;
              }
              
              
             text.text = "Level: " + lvl.ToString() + " Counter: " + targets.hits.ToString();
             
        


    }
}
