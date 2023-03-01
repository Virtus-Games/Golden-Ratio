using UnityEngine;

public class TouchRayTest : MonoBehaviour
{

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.TryGetComponent(out InteractableTest intractable))
                {
                    if (touch.phase == TouchPhase.Moved)
                        intractable.Interact(touch);
                }
            }
        }
    }
}