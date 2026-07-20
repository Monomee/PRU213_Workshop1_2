using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeringNPC : NPC
{
    // Start is called before the first frame update
    void Start()
    {
        int randomIndex = Random.Range(0, 2);
        switch(randomIndex) 
        {
            case 0:
                ChangeState("Cheering", 0.2f);
                break;
            case 1:
                ChangeState("Cheering_1", 0.2f);
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
