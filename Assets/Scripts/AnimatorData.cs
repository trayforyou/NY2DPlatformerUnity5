using UnityEngine;

public static class AnimatorData
{
    public static class Params
    {
        public static readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));
        public static readonly int IsFall = Animator.StringToHash(nameof(IsFall));
        public static readonly int IsJump = Animator.StringToHash(nameof(IsJump));
    }
}