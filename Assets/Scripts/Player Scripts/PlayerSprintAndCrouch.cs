using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public float sprint_Speed = 8.0f;
    public float move_Speed = 4f;
    public float crouch_Speed = 2f;

    private Transform look_Root;
    private float stand_Height = 0.5f;
    private float crouch_Height = -0.2f;

    private bool is_Crouching;

    private PlayerFootsteps player_Footsteps;

    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.1f;
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;

    private float walk_Step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.3f;
    private float crouch_Step_Distance = 0.5f;

    private PlayerStats playerStats;
    private float sprint_Value = 100f;
    private float sprint_Threshold = 10f;

    private CharacterController character_Controller;


    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        look_Root = transform.GetChild(0);
        player_Footsteps = GetComponentInChildren<PlayerFootsteps>();
        playerStats = GetComponent<PlayerStats>();
        character_Controller = GetComponent<CharacterController>();

    }
     void Start()
    {
        player_Footsteps.volume_Min = walk_Volume_Min;
        player_Footsteps.volume_Max = walk_Volume_Max;
        player_Footsteps.step_Distance = walk_Step_Distance;
    }


    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        if(sprint_Value > 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching)
            {

                playerMovement.speed = sprint_Speed;

                player_Footsteps.step_Distance = sprint_Step_Distance;
                player_Footsteps.volume_Min = sprint_Volume;
                player_Footsteps.volume_Max = sprint_Volume;

            }
        }
        
         if(Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching)
            {
            playerMovement.speed = move_Speed;

            player_Footsteps.volume_Min = walk_Volume_Min;
            player_Footsteps.volume_Max = walk_Volume_Max;
            player_Footsteps.step_Distance = walk_Step_Distance;
        }

         if(Input.GetKey(KeyCode.LeftShift) && !is_Crouching && character_Controller.velocity.sqrMagnitude > 0)
        {
            sprint_Value -= sprint_Threshold * Time.deltaTime;

            if(sprint_Value <= 0f)
            {
                sprint_Value = 0f; 
                playerMovement.speed = move_Speed;

                player_Footsteps.volume_Min = walk_Volume_Min;
                player_Footsteps.volume_Max = walk_Volume_Max;
                player_Footsteps.step_Distance = walk_Step_Distance;
                playerStats.Display_StaminaStats(sprint_Value);
            }
        }
        else
        {
            }
            if(sprint_Value != 100)
            {
                sprint_Value += (sprint_Threshold / 2f) * Time.deltaTime;
            if (sprint_Value > 100)
            {
                sprint_Value = 100f;
            }
            playerStats.Display_StaminaStats(sprint_Value);

            
        }
        
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (is_Crouching)
            {
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
                playerMovement.speed = move_Speed;

                player_Footsteps.volume_Min = walk_Volume_Min;
                player_Footsteps.volume_Max = walk_Volume_Max;
                player_Footsteps.step_Distance = walk_Step_Distance;

                is_Crouching = false;
            }

            else
            {
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                playerMovement.speed = crouch_Speed;

                player_Footsteps.step_Distance = crouch_Step_Distance;
                player_Footsteps.volume_Min = crouch_Volume;
                player_Footsteps.volume_Max = crouch_Volume;
               
                is_Crouching = true;

            }
        }
    }
}
