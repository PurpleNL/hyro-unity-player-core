using UnityEngine;
namespace UnityPureMVC.Core.Libraries.UnityLib.Utilities
{
    public class Rotate : MonoBehaviour
    {
        public Vector3 velocity;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(velocity);
        }
    }
}