using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Post : Singleton<Post>
{
     [SerializeField] RawImage postImage;
     [SerializeField] TextMeshProUGUI likesText;

     [SerializeField] AnimationCurve likesTextIncreasingCurve;

     [SerializeField] TextMeshProUGUI userName;
     int likesCount;
     bool startAnimate;
     float increaseTime;

     public Camera cmr;

     public GameObject obj;
     public GameObject NextButton;

     [Header("Comments")]
     public string[] CommentStrings;
     public GameObject CommentPrefab;
     public Transform CommentParent;
     public float CommentSpawnRate;
     float NextTimeToSpawnComment;

     public GameObject Star;
     public Transform[] StarSpawnPoints;

     public void InitializeUserName()
     {
          string username = PlayerPrefs.GetString("Username", "Your Name");
          userName.SetText(username);
     }

     private void Update()
     {
          if (startAnimate)
          {
               int counter;
               float speed = likesTextIncreasingCurve.Evaluate(increaseTime);
               increaseTime += Time.deltaTime;
               counter = (int)(likesCount * speed);
               likesText.text = counter.ToString() + " likes";
               if (Time.time >= NextTimeToSpawnComment)
               {
                    GameObject newCommentObj = Instantiate(CommentPrefab, CommentParent);
                    int randomIndex = Random.Range(0, StarSpawnPoints.Length);
                    // GameObject newStarObj = Instantiate(Star, StarSpawnPoints[randomIndex]);
                    // float time = Random.Range(0.5f, 1.5f);
                    // newCommentObj.GetComponent<Comment>().Initialize(false);
                    string v = CommentStrings[Random.Range(0, CommentStrings.Length)];
                    newCommentObj.GetComponent<Comment>().Initialize(true, v);
                    NextTimeToSpawnComment = Time.time + CommentSpawnRate;
               }
               if (likesCount == counter)
               {
                    NextButton.gameObject.SetActive(true);
                    NextButton.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f, 10, 1);

                    startAnimate = false;
               }
          }
     }

     private void OnEnable()
     {
          GameManager.OnGameStateChanged += OnGameStateChanged;
     }

     private void OnGameStateChanged(GAMESTATE obj)
     {
          if (obj == GAMESTATE.VICTORY) StartCoroutine(WaitAndCheckCam());
     }

     IEnumerator WaitAndCheckCam()
     {

          yield return new WaitForSeconds(1f);
          Post.Instance.gameObject.SetActive(true);
          Post.Instance.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack);
          int like = PlayerPrefs.GetInt("Likes", 1230);
          Initialize(like);
          like = Mathf.Min(like + Random.Range(120, 215), 9999);
          PlayerPrefs.SetInt("Likes", like);
     }

     private void OnDisable()
     {
          GameManager.OnGameStateChanged -= OnGameStateChanged;
     }

     public void Initialize(int _likesCount)
     {
          startAnimate = true;
          InitializeUserName();
          obj.SetActive(true);
          Texture2D _texture = ScreenshotManager.Instance.TakeScreenshot(cmr);
          this.likesCount = _likesCount;
          Sprite newSprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));
          postImage.texture = _texture;
     }
}
