using UnityEngine;

public static class AnimatorData
{
    public static class Params
    {
        public static readonly int Run = Animator.StringToHash(nameof(Run));
        public static readonly int Fall = Animator.StringToHash(nameof(Fall));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int Idle = Animator.StringToHash(nameof(Idle));
    }
}