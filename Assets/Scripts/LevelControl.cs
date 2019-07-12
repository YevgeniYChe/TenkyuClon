﻿using System.Collections;
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
    public float multiSpeed=3f;

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
    void altRotate3()
    {
        playerBallPosition = playerBall.transform.position;

        //touchC = Input.touchCount;

        float tiltAroundZ = 0;
        float tiltAroundX = 0;
        speedZ = 0;
        speedX = 0;

        if (Input.touchCount > 0)
        {
            //Store the first touch detected.
            Touch myTouch = Input.touches[0];

            speedZ = myTouch.deltaPosition.normalized.x;
            speedX = myTouch.deltaPosition.normalized.y;

            if (speedZ > 0)
                tiltAroundZ = 1;
            else if (speedZ < 0)
                tiltAroundZ = -1;
            else
                tiltAroundZ = 0;

            if (speedX > 0)
                tiltAroundX = 1;
            else if (speedX < 0)
                tiltAroundX = -1;
            else
                tiltAroundX = 0;

            speedZ = Mathf.Abs(speedZ);
            speedX = Mathf.Abs(speedX);
        }

        Vector3 pointDirectionXUp = Vector3.zero;
        Vector3 pointDirectionXDowm = Vector3.zero;
        Vector3 pointDirectionZRight = Vector3.zero;
        Vector3 pointDirectionZLeft = Vector3.zero;
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
            transform.RotateAround(playerBallPosition, pointDirectionXUp, speedX * multiSpeed);
        if ((angleX < 180) || (angleX > 360 - maxTiltAngle))
            transform.RotateAround(playerBallPosition, pointDirectionXDowm, speedX * multiSpeed);
        if ((angleZ < 180) || (angleZ > 360 - maxTiltAngle))
            transform.RotateAround(playerBallPosition, pointDirectionZRight, speedZ * multiSpeed);
        if ((angleZ > 180) || (angleZ < maxTiltAngle))
            transform.RotateAround(playerBallPosition, pointDirectionZLeft, speedZ * multiSpeed);

        // Work option for smooth controll. LOCK Y
        if ((angleY > 0) && (angleY < 180))
            transform.RotateAround(playerBallPosition, new Vector3(0, 1, 0), -angleY);
        else if (angleY > 180)
            transform.RotateAround(playerBallPosition, new Vector3(0, 1, 0), 360 - angleY);
    }

    void Update()
    {
        // Section for keyboard
        //altRotate2();

        // Section for touchscreen
        altRotate3();
    }
}