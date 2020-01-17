using System.Collections;

namespace UnityPureMVC.Core.Model.VO
{
    internal sealed class RequestStartCoroutineVO
    {
        internal IEnumerator coroutine { get; set; }
        internal object args { get; set; }
    }
}