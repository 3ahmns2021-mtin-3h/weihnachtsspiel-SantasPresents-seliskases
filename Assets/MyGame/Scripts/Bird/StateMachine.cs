using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State State;

    public enum BirdState
    {
        Searching,
        Targeting,
        BackToCenter,
        CaughtPresents,
        Scared
    }
    public BirdState currentState;

    public void SetState(BirdState inputState, Bird bird)
    {
        if(State!= null)
        {
            StopCoroutine(State.Fly());
        }

        switch(inputState)
        {
            case BirdState.Searching:
                State = new Searching(bird);
                break;
            case BirdState.Targeting:
                State = new Targeting(bird);
                break;
            case BirdState.BackToCenter:
                State = new BackToCenter(bird);
                break;
            case BirdState.CaughtPresents:
                State = new CaughtPresents(bird);
                break;
            case BirdState.Scared:
                State = new Scared(bird);
                break;
        }

        currentState = inputState;
        StartCoroutine(State.Start());
    }
}
