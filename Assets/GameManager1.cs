using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    private Spawner spawner;

    // Start is called before the first frame update
   private void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
