using System.Linq;
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
    public static bool IsScrollingUp => MouseState.ScrollWheelValue > PreviousMouseState.ScrollWheelValue;

    /// <summary>
    /// Returns true if the mouse wheel is scrolling down
    /// </summary>
    public static bool IsScrollingDown => MouseState.ScrollWheelValue < PreviousMouseState.ScrollWheelValue;

    /// <summary>
    /// Returns true if the left mouse button is down
    /// </summary>
    public static bool IsLeftMouseButtonDown => MouseState.LeftButton == ButtonState.Pressed;

    /// <summary>
    /// Returns true if the right mouse button is down
    /// </summary>
    public static bool IsRightMouseButtonDown => MouseState.RightButton == ButtonState.Pressed;

    /// <summary>
    /// Returns true if the left mouse button has been pressed
    /// </summary>
    public static bool IsLeftMouseButtonPressed => MouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released;

    /// <summary>
    /// Returns true if the right mouse button has been pressed
    /// </summary>
    public static bool IsRightMouseButtonPressed => MouseState.RightButton == ButtonState.Pressed && PreviousMouseState.RightButton == ButtonState.Released;

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

    #endregion

}
