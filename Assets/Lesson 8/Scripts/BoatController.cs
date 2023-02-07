using System;
using System.Collections;
using System.Collections.Generic;
using lesson2;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public CharacterController characterController;
    public Transform PlayerPosition;
    public float speed = 3;
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;
    
    private Transform cameraHolder;

    private bool isPlayerOnBoard;
    
    private GameObject playerGo;
    private PlayerMovement player;

    private LoadNewIsland IslandLoader;
    private 
    void Start()
    {
        IslandLoader = GetComponent<LoadNewIsland>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOnBoard)
        {
            Move();
            Rotate();
        }

        
        if (Input.GetKeyDown(KeyCode.E) && player != null)
        {
            Debug.Log("E key pressed");
            if (isPlayerOnBoard)
            {
                //exit
                ExitFromBoat(player, playerGo);
            }
            else
            {
                //enter
                SitOnBoat(player, playerGo);
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerGo = other.gameObject;
            player = playerGo.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerGo = null;
        player = null;
    }

    private void SitOnBoat(PlayerMovement player, GameObject playerGo)
    {
        IslandLoader.LoadIslandOne();

        cameraHolder = player.cameraHolder;
        player.SetControlState(false);
        isPlayerOnBoard = true;
            
        playerGo.transform.SetParent(PlayerPosition);
        playerGo.transform.localPosition = Vector3.zero;
        playerGo.transform.rotation = PlayerPosition.rotation;
    }
    
    private void ExitFromBoat(PlayerMovement player, GameObject playerGo)
    {
        cameraHolder = player.cameraHolder;
        player.SetControlState(true);
        isPlayerOnBoard = false;
            
        playerGo.transform.SetParent(null);
    }

    public void Rotate()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
            
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");

        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
    }
    
    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        var move = transform.forward * verticalMove + transform.right * horizontalMove;
            
        
        characterController.Move(speed * Time.deltaTime * move );
    }
}
