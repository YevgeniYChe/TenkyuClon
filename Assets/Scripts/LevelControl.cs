using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelControl : MonoBehaviour
{
    public float maxTiltAngle;
    public float angleX;
    public float angleZ;
    public float angleY;

    // for keyboard
    public float speed;

    // for touchscreen
    public float speedX;
    public float speedZ;
    public float multiSpeed;

    public GameObject playerBall;
    private Vector3 playerBallPosition;

    // for keyboard. Develop build.
    void altRotate2()
    {
        playerBallPosition = playerBall.transform.position;

        float tiltAroundZ = Input.GetAxisRaw("Horizontal");
        float tiltAroundX = Input.GetAxisRaw("Vertical");

        Vector3 pointDirectionXUp       = Vector3.zero;
        Vector3 pointDirectionXDowm     = Vector3.zero;
        Vector3 pointDirectionZRight    = Vector3.zero;
        Vector3 pointDirectionZLeft     = Vector3.zero;
        switch (tiltAroundX)
        {
            case -1:
                pointDirectionXDowm = new Vector3(-1, 0, 0);
                break;

            case 1:
                pointDirectionXUp = new Vector3(1, 0, 0);
                break;
        }
        switch (tiltAroundZ)
        {
            case -1:
                pointDirectionZLeft = new Vector3(0, 0, 1);
                break;

            case 1:
                pointDirectionZRight = new Vector3(0, 0, -1);
                break;
        }

        angleX = transform.rotation.eulerAngles.x;
        angleZ = transform.rotation.eulerAngles.z;
        angleY = transform.rotation.eulerAngles.y;

        // Rotation limit
        if ((angleX > 180) || (angleX < maxTiltAngle))
            transform.RotateAround(playerBallPosition, pointDirectionXUp, speed);
        if ((angleX < 180) || (angleX > 360 - maxTiltAngle))
            transform.RotateAround(playerBallPosition, pointDirectionXDowm, speed);
        if ((angleZ < 180) || (angleZ > 360 - maxTiltAngle))
            transform.RotateAround(playerBallPosition, pointDirectionZRight, speed);
        if ((angleZ > 180) || (angleZ < maxTiltAngle))
            transform.RotateAround(playerBallPosition, pointDirectionZLeft, speed);

        // Good option, but not working
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, transform.rotation.eulerAngles.z);

        // Work option for smooth controll. LOCK Y
        if((angleY > 0 ) && (angleY < 180))
            transform.RotateAround(playerBallPosition, new Vector3(0, 1, 0), -angleY);
        else if(angleY > 180)
            transform.RotateAround(playerBallPosition, new Vector3(0, 1, 0), 360 - angleY);

    }

   // for mobile phone. Players build.
    void altRotate4()
    {
        playerBallPosition = playerBall.transform.position;

        bool rotateRight    = false;
        bool rotateLeft     = false;
        bool rotateUp       = false;
        bool rotateDown     = false;

        speedZ = 0;
        speedX = 0;

        Vector3 pointDirectionXUp = new Vector3(1, 0, 0);
        Vector3 pointDirectionZRight = new Vector3(0, 0, -1);

        if (Input.touchCount > 0)
        {

            Touch myTouch = Input.touches[0];

            speedZ = myTouch.deltaPosition.normalized.x;
            speedX = myTouch.deltaPosition.normalized.y;

            if (speedZ > 0)
                rotateRight = true;
            else if (speedZ < 0)
                rotateLeft = true;

            if (speedX > 0)
                rotateUp = true;
            else if (speedX < 0)
                rotateDown = true;

            speedZ = myTouch.deltaPosition.x * multiSpeed * Time.deltaTime;
            speedX = myTouch.deltaPosition.y * multiSpeed * Time.deltaTime;

        }

        angleX = transform.rotation.eulerAngles.x;
        angleZ = transform.rotation.eulerAngles.z;
        angleY = transform.rotation.eulerAngles.y;

        // Rotation limit
        if (((angleX > 180) || (angleX < maxTiltAngle)) && rotateUp)                // Up
            transform.RotateAround(playerBallPosition, pointDirectionXUp, speedX);
        else if (((angleX < 180) || (angleX > 360 - maxTiltAngle)) && rotateDown)   // Down
            transform.RotateAround(playerBallPosition, pointDirectionXUp, speedX);
        if (((angleZ < 180) || (angleZ > 360 - maxTiltAngle)) && rotateRight)       // Right
            transform.RotateAround(playerBallPosition, pointDirectionZRight, speedZ);
        else if (((angleZ > 180) || (angleZ < maxTiltAngle)) && rotateLeft)         // Left
            transform.RotateAround(playerBallPosition, pointDirectionZRight, speedZ);

        // Option for smooth controll. LOCK Y
        if ((angleY > 0) && (angleY < 180))
            transform.RotateAround(playerBallPosition, new Vector3(0, 1, 0), -angleY);
        else if (angleY > 180)
            transform.RotateAround(playerBallPosition, new Vector3(0, 1, 0), 360 - angleY);
    }

    void FixedUpdate()
    {
        // Section for keyboard
        altRotate2();

        // Section for touchscreen
        //altRotate3();

        // Section for 
        altRotate4();
    }
}
