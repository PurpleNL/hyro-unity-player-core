using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityPureMVC.Core.Libraries.UnityLib.Utilities.UI
{
    public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        public Transform elementToDrag;

        private Vector2 offset = new Vector2();

        private void Start()
        {
            if(elementToDrag == null)
            {
                elementToDrag = transform;
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            offset = new Vector2(elementToDrag.position.x, elementToDrag.position.y) - eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            elementToDrag.position = eventData.position + offset;
        }
    }
}
