using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceColor : MonoBehaviour
{
    [SerializeField] private Material startMaterial;

    public string CurrentColor;

    private void Awake()
    {
        foreach(Transform childTransform in transform)
        {
            MeshRenderer childMeshRenderer = childTransform.GetComponent<MeshRenderer>();
            childMeshRenderer.material = startMaterial;

            Debug.Log("starting material set " + startMaterial);
        }

        CurrentColor = startMaterial.name;
    }
}
