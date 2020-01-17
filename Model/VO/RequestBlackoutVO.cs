using UnityEngine.Events;

namespace UnityPureMVC.Core.Model.VO
{
    internal enum FadeDirection
    {
        IN,
        OUT
    }

    internal sealed class RequestBlackoutVO
    {
        internal FadeDirection fadeDirection { get; set; }
        internal UnityAction callback { get; set; }
        internal object args { get; set; }
        internal float delay { get; set; }
        internal bool auto { get; set; }
    }
}