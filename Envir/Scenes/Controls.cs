using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Controls : IScene {

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) {}

    public void LoadContent(ContentManager content) {}

    public void UnloadContent() {}

    public void Update(GameTime gameTime) {

        if (IsAnyInputPressed(Keys.B, Buttons.B, Buttons.Back))
            GameState.CurrentScene = GameScene.MainMenu;
    }

    public void FixedUpdate(GameTime gameTime) {}

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();
        spriteBatch.Draw(TLib.ControlsBackground, Vector2.Zero, Color.White);
        spriteBatch.End();
    }

    public void OnSceneStart() {

        System.Diagnostics.Debug.WriteLine("Controls Scene Started");
    }

    public void OnSceneEnd() {

        System.Diagnostics.Debug.WriteLine("Controls Scene Ended");
    }

    #endregion
}
