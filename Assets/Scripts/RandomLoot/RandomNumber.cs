using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        randomNum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void randomNum()
    {
        for(int i=0; i<15; i++)
        {
            int x = Random.Range(1, 100);
            print(x);
        }
        
    }
}
