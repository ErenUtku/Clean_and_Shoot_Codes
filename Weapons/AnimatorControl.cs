using UnityEngine;

namespace Weapons
{
    public class AnimatorControl : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int Fire = Animator.StringToHash("Fire");

        public void FireAnimation()
        {
            animator.SetTrigger(Fire);
        }
    }
}
