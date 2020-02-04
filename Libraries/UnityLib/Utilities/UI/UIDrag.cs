using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityPureMVC.Core.Libraries.UnityLib.Utilities.UI
{
    public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        public RectTransform elementToDrag;

        private Vector2 offset = new Vector2();
        private Vector2 newPosition = new Vector2();
        private Vector2 delta = new Vector2();
        private Vector3[] corners = new Vector3[4];

        private void Start()
        {
            if(elementToDrag == null)
            {
                elementToDrag = GetComponent<RectTransform>();
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            offset = new Vector2(elementToDrag.position.x, elementToDrag.position.y) - eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            newPosition = elementToDrag.position;

            // Get movement delta
            delta.x = elementToDrag.position.x - (eventData.position.x + offset.x);
            delta.y = elementToDrag.position.y - (eventData.position.y + offset.y);

            // Get transform corners (Clockwise from bottom left) 
            elementToDrag.GetWorldCorners(corners);

            if (corners[2].x - delta.x < Screen.width && corners[1].x -  delta.x > 0)
            {
                newPosition.x = elementToDrag.position.x - delta.x;
            }
            if (corners[1].y - delta.y < Screen.height && corners[0].y - delta.y > 0)
            {
                newPosition.y = elementToDrag.position.y - delta.y;
            }

            elementToDrag.position = newPosition;
        }
    }
}
