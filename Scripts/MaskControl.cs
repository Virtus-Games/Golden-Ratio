using UnityEngine;

public class MaskControl : MonoBehaviour
{
     public Vector3 startPos;

     public void SetPos()
     {
          transform.localPosition = startPos;
     }
}
