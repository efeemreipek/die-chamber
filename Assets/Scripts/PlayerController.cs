using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rollSpeed;

    private Rigidbody _rb;

    private bool isMoving;

    private int remainingMovePoints;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        remainingMovePoints = GameManager.Instance.GetRemainingMovePoints();
    }

    private void Update()
    {

        #region Movement
        if (isMoving) return;
        if (GameManager.Instance.IsOnMenu()) return;
        //if (remainingMovePoints <= 0) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            Assemble(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Assemble(Vector3.right);
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            Assemble(Vector3.forward);
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            Assemble(Vector3.back);
        }

        void Assemble(Vector3 dir)
        {
            var anchor = transform.position + (Vector3.down + dir) * 0.5f;
            var axis = Vector3.Cross(Vector3.up, dir);
            StartCoroutine(Roll(anchor, axis));
        }
        #endregion
    }

    IEnumerator Roll(Vector3 anchor, Vector3 axis)
    {


        isMoving = true;
        _rb.isKinematic = true;

        AudioSource audioSource = AudioManager.Instance.GetComponent<AudioSource>();
        audioSource.clip = AudioManager.Instance.diceRollingAudio;
        audioSource.pitch = UnityEngine.Random.Range(0.5f, 1f);
        audioSource.Play();

        for (int i = 0; i < (90 / rollSpeed); i++)
        {
            transform.RotateAround(anchor, axis, rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        isMoving = false;
        _rb.isKinematic = false;

        remainingMovePoints--;
        GameManager.Instance.SetRemainingMovePoints(remainingMovePoints);

        UIManager.Instance.UpdateRemainingMoveText(remainingMovePoints);

        remainingMovePoints = GameManager.Instance.GetRemainingMovePoints();
    }



}
