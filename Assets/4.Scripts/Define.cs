using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{  
    public enum PlayerState
    {
        Idle,
        Crouch,
        Walk,
        Run,
        Jump
    }
    
    public enum PlayerAction
    {
        None,
        Attack,
        Skill
    }
}
