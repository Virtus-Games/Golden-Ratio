using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageObje : MonoBehaviour
{
     public GameObject WhileImage;
     public Transform forwardsPrefab;
     public List<Transform> forwards = new List<Transform>();
     public float distanceX = 0.1f;
     public float distanceY = 0.1f;
     public CharackterType type;
     private float lastDis;
     public Image colorC;
     float dis;


     private Image circleIcon;

     private void Start()
     {
          lastDis = 0;
          dis = 0;
          for (int i = 0; i < forwards.Count; i++)
               forwards[i].transform.rotation = Quaternion.identity;

          

          colorC.sprite = Player.Instance.circleIcon;
          colorC.color = Color.black;
          transform.localScale = new Vector3(0.0406711102f,0.0494901463f,0.0494901463f);
     }



     private void FixedUpdate()
     {
          if (forwards.Count == 0) return;

          for (int i = 0; i < forwards.Count; i++)
          {
               float distanceMe = Vector3.Distance(transform.position, forwards[i].position);


               // if (distanceMe > 0.05f)
                    RotateToRectObjToWhileObj(forwards[i]);

          }
     }

     void RotateToRectObjToWhileObj(Transform icon)
     {
          Vector3 dir = WhileImage.transform.position - icon.position;


          float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
          Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
          Vector3 rotX = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z);

          // ICON ROTATİON Y = 0
          if (type == CharackterType.Kulak)
          {
               rot.y = 0;
               rot.x = 0;
               icon.transform.rotation = Quaternion.identity;
          }
          else
               icon.rotation = Quaternion.Euler(rotX.x, 0, rotX.z);

          
          icon.localPosition = Vector3.zero;

     }


     public void Bounds(Vector3 posTo, float speed)
     {
          foreach (var trans in forwards)
          {
               trans.transform.position = Vector3.Lerp(trans.position, posTo, speed);
          }
     }

     private void ChangeColorComponentImage(Transform icon)
     {
          icon.GetComponent<Image>().color = colorC.color;
     }

     public void ColorControl()
     {
          if (WhileImage == null || colorC == null) return;

          dis = Vector3.Distance(transform.position, WhileImage.transform.position);


          if (dis > lastDis)
               colorC.color = Color.Lerp(colorC.color, Color.red, 0.4f);
          else
               colorC.color = Color.Lerp(colorC.color, Color.green, 0.4f);

          lastDis = dis;
     }

}
