using Controllers.State;
using UnityEngine;

public class LevelColliderControl : MonoBehaviour
{
    [SerializeField] private States stateType;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        switch (stateType)
        {
            case States.Collect:
                StateManager.Instance.stateMachine.ChangeState(new CollectState());
                break;
            case States.Fight:
                StateManager.Instance.stateMachine.ChangeState(new FightState());
                break;
            case States.Score:
                StateManager.Instance.stateMachine.ChangeState(new ScoreState());
                break;
            default:
                Debug.LogWarning($"Couldn't find the proper state for {stateType}");
                break;
        }

        this.gameObject.GetComponent<Collider>().enabled = false; //Already Passed
    }
}

