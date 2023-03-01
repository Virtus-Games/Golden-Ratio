using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PointManager : Singleton<PointManager>
{
     public GameObject pointPrefab;
     public List<GameObject> points = new List<GameObject>();
     public int index;
     public Color color;
     public Sprite trueBox;
     void OnEnable() => GameManager.OnGameStateChanged += OnGameStateChanged;

     private void OnDisable() => GameManager.OnGameStateChanged -= OnGameStateChanged;

     private void Start()
     {
          Starting();
     }
     private void OnGameStateChanged(GAMESTATE obj)
     {
          if (obj == GAMESTATE.PLAY)
          {
               // for (int i = 0; i < CharackterManager.Instance.GetColumnIndexAtPref(); i++)
               //      ImageSetThisColor(false);
          }

          if (obj == GAMESTATE.PLAY)
          {
               foreach (var point in points) point.SetActive(true);
          }
          if (obj == GAMESTATE.VICTORY)
          {
               for (int i = 0; i < points.Count; i++)
                    Destroy(points[i]);
               points.Clear();
               index = 0;
          }
     }

     private void Starting()
     {
          index = 0;
          spriteIndex = 0;

     }

     internal void Sprites()
     {
          for (int i = 0; i < CharackterManager.Instance.GetElementCount(); i++) InstantiateAndAddPoint();

          for (int i = 0; i < CharackterManager.Instance.GetSprites().Length; i++)
          {
               CharackterType type = CharackterManager.Instance.GetSprites()[i];

               points[spriteIndex].GetComponent<Point>().SetSprite(CharackterManager.Instance.GetSprite(type));

               spriteIndex++;
          }

          spriteIndex = 0;

          for (int k = 0; k < CharackterManager.Instance.GetColumnIndexAtPref(); k++) ImageSetThisColor();

     }

     int spriteIndex = 0;

     public void InstantiateAndAddPoint()
     {
          GameObject point = Instantiate(pointPrefab, transform.position, Quaternion.identity);
          point.transform.SetParent(transform);
          point.transform.localScale = Vector3.one;
          point.SetActive(false);
          points.Add(point);


     }

     private void SetSprite()
     {

          if (spriteIndex > points.Count) spriteIndex = 0;

          points[spriteIndex].GetComponent<Point>().SetSprite(trueBox);
          
          if (spriteIndex == 0) points[spriteIndex].GetComponent<Point>().Close();

          spriteIndex++;

     }

     public void ImageSetThisColor() => SetSprite();

     IEnumerator DoShake()
     {
          float duration = 0.5f;
          float strength = 0.5f;
          int vibrato = 10;
          float randomness = 90;
          bool fadeOut = true;
          points[index].transform.DOShakePosition(duration, strength, vibrato, randomness, fadeOut);
          yield return new WaitForSeconds(duration);
          CameraController.Instance.PlayParticles();

     }

     internal void AllPointsTrueBox()
     {
          foreach (var point in points)
          {
               point.GetComponent<Point>().SetSprite(trueBox);
               point.GetComponent<Point>().Close();
          }
     }

     internal void AllPointsDisableBox()
     {
          foreach (var point in points) point.SetActive(false);
     }
}
