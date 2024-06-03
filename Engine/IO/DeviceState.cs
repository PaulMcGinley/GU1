using System.Linq;
using System.Numerics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Engine.IO;

public static class DeviceState {

    private static KeyboardState KeyboardState;
    private static KeyboardState PreviousKeyboardState;

    private static MouseState MouseState;
    private static MouseState PreviousMouseState;

    private static GamePadState[] GamePadsState;
    private static GamePadState[] PreviousGamePadsState;

    /// <summary>
    /// Get the initial state of the devices
    /// </summary>
    public static void Initialize() {

        KeyboardState = Keyboard.GetState();
        MouseState = Mouse.GetState();
        GamePadsState = new GamePadState[16];
        PreviousGamePadsState = new GamePadState[16];

        for (int i = 0; i < GamePadsState.Length; i++) {

            GamePadsState[i] = GamePad.GetState(i);
            PreviousGamePadsState[i] = GamePad.GetState(i);
        }
    }

    /// <summary>
    /// Update the state of the devices
    /// This should be called once per frame
    /// This will update the previous state of the devices which can be used to check for changes
    /// </summary>
    public static void Update() {

        PreviousKeyboardState = KeyboardState;
        KeyboardState = Keyboard.GetState();

        PreviousMouseState = MouseState;
        MouseState = Mouse.GetState();

        for (int i = 0; i < GamePadsState.Length; i++) {

            PreviousGamePadsState[i] = GamePadsState[i];
            GamePadsState[i] = GamePad.GetState(i);
        }
    }

    #region Mouse

    /// <summary>
    /// Returns true if the mouse wheel is scrolling up
    /// </summary>
    public static bool MouseIsScrollingUp => MouseState.ScrollWheelValue > PreviousMouseState.ScrollWheelValue;

    /// <summary>
    /// Returns true if the mouse wheel is scrolling down
    /// </summary>
    public static bool MouseIsScrollingDown => MouseState.ScrollWheelValue < PreviousMouseState.ScrollWheelValue;

    /// <summary>
    /// Returns true if the left mouse button is down
    /// </summary>
    public static bool MouseLeftMouseButtonDown => MouseState.LeftButton == ButtonState.Pressed;

    /// <summary>
    /// Returns true if the right mouse button is down
    /// </summary>
    public static bool MouseRightMouseButtonDown => MouseState.RightButton == ButtonState.Pressed;

    /// <summary>
    /// Returns true if the left mouse button has been pressed
    /// </summary>
    public static bool MouseLeftMouseButtonPressed => MouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released;

    /// <summary>
    /// Returns true if the right mouse button has been pressed
    /// </summary>
    public static bool MouseRightMouseButtonPressed => MouseState.RightButton == ButtonState.Pressed && PreviousMouseState.RightButton == ButtonState.Released;

    /// <summary>
    /// Returns the current mouse position
    /// </summary>
    public static Vector2 MousePosition => new(MouseState.X, MouseState.Y);

    #endregion

    #region Keyboard

    /// <summary>
    /// Returns true if Keys.key is down
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsKeyDown(Keys key) => KeyboardState.IsKeyDown(key);

    /// <summary>
    /// Returns true if Keys.key is pressed
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsKeyPressed(Keys key) => KeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);

    /// <summary>
    /// Returns true if Keys.key is released
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsKeyReleased(Keys key) => KeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key);

    /// <summary>
    /// Returns true if any of the keys are pressed
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public static bool IsAnyOfKeysDown(params Keys[] keys) {

        foreach (Keys key in keys)
            if (KeyboardState.IsKeyDown(key))
                return true;

        return false;
    }

    /// <summary>
    /// Returns true if any of the specified Keys.key are pressed
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public static bool IsAnyOfKeysPressed(params Keys[] keys) {

        foreach (Keys key in keys)
            if (KeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key))
                return true;

        return false;
    }

    /// <summary>
    /// Returns true if any of the specified Keys.key are released
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public static bool IsAnyOfKeysReleased(params Keys[] keys) {

        foreach (Keys key in keys)
            if (KeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key))
                return true;

        return false;
    }

    #endregion

    #region GamePad

    /// <summary>
    /// Returns true if the gamepad of the given index is connected
    /// </summary>
    /// <param name="index">Player controller index number</param>
    /// <returns></returns>
    public static bool IsGamePadConnected(int index) => GamePadsState[index].IsConnected;

    /// <summary>
    /// Returns true if the gamepad of the given index has a specific button down
    /// </summary>
    /// <param name="index">Player controller index number</param>
    /// <param name="button">Controller Button</param>
    /// <returns></returns>
    public static bool IsGamePadButtonDown(int index, Buttons button) => GamePadsState[index].IsButtonDown(button);

    /// <summary>
    /// Returns true if the gamepad of the given index has a specific button pressed
    /// </summary>
    /// <param name="index">Player controller index number</param>
    /// <param name="button">Controller Button</param>
    /// <returns></returns>
    public static bool IsGamePadButtonPressed(int index, Buttons button) => GamePadsState[index].IsButtonDown(button) && PreviousGamePadsState[index].IsButtonUp(button);

    /// <summary>
    /// Returns true if the gamepad of the given index has a specific button released
    /// </summary>
    /// <param name="index">Player controller index number</param>
    /// <param name="button">Controller Button</param>
    /// <returns></returns>
    public static bool IsGamePadButtonReleased(int index, Buttons button) => GamePadsState[index].IsButtonUp(button) && PreviousGamePadsState[index].IsButtonDown(button);

    /// <summary>
    /// Activate the rumble feature of the gamepad of the given index
    /// </summary>
    /// <param name="index"></param>
    /// <param name="leftMotor"></param>
    /// <param name="rightMotor"></param>
    public static void Rumble(int index, float leftMotor, float rightMotor) => GamePad.SetVibration(index, leftMotor, rightMotor);

    public static float GamePadLeftStickX(int index) => GamePadsState[index].ThumbSticks.Left.X;

    public static float GamePadLeftStickY(int index) => -GamePadsState[index].ThumbSticks.Left.Y;

    public static Vector2 GamePadLeftStick(int index) => new(GamePadsState[index].ThumbSticks.Left.X, -GamePadsState[index].ThumbSticks.Left.Y);

    public static Vector2 GamePadRightStick(int index) => new(GamePadsState[index].ThumbSticks.Right.X, -GamePadsState[index].ThumbSticks.Right.Y);

    public static float GamePadRightTrigger(int index) => GamePadsState[index].Triggers.Right;

    public static float GamePadLeftTrigger(int index) => GamePadsState[index].Triggers.Left;

    public static bool GamePadRightTriggerPressed(int index) => GamePadsState[index].Triggers.Right > 0.1f && PreviousGamePadsState[index].Triggers.Right < 0.1f; // I've added the 0.1f threshold to prevent accidental presses

    public static bool GamePadLeftTriggerPressed(int index) => GamePadsState[index].Triggers.Left > 0.1f && PreviousGamePadsState[index].Triggers.Left < 0.1f;

    #endregion

    #region Any Input

    /// <summary>
    /// Returns true if any of the specified input methods are down
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public static bool IsAnyInputDown(params object[] keys) {

        foreach (ButtonState button in keys.Select(v => (ButtonState)v))
            if (MouseState.LeftButton == button || MouseState.RightButton == button)
                return true;

        foreach (Keys key in keys.Select(v => (Keys)v))
            if (IsKeyDown(key))
                return true;

        foreach (Buttons button in keys.Select(v => (Buttons)v))
            for (int i = 0; i < GamePadsState.Length; i++)
                if (IsGamePadConnected(i) && IsGamePadButtonDown(i, button))
                    return true;

        return false;
    }

    public static bool IsAnyInputPressed(params object[] keys) {

        foreach (ButtonState button in keys.Select(v => (ButtonState)v))
            if ((MouseState.LeftButton == button && PreviousMouseState.LeftButton == ButtonState.Released) || (MouseState.RightButton == button && PreviousMouseState.RightButton == ButtonState.Released))
                return true;

        foreach (Keys key in keys.Select(v => (Keys)v))
            if (IsKeyPressed(key))
                return true;

        foreach (Buttons button in keys.Select(v => (Buttons)v))
            for (int i = 0; i < GamePadsState.Length; i++)
                if (IsGamePadConnected(i) && IsGamePadButtonPressed(i, button))
                    return true;

        return false;
    }

    #endregion

}
