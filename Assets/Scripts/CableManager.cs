using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    public static CableManager Instance { get; private set; }

    [SerializeField] private List<Cable> cableList;

    public bool isEveryCableOn = false;
    private bool canThrowEvent = false;

    public event Action OnEveryCableOn;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one CableManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (canThrowEvent)
        {
            OnEveryCableOn?.Invoke();
            canThrowEvent = false;
        }

        if (isEveryCableOn) return;

        for (int i = 0; i < cableList.Count; i++)
        {
            if (!cableList[i].isCableOn) return;
            if (cableList[i].isCableOn) continue;
        }

        isEveryCableOn = true;
        canThrowEvent = true;

    }

}
