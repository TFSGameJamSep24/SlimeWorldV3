using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Player_Movement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement Properties")]
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private Transform gimbal;

    [Header("Rotation Properties")]
    [SerializeField] private Transform model;
    [SerializeField] private float rotationValue;
    private float turnSmoothVel;

    [Header("Ground Check Properties")]
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private LayerMask groundLayer;

    private Animator anim;
    private Player_Blower pb;
    private bool isMoving = false;

    private bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        LevelManager.instance.OnLevelEnd += LevelEnd;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canMove) return;

        //if (!Physics.Raycast(transform.position, -transform.up, groundCheckDistance, groundLayer)) Debug.Log("Not near ground");

        rb.AddForce(gimbal.forward * movementSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        rb.AddForce(gimbal.right * movementSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);

        TurnModel();

        if (Input.GetKeyDown(KeyCode.L)) anim.Play("Dance");
    }

    private void TurnModel()
    {
        Vector3 targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (targetDirection == Vector3.zero)
        {
            if (isMoving)
            {
                isMoving = false;
                anim.CrossFade("Idle_Equipped", 0.1f);
            }
            return;
        }

        if (!isMoving)
        {
            isMoving = true;
            anim.CrossFade("Running_Equipped", 0.1f);
        }

        Quaternion toRotation = Quaternion.LookRotation(gimbal.forward * Input.GetAxis("Vertical") + gimbal.right * Input.GetAxis("Horizontal"), transform.up);

        model.rotation = Quaternion.RotateTowards(model.rotation, toRotation, rotationValue * Time.deltaTime);
    }

    private void LevelEnd()
    {
        Debug.Log("This is called");

        canMove = false;
        anim.Play("Victory");
        LevelManager.instance.OnLevelEnd -= LevelEnd;
    }
}
