using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Material changedMaterial;
    [SerializeField] private GameObject colorChangeParticleEffect;

    private void Start()
    {
        colorChangeParticleEffect.GetComponent<ParticleSystemRenderer>().material = changedMaterial;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform parentCubeTransform = other.gameObject.transform;
            foreach(Transform child in parentCubeTransform)
            {
                MeshRenderer childMeshRenderer = child.GetComponent<MeshRenderer>();

                //if (childMeshRenderer.material == changedMaterial) continue;

                childMeshRenderer.material = changedMaterial;
                Debug.Log("material changed to " + changedMaterial);
            }

            DiceColor diceColor = other.GetComponent<DiceColor>();
            diceColor.CurrentColor = changedMaterial.name;
        }
    }
}
