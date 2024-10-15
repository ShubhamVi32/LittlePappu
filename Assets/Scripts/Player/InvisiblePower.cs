using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePower 
{
    float runningTimer;
    float timeLeft;

    public InvisiblePower(float MaxTime)
    {
        runningTimer = MaxTime;
    }

    public float MakeInivisble()
    {
        runningTimer = runningTimer - Time.deltaTime;
        return runningTimer;
    }
}
