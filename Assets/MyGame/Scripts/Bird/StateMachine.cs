using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State State;

    public enum States
    {
        Searching,
        Targeting,
        BackToCenter,
        CaughtPresents,
        Scared
    }
    public States currentState;

    public void SetState(States inputState, Bird bird)
    {
        if(State!= null)
        {
            StopCoroutine(State.Fly());
        }

        switch(inputState)
        {
            case States.Searching:
                State = new Searching(bird);
                break;
            case States.Targeting:
                State = new Targeting(bird);
                break;
            case States.BackToCenter:
                State = new BackToCenter(bird);
                break;
            case States.CaughtPresents:
                State = new CaughtPresents(bird);
                break;
            case States.Scared:
                State = new Scared(bird);
                break;
        }

        currentState = inputState;
        StartCoroutine(State.Start());
    }
}
