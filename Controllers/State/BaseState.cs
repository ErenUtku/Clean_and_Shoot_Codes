using System;
using UnityEngine;

namespace Controllers.State
{
    public abstract class BaseState
    {
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
    }


    public class CollectState : BaseState
    {
        public static Action<bool> CollectStateActive;
        public override void EnterState()
        {
            Debug.Log($"Enter State of {this}");

            StateManager.Instance.currentState = States.Collect;
            
            CollectStateActive?.Invoke(true);
        }

        public override void UpdateState()
        {
            Debug.Log($"Update State of {this}");
        }

        public override void ExitState()
        {
            Debug.Log($"Exit State of {this}");
            
            CollectStateActive?.Invoke(false);
        }
    }
    
    public class FightState : BaseState
    {
        public static Action<bool> FightStateActive;
        public override void EnterState()
        {
            Debug.Log($"Enter State of {this}");
            
            StateManager.Instance.currentState = States.Fight;
            
            FightStateActive?.Invoke(true);
        }

        public override void UpdateState()
        {
            Debug.Log($"Update State of {this}");
        }

        public override void ExitState()
        {
            Debug.Log($"Exit State of {this}");
            
        }
    }
    
    public class ScoreState : BaseState
    {
        public static Action<bool> ScoreStateActive;
        public override void EnterState()
        {
            Debug.Log($"Enter State of {this}");
            
            StateManager.Instance.currentState = States.Score;
        }

        public override void UpdateState()
        {
            Debug.Log($"Update State of {this}");
        }

        public override void ExitState()
        {
            Debug.Log($"Exit State of {this}");
            
            ScoreStateActive?.Invoke(false);

            LevelManager.Instance.LevelComplete();
        }
    }

   
}