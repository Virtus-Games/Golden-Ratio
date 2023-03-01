using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractableTest : MonoBehaviour
{
    public int indexLeft, indexRight, indexTop, indexDown;
    public float speed = 15;
    public GameObject image;
    public Vector3 clampImage;

    [SerializeField] Post post;
    [SerializeField] Camera screenShotCam;

    public virtual void Interact(Touch touch)
    {
        Vector2 pos = touch.position;


        Vector2 dir = (touch.deltaPosition).normalized;


        float values = Player.Instance.AllValueIsZero();
        Vector3 move = Vector3.zero;

        if (values == 0)
        {
            gameObject.SetActive(false);
            Debug.Log("Done");
            //post.Initialize(ScreenshotManager.Instance.TakeScreenshot(screenShotCam),Random.Range(100,10000));
            post.gameObject.SetActive(true);
            return;
        }

        ImageMove(dir);
        ClampImage();

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
    }

    private void ImageMove(Vector2 dir)
    {
        image.transform.localPosition += new Vector3(dir.x, dir.y, 0) * speed * Time.deltaTime;
        Vector3 pos = image.transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, -clampImage.x, -clampImage.x);
        pos.y = Mathf.Clamp(pos.y, clampImage.y, clampImage.y);
        image.transform.localPosition = pos;
    }

    private void ClampImage()
    {

    }

    public void SetBlendSheep(int index)
    {
        float val = Player.Instance.GetBlendShapeWeight(index);

        if (val == 0) return;

        Player.Instance.SetBlendSheep(index, Mathf.Lerp(val, 0, Time.deltaTime * speed));

        val = Player.Instance.GetBlendShapeWeight(index);

        if (val < 7)
            Player.Instance.SetBlendSheep(index, 0);

    }

    public void SetBlendSheepMinus(int index)
    {
        float val = Player.Instance.GetBlendShapeWeight(index);
        if (val == 0) return;
        Player.Instance.SetBlendSheep(index, Mathf.Lerp(val, 100, Time.deltaTime * speed));

    }
}
