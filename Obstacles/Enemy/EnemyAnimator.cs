using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private List<Animator> animators;
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Kill = Animator.StringToHash("Kill");

    public void TriggerHit()
    {
        foreach (var animator in animators)
        {
            animator.SetTrigger(Hit);
        }
    }

    public void KillCharacter()
    {
        foreach (var animator in animators)
        {
            animator.SetBool(Kill,true);
        }
    }
}
