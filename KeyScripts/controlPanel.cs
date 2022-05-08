using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System;
using System.Runtime.InteropServices;




public class controlPanel : MonoBehaviour
{
    public enum KeyCode : ushort
    {
        #region Media
        MEDIA_NEXT_TRACK = 0xb0,
        MEDIA_PREV_TRACK = 0xb1,
        MEDIA_STOP = 0xb2,
        MEDIA_PLAY_PAUSE = 0xb3,
        #endregion

        #region Math Functions
        MULTIPLY = 0x6a, // '*'
        ADD = 0x6b,
        SUBTRACT = 0x6d,
        DIVIDE = 0x6f,
        #endregion

        #region Browser
        BROWSER_BACK = 0xa6,
        BROWSER_FORWARD = 0xa7,
        BROWSER_REFRESH = 0xa8,
        BROWSER_STOP = 0xa9,
        BROWSER_SEARCH = 0xaa,
        BROWSER_FAVORITES = 0xab,
        BROWSER_HOME = 0xac,
        #endregion

        #region Numpad numbers
        NUMPAD0 = 0x60,
        NUMPAD1 = 0x61,
        NUMPAD2 = 0x62,
        NUMPAD3 = 0x63,
        NUMPAD4 = 0x64, // 100
        NUMPAD5 = 0x65,
        NUMPAD6 = 0x66,
        NUMPAD7 = 0x67,
        NUMPAD8 = 0x68,
        NUMPAD9 = 0x69,
        #endregion

        #region Function Keys
        F1 = 0x70,
        F2 = 0x71,
        F3 = 0x72,
        F4 = 0x73,
        F5 = 0x74,
        F6 = 0x75,
        F7 = 0x76,
        F8 = 0x77,
        F9 = 0x78,
        F10 = 0x79,
        F11 = 0x7a,
        F12 = 0x7b,
        F13 = 0x7c,
        F14 = 0x7d,
        F15 = 0x7e,
        F16 = 0x7f,
        F17 = 0x80,
        F18 = 0x81,
        F19 = 130,
        F20 = 0x83,
        F21 = 0x84,
        F22 = 0x85,
        F23 = 0x86,
        F24 = 0x87,
        #endregion

        #region Other 
        // see https://lists.w3.org/Archives/Public/www-dom/2010JulSep/att-0182/keyCode-spec.html
        OEM_COLON = 0xba, // OEM_1
        OEM_102 = 0xe2,
        OEM_2 = 0xbf,
        OEM_3 = 0xc0,
        OEM_4 = 0xdb,
        OEM_BACK_SLASH = 0xdc, // OEM_5
        OEM_6 = 0xdd,
        OEM_7 = 0xde,
        OEM_8 = 0xdf,
        OEM_CLEAR = 0xfe,
        OEM_COMMA = 0xbc,
        OEM_MINUS = 0xbd, // Underscore
        OEM_PERIOD = 0xbe,
        OEM_PLUS = 0xbb,
        #endregion

        #region KEYS
        KEY_0 = 0x30,
        KEY_1 = 0x31,
        KEY_2 = 0x32,
        KEY_3 = 0x33,
        KEY_4 = 0x34,
        KEY_5 = 0x35,
        KEY_6 = 0x36,
        KEY_7 = 0x37,
        KEY_8 = 0x38,
        KEY_9 = 0x39,
        KEY_A = 0x41,
        KEY_B = 0x42,
        KEY_C = 0x43,
        KEY_D = 0x44,
        KEY_E = 0x45,
        KEY_F = 0x46,
        KEY_G = 0x47,
        KEY_H = 0x48,
        KEY_I = 0x49,
        KEY_J = 0x4a,
        KEY_K = 0x4b,
        KEY_L = 0x4c,
        KEY_M = 0x4d,
        KEY_N = 0x4e,
        KEY_O = 0x4f,
        KEY_P = 0x50,
        KEY_Q = 0x51,
        KEY_R = 0x52,
        KEY_S = 0x53,
        KEY_T = 0x54,
        KEY_U = 0x55,
        KEY_V = 0x56,
        KEY_W = 0x57,
        KEY_X = 0x58,
        KEY_Y = 0x59,
        KEY_Z = 0x5a,
        #endregion

        #region volume
        VOLUME_MUTE = 0xad,
        VOLUME_DOWN = 0xae,
        VOLUME_UP = 0xaf,
        #endregion

        SNAPSHOT = 0x2c,
        RIGHT_CLICK = 0x5d,
        BACKSPACE = 8,
        CANCEL = 3,
        CAPS_LOCK = 20,
        CONTROL = 0x11,
        ALT = 18,
        DECIMAL = 110,
        DELETE = 0x2e,
        DOWN = 40,
        END = 0x23,
        ESC = 0x1b,
        HOME = 0x24,
        INSERT = 0x2d,
        LAUNCH_APP1 = 0xb6,
        LAUNCH_APP2 = 0xb7,
        LAUNCH_MAIL = 180,
        LAUNCH_MEDIA_SELECT = 0xb5,
        LCONTROL = 0xa2,
        LEFT = 0x25,
        LSHIFT = 0xa0,
        LWIN = 0x5b,
        PAGEDOWN = 0x22,
        NUMLOCK = 0x90,
        PAGE_UP = 0x21,
        RCONTROL = 0xa3,
        ENTER = 13,
        RIGHT = 0x27,
        RSHIFT = 0xa1,
        RWIN = 0x5c,
        SHIFT = 0x10,
        SPACE_BAR = 0x20,
        TAB = 9,
        UP = 0x26,
    }

    [Flags]
    public enum MouseEventFlags : uint
    {
        MOUSEEVENT_MOVE = 0x0001,
        MOUSEEVENT_LEFTDOWN = 0x0002,
        MOUSEEVENT_LEFTUP = 0x0004,
        MOUSEEVENT_RIGHTDOWN = 0x0008,
        MOUSEEVENT_RIGHTUP = 0x0010,
        MOUSEEVENT_MIDDLEDOWN = 0x0020,
        MOUSEEVENT_MIDDLEUP = 0x0040,
        MOUSEEVENT_XDOWN = 0x0080,
        MOUSEEVENT_XUP = 0x0100,
        MOUSEEVENT_WHEEL = 0x0800,
        MOUSEEVENT_VIRTUALDESK = 0x4000,
        MOUSEEVENT_ABSOLUTE = 0x8000
    }

    [Flags]
    public enum SendInputEventType : uint
    {
        InputMouse,
        InputKeyboard,
        InputHardware
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public MouseEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBOARDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct MOUSEANDKEYBOARDINPUT
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;

        [FieldOffset(0)]
        public KEYBOARDINPUT ki;

        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public SendInputEventType type;
        public MOUSEANDKEYBOARDINPUT mkhi;
    }

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);

    public static void KeyRelease(int keyCode)
    {
        UnityEngine.Debug.Log("KeyReleaseDetected");
        INPUT input = new INPUT
        {
            type = SendInputEventType.InputKeyboard,
            mkhi = new MOUSEANDKEYBOARDINPUT
            {
                ki = new KEYBOARDINPUT
                {
                    wVk = 0,
                    wScan = (ushort)keyCode,
                    dwFlags = 0x0008 | 0x0002, // if nothing, key down
                    time = 0,
                    dwExtraInfo = IntPtr.Zero,
                }
            }
        };


        INPUT[] inputs = new INPUT[] { input }; // Combined, it's a keystroke
        SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
    }

    void releaseAttack()
    {
        KeyRelease(0x24);
    }
    void releaseDefense()
    {
        KeyRelease(0x25);
        inDefense = false;
    }
    void releaseZ()
    {
        KeyRelease(0x2C);
    }
    void releaseC()
    {
        KeyRelease(0x2E);
    }
    void releaseF()
    {
        KeyRelease(0x21);
    }
    void releaseESC()
    {
        KeyRelease(0x01);
    }
    void releaseQ()
    {
        KeyRelease(0x10);
    }
    void releaseCtrl()
    {
        KeyRelease(0x1D);
    }
    void releaseK()
    {
        KeyRelease(0x08);
    }
    void releaseR()
    {
        KeyRelease(0x13);
    }

    void release5()
    {
        KeyRelease(0x06);
    }

    public static void KeyPress(int keyCode)
    {
        UnityEngine.Debug.Log("KeyPressDetected");
        
        INPUT input = new INPUT
        {
            type = SendInputEventType.InputKeyboard,
            mkhi = new MOUSEANDKEYBOARDINPUT
            {
                ki = new KEYBOARDINPUT
                {
                    wVk = 0,
                    wScan = (ushort)keyCode,
                    dwFlags = 0x0008, // if nothing, key down
                    time = 0,
                    dwExtraInfo = IntPtr.Zero,
                }
            }
        };


        INPUT[] inputs = new INPUT[] { input }; // Combined, it's a keystroke
        SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
    }
    bool attackFreeze;
    bool parryFreeze;
    bool _5Freeze;
    bool _6Pressing;
    bool Epressing;
    //No Defense freeze based on the game setting. (one can always attempt to defense)
    bool toolFreeze;
    bool zFreeze;
    bool cFreeze;
    bool QFreeze;
    bool inDefense;
    bool inCrouch;
    bool defenseBreak;//if the player play any action that disrupt the defense pose of the avatar
    
    bool inMove;
//recording the pressing of moving button of the previous frame


    Vector3 standardRPosition;
    Vector3 standardLPosition;//Used to judge displacement-type actions
    float attackThres;
    float defenseThres;
    float defenseHeight;
    bool wPressed;
    bool aPressed;
    bool sPressed;
    bool dPressed;
    bool wPressed2;
    bool aPressed2;
    bool sPressed2;
    bool dPressed2;
    bool lowStance;
    bool forceDown;
    bool inWalk;
    // Use this for initialization
    void Start()
    {
        wPressed = false;
        aPressed = false;
        sPressed = false;
        dPressed = false;
        wPressed2 = false;
        aPressed2 = false;
        sPressed2 = false;
        dPressed2 = false;
        QFreeze = false;
        lowStance = false;
        forceDown = false;
        Epressing = false;
        inWalk = false;
        zFreeze = false;
        cFreeze = false;
        attackFreeze = false;
        parryFreeze = false;
        toolFreeze = false;
        _5Freeze = false;
        _6Pressing = false;
        inDefense = false;
        inCrouch = false;
        defenseBreak = false;
        inMove = false;
        attackThres = 1.8f;
        defenseThres = 1.6f;
        defenseHeight = 0.13f;
        OVRInput.Update();
        standardLPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        standardRPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    }

    void removeQFreeze()
    {
        QFreeze = false;
    }

    void removeAttackFreeze()
    {
        attackFreeze = false;
    }
    void removeParryFreeze()
    {
        parryFreeze = false;
    }
    void removeZFreeze()
    {
        zFreeze = false;
    }
    void remove5Freeze()
    {
        _5Freeze = false;
    }
    // Update is called once per frame
    void Update()
    {

        //run_cmd("D:\\Academic\\sekiroScripts\\activateMouse.py", null);
        //Detecting Request to calibrate should be highest
        //LeftHand System: Defense, Parry, Hook
        //RightHand System: Attack, Use tool, 
        //Importance Should be: Defense  > Attack > Use of tools > Use of items > Hook > menus > [to be continue]
        //Move functions are independent because there are defense-with-move and attack-with-move settings
        //If the detection of a certain action is more complicated than another one, we can reverse the order of detection
        //Calibrate

        //OVRInput.Update();
        //UnityEngine.Debug.Log("height:" + OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).y); -0.85 is a good value
        float leftAxisHorizontal = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick)[0];
        float leftAxisVertical = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick)[1];
        float leftMagnitude = (float)Math.Sqrt(leftAxisHorizontal * leftAxisHorizontal + leftAxisVertical * leftAxisVertical);
        float rightAxisHorizontal = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick)[0];
        float rightAxisVertical = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick)[1];
        float rightMagnitude = (float)Math.Sqrt(rightAxisHorizontal * rightAxisHorizontal + rightAxisVertical * rightAxisVertical);
        /*
        if (leftMagnitude > 0.1f && leftMagnitude < 0.5f)
        {
            inWalk = true;
            KeyPress(0x38);
            //no return, just a change of status
        }
        else if (inWalk)
        {
            inWalk = false;
            KeyRelease(0x38);
            //no return, just a change of status
        }
        */
        //should handle motion keys (wsad)
        if (leftMagnitude > 0.1f)
        {
            //right or no
            if (leftAxisHorizontal >= 0.00f)
            {
                //Up or no
                if (leftAxisVertical > 0.005f)
                {
                    float x_over_y = leftAxisHorizontal / leftAxisVertical;
                    //Solely right
                    if (x_over_y > 2.0f)
                    {
                        if (!dPressed)
                        {
                            dPressed = true;
                            KeyPress(0x20);
                        }
                        if (aPressed)
                        {
                            aPressed = false;
                            KeyRelease(0x1E);
                        }
                        if (wPressed)
                        {
                            wPressed = false;
                            KeyRelease(0x11);
                        }
                        if (sPressed)
                        {
                            sPressed = false;
                            KeyRelease(0x1F);
                        }
                    }
                    //solely up/front
                    else if (x_over_y < 0.5f)
                    {
                        if (!wPressed)
                        {
                            wPressed = true;
                            KeyPress(0x11);
                        }
                        if (aPressed)
                        {
                            aPressed = false;
                            KeyRelease(0x1E);
                        }
                        if (dPressed)
                        {
                            dPressed = false;
                            KeyRelease(0x20);
                        }
                        if (sPressed)
                        {
                            sPressed = false;
                            KeyRelease(0x1F);
                        }
                    }
                    //both right and up
                    else
                    {
                        if (!wPressed)
                        {
                            wPressed = true;
                            KeyPress(0x11);
                        }
                        if (aPressed)
                        {
                            aPressed = false;
                            KeyRelease(0x1E);
                        }
                        if (!dPressed)
                        {
                            dPressed = true;
                            KeyPress(0x20);
                        }
                        if (sPressed)
                        {
                            sPressed = false;
                            KeyRelease(0x1F);
                        }
                    }
                }
                else if (leftAxisVertical < -0.005f)//Down or no
                {
                    float x_over_y = -leftAxisHorizontal / leftAxisVertical;
                    //Solely right
                    if (x_over_y > 2.0f)
                    {
                        if (!dPressed)
                        {
                            dPressed = true;
                            KeyPress(0x20);
                        }
                        if (aPressed)
                        {
                            aPressed = false;
                            KeyRelease(0x1E);
                        }
                        if (wPressed)
                        {
                            wPressed = false;
                            KeyRelease(0x11);
                        }
                        if (sPressed)
                        {
                            sPressed = false;
                            KeyRelease(0x1F);
                        }
                    }
                    //Solely Down/Back
                    else if (x_over_y < 0.5f)
                    {
                        if (wPressed)
                        {
                            wPressed = false;
                            KeyRelease(0x11);
                        }
                        if (aPressed)
                        {
                            aPressed = false;
                            KeyRelease(0x1E);
                        }
                        if (dPressed)
                        {
                            dPressed = false;
                            KeyRelease(0x20);
                        }
                        if (!sPressed)
                        {
                            sPressed = true;
                            KeyPress(0x1F);
                        }
                    }
                    //Both Right and Down/Back
                    else
                    {
                        if (wPressed)
                        {
                            wPressed = false;
                            KeyRelease(0x11);
                        }
                        if (aPressed)
                        {
                            aPressed = false;
                            KeyRelease(0x1E);
                        }
                        if (!dPressed)
                        {
                            dPressed = true;
                            KeyPress(0x20);
                        }
                        if (!sPressed)
                        {
                            sPressed = true;
                            KeyPress(0x1F);
                        }
                    }
                }
                //Solely Right
                else
                {
                    if (!dPressed)
                    {
                        dPressed = true;
                        KeyPress(0x20);
                    }
                    if (aPressed)
                    {
                        aPressed = false;
                        KeyRelease(0x1E);
                    }
                    if (wPressed)
                    {
                        wPressed = false;
                        KeyRelease(0x11);
                    }
                    if (sPressed)
                    {
                        sPressed = false;
                        KeyRelease(0x1F);
                    }
                }
            }
            //Left or no
            else
            {
                //Up or no
                if (leftAxisVertical > 0.005f)
                {
                    float x_over_y = -leftAxisHorizontal / leftAxisVertical;
                    //Solely left
                    if (x_over_y > 2.0f)
                    {
                        if (dPressed)
                        {
                            dPressed = false;
                            KeyRelease(0x20);
                        }
                        if (!aPressed)
                        {
                            aPressed = true;
                            KeyPress(0x1E);
                        }
                        if (wPressed)
                        {
                            wPressed = false;
                            KeyRelease(0x11);
                        }
                        if (sPressed)
                        {
                            sPressed = false;
                            KeyRelease(0x1F);
                        }
                    }
                    //solely up/front
                    else if (x_over_y < 0.5f)
                    {
                        if (!wPressed)
                        {
                            wPressed = true;
                            KeyPress(0x11);
                        }
                        if (aPressed)
                        {
                            aPressed = false;
                            KeyRelease(0x1E);
                        }
                        if (dPressed)
                        {
                            dPressed = false;
                            KeyRelease(0x20);
                        }
                        if (sPressed)
                        {
                            sPressed = false;
                            KeyRelease(0x1F);
                        }
                    }
                    //both left and up
                    else
                    {
                        if (!wPressed)
                        {
                            wPressed = true;
                            KeyPress(0x11);
                        }
                        if (!aPressed)
                        {
                            aPressed = true;
                            KeyPress(0x1E);
                        }
                        if (dPressed)
                        {
                            dPressed = false;
                            KeyRelease(0x20);
                        }
                        if (sPressed)
                        {
                            sPressed = false;
                            KeyRelease(0x1F);
                        }
                    }
                }
                else if (leftAxisVertical < -0.005f)//Down or no
                {
                    float x_over_y = leftAxisHorizontal / leftAxisVertical;
                    //Solely left
                    if (x_over_y > 2.0f)
                    {
                        if (dPressed)
                        {
                            dPressed = false;
                            KeyRelease(0x20);
                        }
                        if (!aPressed)
                        {
                            aPressed = true;
                            KeyPress(0x1E);
                        }
                        if (wPressed)
                        {
                            wPressed = false;
                            KeyRelease(0x11);
                        }
                        if (sPressed)
                        {
                            sPressed = false;
                            KeyRelease(0x1F);
                        }
                    }
                    //Solely Down/Back
                    else if (x_over_y < 0.5f)
                    {
                        if (wPressed)
                        {
                            wPressed = false;
                            KeyRelease(0x11);
                        }
                        if (aPressed)
                        {
                            aPressed = false;
                            KeyRelease(0x1E);
                        }
                        if (dPressed)
                        {
                            dPressed = false;
                            KeyRelease(0x20);
                        }
                        if (!sPressed)
                        {
                            sPressed = true;
                            KeyPress(0x1F);
                        }
                    }
                    //Both left and Down/Back
                    else
                    {
                        if (wPressed)
                        {
                            wPressed = false;
                            KeyRelease(0x11);
                        }
                        if (!aPressed)
                        {
                            aPressed = true;
                            KeyPress(0x1E);
                        }
                        if (dPressed)
                        {
                            dPressed = false;
                            KeyRelease(0x20);
                        }
                        if (!sPressed)
                        {
                            sPressed = true;
                            KeyPress(0x1F);
                        }
                    }
                }
                //Solely left
                else
                {
                    if (dPressed)
                    {
                        dPressed = false;
                        KeyRelease(0x20);
                    }
                    if (!aPressed)
                    {
                        aPressed = true;
                        KeyPress(0x1E);
                    }
                    if (wPressed)
                    {
                        wPressed = false;
                        KeyRelease(0x11);
                    }
                    if (sPressed)
                    {
                        sPressed = false;
                        KeyRelease(0x1F);
                    }
                }
            }
        }
        else
        {
            if (wPressed)
            {
                KeyRelease(0x11);
                wPressed = false;
            }
            if (sPressed)
            {
                KeyRelease(0x1F);
                sPressed = false;
            }
            if (aPressed)
            {
                KeyRelease(0x1E);
                aPressed = false;
            }
            if (dPressed)
            {
                KeyRelease(0x20);
                dPressed = false;
            }
        }
        if (rightMagnitude > 0.1f)
        {
            //right or no
            if (rightAxisHorizontal >= 0.00f)
            {
                //Up or no
                if (rightAxisVertical > 0.005f)
                {
                    float x_over_y = rightAxisHorizontal / rightAxisVertical;
                    //Solely right
                    if (x_over_y > 2.0f)
                    {
                        if (!dPressed2)
                        {
                            dPressed2 = true;
                            KeyPress(0x05);
                        }
                        if (aPressed2)
                        {
                            aPressed2 = false;
                            KeyRelease(0x04);
                        }
                        if (wPressed2)
                        {
                            wPressed2 = false;
                            KeyRelease(0x02);
                        }
                        if (sPressed2)
                        {
                            sPressed2 = false;
                            KeyRelease(0x03);
                        }
                    }
                    //solely up/front
                    else if (x_over_y < 0.5f)
                    {
                        if (!wPressed2)
                        {
                            wPressed2 = true;
                            KeyPress(0x02);
                        }
                        if (aPressed2)
                        {
                            aPressed2 = false;
                            KeyRelease(0x04);
                        }
                        if (dPressed2)
                        {
                            dPressed2 = false;
                            KeyRelease(0x05);
                        }
                        if (sPressed2)
                        {
                            sPressed2 = false;
                            KeyRelease(0x03);
                        }
                    }
                    //both right and up
                    else
                    {
                        if (!wPressed2)
                        {
                            wPressed2 = true;
                            KeyPress(0x02);
                        }
                        if (aPressed2)
                        {
                            aPressed2 = false;
                            KeyRelease(0x04);
                        }
                        if (!dPressed2)
                        {
                            dPressed2 = true;
                            KeyPress(0x05);
                        }
                        if (sPressed2)
                        {
                            sPressed2 = false;
                            KeyRelease(0x03);
                        }
                    }
                }
                else if (rightAxisVertical < -0.005f)//Down or no
                {
                    float x_over_y = -rightAxisHorizontal / rightAxisVertical;
                    //Solely right
                    if (x_over_y > 2.0f)
                    {
                        if (!dPressed2)
                        {
                            dPressed2 = true;
                            KeyPress(0x05);
                        }
                        if (aPressed2)
                        {
                            aPressed2 = false;
                            KeyRelease(0x04);
                        }
                        if (wPressed2)
                        {
                            wPressed2 = false;
                            KeyRelease(0x02);
                        }
                        if (sPressed2)
                        {
                            sPressed2 = false;
                            KeyRelease(0x03);
                        }
                    }
                    //Solely Down/Back
                    else if (x_over_y < 0.5f)
                    {
                        if (wPressed2)
                        {
                            wPressed2 = false;
                            KeyRelease(0x02);
                        }
                        if (aPressed2)
                        {
                            aPressed2 = false;
                            KeyRelease(0x04);
                        }
                        if (dPressed2)
                        {
                            dPressed2 = false;
                            KeyRelease(0x05);
                        }
                        if (!sPressed2)
                        {
                            sPressed2 = true;
                            KeyPress(0x03);
                        }
                    }
                    //Both Right and Down/Back
                    else
                    {
                        if (wPressed2)
                        {
                            wPressed2 = false;
                            KeyRelease(0x02);
                        }
                        if (aPressed2)
                        {
                            aPressed2 = false;
                            KeyRelease(0x04);
                        }
                        if (!dPressed2)
                        {
                            dPressed2 = true;
                            KeyPress(0x05);
                        }
                        if (!sPressed2)
                        {
                            sPressed2 = true;
                            KeyPress(0x03);
                        }
                    }
                }
                //Solely Right
                else
                {
                    if (!dPressed2)
                    {
                        dPressed2 = true;
                        KeyPress(0x05);
                    }
                    if (aPressed2)
                    {
                        aPressed2 = false;
                        KeyRelease(0x04);
                    }
                    if (wPressed2)
                    {
                        wPressed2 = false;
                        KeyRelease(0x02);
                    }
                    if (sPressed2)
                    {
                        sPressed2 = false;
                        KeyRelease(0x03);
                    }
                }
            }
            //Left or no
            else
            {
                //Up or no
                if (rightAxisVertical > 0.005f)
                {
                    float x_over_y = -rightAxisHorizontal / rightAxisVertical;
                    //Solely left
                    if (x_over_y > 2.0f)
                    {
                        if (dPressed2)
                        {
                            dPressed2 = false;
                            KeyRelease(0x05);
                        }
                        if (!aPressed2)
                        {
                            aPressed2 = true;
                            KeyPress(0x04);
                        }
                        if (wPressed2)
                        {
                            wPressed2 = false;
                            KeyRelease(0x02);
                        }
                        if (sPressed2)
                        {
                            sPressed2 = false;
                            KeyRelease(0x03);
                        }
                    }
                    //solely up/front
                    else if (x_over_y < 0.5f)
                    {
                        if (!wPressed2)
                        {
                            wPressed2 = true;
                            KeyPress(0x02);
                        }
                        if (aPressed2)
                        {
                            aPressed2 = false;
                            KeyRelease(0x04);
                        }
                        if (dPressed2)
                        {
                            dPressed2 = false;
                            KeyRelease(0x05);
                        }
                        if (sPressed2)
                        {
                            sPressed2 = false;
                            KeyRelease(0x03);
                        }
                    }
                    //both left and up
                    else
                    {
                        if (!wPressed2)
                        {
                            wPressed2 = true;
                            KeyPress(0x02);
                        }
                        if (!aPressed2)
                        {
                            aPressed2 = true;
                            KeyPress(0x04);
                        }
                        if (dPressed2)
                        {
                            dPressed2 = false;
                            KeyRelease(0x05);
                        }
                        if (sPressed2)
                        {
                            sPressed2 = false;
                            KeyRelease(0x03);
                        }
                    }
                }
                else if (rightAxisVertical < -0.005f)//Down or no
                {
                    float x_over_y = rightAxisHorizontal / rightAxisVertical;
                    //Solely left
                    if (x_over_y > 2.0f)
                    {
                        if (dPressed2)
                        {
                            dPressed2 = false;
                            KeyRelease(0x05);
                        }
                        if (!aPressed2)
                        {
                            aPressed2 = true;
                            KeyPress(0x04);
                        }
                        if (wPressed2)
                        {
                            wPressed2 = false;
                            KeyRelease(0x02);
                        }
                        if (sPressed2)
                        {
                            sPressed2 = false;
                            KeyRelease(0x03);
                        }
                    }
                    //Solely Down/Back
                    else if (x_over_y < 0.5f)
                    {
                        if (wPressed2)
                        {
                            wPressed2 = false;
                            KeyRelease(0x02);
                        }
                        if (aPressed2)
                        {
                            aPressed2 = false;
                            KeyRelease(0x04);
                        }
                        if (dPressed2)
                        {
                            dPressed2 = false;
                            KeyRelease(0x05);
                        }
                        if (!sPressed2)
                        {
                            sPressed2 = true;
                            KeyPress(0x03);
                        }
                    }
                    //Both left and Down/Back
                    else
                    {
                        if (wPressed2)
                        {
                            wPressed2 = false;
                            KeyRelease(0x02);
                        }
                        if (!aPressed2)
                        {
                            aPressed2 = true;
                            KeyPress(0x04);
                        }
                        if (dPressed2)
                        {
                            dPressed2 = false;
                            KeyRelease(0x05);
                        }
                        if (!sPressed2)
                        {
                            sPressed2 = true;
                            KeyPress(0x03);
                        }
                    }
                }
                //Solely left
                else
                {
                    if (dPressed2)
                    {
                        dPressed2 = false;
                        KeyRelease(0x05);
                    }
                    if (!aPressed2)
                    {
                        aPressed2 = true;
                        KeyPress(0x04);
                    }
                    if (wPressed2)
                    {
                        wPressed2 = false;
                        KeyRelease(0x02);
                    }
                    if (sPressed2)
                    {
                        sPressed2 = false;
                        KeyRelease(0x03);
                    }
                }
            }
        }
        //release all pressed movement buttons
        else
        {
            if (wPressed2)
            {
                KeyRelease(0x02);
                wPressed2 = false;
            }
            if (sPressed2)
            {
                KeyRelease(0x03);
                sPressed2 = false;
            }
            if (aPressed2)
            {
                KeyRelease(0x04);
                aPressed2 = false;
            }
            if (dPressed2)
            {
                KeyRelease(0x05);
                dPressed2 = false;
            }
        }
        if(OVRInput.Get(OVRInput.Button.SecondaryThumbstick) && ! _5Freeze)
        {
            _5Freeze = true;
            Invoke("remove5Freeze", 0.10f);
            Invoke("release5", 0.08f);
            KeyPress(0x06);
        }
        //slide & dash
        if (OVRInput.Get(OVRInput.Button.One) && !_6Pressing)
        {
            _6Pressing = true;
            KeyPress(0x07);
        }

        if(_6Pressing && !OVRInput.Get(OVRInput.Button.One))
        {
            _6Pressing = false;
            KeyRelease(0x07);
        }

        if(OVRInput.Get(OVRInput.Button.Three) && !Epressing)
        {
            Epressing = true;
            KeyPress(0x12);
        }
        if (Epressing && !OVRInput.Get(OVRInput.Button.Three))
        {
            Epressing = false;
            KeyRelease(0x12);
        }
        if(OVRInput.Get(OVRInput.Button.Start))
        {
            KeyPress(0x01);
            Invoke("releaseESC", 0.08f);
        }
        if(OVRInput.Get(OVRInput.Button.Two))
        {
            KeyPress(0x08);
            Invoke("releaseK", 0.3f);
        }

        if(OVRInput.Get(OVRInput.Button.Four))
        {
            KeyPress(0x13);
            Invoke("releaseR", 0.10f);
        }
        if (!forceDown && !QFreeze && !lowStance && (OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).y < -0.65f))
        {
            lowStance = true;
            QFreeze = true;
            KeyPress(0x10);
            Invoke("removeQFreeze", 0.2f);
            Invoke("releaseQ", 0.08f);
        }
        if (!QFreeze && !lowStance && OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
        {
            lowStance = true;
            QFreeze = true;
            forceDown = true;
            KeyPress(0x10);
            Invoke("removeQFreeze", 0.2f);
            Invoke("releaseQ", 0.08f);
        }

        if (!forceDown && !QFreeze && lowStance && OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).y >= -0.6f)
        {
            lowStance = false;
            QFreeze = true;
            KeyPress(0x10);
            Invoke("removeQFreeze", 0.2f);
            Invoke("releaseQ", 0.08f);
        }
        if (!QFreeze && lowStance && OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
        {
            forceDown = false;
            lowStance = false;
            QFreeze = true;
            KeyPress(0x10);
            Invoke("removeQFreeze", 0.2f);
            Invoke("releaseQ", 0.08f);
        }

        //UnityEngine.Debug.Log(Input.GetButton("Abutton"));
        //UnityEngine.Debug.Log(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).y - standardLPosition.y);
        /*calibrate is here
        if (OVRInput.Get(OVRInput.RawButton.B, OVRInput.Controller.RTouch))
        {
            standardLPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            standardRPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            return;
        }
        */
        if (inDefense)
        {
            //detection of remove defense pose
            if((OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).y - standardLPosition.y) < defenseHeight)
            {
                inDefense = false;
                KeyRelease(0x25);
                return;
                //Not sure whether early return here
            }
        }
        //UnityEngine.Debug.Log((OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch) - prevLPosition).magnitude);
        //hook
        if(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).magnitude > defenseThres && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.75f)
        {
            KeyPress(0x21);
            Invoke("releaseF", 0.08f);
            return;
        }
        if (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).magnitude > attackThres && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.75f)
        {
            KeyPress(0x1D);
            Invoke("releaseCtrl", 0.08f);
            return;
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.75f && !zFreeze)//shuffle item
        {
            KeyPress(0x2C);
            Invoke("releaseZ", 0.08f);
            zFreeze = true;
            Invoke("removeZFreeze", 0.1f);
            return;
        }
        if(OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.75f && !cFreeze)//shuffle tool
        {
            KeyPress(0x2E);
            Invoke("releaseC", 0.08f);
            cFreeze = true;
            Invoke("removeCFreeze", 0.1f);
            return;
        }
        //Defense - in pose
        if (!inDefense && (OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).y - standardLPosition.y) > defenseHeight)
        {
            inDefense = true;
            KeyPress(0x25);
            return;
        }
        //Defense - parry
        if (!parryFreeze && !inDefense)
        {
            if (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch).magnitude > defenseThres)
            {
                inDefense = true;
                KeyPress(0x25);
                Invoke("releaseDefense", 0.08f);
                parryFreeze = true;
                Invoke("removeParryFreeze", 0.09f);
            }
        }



        //Attack
        if (!attackFreeze)
        {
            if (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).magnitude > attackThres)
            {
                //trigger attack
                KeyPress(0x24);
                Invoke("releaseAttack", 0.1f);
                attackFreeze = true;
                Invoke("removeAttackFreeze", 0.25f);
                /* Comment for battle technique research
                if(inDefense)
                {
                    KeyRelease(0x25);
                    inDefense = false;
                }
                */
                return;
            }
        }


    }

    private void run_cmd(string cmd, string args)
    {
        
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "\"D:\\Program and Files\\Anaconda\\python.exe\"";
        start.Arguments = string.Format("{0} {1}", cmd, args);
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.CreateNoWindow = true;
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.Write(result);
            }
        }
        
    }

}
