using UnityEngine;
public class TouchRay : MonoBehaviour
{
     public Camera cmr;
     void Update()
     {
          if (Input.touchCount > 0)
          {
               Touch touch = Input.GetTouch(0);
               Vector2 touchPosition = touch.position;
               Ray ray = cmr.ScreenPointToRay(touchPosition);
               RaycastHit hit;
               if (Physics.Raycast(ray, out hit))
               {
                    if (hit.collider.TryGetComponent(out Intractable intractable))
                    {
                         if (touch.phase == TouchPhase.Moved){
                              intractable.Interact(touch);
                         }
                    }
               }
          }
     }
}