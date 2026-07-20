using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingNPC : NPC
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeState("Walking", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
