using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State State;

    public void SetState(State state)
    {
        if(State != null)
        {
            StopCoroutine(State.Fly());
        }        

        State = state;
        StartCoroutine(State.Start());
    }
}
