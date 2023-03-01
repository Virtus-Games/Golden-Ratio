using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Comment : MonoBehaviour
{
     public float MovementSpeed;
     public bool isText = false;

     public void Initialize(bool isText, string _commentText = "")
     {
          if (isText)
          {
               this.isText = true;
               GetComponent<TextMeshProUGUI>().SetText(_commentText);
          }
     }

     // Update is called once per frame
     void Update()
     {
          if (isText)
          {
               GetComponent<RectTransform>().localPosition += new Vector3(0f, MovementSpeed * Time.deltaTime, 0f);
               if (GetComponent<RectTransform>().localPosition.y >= 150f)
                    Destroy(this.gameObject);
          }
          else
          {
               GetComponent<RectTransform>().localPosition -= new Vector3(0f, MovementSpeed * Time.deltaTime, 0f);
               if (GetComponent<RectTransform>().localPosition.y <= -600f)
                    Destroy(this.gameObject);
          }
     }
}
