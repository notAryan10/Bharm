using UnityEngine;

namespace SG {
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;
        public bool b_Input;
        public bool rollFlag;
        public float rollInputTimer;
        public bool sprintFlag;

        PlayerControls inputActions;


        Vector2 movementInput;
        Vector2 cameraInput;

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
            }
            inputActions.Enable();
        } 

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
        }
        private void MoveInput(float delta)
        {
            movementInput = inputActions.PlayerMovement.Movement.ReadValue<Vector2>();
            cameraInput = inputActions.PlayerMovement.Camera.ReadValue<Vector2>();

            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            // ponytail: stick-drift deadzone. Raise 0.1f if the character still creeps.
            if (moveAmount < 0.1f)
            {
                horizontal = 0;
                vertical = 0;
                moveAmount = 0;
            }

            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
           if (b_Input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else
            {
                sprintFlag = false;

                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    rollFlag = true;
                }
                rollInputTimer = 0;
            }
        }
    }
}
