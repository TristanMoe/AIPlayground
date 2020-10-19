using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeABreak : GAction
{
    public override bool PrePerform()
    {
        return true; 
    }
    public override bool PostPerform()
    {
        agentBeliefs.RemoveState("exhausted"); 
        return true; 
    }
}
