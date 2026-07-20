using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingNPC : NPC
{
    // Start is called before the first frame update
    void Start()
    {
        int randomIndex = Random.Range(0, 2);
        switch (randomIndex)
        {
            case 0:
                ChangeState("Sitting", 0.2f);
                break;
            case 1:
                ChangeState("Sitting_Talk", 0.2f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
