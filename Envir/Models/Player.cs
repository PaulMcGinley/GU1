using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Player : Actor {

    readonly Random rand = new();

    public CamView CameraView { get; set; }
    public long GroupID { get; set; }

    public ActorType Role { get; set; }
    /// <summary>
    /// The index of the controller that the player is using.
    /// </summary>
    public int ControllerIndex { get; set; } = -1;

    public int Score { get; set; } = 0;

    bool playedAsNessie = false;         // Track if the player has played as Nessie before
    bool playedAsTourist = false;        // Track if the player has played as Tourist before
    public bool playedBothRoles => playedAsNessie && playedAsTourist;

    const int MaxPhotos = 5;
    public Player(int controllerIndex) {

       ControllerIndex = controllerIndex;

        // TODO: Create a list of colours
       Color c = new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
       CameraView = new CamView(c, GameState.MaxPhotos);
    }

    public void SetName(string name) => CameraView.playerName = name;

    /// <summary>
    /// Player is responsible for choosing their preferred role.
    /// If they have played as Nessie or Tourist before, they will prefer the other role.
    /// If they have not played before, they will randomly choose a role.
    /// If they have played both roles, they will randomly choose a role.
    /// </summary>
    /// <returns></returns>
    public ActorType PreferredRole() {

        if (!playedAsNessie)
            return ActorType.Nessie;

        if (!playedAsTourist)
            return ActorType.Tourist;

        return ActorType.Tourist;
    }

    /// <summary>
    /// Set the player's role.
    /// </summary>
    /// <param name="type"></param>
    public void SetPlayedAs(ActorType role) {

        Role = role;

        if (role == ActorType.Nessie)
            playedAsNessie = true;

        if (role == ActorType.Tourist)
            playedAsTourist = true;

        CameraView.Reset();
    }

    /// <summary>
    /// Reset the player.
    /// </summary>
    public void Reset() {

        playedAsNessie = false;
        playedAsTourist = false;
        Score = 0;
        CameraView.Reset();
    }

    public void AddScore(int score) => Score += score;

    public new void Update(GameTime gameTime) {

        // Guardian clause: if the player is not a tourist then don't update the camera view
        if (Role != ActorType.Tourist) return;

        CameraView.offset += (GamePadRightStick(ControllerIndex) * (float)gameTime.ElapsedGameTime.TotalSeconds) * (GameState.ControllerSensitivity*500);

        CameraView.position = GameState.Boat.Position;
        CameraView.Update(gameTime);
    }

    public new void FixedTimestampUpdate(GameTime gameTime) {

    }

    public new void Draw(SpriteBatch spriteBatch) {

        // Guardian clause: if the player is not a tourist then don't draw the camera view
        if (Role != ActorType.Tourist) return;

        CameraView.Draw(spriteBatch);
    }
}
