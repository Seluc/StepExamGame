using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace lesson2
{
    public class PlayerMovement : MonoBehaviour
    {

        public CharacterController characterController;
        public float speed = 3;

        public Animator animator;

        // camera and rotation
        public float jumpHeight = 1.5f;
        public Transform cameraHolder;
        public float mouseSensitivity = 2f;
        public float upLimit = -50;
        public float downLimit = 50;

        // gravity
        private float gravity = -9.87f;
        //private float verticalSpeed = 0;
        private Vector3 playerVelocity;
        [SerializeField]
        private bool isPlayerGrounded;

        private bool isPlayerCanMove = true;

        private bool _isMoving;
        public bool IsMoving
        {
            get => _isMoving;
            private set => _isMoving = value;
        }

        void Update()
        {
            if (isPlayerCanMove)
            {
                isPlayerGrounded = characterController.isGrounded;
                Jump();
                Move();
                Rotate();
            }
        }

        public void SetControlState(bool isActive)
        {
            isPlayerCanMove = isActive;

            if (isActive == false)
            {
                animator.SetBool("IsRunForward", false);
                animator.SetBool("IsRunBackward", false);
                animator.SetBool("IsRunRight", false);
                animator.SetBool("IsRunLeft", false);
            }
        }

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Jump()
        {
            if (Input.GetButtonDown("Jump") && isPlayerGrounded)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f *gravity);
                characterController.Move(playerVelocity * Time.deltaTime);
            }
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

            if (isPlayerGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0;
            }

            var move = transform.forward * verticalMove + transform.right * horizontalMove;
            
            playerVelocity.y += gravity * Time.deltaTime;
            characterController.Move(speed * Time.deltaTime * move + playerVelocity * Time.deltaTime);

            if (horizontalMove != 0 || verticalMove != 0)
            {
                IsMoving = true;
            }
            else
            {
                IsMoving = false;
            }

            animator.SetBool("IsRunForward", verticalMove > 0);
            animator.SetBool("IsRunBackward", verticalMove < 0);
            animator.SetBool("IsRunRight", horizontalMove > 0);
            animator.SetBool("IsRunLeft", horizontalMove < 0);
        }
    }
}
