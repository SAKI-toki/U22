/// <summary>
/// ステート
/// </summary>
public class State<T>
{
    protected T owner;

    public State(T owner)
    {
        this.owner = owner;
    }
    //ステートに遷移するときに一度だけ呼ばれる
    public virtual void Enter() { }
    //このステートである間、毎フレーム呼ばれる
    public virtual void Execute() { }
    //ほかのステートに遷移するときに一度だけ呼ばれる
    public virtual void Exit() { }
}

