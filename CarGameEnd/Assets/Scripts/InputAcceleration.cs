using JoostenProductions;
using Tools;
using UnityEngine;

public class InputAcceleration : BaseInputView
{
    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        UpdateManager.SubscribeToUpdate(Move);
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(Move);
    }

    private void Move()
    {
        //Debug.Log("мува");

        var direction = Vector3.zero; 
        //direction.x = -Input.acceleration.y;
        direction.z = Input.acceleration.x;

        //Debug.Log($"acceleration.y {direction.x} --- acceleration.x {direction.z}");

        if (direction.sqrMagnitude > 1)
            direction.Normalize();

        var moveStep = direction.sqrMagnitude / 20 * _speed;

        if (direction.z > 0)
            OnRightMove(moveStep);
        else if (direction.z < 0)
            OnLeftMove(moveStep);

        //OnRightMove(direction.sqrMagnitude / 20 * _speed);
    }
}

