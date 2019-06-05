using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステートを持つオブジェクトの基底
/// </summary>
public abstract class StatefulObjectBase<T,TEnum> : MonoBehaviour
    where T:class where TEnum:System.IConvertible
{
    protected List<State<T>> StateLIst = new List<State<T>>();

    protected StateMachine<T> StateMachine;

    public virtual void ChangeState(TEnum state)
    {
        if(StateMachine==null)
        {
            return;
        }
        StateMachine.ChangeState(StateLIst[state.ToInt32(null)]);
    }

    public virtual bool IsCurrentState(TEnum state)
    {
        if(StateMachine==null)
        {
            return false;
        }
        return StateMachine.GetCurrentState == StateLIst[state.ToInt32(null)];
    }

    protected virtual void Update()
    {
        if(StateMachine!=null)
        {
            StateMachine.Update();
        }
    }
}
