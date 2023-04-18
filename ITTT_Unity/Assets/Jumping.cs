using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public float jumpForceShort;
    public float jumpForceLong;
    public float minimumHeldDuration;

    private float spacePressedTime;

    private bool longJumpCalled = false;
    private bool jumping;
    private bool spaceHeld;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            spacePressedTime = Time.timeSinceLevelLoad;
            spaceHeld = false;
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            if (!spaceHeld) {
                if (!jumping) {
                    GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForceShort, 0), ForceMode.Impulse);
                    jumping = true;
                }
            }
            spaceHeld = false;
            longJumpCalled = false;
        }

        if (Input.GetKey(KeyCode.Space)) {
            if (Time.timeSinceLevelLoad - spacePressedTime > minimumHeldDuration) {
                spaceHeld = true;
                if (!longJumpCalled) {
                    if (!jumping) {
                        GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForceLong, 0), ForceMode.Impulse);
                        longJumpCalled = true;
                        jumping = true;
                    }
                }

            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag.Equals("Ground")) {
            jumping = false;
        }
    }
}
