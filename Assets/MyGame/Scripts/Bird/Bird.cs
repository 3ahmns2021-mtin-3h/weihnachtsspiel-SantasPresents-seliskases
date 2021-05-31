using UnityEngine;

public class Bird : StateMachine
{
    public AnimationCurve standardCurve;
    public AnimationCurve targetCurve;
    public float minSpeed;
    public float maxSpeed;
    public float detectionRadius;
    public float maxDistance;

    public static bool isScared;

    private void OnEnable()
    {
        WeihnachtsmannController.OnBirdScared += Scared;
    }

    private void Start()
    {
        SetState(BirdState.BackToCenter, this);
    }

    public void Scared()
    {
        if(currentState != BirdState.Targeting)
        {
            SetState(BirdState.Scared, this);
        }
    }

    private void OnDisable()
    {
        WeihnachtsmannController.OnBirdScared -= Scared;
    }
}
