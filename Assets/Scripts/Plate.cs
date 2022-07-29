using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private int plateValue;
    [SerializeField] private Material plateMaterial;
    [Header("Cable")]
    [SerializeField] private Cable nearCable;

    public string PlateColor;


    private void Awake()
    {
        MeshRenderer childMeshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        childMeshRenderer.material = plateMaterial;
        PlateColor = plateMaterial.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        DiceSide diceSide = other.GetComponent<DiceSide>();
        DiceColor diceColor = diceSide.GetComponentInParent<DiceColor>();
        if (diceSide.SideValue == plateValue && diceColor.CurrentColor == PlateColor)
        {
            Debug.Log("Same Value. plateValue: " + plateValue + " sideValue: " + diceSide.SideValue);
            nearCable.GetComponent<MeshRenderer>().material = nearCable.cableOnMaterial;

            AudioSource audioSource = AudioManager.Instance.GetComponent<AudioSource>();
            audioSource.clip = AudioManager.Instance.platePressedAudio;
            audioSource.pitch = 1f;
            audioSource.Play();

            nearCable.isCableOn = true;

        }
    }
}
