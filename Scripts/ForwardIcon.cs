
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ForwardIcon : MonoBehaviour
{
     Image image;
     void Start()
     {
          image = GetComponent<Image>();
          image.sprite = Player.Instance.forwardIcon;
          image.color = Color.black;
          transform.localPosition = Vector3.zero;
          image.GetComponent<RectTransform>().localScale = Player.Instance.forwardScale;
     }


     // Update is called once per frame
     float time = .8f;
     public float timeDistance = 1.5f;
     void FixedUpdate()
     {
          if (image.color.a < 0.5f)
          {
               if (time > 0)
                    time -= Time.fixedDeltaTime;
               else
               {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
                    time = timeDistance;
               }
          }
          else
          {    
               if (time > 0)
                    time -= Time.fixedDeltaTime;
               else
               {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, .4f);
                    time = timeDistance;
               }
          }
     }
}
