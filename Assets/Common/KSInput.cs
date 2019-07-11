using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Common
{
    public static class KSInput
    {

        private static Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
        private static float width = (float)Screen.width / 2.0f;
        private static float height = (float)Screen.height / 2.0f;
        private static Vector3 inputStartPosition;

        public enum InputPhase
        {
            Start,
            End,
            Moved,
            None
        }

        public enum PlatformType
        {
            Mobile,
            PC
        }

        public static bool HasInput()
        {
            switch (GetPlatformType())
            {
                case PlatformType.Mobile:
                    return Input.touchCount > 0;
                case PlatformType.PC:
                    return Input.GetMouseButton(0);
            }

            return false;
        }
        public static InputPhase GetPhase()
        {

            switch (GetPlatformType())
            {
                case PlatformType.Mobile:
                    switch (Input.GetTouch(0).phase)
                    {
                        case TouchPhase.Began:
                            return InputPhase.Start;
                        case TouchPhase.Moved:
                            return InputPhase.Moved;
                        case TouchPhase.Stationary:
                            return InputPhase.None;
                        case TouchPhase.Ended:
                            return InputPhase.End;
                        case TouchPhase.Canceled:
                            return InputPhase.None;
                    }
                    return InputPhase.None;
                case PlatformType.PC:
                    if (Input.GetMouseButtonDown(0))
                    {
                        return InputPhase.Start;
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        return InputPhase.End;
                    }
                    if (Input.GetMouseButton(0))
                    {
                        return InputPhase.Moved;
                    }
                    return InputPhase.None;
            }
            return InputPhase.None;
        }

        public static void SetStartPosition()
        {
            if (GetPhase() == InputPhase.Start)
            {
                inputStartPosition = GetInputPosition();
            }
        }

        public static Vector3 GetDeltaPosition()
        {
            if (GetPhase() == InputPhase.Moved)
            {
                Vector3 direction = GetInputPosition() - inputStartPosition;
                direction.y *= (height / width);
                return direction;
            }
            return Vector3.zero;
        }

        public static Vector3 GetInputPosition()
        {

            switch (GetPlatformType())
            {
                case PlatformType.Mobile:
                    Touch touch = Input.GetTouch(0);
                    Vector2 posMobile = touch.position;
                    //Vector2 posMobile = Camera.main.ScreenToWorldPoint(touch.position);
                    posMobile.x = (posMobile.x - width) / width;
                    posMobile.y = (posMobile.y - height) / height;
                    position = new Vector3(posMobile.x, posMobile.y, 0.0f);
                    return position;
                case PlatformType.PC:
                    //Input.GetMouseButton(0);
                    Vector2 posPC = Input.mousePosition;
                    posPC.x = (posPC.x - width) / width;
                    posPC.y = (posPC.y - height) / height;
                    position = new Vector3(posPC.x, posPC.y, 0.0f);
                    return position;
            }

            return Vector3.zero;
        }


        public static PlatformType GetPlatformType()
        {
#if UNITY_EDITOR
            return PlatformType.PC;
#elif UNITY_IPHONE
            return PlatformType.Mobile;
#elif UNITY_ANDROID
            return PlatformType.Mobile;
#else
            return PlatformType.PC;
#endif
        }
    }


}
