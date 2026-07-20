using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingNPC : NPC
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeState("Talking", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
