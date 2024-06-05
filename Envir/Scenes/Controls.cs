using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GU1.Envir.Scenes;

public class Controls : IScene {

    Slide CurrentSlide = Slide.Buttons;

    #region IScene Implementation

    public void Initialize(GraphicsDevice device) {}

    public void LoadContent(ContentManager content) {}

    public void UnloadContent() {}

    public void Update(GameTime gameTime) {

        if (IsAnyInputPressed(Keys.B, Buttons.Back))
            GameState.CurrentScene = GameScene.MainMenu;

        if (IsAnyInputPressed(Keys.Left, Buttons.LeftThumbstickLeft, Buttons.DPadLeft, Buttons.B))
            CurrentSlide = (int)CurrentSlide == Enum.GetValues(typeof(Slide)).Length-1 ? 0 : CurrentSlide+1;

        if (IsAnyInputPressed(Keys.Right, Buttons.LeftThumbstickRight, Buttons.DPadRight, Buttons.A))
            CurrentSlide = (int)CurrentSlide == 0 ? (Slide)Enum.GetValues(typeof(Slide)).Length-1 : CurrentSlide-1;
    }

    public void FixedUpdate(GameTime gameTime) {

        Update_ButtonsSlide(gameTime);
        Update_NessieSlide(gameTime);
        Update_TouristSlide(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Begin();
        spriteBatch.Draw(TLib.ControlsBackground, Vector2.Zero, Color.White);

        switch (CurrentSlide) {

            case Slide.Nessie:
                // Avatar
                spriteBatch.Draw(TLib.NessieAvatar, new Vector2(1920 / 2, (1080 / 4)+100), null, Color.White, 0, new Vector2(TLib.NessieAvatar.Width / 2, TLib.NessieAvatar.Height / 2), 1.35f, SpriteEffects.None, 0);
                // Title
                spriteBatch.Draw(TLib.NessieTitle, new Vector2(1920 / 2, (1080 / 4) - 125), null, Color.Black, 0, new Vector2(TLib.NessieTitle.Width / 2, TLib.NessieTitle.Height / 2), 0.5f, SpriteEffects.None, 0);
                break;

            case Slide.Tourist:
                // Avatar
                spriteBatch.Draw(TLib.TouristAvatar, new Vector2(1920 / 2, (1080 / 4) + 100), null, Color.White, 0, new Vector2(TLib.TouristAvatar.Width / 2, TLib.TouristAvatar.Height / 2), 1.2f, SpriteEffects.None, 0);
                // Title
                spriteBatch.Draw(TLib.TouristTitle, new Vector2(1920 / 2, (1080 / 4) - 125), null, Color.Black, 0, new Vector2(TLib.TouristTitle.Width / 2, TLib.TouristTitle.Height / 2), 0.5f, SpriteEffects.None, 0);
                break;
        }

        if (buttonsSlideOpacity > 0)
            Draw_ButtonSlide(spriteBatch);

        if (nessieSlideOpacity[0] > 0)
            Draw_NessieSlide(spriteBatch);

        if (touristSlideOpacity[0] > 0)
            Draw_TouristSlide(spriteBatch);


        spriteBatch.End();
    }

    public void OnSceneStart() {

        Reset_ButtonsSlide();
        Reset_NessieSlide();
        Reset_TouristSlide();
    }

    public void OnSceneEnd() {

        CurrentSlide = Slide.Buttons;
    }

    #endregion


    #region Button Slide

    float buttonsSlideOpacity;
    float buttonsSlideY;

    void Update_ButtonsSlide(GameTime gameTime) {

        // Fade out

        // If we're not on the buttons slide, and the buttons slide is visible, fade it out
        if (buttonsSlideOpacity > 0 && CurrentSlide != Slide.Buttons) {

            buttonsSlideOpacity = MathHelper.Lerp(buttonsSlideOpacity, 0f, 0.3f);
            buttonsSlideY = MathHelper.Lerp(buttonsSlideY, 1080+TLib.ButtonGuide.Height, 0.005f);

            if (Math.Abs(buttonsSlideOpacity) < 0.1f)
                buttonsSlideOpacity = 0f;

            return;
        }

        // If we're not of the buttons slide, return
        if (CurrentSlide != Slide.Buttons)
            return;

        // Fade in
        if (buttonsSlideOpacity < 1f) {

            buttonsSlideOpacity = MathHelper.Lerp(buttonsSlideOpacity, 1f, 0.1f);
            if (Math.Abs(buttonsSlideOpacity - 1f) < 0.01f)
                buttonsSlideOpacity = 1f;
        }

        if (buttonsSlideY > 1080-TLib.ButtonGuide.Height) {

            buttonsSlideY = MathHelper.Lerp(buttonsSlideY, 1080-TLib.ButtonGuide.Height, 0.5f);
            if (Math.Abs(buttonsSlideY - (1080-TLib.ButtonGuide.Height)) < 1f)
                buttonsSlideY = 1080-TLib.ButtonGuide.Height;
        }
    }

    void Draw_ButtonSlide(SpriteBatch spriteBatch) {

        spriteBatch.Draw(TLib.ButtonGuide, new Vector2(0,buttonsSlideY), Color.White * buttonsSlideOpacity);
    }

    void Reset_ButtonsSlide() {

            buttonsSlideOpacity = 0f;
            buttonsSlideY = 1080+TLib.ButtonGuide.Height;
    }

    #endregion


    #region Nessie Slide

    float[] nessieSlideOpacity = new float[3] { 0f, 0f, 0f };
    float nessieSlideY;
    float nessieSlideYGoal;
    float[] nessieSlideX = new float[3] { 0f, 0f, 0f };

    void Update_NessieSlide(GameTime gameTime) {

        // Fade out

        // If we're not on the nessie slide, and the nessie slide is visible, fade it out
        if (nessieSlideOpacity[0] > 0 && CurrentSlide != Slide.Nessie) {

            for (int i = 0; i < nessieSlideOpacity.Length; i++)
                nessieSlideOpacity[i] = MathHelper.Lerp(nessieSlideOpacity[i], 0f, 0.3f);

            nessieSlideY = MathHelper.Lerp(nessieSlideY, 1080+TLib.NessieGuide[0].Height, 0.005f);

            if (Math.Abs(nessieSlideOpacity[0]) < 0.1f)
                Reset_NessieSlide();

            return;
        }

        // If we're not of the nessie slide, return
        if (CurrentSlide != Slide.Nessie)
            return;

        // Fade in
        if (nessieSlideOpacity[0] < 1f) {

            for (int i = 0; i < nessieSlideOpacity.Length; i++)
                nessieSlideOpacity[i] = MathHelper.Lerp(nessieSlideOpacity[i], 1f, 0.1f);

            if (Math.Abs(nessieSlideOpacity[0] - 1f) < 0.01f)
                nessieSlideOpacity[0] = 1f;
        }

        if (nessieSlideY > nessieSlideYGoal) {

            nessieSlideY = MathHelper.Lerp(nessieSlideY, nessieSlideYGoal, 0.5f);
            if (Math.Abs(nessieSlideY - (nessieSlideYGoal)) < 1f)
                nessieSlideY = nessieSlideYGoal;
        }
    }

    void Draw_NessieSlide(SpriteBatch spriteBatch) {

        for (int i = 0; i < nessieSlideOpacity.Length; i++)
            spriteBatch.Draw(TLib.NessieGuide[i], new Vector2(nessieSlideX[i],nessieSlideY), Color.White * nessieSlideOpacity[i]);
    }

    void Reset_NessieSlide() {

        for (int i = 0; i < nessieSlideOpacity.Length; i++)
            nessieSlideOpacity[i] = 0f;

        nessieSlideY = 1080 + TLib.NessieGuide[0].Height;
        nessieSlideYGoal = 1080 - 50 - TLib.NessieGuide[0].Height;

        int x = 1920/3;
        int xx = x/2;

        for (int i = 0; i < nessieSlideX.Length; i++)
            nessieSlideX[i] = xx + (i * x) - TLib.NessieGuide[i].Width/2;
    }

    #endregion


    #region Tourist Slide

    float[] touristSlideOpacity = new float[3] { 0f, 0f, 0f };
    float touristSlideY;
    float touristSlideYGoal;
    float[] touristSlideX = new float[3] { 0f, 0f, 0f };

    void Update_TouristSlide(GameTime gameTime) {

        // Fade out

        // If we're not on the tourist slide, and the tourist slide is visible, fade it out
        if (touristSlideOpacity[0] > 0 && CurrentSlide != Slide.Tourist) {

            for (int i = 0; i < touristSlideOpacity.Length; i++)
                touristSlideOpacity[i] = MathHelper.Lerp(touristSlideOpacity[i], 0f, 0.3f);

            touristSlideY = MathHelper.Lerp(touristSlideY, 1080+TLib.TouristGuide[0].Height, 0.005f);

            if (Math.Abs(touristSlideOpacity[0]) < 0.1f)
                Reset_TouristSlide();

            return;
        }

        // If we're not of the tourist slide, return
        if (CurrentSlide != Slide.Tourist)
            return;

        // Fade in
        if (touristSlideOpacity[0] < 1f) {

            for (int i = 0; i < touristSlideOpacity.Length; i++)
                touristSlideOpacity[i] = MathHelper.Lerp(touristSlideOpacity[i], 1f, 0.1f);

            if (Math.Abs(touristSlideOpacity[0] - 1f) < 0.01f)
                touristSlideOpacity[0] = 1f;
        }

        if (touristSlideY > touristSlideYGoal) {

            touristSlideY = MathHelper.Lerp(touristSlideY, touristSlideYGoal, 0.5f);
            if (Math.Abs(touristSlideY - (touristSlideYGoal)) < 1f)
                touristSlideY = touristSlideYGoal;
        }
    }

    void Draw_TouristSlide(SpriteBatch spriteBatch) {

        for (int i = 0; i < touristSlideOpacity.Length; i++)
            spriteBatch.Draw(TLib.TouristGuide[i], new Vector2(touristSlideX[i],touristSlideY), Color.White * touristSlideOpacity[i]);
    }

    void Reset_TouristSlide() {

        for (int i = 0; i < touristSlideOpacity.Length; i++)
            touristSlideOpacity[i] = 0f;

        touristSlideY = 1080 + TLib.TouristGuide[0].Height;
        touristSlideYGoal = 1080 - 50 - TLib.TouristGuide[0].Height;

        int x = 1920/3;
        int xx = x/2;

        for (int i = 0; i < touristSlideX.Length; i++)
            touristSlideX[i] = xx + (i * x) - TLib.TouristGuide[i].Width/2;
    }

    #endregion

    enum Slide {

        Buttons = 0,
        Nessie = 1,
        Tourist = 2
    }
}
