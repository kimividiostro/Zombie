using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Igra
{
    public class Game1 : Game
    {
        private void NewGame()
        {
            gameIsOver = false;

            ground = (graphics.PreferredBackBufferHeight / 2);
            player = new Player(texture: playerRight, health: 100, position: new Vector2(graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2));
            damage = new Random();
            enemy = new Enemy(texture: enemyRight, position: new Vector2((player.Position.X + 200), player.Position.Y), damage: 1);


            zombiesKilled = 0;
            zombieRespawnX = 500;
            gravity = new Vector2(0, 2.5f);
            throwGravity = new Vector2(0, 0.5f);
            groundPosition = new Vector2(-5, (graphics.PreferredBackBufferHeight / 2 + 50));

            healthSpritePosition = new Vector2(10, 10);
            scoreSpritePosition = new Vector2(650, 10);

            knife = new Knife(player, knifeTextures, player.Position);
            knifeAnimationTime = 0;
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont healthSprite;
        SpriteFont gameOverSprite;

        Vector2 gravity;
        Vector2 throwGravity;
        Vector2 groundPosition;
        Vector2 knifeThrowPosition;
        Vector2 healthSpritePosition, scoreSpritePosition;

        Knife knife;

        Texture2D playerLeft, playerRight, enemyLeft, enemyRight, groundTexture;
        Texture2D[] knifeTextures; 
        int knifeTextureIndex;
        float knifeAnimationTime;

        Player player;
        Enemy enemy;

        Color backgroundColor;

        bool gameIsOver;
        bool playerFacingLeft;

        int ground;
        int zombiesKilled;
        int zombieRespawnX;
        Random damage;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.ToggleFullScreen();
            graphics.PreferredBackBufferHeight = 1024;
            graphics.PreferredBackBufferHeight = 768;
        }


        protected override void Initialize()
        {
            #region knifeTextures
            knifeTextureIndex = 0;
            Texture2D knife1 = Content.Load<Texture2D>(@"Knife\knife1");
            Texture2D knife2 = Content.Load<Texture2D>(@"Knife\knife2");
            Texture2D knife3 = Content.Load<Texture2D>(@"Knife\knife3");
            Texture2D knife4 = Content.Load<Texture2D>(@"Knife\knife4");
            knifeTextures = new Texture2D[4];
            knifeTextures[0] = knife1; knifeTextures[1] = knife2; knifeTextures[2] = knife3; knifeTextures[3] = knife4;
            #endregion
            Window.IsBorderless = true; 
            playerRight = Content.Load<Texture2D>("player");
            enemyRight = Content.Load<Texture2D>("enemy");
            NewGame();
            /*
            gameIsOver = false;

            ground = (graphics.PreferredBackBufferHeight / 2);
            player = new Player(texture: playerRight, health: 100, position: new Vector2(graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2));
            damage = new Random();
            enemy = new Enemy(texture: enemyRight, position: new Vector2((player.Position.X + 200), player.Position.Y),damage:1);


            zombiesKilled = 0;
            zombieRespawnX = 500;
            gravity = new Vector2(0, 2.5f);
            throwGravity = new Vector2(0, 0.5f);
            groundPosition = new Vector2(-5, (graphics.PreferredBackBufferHeight / 2 + 50));

            healthSpritePosition = new Vector2(10, 10);
            scoreSpritePosition = new Vector2(650, 10);

            knife = new Knife(player, knifeTextures, player.Position);
            knifeAnimationTime = 0;*/


            backgroundColor = Color.AntiqueWhite;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            healthSprite = Content.Load<SpriteFont>("HealthSprite");
            gameOverSprite = Content.Load<SpriteFont>("GameOverSprite");
            

            playerLeft = Content.Load<Texture2D>("playerLeft");
            enemyLeft = Content.Load<Texture2D>("enemyLeft");
            groundTexture = Content.Load<Texture2D>("ground");

            // TODO: use this.Content to load your game content here
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.CollisionSpace = new Rectangle((int)player.Position.X, (int)player.Position.Y, (int)player.Texture.Width, (int)player.Texture.Height);
            enemy.CollisionSpace = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, (int)enemy.Texture.Width, (int)enemy.Texture.Height);

            if (gameIsOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                    NewGame();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            #region check if player is within bounds
            if ((player.Position.X + 24) >= GraphicsDevice.Viewport.Width)
                player.Position.X = GraphicsDevice.Viewport.Width - 24;
            if ((player.Position.X - 24) <= 0)
                player.Position.X = 24;
            #endregion
            if (!gameIsOver)
                backgroundColor = Color.AntiqueWhite;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                player.MoveLeft(time, playerLeft);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                player.MoveRight(time, playerRight);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                player.IsJumping();
            if(player.Jumping)
                    player.Jump(ground, gravity);

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !knife.IsThrown && player.Alive)
            {
                knife.IsThrown = true;
                playerFacingLeft = player.IsFacingLeft(); 
            }
            if (knife.IsThrown && !knife.HitGround)
            {
                if (playerFacingLeft)
                    knife.Throw(true, knifeThrowPosition, gravity);
                else
                    knife.Throw(false, knifeThrowPosition, gravity);
                if (knife.Position.Y >= (ground + 50))
                    knife.HitGround = true;
                if(knife.HitGround)
                    knife.ResetThrowVelocity();
            }

            if (!knife.IsThrown)
                knife.Position = player.Position;

            if (knife.IsThrown && knife.EnemyIsHit(enemy))
            {
                knife.ResetThrowVelocity();
                if (zombiesKilled < 15)
                    zombieRespawnX -= 20;
                if(damage.Next(0,2) == 0)
                    enemy.Position = new Vector2((player.Position.X + zombieRespawnX), ground);
                else
                    enemy.Position = new Vector2((player.Position.X - zombieRespawnX), ground);
                zombiesKilled++;

                if (zombiesKilled % 10 == 0 && player.Health < 50)
                    player.Health += 10;

                if (zombiesKilled < 30)
                {
                    enemy.Speed *= 1.05f;
                    player.Speed *= 1.06f;
                }
                if(zombiesKilled > 80 && zombiesKilled < 100)
                {
                    if(zombiesKilled >= 90)
                    {
                        enemy.Speed *= 1.01f;
                        player.Speed *= 1.02f;
                    }
                    zombieRespawnX -= 1;
                }
            }
            if (enemy.Colided && player.Alive)
                backgroundColor = Color.Crimson;

            if (player.Alive && enemy.Alive)
                enemy.SeekPlayer(player: player, left: enemyLeft, right: enemyRight, time: time);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);
            spriteBatch.Begin();
            if (player.Alive)
            {
                spriteBatch.Draw(groundTexture, groundPosition, Color.White);
                spriteBatch.DrawString(healthSprite, ("Health: " + player.Health), healthSpritePosition, Color.Red);
                spriteBatch.Draw(texture: player.Texture, position: player.Position, color: Color.White);
            }
            else
            {
                gameIsOver = true;
                player.Position = new Vector2(0, 0);
                enemy.Position = new Vector2(100, 100);
                backgroundColor = Color.Black;
                spriteBatch.DrawString(spriteFont: gameOverSprite, text: "GAME OVER!", position: new Vector2(140, 180), color: Color.LawnGreen);
                spriteBatch.DrawString(spriteFont: healthSprite, text: "Press 'R' to restart", position: new Vector2(320, 280), color: Color.LawnGreen);
            }
            if (enemy.Alive && player.Alive)
                spriteBatch.Draw(texture: enemy.Texture, position: enemy.Position, color: Color.White);
            spriteBatch.DrawString(healthSprite, ("Zombies killed: " + zombiesKilled), scoreSpritePosition, Color.Green);

            if (knife.IsThrown)
            {
                knifeAnimationTime += gameTime.ElapsedGameTime.Milliseconds;
                if (knifeTextureIndex == 4)
                    knifeTextureIndex = 0;
                if(knifeAnimationTime >= 5)
                { 
                    spriteBatch.Draw(knifeTextures[knifeTextureIndex], knife.Position, Color.White);
                    knifeTextureIndex++;
                    knifeAnimationTime = 0;
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
