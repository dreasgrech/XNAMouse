using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAMouse
{
    public class MouseCursor : DrawableGameComponent
    {
        public bool LeftPressed;
        public bool MiddlePressed;
        public bool RightPressed;
        public int ScrollWheelValue { get; set; }
        public Texture2D CursorTexture{ get; set;}


        public event EventHandler<MouseClickEventArgs> LeftClick = delegate { };
        public event EventHandler<MouseClickEventArgs> LeftDrag = delegate { };
        public event EventHandler<MouseClickEventArgs> MiddleClick = delegate { };
        public event EventHandler<MouseClickEventArgs> RightClick = delegate { };
        public event EventHandler<MouseClickEventArgs> RightDrag = delegate { };
        public event EventHandler<ScrollWheelValueChangedEventArgs> ScrollWheelValueChanged = delegate { };

        private readonly SpriteBatch spriteBatch;

        private Vector2 last_position;
        public MouseCursor(Game game, SpriteBatch spriteBatch, Texture2D texture) : base(game)
        {
            this.spriteBatch = spriteBatch;
            CursorTexture = texture;
            var state = Mouse.GetState();
            last_position = new Vector2(state.X, state.Y);
        }

        public override void Draw(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            var position = new Vector2(state.X, state.Y);
            spriteBatch.Begin();
            spriteBatch.Draw(CursorTexture, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            var state = Mouse.GetState();
            var currentPosition = new Vector2(state.X, state.Y);
            HandleClick(state, state.LeftButton, ref LeftPressed, LeftClick);
            HandleClick(state, state.RightButton, ref RightPressed, RightClick);
            HandleClick(state, state.MiddleButton, ref MiddlePressed, MiddleClick);
            if (ScrollWheelValue != state.ScrollWheelValue)
            {
                ScrollWheelValue = state.ScrollWheelValue;
                ScrollWheelValueChanged(this, new ScrollWheelValueChangedEventArgs(state.ScrollWheelValue));
            }
            if (currentPosition != last_position)
            {
                HandleDrag(state, ref LeftPressed, LeftDrag);
                HandleDrag(state, ref RightPressed, RightDrag);
            }
            last_position = currentPosition;
            base.Update(gameTime);
        }

        void HandleDrag(MouseState state, ref bool flag, EventHandler<MouseClickEventArgs> eventHandler)
        {
            if (flag)
            {
                eventHandler(this,new MouseClickEventArgs(new Point(state.X, state.Y)));
            }
        }

        void HandleClick(MouseState state, ButtonState buttonState, ref bool flag, EventHandler<MouseClickEventArgs> eventHandler)
        {
            if (buttonState == ButtonState.Pressed)
            {
                if (!flag)
                {
                    flag = true;
                    eventHandler(this, new MouseClickEventArgs(new Point(state.X, state.Y)));
                }
            }
            else
            {
                flag = false;
            }
        }
    }
}