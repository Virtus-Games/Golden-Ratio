using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum IntractableType
{
     Left,
     Right,
     UNDEFINED
}

public class Intractable : MonoBehaviour
{

     public int indexLeft, indexRight, indexTop, indexDown;
     public Transform Left, Right, Top, Down;
     [Space]
     public GameObject image;
     public IntractableType intractableType = IntractableType.UNDEFINED;
     public GameObject whiteImage;
     public ImageObje ImageObje;

     [Space]
     public CharackterType charackterType;
     public Sprite mask;

     public float leftRoad, rightRoad, topRoad, downRoad;
     [Space]
     public float speed = 15;
     public float imgSlowSpx = 15;
     public Vector3 strtImage;
     public float imgMoveSpx = 15;
     private Vector3 posImage = Vector3.zero;
     public Transform CameraPosition;
     public Transform SetCameraPosition() => CameraPosition;
     private void OnEnable() => GameManager.OnGameStateChanged += OnGameStateChanged;
     private void OnGameStateChanged(GAMESTATE obj)
     {
          if (obj == GAMESTATE.VICTORY) gameObject.SetActive(false);
     }

     private void OnDisable() => GameManager.OnGameStateChanged -= OnGameStateChanged;
     public virtual void Interact(Touch touch)
     {


          Vector2 dir = (touch.deltaPosition).normalized;


          Vector3 move = Vector3.zero;

          if (valX == 0)
          {
               Open(false);
               PointManager.Instance.ImageSetThisColor();
               CharackterManager.Instance.OnLevelController();
               gameObject.SetActive(false);
               return;
          }
          else Moving(touch, dir);

     }


     private void Awake()
     {
          image.GetComponent<ImageObje>().type = charackterType;
          whiteImage.GetComponentInChildren<Image>().sprite = Player.Instance.circleIcon;

          if (charackterType == CharackterType.Kulak) whiteImage.GetComponent<RectTransform>().localScale = new Vector3(0.00302987359f, 0.00294766715f, 0.000967327971f);
          else if (charackterType == CharackterType.Cene) whiteImage.GetComponent<RectTransform>().localScale = new Vector3(0.00307152048f, 0.00610902719f, 0.0053011342f);
          else whiteImage.GetComponent<RectTransform>().localScale = new Vector3(0.00318155414f, 0.00411077822f, 0.00356714521f);

          ImageObje.transform.localScale = new Vector3(0.0513653941f, 0.0399565212f, 0.0399565212f);

     }

     public float valX = 0;

     internal void WhiteMaskEnabled()
     {
          Player.Instance.whiteMask.Active();
          Player.Instance.whiteMask.SetSprite(mask);
     }

     internal void ValuesController(float index)
     {

          valX += index;
          valX = Mathf.Clamp(valX, 0, 100);
     }
     private void Moving(Touch touch, Vector2 dir)
     {

          if (dir.x > 0)
          {
               SetBlendSheep(indexRight);
               SetBlendSheepMinus(indexLeft);
          }
          if (dir.x < 0)
          {
               SetBlendSheep(indexLeft);
               SetBlendSheepMinus(indexRight);
          }
          if (dir.y > 0)
          {
               SetBlendSheep(indexTop);
               SetBlendSheepMinus(indexDown);

          }
          if (dir.y < 0)
          {
               SetBlendSheepMinus(indexTop);
               SetBlendSheep(indexDown);
          }

          Vector2 pos = touch.deltaPosition;

          Vector3 view = image.transform.position;

          MoveAndClampImage(pos, view, imgSlowSpx);
     }
     private void MoveAndClampImage(Vector2 pos, Vector3 view, float spdx)
     {
          view.x += pos.x * spdx * Time.deltaTime;
          view.y += pos.y * spdx * Time.deltaTime;

          view.x = Mathf.Clamp(view.x, Right.position.x, Left.position.x);
          view.y = Mathf.Clamp(view.y, Down.position.y, Top.position.y);
          ImageObje.ColorControl();
          float spd = Time.fixedDeltaTime * imgMoveSpx;
          image.transform.position = Vector3.Lerp(image.transform.position, view, spd);
          image.GetComponent<ImageObje>().Bounds(view, spd);
     }
     public void Open(bool val)
     {
          if (image == null || whiteImage == null) return;
          image.SetActive(val);
          whiteImage.SetActive(val);

     }
     internal void StartPosition()
     {
          image.transform.localPosition = strtImage;
          whiteImage.transform.localPosition = strtImage;
          posImage = strtImage;
     }
     public void SetBlendSheep(int index)
     {
          float v = Player.Instance.GetBlendShapeWeight(index);
          float val = v;

          if (val == 0) return;

          Player.Instance.SetBlendSheep(index, Mathf.Lerp(val, 0, Time.deltaTime * speed));
          val = v;

          if (val < 7)
          {
               Player.Instance.SetBlendSheep(index, 0);
               Player.Instance.SliderController(v);

               // FIXME
               Player.Instance.whiteMask.Deactive();
               ValuesController(-100);
               valX = Mathf.Max(0, valX);

          }
     }
     public void SetBlendSheepMinus(int index)
     {
          if (index > Player.Instance.GetBlendShapeCount()) return;

          float val = Player.Instance.GetBlendShapeWeight(index);
          if (val == 0) return;
          Player.Instance.SetBlendSheep(index, Mathf.Lerp(val, 100, Time.deltaTime * speed));
     }
     public void ImagePosition(IndexMove indexA)
     {
          switch (indexA)
          {
               case IndexMove.Left:
                    posImage.x = leftRoad;
                    break;
               case IndexMove.Right:
                    posImage.x = rightRoad;
                    break;
               case IndexMove.Top:
                    posImage.y = topRoad;
                    break;
               case IndexMove.Down:
                    posImage.y = downRoad;
                    break;

          }
          image.transform.localPosition = posImage;

     }
}
