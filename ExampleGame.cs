using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LightsAndCameraExercise;

public class ExampleGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // The crates in the scene
    Crate[] crates;

    // The camera
    FPSCamera camera;

    public ExampleGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Make some crates
        crates = new Crate[] {
            new Crate(this, CrateType.DarkCross, Matrix.Identity),
            new Crate(this, CrateType.Slats, Matrix.CreateTranslation(4, 0, 5)),
            new Crate(this, CrateType.Cross, Matrix.CreateTranslation(-8, 0, 3)),
            new Crate(this, CrateType.DarkCross, Matrix.CreateRotationY(MathHelper.PiOver4) * Matrix.CreateTranslation(1, 0, 7)),
            new Crate(this, CrateType.Slats, Matrix.CreateTranslation(3, 0, -3)),
            new Crate(this, CrateType.Cross, Matrix.CreateRotationY(3) * Matrix.CreateTranslation(3, 2, -3))
        };

        // Initialize the camera
        camera = new FPSCamera(this, new Vector3(0, 3, 10));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Update the camera
        camera.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Ensure depth testing is on (SpriteBatch can change it)
        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

        // Draw the crates
        foreach (Crate crate in crates)
        {
            crate.Draw(camera);
        }

        base.Draw(gameTime);
    }
}
