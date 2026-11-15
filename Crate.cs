using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LightsAndCameraExercise;

/// <summary>
/// A class for rendering a textured, lit crate
/// </summary>
public class Crate
{
    // The game this crate belongs to
    Game game;

    // The VertexBuffer of crate vertices
    VertexBuffer vertexBuffer;

    // The IndexBuffer defining the crate's triangles
    IndexBuffer indexBuffer;

    // The effect to render the crate with
    BasicEffect effect;

    // The texture to apply to the crate
    Texture2D texture;

    /// <summary>
    /// Creates a new crate instance
    /// </summary>
    /// <param name="game">The game this crate belongs to</param>
    /// <param name="type">The type of crate to use</param>
    /// <param name="world">The position and orientation of the crate in the world</param>
    public Crate(Game game, CrateType type, Matrix world)
    {
        this.game = game;
        this.texture = game.Content.Load<Texture2D>($"crate{(int)type}_diffuse");
        InitializeVertices();
        InitializeIndices();
        InitializeEffect();
        effect.World = world;
    }

    /// <summary>
    /// Initialize the vertex buffer
    /// </summary>
    public void InitializeVertices()
    {
        var vertexData = new VertexPositionNormalTexture[] {
            // Front Face
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Forward },

            // Back Face (the textbook uses Forward for three of these; Backward is correct)
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Backward },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, -1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Backward },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f,  1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Backward },
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f,  1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Backward },

            // Top Face
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, 1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Up },
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, 1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Up },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Up },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Up },

            // Bottom Face
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Down },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Down },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Down },
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Down },

            // Left Face
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Left },
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Left },
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Left },
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Left },

            // Right Face
            new VertexPositionNormalTexture() { Position = new Vector3(1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Right },
            new VertexPositionNormalTexture() { Position = new Vector3(1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Right },
            new VertexPositionNormalTexture() { Position = new Vector3(1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Right },
            new VertexPositionNormalTexture() { Position = new Vector3(1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Right },
        };
        vertexBuffer = new VertexBuffer(game.GraphicsDevice, typeof(VertexPositionNormalTexture), vertexData.Length, BufferUsage.None);
        vertexBuffer.SetData<VertexPositionNormalTexture>(vertexData);
    }

    /// <summary>
    /// Initializes the index buffer
    /// </summary>
    public void InitializeIndices()
    {
        var indexData = new short[]
        {
            0, 2, 1,  0, 3, 2,       // Front
            4, 6, 5,  4, 7, 6,       // Back
            8, 10, 9,  8, 11, 10,    // Top
            12, 14, 13,  12, 15, 14, // Bottom
            16, 18, 17,  16, 19, 18, // Left
            20, 22, 21,  20, 23, 22  // Right
        };
        indexBuffer = new IndexBuffer(game.GraphicsDevice, IndexElementSize.SixteenBits, indexData.Length, BufferUsage.None);
        indexBuffer.SetData<short>(indexData);
    }

    /// <summary>
    /// Initializes the BasicEffect to render our crate
    /// </summary>
    void InitializeEffect()
    {
        effect = new BasicEffect(game.GraphicsDevice);
        effect.World = Matrix.CreateScale(2.0f);
        effect.View = Matrix.CreateLookAt(new Vector3(8, 9, 12), new Vector3(0, 0, 0), Vector3.Up);
        effect.Projection = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.PiOver4, game.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100.0f);
        effect.TextureEnabled = true;
        effect.Texture = texture;

        // Turn on lighting
        effect.LightingEnabled = true;

        // Set up light 0
        effect.DirectionalLight0.Enabled = true;
        effect.DirectionalLight0.Direction = new Vector3(1f, 0, 1f);
        effect.DirectionalLight0.DiffuseColor = new Vector3(0.8f, 0, 0);
        effect.DirectionalLight0.SpecularColor = new Vector3(1f, 0.4f, 0.4f);

        // Ambient light so shadowed faces are still faintly visible
        effect.AmbientLightColor = new Vector3(0.3f, 0.3f, 0.3f);
    }

    /// <summary>
    /// Draws the crate
    /// </summary>
    /// <param name="camera">The camera to use to draw the crate</param>
    public void Draw(ICamera camera)
    {
        effect.View = camera.View;
        effect.Projection = camera.Projection;
        effect.CurrentTechnique.Passes[0].Apply();
        game.GraphicsDevice.SetVertexBuffer(vertexBuffer);
        game.GraphicsDevice.Indices = indexBuffer;
        game.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 12);
    }
}
