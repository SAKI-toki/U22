using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Enemystate
{
    Wander,
    Tracking,
    Disappear
}

public class Enemy : StatefulObjectBase<Enemy,Enemystate>
{
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {

        StateLIst.Add(new StateWander(this));
        StateLIst.Add(new StateTracking(this));
        StateLIst.Add(new StateDisappear(this));

        StateMachine = new StateMachine<Enemy>();

        ChangeState(Enemystate.Wander);
    }

    private class StateWander : State<Enemy>
    {
        public StateWander(Enemy owner) : base(owner) { }

        public override void Enter()
        {
            Debug.Log("徘徊呼ばれた");
        }

        public override void Execute()
        {
            Debug.Log("徘徊中");
            if(Input.GetKeyDown(KeyCode.Return))
            {
                owner.ChangeState(Enemystate.Tracking);
            }
        }
        public override void Exit()
        {
            Debug.Log("徘徊終了");
        }
    }

    private class StateTracking : State<Enemy>
    {
        public StateTracking(Enemy owner) : base(owner) { }

        public override void Enter()
        {
            Debug.Log("追跡開始");
        }

        public override void Execute()
        {
            Debug.Log("追跡中");
            if (Input.GetKeyDown(KeyCode.Return))
            {
                owner.ChangeState(Enemystate.Disappear);
            }
        }
        public override void Exit()
        {
            Debug.Log("追跡終了");
        }
    }

    private class StateDisappear : State<Enemy>
    {
        public StateDisappear(Enemy owner) : base(owner) { }

        public override void Enter()
        {
            Debug.Log("死亡開始");
        }

        public override void Execute()
        {
            Debug.Log("死亡中");
            if (Input.GetKeyDown(KeyCode.Return))
            {
                owner.ChangeState(Enemystate.Wander);
            }
        }
        public override void Exit()
        {
            Debug.Log("死亡終了");
        }
    }
}
