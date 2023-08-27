using System;
using UnityEngine;

namespace Controllers.State
{
    public class StateManager : MonoBehaviour
    {
        public StateMachine stateMachine;
        public States currentState;

        public static StateManager Instance;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            stateMachine = new StateMachine();
            stateMachine.Initialize();
        }

        private void Update()
        {
            stateMachine.Update();
        }
    }

    public enum States
    {
        Collect,
        Fight,
        Score
    }
}
