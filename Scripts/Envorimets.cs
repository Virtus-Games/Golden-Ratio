using System;
using UnityEngine;

public class Envorimets : MonoBehaviour
{
     public MeshRenderer meshRenderer;

     private void Awake()
     {
          meshRenderer.material.color = GameManager.Instance.GetColor();
     }
     private void OnEnable() => GameManager.OnColorChanged += UpdateColor;

     private void UpdateColor(Color obj)
     {
          meshRenderer.material.color = obj;
     }

     private void OnDisable() => GameManager.OnColorChanged -= UpdateColor;
}