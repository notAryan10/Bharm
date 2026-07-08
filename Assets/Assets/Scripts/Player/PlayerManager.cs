using UnityEngine;

namespace SG {
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        AnimatorHandler anim;
        public bool isInteracting;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;
        [Header("Player Flags")]
        public bool isSprinting;
        
        private void Awake()
        {
            cameraHandler = CameraHandler.singleton;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<AnimatorHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        // Update is called once per frame
        void Update()
        {
            isInteracting = anim.anim.GetBool("isInteracting");
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
                        float delta = Time.deltaTime;

            if (playerLocomotion.animatorHandler.anim.GetBool("isInteracting") &&
                !playerLocomotion.animatorHandler.anim.GetCurrentAnimatorStateInfo(0).IsName("Rolling") &&
                !playerLocomotion.animatorHandler.anim.IsInTransition(0))
            {
                playerLocomotion.animatorHandler.anim.SetBool("isInteracting", false);
                playerLocomotion.animatorHandler.anim.applyRootMotion = false;
            }

            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);

        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            isSprinting = inputHandler.b_Input;
        }
    }
}