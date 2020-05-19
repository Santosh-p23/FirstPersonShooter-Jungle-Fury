using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character_Controller;
    private Vector3 move_Direction;

    public float speed = 4.0f;
    private float gravity = 20.0f;

    public float jump_Force = 10.0f;
    private float vertical_Velocity;

    void Awake()
    {
        character_Controller = GetComponent<CharacterController>();


    }


    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL),0f,Input.GetAxis(Axis.VERTICAL));
        move_Direction = transform.TransformDirection(move_Direction);

        move_Direction = move_Direction * speed * Time.deltaTime;

        ApplyGravity();
        character_Controller.Move(move_Direction);


    }

    void ApplyGravity()
    {
        if (character_Controller.isGrounded)
        {
            vertical_Velocity -= gravity * Time.deltaTime;
            PlayerJump();
        }
        else
        {
            vertical_Velocity -= gravity * Time.deltaTime;
        }

        move_Direction.y = vertical_Velocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if(character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Velocity = jump_Force;
        }
    }
}
