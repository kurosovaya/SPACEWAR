using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using TextureAtlas;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Spacewar
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Texture2D background;
        Texture2D ShellTexture;
        SpaceShip shuttle;        
        Entity2D animeGirl;
        private float angle = 0f;


        private Texture2D blue;
        private Texture2D green;
        private Texture2D red;
        private float blueAngle = 0;
        private float greenAngle = 0;
        private float redAngle = 0;

        private float blueSpeed = 0.025f;
        private float greenSpeed = 0.017f;
        private float redSpeed = 0.022f;

        private float distance = 100;

        public static List<Shell> arrayOfShells = new List<Shell>();
        public static List<Boss> arrayOfBosses = new List<Boss>();
        public static string jopa = "afafaf";

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Song song;       
        //private AnimatedSprite animatedSprite;
        private SpaceBackground spaceBackground;
        public static ContentManager MyContent;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here   
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MyContent = Content;

            // TODO: use this.Content to load your game content here           

            background = Content.Load<Texture2D>("BlackSky");
            spriteFont = Content.Load<SpriteFont>("Text");
            Texture2D texture = Content.Load<Texture2D>("SmileyWalk");
            //animatedSprite = new AnimatedSprite(texture, 4, 4);
            spaceBackground = new SpaceBackground(
                graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight,
                100,
                new Texture2D[] {
                Content.Load<Texture2D>("blue2"),
                Content.Load<Texture2D>("blue3"),
                Content.Load<Texture2D>("yellow"),
                Content.Load<Texture2D>("white"),
                Content.Load<Texture2D>("white"),
                Content.Load<Texture2D>("white"),
                }
            );
            arrayOfBosses.Add(new Boss(0, 0, Content.Load<Texture2D>("Cat"), 0.2f, 5f, 90f));
            arrayOfBosses.Add(new Boss(0, 1000, Content.Load<Texture2D>("ZLR2nRL5huk"), 0.2f, 5f, 90f));
            spaceBackground.StarsGenerate();

            blue = Content.Load<Texture2D>("blue");
            green = Content.Load<Texture2D>("green");
            red = Content.Load<Texture2D>("red");

            ShellTexture = Content.Load<Texture2D>("s120014250");

            shuttle = new SpaceShip(
                graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2,
                Content.Load<Texture2D>("shuttle"), ShellTexture, arrayOfShells, 0.5f, 600f, 150f, 90f);

            animeGirl = new Entity2D(
                graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2,
                Content.Load<Texture2D>("s120014250"), 0.05f, 0f);


            this.song = Content.Load<Song>("Kyle Richards - Wave Fight 5");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.0f;                       
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();
            shuttle.Move(kstate, gameTime);
            foreach (Shell shell in arrayOfShells) {
                shell.Move();
            }
            shuttle.Collision(graphics);

            foreach (Boss boss in arrayOfBosses) {
                boss.MoveToTarget(shuttle);
                boss.Hit();
            }

            for (int i = 0; i < arrayOfBosses.Count; i++) {
                if (arrayOfBosses[i].Exist == false) {
                    arrayOfBosses.Remove(arrayOfBosses[i]);

                }
            }


            //animatedSprite.Update();


            animeGirl.Angle = angle;
            angle += 1000f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            angle = angle > 360f ? 0f : angle;


            blueAngle += blueSpeed;
            greenAngle += greenSpeed;
            redAngle += redSpeed;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            Vector2 bluePosition = new Vector2(
                (float)Math.Cos(blueAngle) * distance,
                (float)Math.Sin(blueAngle) * distance);
            Vector2 greenPosition = new Vector2(
                (float)Math.Cos(greenAngle) * distance,
                (float)Math.Sin(greenAngle) * distance);
            Vector2 redPosition = new Vector2(
                (float)Math.Cos(redAngle) * distance,
                (float)Math.Sin(redAngle) * distance);

            Vector2 center = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);


            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            shuttle.Draw(spriteBatch);

            foreach (Shell shell in arrayOfShells) {
                shell.Draw(spriteBatch);
            }

            animeGirl.Draw(spriteBatch);
            spriteBatch.DrawString(spriteFont, "SPACEWAR Pre Alpha build", new Vector2(100, 100), Color.White);

            foreach (Boss boss in arrayOfBosses) {
                boss.Draw(spriteBatch);
            }

            spriteBatch.End();



            spaceBackground.Next(spriteBatch);
            //animatedSprite.Draw(spriteBatch, new Vector2(400, 200));


            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            spriteBatch.Draw(blue, center + bluePosition - new Vector2(blue.Width, blue.Height) / 2, Color.White);
            spriteBatch.Draw(green, center + greenPosition - new Vector2(green.Width, green.Height) / 2, Color.White);
            spriteBatch.Draw(red, center + redPosition - new Vector2(red.Width, red.Height) / 2, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
