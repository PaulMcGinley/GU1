using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GU1.Envir.Models;

public class Player : Actor {

    readonly Random rand = new();

    public CamView CameraView { get; set; }

    public ActorType Role { get; set; }
    /// <summary>
    /// The index of the controller that the player is using.
    /// </summary>
    public int ControllerIndex { get; set; } = -1;

    public int Score { get; set; } = 0;

    bool playedAsNessie = false;         // Track if the player has played as Nessie before
    bool playedAsTourist = false;        // Track if the player has played as Tourist before
    public bool playedBothRoles => playedAsNessie && playedAsTourist;

    public Player(int controllerIndex) {

       ControllerIndex = controllerIndex;
       CameraView = new CamView(Color.White, 5);
    }

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

        return rand.Bool() ? ActorType.Nessie : ActorType.Tourist;
    }

    /// <summary>
    /// Set the player's role.
    /// </summary>
    /// <param name="type"></param>
    public void SetPlayedAs(ActorType type) {

        Type = type;

        if (type == ActorType.Nessie)
            playedAsNessie = true;

        if (type == ActorType.Tourist)
            playedAsTourist = true;
    }

    /// <summary>
    /// Reset the player.
    /// </summary>
    public void Reset() {

        playedAsNessie = false;
        playedAsTourist = false;
        Score = 0;
    }

    public void AddScore(int score) => Score += score;

    public new void Update(GameTime gameTime) {

        // Guardian clause: if the player is not a tourist then don't update the camera view
        if (Role != ActorType.Tourist) return;

        CameraView.offset += GamePadRightStick(ControllerIndex) * 5;

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
