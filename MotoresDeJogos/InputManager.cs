using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MotoresDeJogos
{
    public enum Input
    {
        None,
        Up,
        Left,
        Down,
        Right
    }

    public enum MouseInput
    {
        None,
        LeftButton,
        MiddleButton,
        RightButton,
        Button1,
        Button2
    }

    class InputManager : GameComponent
    {
        public int GamePadIndex;
        public GamePadState CurrentState;
        public GamePadState PreviousState;
        // Will work on this later
        public Dictionary<Input, Keys> KeyboardMap;

        private const int KEYBOARD_INDEX = -1;
        private const int MOUSE_INDEX = -2;
        private Array inputValues;
        private Array mouseValues;
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;
        public MouseState currentMouseState;
        public MouseState previousMouseState;

        public InputManager(Game game) : base(game)
        {
            inputValues = Enum.GetValues(typeof(Input));
            mouseValues = Enum.GetValues(typeof(MouseInput));
            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = currentKeyboardState;
            currentMouseState = Mouse.GetState();
            previousMouseState = currentMouseState;
        }

        public override void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        // keyboard
        public bool IsPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }
        public bool IsHeld(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }

        public bool JustPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }

        public bool JustReleased(Keys key)
        {
            return !currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }

        // mouse
        public bool IsPressed(MouseInput input)
        {
            return IsPressed(currentMouseState, input);
        }

        public bool IsHeld(MouseInput input)
        {
            return IsPressed(currentMouseState, input) && IsPressed(previousMouseState, input);
        }

        public bool JustPressed(MouseInput input)
        {
            return IsPressed(currentMouseState, input) && !IsPressed(previousMouseState, input);
        }

        public bool JustReleased(MouseInput input)
        {
            return !IsPressed(currentMouseState, input) && IsPressed(previousMouseState, input);
        }

        public Point GetMousePosition()
        {
            return currentMouseState.Position;
        }
        public bool IsMouseMoved()
        {
            return currentMouseState.X != previousMouseState.X || currentMouseState.Y != previousMouseState.Y;
        }
        public int GetMouseScroll()
        {
            return currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;
        }

        private bool IsPressed(MouseState state, MouseInput input)
        {
            switch (input)
            {
                case MouseInput.LeftButton:
                    return state.LeftButton == ButtonState.Pressed;
                case MouseInput.MiddleButton:
                    return state.MiddleButton == ButtonState.Pressed;
                case MouseInput.RightButton:
                    return state.RightButton == ButtonState.Pressed;
                case MouseInput.Button1:
                    return state.XButton1 == ButtonState.Pressed;
                case MouseInput.Button2:
                    return state.XButton2 == ButtonState.Pressed;
            }
            return false;
        }
    }
}
