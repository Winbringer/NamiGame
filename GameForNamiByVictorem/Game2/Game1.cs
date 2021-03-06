﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using GameForNamiFromVictorem.Model;
using System.Linq;
using System.IO;
using System;
using System.Threading;

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
        SoundEffect soundEffect;
        SoundEffectInstance soundEffectInstance;
        const int frameWidth = 400;
        const int frameHeight = 400;
        public int framesY;
        List<AbstractGameCharakter> chars;
        Nami player;
        int period = 300;
        int period2 = 0;
        int memSpeed = 1;
        public int fuckSpeed = 10;
        int playerSpeed = 8;
        public int TotalKilled = 0;
        bool IsNewLvl = false;
        int TimePassed2 = 0;
        int period3 = 2000;

        public bool IsPaused { get; set; }
        public bool IsLoose { get; set; }
        public int GameLVL { get; set; }
        public int TimePassed { get; set; }
        public int FrameWidth { get { return frameWidth; } }
        public int FrameHeight { get { return frameHeight; } }
        public float Scale { get; set; }
        public SpriteBatch SpiteBatch { get { return this.spriteBatch; } }
        public List<AbstractGameCharakter> Chars { get { return chars; } }
        public Texture2D Fuck { get { return fuck; } }
        public int Heiters { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            int ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.PreferredBackBufferWidth = ScreenWidth;
            Window.Title = "For Nami from Victorem <3";
            Window.AllowUserResizing = false;
            chars = new List<AbstractGameCharakter>();
            this.Scale = 0.2f;
            IsLoose = false;
            IsPaused = true;
            framesY = (int)(ScreenHeight / (frameHeight * Scale));
            Heiters = 0;
            GameLVL = 0;
            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
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
            soundEffect = Content.Load<SoundEffect>("sound");
            player = new Nami(this)
            {
                Alive = true,
                game = this,
                Position = new Vector2(5f, Window.ClientBounds.Height / 2 - 50),
                Texture = Sprite,
                Size = new Microsoft.Xna.Framework.Point(frameWidth, frameHeight),
                Speed = playerSpeed
            };
            chars.Add(player);
            soundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.IsLooped = true;
            soundEffectInstance.Play();
           
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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                Exit();
                return;
            }
            else
            {
                if (IsActive)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) { IsPaused = false; }
                    if (Keyboard.GetState().IsKeyDown(Keys.P)) IsPaused = true;
                    if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))if(soundEffectInstance.Volume>0.02f) soundEffectInstance.Volume -= 0.01f;
                    if (Keyboard.GetState().IsKeyDown(Keys.OemPlus)) if (soundEffectInstance.Volume < 1f) soundEffectInstance.Volume += 0.01f;
                    if (!IsPaused && !IsLoose)
                    {
                        chars = chars.Where(x => x.Alive).ToList();
                        AddEnemies(gameTime);
                        LvlUp();
                        player.Shoot();
                        foreach (var item in chars)
                        {
                            item.Move();
                        }
                        Collide();
                        base.Update(gameTime);
                    }
                    NewGame();
                }
            }
        }
            
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(fon, Vector2.Zero, Microsoft.Xna.Framework.Color.White);
            if (IsPaused)
            {
                PauseDraw();
            }
            else if (IsLoose)
            {
                LooseDraw();
            }
            else
            {
                foreach (var item in chars)
                {
                    item.Draw();
                }
            }
            spriteBatch.End();
            if (IsNewLvl)
            {
                IsPaused = true;
                NewLvlDraw();
                TimePassed2 += gameTime.ElapsedGameTime.Milliseconds;
                if (TimePassed2 > period3)
                {
                    IsNewLvl = false;
                    IsPaused = false;
                }
            }
            base.Draw(gameTime);
        }

        #region Вспомогательные методы для Update

        private void AddEnemies(GameTime gameTime)
        {
            TimePassed += gameTime.ElapsedGameTime.Milliseconds;
            if (TimePassed > period)
            {
                TimePassed -= period;
                chars.Add(new Mem(this) { Speed = memSpeed, Texture = Sprite });
                ++period2;
            }
        }

        private void LvlUp()
        {
            if (Heiters > 50)
            {
                Heiters -= 50;
                period2 = 0;
                fuckSpeed += 1;
                memSpeed += 1;
                playerSpeed += 1;
                chars = new List<AbstractGameCharakter>();
                player = new Nami(this)
                {
                    Alive = true,
                    Position = new Vector2(5f, Window.ClientBounds.Height / 2 - 50),
                    Texture = Sprite,
                    Size = new Microsoft.Xna.Framework.Point(frameWidth, frameHeight),
                    Speed = playerSpeed
                };
                GameLVL += 1;
                IsNewLvl = true;
                chars.Add(player);
                TimePassed2 = 0;

            }

        }

        private void Collide()
        {
            foreach (var i in chars)
            {
                foreach (var j in chars)
                {
                    if (!i.Equals(j) && i.Collide(j))
                    {
                        if (!(i is Mem) || !(j is Mem))
                        {
                            i.Die();
                            j.Die();
                        }
                    }
                }
            }
        }

        private void NewGame()
        {
            if (IsLoose && Keyboard.GetState().IsKeyDown(Keys.N))
            {
                chars = new List<AbstractGameCharakter>();
                player = new Nami(this)
                {
                    Alive = true,
                    game = this,
                    Position = new Vector2(5f, Window.ClientBounds.Height / 2 - 50),
                    Texture = Sprite,
                    Size = new Microsoft.Xna.Framework.Point(frameWidth, frameHeight),
                    Speed = playerSpeed
                };
                chars.Add(player);
                memSpeed = 1;
                fuckSpeed = 10;
                playerSpeed = 8;
                Heiters = 0;
                TotalKilled = 0;
                IsLoose = false;
                GameLVL = 0;
            }
        }
        #endregion

        #region Вспомогательные методы для Draw

        private void LooseDraw()
        {
            int now = 0;
            if (File.Exists("Records.txt"))
            {
                string record = File.ReadAllText("Records.txt");
                now = Convert.ToInt32(record);
            }
            if (now < Heiters) File.WriteAllText("Records.txt", Heiters.ToString());
            Microsoft.Xna.Framework.Rectangle rect = new Microsoft.Xna.Framework.Rectangle(FrameWidth * 2, FrameHeight * 2, FrameWidth, FrameHeight);
            Vector2 pos = new Vector2((Window.ClientBounds.Width / 2 - FrameWidth / 2), (Window.ClientBounds.Height / 2 - FrameHeight / 2));
            SpiteBatch.Draw(Sprite, pos, rect, Microsoft.Xna.Framework.Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            string end = "ВЫ ПРОИГРАЛИ! \n\rХЕЙТЕРОВ УНИЧТОЖЕННО: " + TotalKilled + "\n\rРЕКОРД ЗА ВСЕ ВРЕМЯ: " + now + "\n\rДля продолжения \n\rнажмите английскую \n\rклавишу N на клавиатуре \n\rДля выхода нажмите Esc";
            spriteBatch.DrawString(font, end, pos, Microsoft.Xna.Framework.Color.Purple);
        }

        private void PauseDraw()
        {
            string text = "  Для выхода из игры нажмите \"Esc\" или \"Alt+F4\". \n\rДля переключения между окнами нажмите \"Alt + Tab\" \n\rДля того чтобы начать игру нажмите \"Пробел\" - \"Space\". \n\rДля того чтобы поставить игру на паузу нажмите английскую букву \"P\" на клавиатуре \n\rДля движения используйте клавищи \"W A S D\" или Стрелочки на клавиатуре. \n\rДля стрельбы используйте левую кнопку мыши или клавишу ввод - \"Enter\" \n\rДля того чтобы сделать музыку громче нажмите \"+\", тише \"-\" \n\rГлавный Альфа, Бетта, Гамма тестер игры \n\rи просто классный парень Yoko aka Иван Бондарев \n\r2015 год. Victorem для Nami <3";
            spriteBatch.DrawString(font, text, Vector2.Zero, Microsoft.Xna.Framework.Color.White);
        }

        void NewLvlDraw()
        {
            int x=4;
            int y=2;

            switch (GameLVL)
            {
                case 1: x = 4; y = 2; break;
                case 2: x = 5; y = 2; break;
                case 3: x = 0; y = 3; break;
                case 4: x = 1; y = 3; break;
                case 5: x = 2; y = 3; break;
                case 6: x = 3; y = 3; break;
                case 7: x = 4; y = 3; break;
                case 8: x = 5; y = 3; break;
                case 9: x = 0; y = 4; break;
                case 10: x = 1; y = 4; break;
                case 11: x = 2; y = 4; break;
                case 12: x = 3; y = 4; break;
                case 13: x = 4; y = 4; break;
                case 14: x = 5; y = 4; break;
                case 15: x = 0; y = 5; break;
                case 16: x = 1; y = 5; break;
                case 17: x = 2; y = 5; break;
                case 18: x = 3; y = 5; break;
                case 19: x = 4; y = 5; break;
                case 20: x = 5; y = 5; break;
                default: break;
            }
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(fon, Vector2.Zero, Microsoft.Xna.Framework.Color.White);
            Microsoft.Xna.Framework.Rectangle rect = new Microsoft.Xna.Framework.Rectangle(FrameWidth * x,
                FrameHeight * y,
                FrameWidth,
                FrameHeight);
            Vector2 pos = new Vector2((Window.ClientBounds.Width / 2 - FrameWidth / 2),
                (Window.ClientBounds.Height / 2 - FrameHeight / 2));
            SpiteBatch.Draw(Sprite, pos, rect, Microsoft.Xna.Framework.Color.White, 0,
                Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.End();
        }
        #endregion
    }
}
