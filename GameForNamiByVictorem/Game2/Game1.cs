using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using GameForNamiFromVictorem.Model;
using System;

//using System.Windows.Forms;

namespace GameForNamiFromVictorem
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D fon;
        Texture2D Sprite;
        Texture2D fuck;
        SoundEffect shoot;
        SpriteFont font;
        const int frameWidth = 400;
        const int frameHeight = 400;         
      public  bool IsPaused { get; set; }
      public bool IsLoose
        {
            get; set;
        }
        List<AbstractGameCharakter> chars;
        Nami player;
       
       public int TimePassed { get; set; }

        public int FrameWidth { get { return frameWidth; } }
        public int FrameHeight { get { return frameHeight; } }        
        public float Scale { get; set; }
        public SpriteBatch SpiteBatch { get { return this.spriteBatch; } }
        public List<AbstractGameCharakter> Chars { get { return chars; } }
        public Texture2D Fuck { get { return fuck; } }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            int ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.PreferredBackBufferWidth = ScreenWidth;
            Window.Title = "For Nami from Victorem <3";
            Window.AllowUserResizing = true;
            chars = new List<AbstractGameCharakter>();
            this.Scale = 0.1f;
            IsLoose = false;
            IsPaused = false;
            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //graphics.IsFullScreen = true;       
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

            // TODO: use this.Content to load your game content here
            fon = Content.Load<Texture2D>("fon");
            Sprite = Content.Load<Texture2D>("Sprite");
            fuck = Content.Load<Texture2D>("fuck");            
            font = Content.Load<SpriteFont>("font");
            player = new Nami() { Alive = true, game = this, Position = Vector2.Zero, Texture = Sprite, Size = new Microsoft.Xna.Framework.Point(frameWidth, frameHeight), Speed = 5 };
            chars.Add(player);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                Exit();
            }
            else
            {
                TimePassed = gameTime.ElapsedGameTime.Milliseconds;
                player.Shoot();
                foreach (var item in chars)
                {                    
                    item.Move();
                }
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(fon, Vector2.Zero, Microsoft.Xna.Framework.Color.White);
            string text = "Для выхода из игры нажмите Esc или Alt+F4. \n\rДля переключения между окнами нажмите Alt + Tab \n\rДля паузы / играть нажмите пробел. \n\rДля движения используйте клавищи W S A D или Стрелочки на клавиатуре. \n\rДля стрельбы используйте левую кнопку мыши или клавишу ввод (Enter) \n\r2015 год. Victorem для Nami <3 \n\rГруппа стримерши Юля Nami: vk.com/kezumie \n\rОбъектов "+chars.Count;
            spriteBatch.DrawString(font, text, Vector2.Zero, Microsoft.Xna.Framework.Color.Red);
            foreach (var item in chars)
            {
                item.Draw();
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
