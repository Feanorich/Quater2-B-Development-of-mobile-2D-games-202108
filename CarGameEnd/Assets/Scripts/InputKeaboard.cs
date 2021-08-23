using JoostenProductions;
using Tools;
using UnityEngine;

public class InputKeaboard : BaseInputView
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
        Debug.Log("клава-мува");

        var direction = Vector3.zero;
        direction.x = -Input.GetAxis("Vertical");
        direction.z = Input.GetAxis("Horizontal");

        if (direction.sqrMagnitude > 1)
            direction.Normalize();

        Debug.Log(direction.z);
        if (direction.z < 0.0f)
        {
            OnLeftMove(direction.sqrMagnitude / 20 * _speed);
        }
        else
        {
            OnRightMove(direction.sqrMagnitude / 20 * _speed);
        }
        
    }
}
