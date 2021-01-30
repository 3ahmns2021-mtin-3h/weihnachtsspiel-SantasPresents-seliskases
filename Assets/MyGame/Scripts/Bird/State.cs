using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Bird Bird;

    public State(Bird bird)
    {
        Bird = bird;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator Fly()
    {
        yield break;
    }
}
