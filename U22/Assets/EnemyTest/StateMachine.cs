/// <summary>
/// ステートマシン
/// </summary>
public class StateMachine<T>
{
    private State<T> CurrentState;

    public StateMachine()
    {
        CurrentState = null;
    }

    public State<T> GetCurrentState
    {
        get { return CurrentState; }
    }
    /// <summary>
    /// ステートの遷移
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(State<T> state)
    {
        if(CurrentState!=null)
        {
            CurrentState.Exit();
        }
        CurrentState = state;
        CurrentState.Enter();
    }

    /// <summary>
    /// 現在のステート
    /// </summary>
    public void Update()
    {
        if(CurrentState!=null)
        {
            CurrentState.Execute();
        }
    }
}

