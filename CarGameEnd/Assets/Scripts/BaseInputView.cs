using Tools;
using UnityEngine;

public abstract class BaseInputView : MonoBehaviour
{
    private IReadOnlySubscriptionProperty<float> _leftMove;
    private IReadOnlySubscriptionProperty<float> _rightMove;
    
    protected float _speed;
    
    public virtual void Init(IReadOnlySubscriptionProperty<float> leftMove, IReadOnlySubscriptionProperty<float> rightMove, float speed)
    {
        _leftMove = leftMove;
        _rightMove = rightMove;
        _speed = speed;
    }
    
    protected void OnLeftMove(float value)
    {
        _leftMove.Value = value;
    }

    protected void OnRightMove(float value)
    {
        _rightMove.Value = value;
    }
}

