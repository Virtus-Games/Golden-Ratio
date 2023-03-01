using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotManager : Singleton<ScreenshotManager>
{

    public Texture2D TakeScreenshot(Camera _cam)
    {
        if(_cam.targetTexture != null)
        {
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = _cam.targetTexture;
            _cam.Render();
            Texture2D imageOverview = new Texture2D(_cam.targetTexture.width, _cam.targetTexture.height, TextureFormat.RGB24, false);
            imageOverview.ReadPixels(new Rect(0, 0, _cam.targetTexture.width, _cam.targetTexture.height), 0, 0);
            imageOverview.Apply();
            RenderTexture.active = currentRT;
            return imageOverview;
        }
        else
            return new Texture2D(512,512);
    }
}
