using System;
using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class CameraController : Singleton<CameraController>
{
     [SerializeField]
     private Transform pointStart;
     [SerializeField]
     private Transform pointPlayerFace;
     public Transform target;
     [SerializeField]
     [Range(0, 50)]
     private float speedLerp = 2, rotationLerp = 2;
     Camera cam;
     private bool isCompletedMovement;
     private bool isOrthographic;
     public float orthographicSize = 0.66f;
     private bool isFinish = false;
     public ParticleSystem[] confeties;
     public ParticleSystem[] confetiesEnd;
     public Transform LeftCameraPos;
     public Transform RightCameraPos;
     public Transform[] cameraPosEnd;
     internal Transform endPosFinal;
     public Camera TextureCamera;
     public Transform FinalyCameraPos;

     public AudioSource audioSource;


     internal void PlayParticles()
     {
          for (int i = 0; i < confeties.Length; i++)
               confeties[i].Play();
     }
     internal void PlayParticlesEnd()
     {
          for (int i = 0; i < confetiesEnd.Length; i++)
               confetiesEnd[i].Play();
     }
     int randPos;
     public Transform GetCameraPosition()
     {
          Transform val;
          // * Don't forget to change this value if you add new camera position
          val = SetPosAndTriggerAnim(cameraPosEnd[randPos], GetAnimName());
          return val;
     }

     internal String GetAnimName()
     {
          string[] anims = new string[] { "left", "right", "front" };
          int rand = UnityEngine.Random.Range(0, anims.Length);
          return anims[rand];
     }
     Transform endPos;
     public float WaitSeconds = 5;

     public void FinaleCameraPosTrigger(Transform pos)
     {
          endPos = pos;
          StartCoroutine(FinalyEnumerator(FinalyCameraPos));
     }

     Transform SetPosAndTriggerAnim(Transform pos, String animName)
     {
          Transform val;
          val = pos;
          if (GameManager.Instance.isPlay == false)
          {

          }
          return val;
     }

     void Start()
     {
          cam = GetComponent<Camera>();
          isOrthographic = false;
          ChangeTransform(pointStart, false);
          randPos = UnityEngine.Random.Range(0, cameraPosEnd.Length);
          endPosFinal = cameraPosEnd[randPos];
          CharackterManager.Instance.camPosEnd = endPosFinal;

     }
     public void ChangeTransform(Transform position, bool isOrthographic)
     {
          if (position == null || target == position) return;
          isCompletedMovement = false;
          target = null;

          StartCoroutine(ChangeController(position, isOrthographic));
     }
     public void SetPlayerFace(Transform pos, bool val = true, bool isFinish = false)
     {
          isCompletedMovement = false;
          isOrthographic = false;
          target = pos;
          this.isFinish = isFinish;
          ChangeTransform(pos, val);
     }
     public void SetProjection(bool val)
     {
          cam.orthographic = val;
          cam.orthographicSize = orthographicSize;
     }

     private void Update()
     {
          if (isCompletedMovement) return;
          if (target == null) return;

          if (Vector3.Distance(transform.position, target.position) >= 0.01f)
          {
               transform.position = Vector3.Slerp(transform.position, target.position, Time.deltaTime * speedLerp);
               transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotationLerp);
          }
          else
          {
               SetProjection(isOrthographic);
               if (isFinish)
               {
                    GameManager.Instance.UpdateGameState(GAMESTATE.VICTORY);
                    TextureCamera.transform.position = transform.position;
                    TextureCamera.transform.rotation = transform.rotation;
                    isFinish = false;
               }
          }
     }
     IEnumerator ChangeController(Transform position, bool isOrthographic)
     {
          target = position;
          while (!isCompletedMovement)
          {
               transform.position = Vector3.Slerp(transform.position, target.position, speedLerp * Time.deltaTime);
               float val = rotationLerp * Time.deltaTime;
               if (isOrthographic) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), val);
               else transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, val);
               if (Vector3.Distance(target.position, transform.position) <= 1f) isCompletedMovement = true;
               yield return null;
          }
          SetProjection(isOrthographic);
     }

     IEnumerator FinalyEnumerator(Transform position)
     {
          bool value = true;
          target = position;
          while (value)
          {
               transform.position = Vector3.Slerp(transform.position, target.position, speedLerp * Time.deltaTime);
               float val = rotationLerp * Time.deltaTime;
               if (isOrthographic) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), val);
               else transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, val);
               if (Vector3.Distance(target.position, transform.position) <= 0.5f) value = false;
               yield return null;
          }
          target = null;

          yield return new WaitForSeconds(1f);
          PlayParticles();
          yield return new WaitForSeconds(1.5f);
          CharackterManager.Instance.PlayerSet(randPos);
          yield return new WaitForSeconds(0.02f);
          SetPlayerFace(CameraController.Instance.GetCameraPosition(), false, true);
          SetBeachPos();
     }

     internal void SetBeachPos()
     {
          transform.position = endPos.position;
          transform.rotation = endPos.rotation;
     }
}
