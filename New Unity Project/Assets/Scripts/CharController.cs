using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    [System.Serializable]
    public class MoveSettings {
        public float forwardVel = 12f;
        public float rotateVel = 100f;
        public float jumpVel = 25;
        public float distToGround = 0.1f;
        public LayerMask ground;
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.75f;

    }

    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
    }

    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();

    Vector3 velocity = Vector3.zero;
    Quaternion t;
    Rigidbody r;
    float forwardInput, turnInput, jumpInput;

    bool Grounded() {
        return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGround, moveSetting.ground);
    }

	// Use this for initialization
	void Start () {
        t = transform.rotation;
        r = GetComponent<Rigidbody>();

        forwardInput = turnInput = 0;
	}

    void GetInput() {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");
    }

	// Update is called once per frame
	void Update () {
        GetInput();
        Turn();
	}

    void FixedUpdate() {
        Run();
        Jump();

        r.velocity = transform.TransformDirection(velocity);
    }

    void Run() {
        if (Mathf.Abs(forwardInput) > inputSetting.inputDelay)
        {
            velocity.z = forwardInput * moveSetting.forwardVel;
        }
        else {
            velocity.z = 0;
        }
    }

    void Turn() {
        if (Mathf.Abs(turnInput) > inputSetting.inputDelay)
        {
            t *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }

        transform.rotation = t;
    }

    void Jump() {
        if (jumpInput > 0 && Grounded())
        {
            velocity.y = moveSetting.jumpVel;
        }
        else if (jumpInput == 0 && Grounded())
        {
            velocity.y = 0;
        }
        else {
            velocity.y -= physSetting.downAccel;
        }
    }
}
