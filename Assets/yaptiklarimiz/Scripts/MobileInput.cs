using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    private const float DEADZONE = 100.0f;
    public static MobileInput Instance { set; get; }
    private bool tap, swipeRight, swipeLeft, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;
    public bool Tap { get { return tap; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public Vector2 StartTouch { get { return startTouch; } }




    private void Awake()
    {
        Instance = this; //Instance kendine bind etcek demek
    }
    private void Update()
    {


        //RESET ALL BOOLEANS TO FALSE
        tap = swipeDown = swipeLeft = swipeRight = swipeUp = false;
        //END

        //CHECK FOR INPUTS
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0)) //Click
        {
            tap = true;
            startTouch = Input.mousePosition;
        }else if (Input.GetMouseButtonUp(0)) //Clickten el çekilince yani serbest bırakılınca
        {
            startTouch = swipeDelta = Vector2.zero;
        }
        #endregion

        #region Mobile Inputs
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
            }else if (Input.touches[0].phase==TouchPhase.Ended|| Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }
        }
        #endregion
        //END


        //CALCULATE DISTANCE
        swipeDelta = Vector2.zero;
        if (StartTouch != Vector2.zero)
        {
            //CHECK MOBILE
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            //END

            //CHECK STANDALONE
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
            //END
        }
        //END

        //CHECK IF BEYOND DEADZONE
        if (swipeDelta.magnitude > DEADZONE)
        {
            //This is a confirmed swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0) swipeLeft = true;
                else swipeRight = true;
            }
            else
            {
                //Up or Down
                if (y < 0) swipeDown = true;
                else swipeUp = true;
            }

            startTouch = swipeDelta = Vector2.zero;
        }
        //END


















    }



}
