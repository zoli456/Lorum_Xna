using System;
using System.Threading;
using GameStateManagement;
using Lórum.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Nuclex.Game.Content;

namespace Lórum.Screens
{
    public class GameTable : GameScreen
    {
        public static bool showhintprice;
        public static bool player2showspeech;
        public static bool player3showspeech;
        public static bool player4showspeech;
        public static int player2showspeech_mode;
        public static int player3showspeech_mode;
        public static int player4showspeech_mode;
        public static bool showwish;
        public static bool firtRun = true;
        public static SoundEffect[] SoundEffect = new SoundEffect[8];
        public static double RollerTime = 10;
        public static bool EnabledCard;
        public static int JátékosKártyákSzáma = 8;
        public static int Játékos2KártyákSzáma = 8;
        public static int Játékos3KártyákSzáma = 8;
        public static int Játékos4KártyákSzáma = 8;
        public static bool VisibleArrow;
        public static int TextX = 260, TextY;
        public static bool GameEndead;
        public static int GameEndedTitle;
        public static bool VisitEnemies;
        private static readonly Texture2D[] CardsDeck1 = new Texture2D[33];
        private static readonly Texture2D[] CardsDeck2 = new Texture2D[33];
        private static readonly Texture2D[] CardsDeck3 = new Texture2D[33];
        private static Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
        private readonly Video[] _video = new Video[11];
        private readonly Texture2D[] Arrow = new Texture2D[2];
        private readonly Texture2D[] Buttons = new Texture2D[4];
        private readonly Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);
        private readonly Texture2D[] Items = new Texture2D[7];
        private readonly Main main = new Main();
        private readonly Random random = new Random();
        private readonly Texture2D[] Speech = new Texture2D[3];
        private readonly SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
        private readonly int videonumber;
        private SpriteFont _font;
        private SpriteFont _font2;
        private float _pauseAlpha;
        private VideoPlayer _player;
        private Texture2D _videoTexture;
        public int ArrowRotate = 22;
        public int ArrowStatusSlide;
        public int ArrowStatusUp = 1;
        public int ArrowX = 160, ArrowY = 570;
        private Texture2D blank;
        private Texture2D CardPlace;
        private LzmaContentManager Content;
        private bool controller;
        private Texture2D Lefordított;
        private MouseState mouseStateCurrent, mouseStatePrevious;
        private int penalty;
        public Rectangle player2, player3, player4;
        public bool player2showtick, player3showtick, player4showtick;
        private Texture2D Question_mark;
        private Rectangle rectangle1, rectangle2;
        private Texture2D Tick;

        public GameTable()
        {
            Main.Passz_Engedve = false;
            Main.Adás_Engedve = false;
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            videonumber = random.Next(1, 10);
            firtRun = true;
            player2 = new Rectangle((int)(725 * LórumGame.scale), (int)(100 * LórumGame.scale2),
                (int)(60 * LórumGame.scale2), (int)(240 * LórumGame.scale2));
            player3 = new Rectangle((int)(295 * LórumGame.scale), (int)(10 * LórumGame.scale2),
                (int)(210 * LórumGame.scale2), (int)(100 * LórumGame.scale2));
            player4 = new Rectangle((int)(25 * LórumGame.scale), (int)(120 * LórumGame.scale2),
                (int)(60 * LórumGame.scale2), (int)(220 * LórumGame.scale2));
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                Color.CornflowerBlue, 0, 0);
        }

        public override void LoadContent()
        {
            //  if (Content == null)
            //    Content = new ContentManager(ScreenManager.Game.Services, "Content");
            if (Content == null)
                Content = new LzmaContentManager(
                    ScreenManager.Game.Services, "Main.pack", true);
            blank = new Texture2D(ScreenManager.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
            _font = Content.Load<SpriteFont>("Content/Fonts/Font1");
            _font2 = Content.Load<SpriteFont>("Content/Fonts/Font2");
            CardPlace = Content.Load<Texture2D>("Content/Images/CardPlace");
            Tick = Content.Load<Texture2D>("Content/Images/tick");
            Lefordított = Content.Load<Texture2D>("Content/Images/Leforditott");
            SoundEffect[0] = Content.Load<SoundEffect>("Content/Sound/UI_Clicks01");
            SoundEffect[1] = Content.Load<SoundEffect>("Content/Sound/UIerror2");
            SoundEffect[2] = Content.Load<SoundEffect>("Content/Sound/UI_Clicks02");
            SoundEffect[3] = Content.Load<SoundEffect>("Content/Sound/Cheering1");
            SoundEffect[4] = Content.Load<SoundEffect>("Content/Sound/Cheering2");
            SoundEffect[5] = Content.Load<SoundEffect>("Content/Sound/Evil1");
            SoundEffect[6] = Content.Load<SoundEffect>("Content/Sound/Evil2");
            SoundEffect[7] = Content.Load<SoundEffect>("Content/Sound/coins_1");
            Arrow[0] = Content.Load<Texture2D>("Content/Images/Forward Arrow");
            Buttons[1] = Content.Load<Texture2D>("Content/images/passz");
            Buttons[2] = Content.Load<Texture2D>("Content/images/új játék");
            Items[0] = Content.Load<Texture2D>("Content/Images/Győzelem");
            Items[1] = Content.Load<Texture2D>("Content/Images/Vereség");
            Items[2] = Content.Load<Texture2D>("Content/Images/LórumJó");
            Items[3] = Content.Load<Texture2D>("Content/Images/LórumRossz");
            Items[4] = Content.Load<Texture2D>("Content/Images/Player2Buy");
            Items[5] = Content.Load<Texture2D>("Content/Images/Player3Buy");
            Items[6] = Content.Load<Texture2D>("Content/Images/Player4Buy");
            Speech[0] = Content.Load<Texture2D>("Content/Images/speech_right");
            Question_mark = Content.Load<Texture2D>("Content/Images/Question_mark");
            Speech[1] = Content.Load<Texture2D>("Content/Images/speech_left");
            Speech[2] = Content.Load<Texture2D>("Content/Images/speech_up_right");
            switch (Main.Pakli)
            {
                case 1:
                    CardsDeck1[17] = Content.Load<Texture2D>("Content/Images/makk hetes_148x240");
                    CardsDeck1[18] = Content.Load<Texture2D>("Content/Images/makk nyolcas_148x240");
                    CardsDeck1[19] = Content.Load<Texture2D>("Content/Images/makk kilences_148x240");
                    CardsDeck1[20] = Content.Load<Texture2D>("Content/Images/makk tizes_148x240");
                    CardsDeck1[21] = Content.Load<Texture2D>("Content/Images/makk alsó_148x240");
                    CardsDeck1[22] = Content.Load<Texture2D>("Content/Images/makk felső_148x240");
                    CardsDeck1[23] = Content.Load<Texture2D>("Content/Images/makk csikó_147x240");
                    CardsDeck1[24] = Content.Load<Texture2D>("Content/Images/makk ász_147x240");
                    CardsDeck1[1] = Content.Load<Texture2D>("Content/Images/piros hetes_146x240");
                    CardsDeck1[2] = Content.Load<Texture2D>("Content/Images/piros nyolcas_144x240");
                    CardsDeck1[3] = Content.Load<Texture2D>("Content/Images/piros kilences_146x240");
                    CardsDeck1[4] = Content.Load<Texture2D>("Content/Images/piros tizes_152x240");
                    CardsDeck1[5] = Content.Load<Texture2D>("Content/Images/piros alsó_145x240");
                    CardsDeck1[6] = Content.Load<Texture2D>("Content/Images/piros felső_145x240");
                    CardsDeck1[7] = Content.Load<Texture2D>("Content/Images/piros csikó_145x240");
                    CardsDeck1[8] = Content.Load<Texture2D>("Content/Images/piros ász_145x240");
                    CardsDeck1[9] = Content.Load<Texture2D>("Content/Images/zöld hetes_146x240");
                    CardsDeck1[10] = Content.Load<Texture2D>("Content/Images/zöld nyolcas_147x240");
                    CardsDeck1[11] = Content.Load<Texture2D>("Content/Images/zöld kilences_147x240");
                    CardsDeck1[12] = Content.Load<Texture2D>("Content/Images/zöld tizes_145x240");
                    CardsDeck1[13] = Content.Load<Texture2D>("Content/Images/zöld alsó_145x240");
                    CardsDeck1[14] = Content.Load<Texture2D>("Content/Images/zöld felső_145x240");
                    CardsDeck1[15] = Content.Load<Texture2D>("Content/Images/zöld csikó_145x240");
                    CardsDeck1[16] = Content.Load<Texture2D>("Content/Images/zöld ász_147x240");
                    CardsDeck1[25] = Content.Load<Texture2D>("Content/Images/tök hetes_147x240");
                    CardsDeck1[26] = Content.Load<Texture2D>("Content/Images/tök nyolcas_147x240");
                    CardsDeck1[27] = Content.Load<Texture2D>("Content/Images/tök kilences_147x240");
                    CardsDeck1[28] = Content.Load<Texture2D>("Content/Images/tök tizes_147x240");
                    CardsDeck1[29] = Content.Load<Texture2D>("Content/Images/tök alsó_147x240");
                    CardsDeck1[30] = Content.Load<Texture2D>("Content/Images/tök felső_147x240");
                    CardsDeck1[31] = Content.Load<Texture2D>("Content/Images/tök csikó_147x240");
                    CardsDeck1[32] = Content.Load<Texture2D>("Content/Images/tök ász_148x240");
                    break;
                case 2:
                    CardsDeck2[17] = Content.Load<Texture2D>("Content/Images/makk hetes_2");
                    CardsDeck2[18] = Content.Load<Texture2D>("Content/Images/makk nyolcas_2");
                    CardsDeck2[19] = Content.Load<Texture2D>("Content/Images/makk kilences_2");
                    CardsDeck2[20] = Content.Load<Texture2D>("Content/Images/makk tízes_2");
                    CardsDeck2[21] = Content.Load<Texture2D>("Content/Images/makk alsó_2");
                    CardsDeck2[22] = Content.Load<Texture2D>("Content/Images/makk felső_2");
                    CardsDeck2[23] = Content.Load<Texture2D>("Content/Images/makk csikó_2");
                    CardsDeck2[24] = Content.Load<Texture2D>("Content/Images/makk ász_2");
                    CardsDeck2[1] = Content.Load<Texture2D>("Content/Images/piros hetes_2");
                    CardsDeck2[2] = Content.Load<Texture2D>("Content/Images/piros nyolcas_2");
                    CardsDeck2[3] = Content.Load<Texture2D>("Content/Images/piros kilences_2");
                    CardsDeck2[4] = Content.Load<Texture2D>("Content/Images/piros tízes_2");
                    CardsDeck2[5] = Content.Load<Texture2D>("Content/Images/piros alsó_2");
                    CardsDeck2[6] = Content.Load<Texture2D>("Content/Images/piros felső_2");
                    CardsDeck2[7] = Content.Load<Texture2D>("Content/Images/piros csikó_2");
                    CardsDeck2[8] = Content.Load<Texture2D>("Content/Images/piros ász_2");
                    CardsDeck2[9] = Content.Load<Texture2D>("Content/Images/zöld hetes_2");
                    CardsDeck2[10] = Content.Load<Texture2D>("Content/Images/zöld nyolcas_2");
                    CardsDeck2[11] = Content.Load<Texture2D>("Content/Images/zöld kilences_2");
                    CardsDeck2[12] = Content.Load<Texture2D>("Content/Images/zöld tízes_2");
                    CardsDeck2[13] = Content.Load<Texture2D>("Content/Images/zöld alsó_2");
                    CardsDeck2[14] = Content.Load<Texture2D>("Content/Images/zöld felső_2");
                    CardsDeck2[15] = Content.Load<Texture2D>("Content/Images/zöld csikó_2");
                    CardsDeck2[16] = Content.Load<Texture2D>("Content/Images/zöld ász_2");
                    CardsDeck2[25] = Content.Load<Texture2D>("Content/Images/tök hetes_2");
                    CardsDeck2[26] = Content.Load<Texture2D>("Content/Images/tök nyolcas_2");
                    CardsDeck2[27] = Content.Load<Texture2D>("Content/Images/tök kilences_2");
                    CardsDeck2[28] = Content.Load<Texture2D>("Content/Images/tök tízes_2");
                    CardsDeck2[29] = Content.Load<Texture2D>("Content/Images/tök alsó_2");
                    CardsDeck2[30] = Content.Load<Texture2D>("Content/Images/tök felső_2");
                    CardsDeck2[31] = Content.Load<Texture2D>("Content/Images/tök csikó_2");
                    CardsDeck2[32] = Content.Load<Texture2D>("Content/Images/tök ász_2");
                    break;
                case 3:
                    CardsDeck3[1] = Content.Load<Texture2D>("Content/Deck3/1");
                    CardsDeck3[2] = Content.Load<Texture2D>("Content/Deck3/2");
                    CardsDeck3[3] = Content.Load<Texture2D>("Content/Deck3/3");
                    CardsDeck3[4] = Content.Load<Texture2D>("Content/Deck3/4");
                    CardsDeck3[5] = Content.Load<Texture2D>("Content/Deck3/5");
                    CardsDeck3[6] = Content.Load<Texture2D>("Content/Deck3/6");
                    CardsDeck3[7] = Content.Load<Texture2D>("Content/Deck3/7");
                    CardsDeck3[8] = Content.Load<Texture2D>("Content/Deck3/8");
                    CardsDeck3[9] = Content.Load<Texture2D>("Content/Deck3/9");
                    CardsDeck3[10] = Content.Load<Texture2D>("Content/Deck3/10");
                    CardsDeck3[11] = Content.Load<Texture2D>("Content/Deck3/11");
                    CardsDeck3[12] = Content.Load<Texture2D>("Content/Deck3/12");
                    CardsDeck3[13] = Content.Load<Texture2D>("Content/Deck3/13");
                    CardsDeck3[14] = Content.Load<Texture2D>("Content/Deck3/14");
                    CardsDeck3[15] = Content.Load<Texture2D>("Content/Deck3/15");
                    CardsDeck3[16] = Content.Load<Texture2D>("Content/Deck3/16");
                    CardsDeck3[17] = Content.Load<Texture2D>("Content/Deck3/17");
                    CardsDeck3[18] = Content.Load<Texture2D>("Content/Deck3/18");
                    CardsDeck3[19] = Content.Load<Texture2D>("Content/Deck3/19");
                    CardsDeck3[20] = Content.Load<Texture2D>("Content/Deck3/20");
                    CardsDeck3[21] = Content.Load<Texture2D>("Content/Deck3/21");
                    CardsDeck3[22] = Content.Load<Texture2D>("Content/Deck3/22");
                    CardsDeck3[23] = Content.Load<Texture2D>("Content/Deck3/23");
                    CardsDeck3[24] = Content.Load<Texture2D>("Content/Deck3/24");
                    CardsDeck3[25] = Content.Load<Texture2D>("Content/Deck3/25");
                    CardsDeck3[26] = Content.Load<Texture2D>("Content/Deck3/26");
                    CardsDeck3[27] = Content.Load<Texture2D>("Content/Deck3/27");
                    CardsDeck3[28] = Content.Load<Texture2D>("Content/Deck3/28");
                    CardsDeck3[29] = Content.Load<Texture2D>("Content/Deck3/29");
                    CardsDeck3[30] = Content.Load<Texture2D>("Content/Deck3/30");
                    CardsDeck3[31] = Content.Load<Texture2D>("Content/Deck3/31");
                    CardsDeck3[32] = Content.Load<Texture2D>("Content/Deck3/32");
                    break;
            }

            switch (videonumber)
            {
                case 1:
                    _video[1] = Content.Load<Video>("Content/Videos/Menu");
                    break;
                case 2:
                    _video[2] = Content.Load<Video>("Content/Videos/Background2");
                    break;
                case 3:
                    _video[3] = Content.Load<Video>("Content/Videos/Background3");
                    break;
                case 4:
                    _video[4] = Content.Load<Video>("Content/Videos/Background4");
                    break;
                case 5:
                    _video[5] = Content.Load<Video>("Content/Videos/Background5");
                    break;
                case 6:
                    _video[6] = Content.Load<Video>("Content/Videos/Background6");
                    break;
                case 7:
                    _video[7] = Content.Load<Video>("Content/Videos/Background7");
                    break;
                case 8:
                    _video[8] = Content.Load<Video>("Content/Videos/Background9");
                    break;
                case 9:
                    _video[9] = Content.Load<Video>("Content/Videos/Background10");
                    break;
            }

            _player = new VideoPlayer();
            Thread.Sleep(1000);
            ScreenManager.Game.ResetElapsedTime();
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
            bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            // Gradually fade in or out depending on whether we are covered by the pause screen.
            _pauseAlpha = coveredByOtherScreen
                ? Math.Min(_pauseAlpha + 1f / 32, 1)
                : Math.Max(_pauseAlpha - 1f / 32, 0);

            if (!IsActive) return;
            Main.TextMover();
            base.Update(gameTime, otherScreenHasFocus, false);
            if (Main.JátékFolyamatban)
            {
                RollerTime += gameTime.ElapsedGameTime.TotalSeconds;
                Roller.PlayerRoller();
            }

            if (_player.State == MediaState.Stopped)
            {
                _player.IsLooped = true;
                _player.Play(_video[videonumber]);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState();
            if (_player.State != MediaState.Stopped)
                _videoTexture = _player.GetTexture();
            spriteBatch.Begin();
            if (_videoTexture != null)
                spriteBatch.Draw(_videoTexture, fullscreen,
                    new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));

            spriteBatch.Draw(CardPlace,
                new Rectangle((int)(161 * LórumGame.scale), (int)(176 * LórumGame.scale2),
                    (int)(125 * LórumGame.scale2), (int)(175 * LórumGame.scale2)), Color.White);
            spriteBatch.Draw(CardPlace,
                new Rectangle((int)(281 * LórumGame.scale), (int)(176 * LórumGame.scale2),
                    (int)(125 * LórumGame.scale2), (int)(175 * LórumGame.scale2)), Color.White);
            spriteBatch.Draw(CardPlace,
                new Rectangle((int)(401 * LórumGame.scale), (int)(176 * LórumGame.scale2),
                    (int)(125 * LórumGame.scale2), (int)(175 * LórumGame.scale2)), Color.White);
            spriteBatch.Draw(CardPlace,
                new Rectangle((int)(521 * LórumGame.scale), (int)(176 * LórumGame.scale2),
                    (int)(125 * LórumGame.scale2), (int)(175 * LórumGame.scale2)), Color.White);
            spriteBatch.Draw(Question_mark,
                new Rectangle((int)(730 * LórumGame.scale), (int)(530 * LórumGame.scale2),
                    (int)(40 * LórumGame.scale2), (int)(40 * LórumGame.scale2)), Color.White);

            foreach (var t in Main.card)
                t.Draw();

            for (var i = 1; i <= 8; i++)
                if (Main.Player2CardId[i] != 0)
                    spriteBatch.Draw(VisitEnemies ? KártyaSzám(Main.Player2CardId[i]) : Lefordított,
                        new Rectangle((int)(725 * LórumGame.scale),
                            (int)(100 * LórumGame.scale2 + i * 20 * LórumGame.scale2),
                            (int)(60 * LórumGame.scale2), (int)(100 * LórumGame.scale2)),
                        Color.White);
            //spriteBatch.Draw(Lefordított,new Rectangle(725, 100 + (i*20), 60, 100), Color.White);
            for (var i = 1; i <= 8; i++)
                if (Main.Player4CardId[i] != 0)
                    spriteBatch.Draw(VisitEnemies ? KártyaSzám(Main.Player4CardId[i]) : Lefordított,
                        new Rectangle((int)(25 * LórumGame.scale),
                            (int)(100 * LórumGame.scale2 + i * 20 * LórumGame.scale2),
                            (int)(60 * LórumGame.scale2), (int)(100 * LórumGame.scale2)),
                        Color.White);
            //    spriteBatch.Draw(Lefordított, new Rectangle(25, 100 + (i*20), 60, 100), Color.White);
            for (var i = 1; i <= 8; i++)
                if (Main.Player3CardId[i] != 0)
                    spriteBatch.Draw(VisitEnemies ? KártyaSzám(Main.Player3CardId[i]) : Lefordított,
                        new Rectangle((int)(275 * LórumGame.scale + i * 20 * LórumGame.scale2),
                            (int)(10 * LórumGame.scale2), (int)(60 * LórumGame.scale2),
                            (int)(100 * LórumGame.scale2)), Color.White);
            if (rectangle1.Intersects(player2) && VisibleArrow)
            {
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(725 * LórumGame.scale, 120 * LórumGame.scale2),
                    new Vector2(785 * LórumGame.scale2, 120 * LórumGame.scale2));
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(725 * LórumGame.scale, 120 * LórumGame.scale),
                    new Vector2(725 * LórumGame.scale, 360 * LórumGame.scale));
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(785 * LórumGame.scale, 120 * LórumGame.scale),
                    new Vector2(785 * LórumGame.scale, 360 * LórumGame.scale));
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(725 * LórumGame.scale, 360 * LórumGame.scale2),
                    new Vector2(785 * LórumGame.scale2, 360 * LórumGame.scale2));
            }

            if (rectangle1.Intersects(player3) && VisibleArrow)
            {
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(295 * LórumGame.scale, 10 * LórumGame.scale2),
                    new Vector2(495 * LórumGame.scale2, 10 * LórumGame.scale2));
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(295 * LórumGame.scale, 10 * LórumGame.scale),
                    new Vector2(295 * LórumGame.scale, 110 * LórumGame.scale));
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(295 * LórumGame.scale, 110 * LórumGame.scale),
                    new Vector2(495 * LórumGame.scale, 110 * LórumGame.scale));
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(495 * LórumGame.scale, 10 * LórumGame.scale2),
                    new Vector2(495 * LórumGame.scale2, 110 * LórumGame.scale2));
            }

            if (rectangle1.Intersects(player4) && VisibleArrow)
            {
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(25 * LórumGame.scale, 120 * LórumGame.scale2),
                    new Vector2(85 * LórumGame.scale2, 120 * LórumGame.scale2));
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(25 * LórumGame.scale, 120 * LórumGame.scale),
                    new Vector2(25 * LórumGame.scale, 360 * LórumGame.scale));
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(85 * LórumGame.scale, 120 * LórumGame.scale),
                    new Vector2(85 * LórumGame.scale, 360 * LórumGame.scale));
                DrawLine(spriteBatch, blank, 2, Color.White, new Vector2(25 * LórumGame.scale, 360 * LórumGame.scale2),
                    new Vector2(85 * LórumGame.scale2, 360 * LórumGame.scale2));
            }

            spriteBatch.DrawString(_font, "Pontszám: " + Main.Pontok1,
                new Vector2(180 * LórumGame.scale2, 410 * LórumGame.scale2), Color.White);
            spriteBatch.DrawString(_font, "Játékos1", new Vector2(180 * LórumGame.scale2, 395 * LórumGame.scale2),
                Color.White);
            if (showwish)
                spriteBatch.DrawString(_font, "Szerintem tedd ki a " + Main.KátyaNév(Main.helpcard) + "-t.",
                    new Vector2(350 * LórumGame.scale2, 395 * LórumGame.scale2), Color.Red);
            spriteBatch.DrawString(_font, "Pontszám: " + Main.Pontok2,
                new Vector2(640 * LórumGame.scale, 90 * LórumGame.scale2), Color.White);
            spriteBatch.DrawString(_font, "Játékos2", new Vector2(640 * LórumGame.scale, 75 * LórumGame.scale2),
                Color.White);
            spriteBatch.DrawString(_font, "Pontszám: " + Main.Pontok3,
                new Vector2(510 * LórumGame.scale, 10 * LórumGame.scale2), Color.White);
            spriteBatch.DrawString(_font, "Játékos3", new Vector2(510 * LórumGame.scale, 25 * LórumGame.scale2),
                Color.White);
            spriteBatch.DrawString(_font, "Pontszám: " + Main.Pontok4,
                new Vector2(10 * LórumGame.scale, 360 * LórumGame.scale2), Color.White);
            spriteBatch.DrawString(_font, "Játékos4", new Vector2(10 * LórumGame.scale2, 375 * LórumGame.scale2),
                Color.White);
            //  spriteBatch.Draw(Lefordított, new Rectangle(275 + (i*20), 10, 60, 100), Color.White);
            //nénány teszt kiírás
            /* spriteBatch.DrawString(_font,
                                       Main.kitettPiros + " " + Main.kitettZöld + " " + Main.kitettMakk + " " +
                                       Main.kitettTök, new Vector2(100, 100), Color.White);*/
            spriteBatch.Draw(Buttons[2],
                new Rectangle((int)(13 * LórumGame.scale), (int)(520 * LórumGame.scale2),
                    (int)(110 * LórumGame.scale2), (int)(55 * LórumGame.scale2)), Color.White);
            spriteBatch.Draw(Buttons[1],
                new Rectangle((int)(13 * LórumGame.scale), (int)(470 * LórumGame.scale2),
                    (int)(110 * LórumGame.scale2), (int)(55 * LórumGame.scale2)), Color.White);
            if (GameEndead)
                spriteBatch.Draw(Items[GameEndedTitle],
                    new Rectangle((int)(TextX * LórumGame.scale), (int)(TextY * LórumGame.scale2),
                        (int)(350 * LórumGame.scale2), (int)(210 * LórumGame.scale2)), Color.White);
            // Győzelem/Vereség kiírás
            if (VisibleArrow)
            {
                var rollerTime2 = (int)RollerTime;
                switch (rollerTime2)
                {
                    case 1:
                    case 2:
                    case 3:
                        spriteBatch.Draw(Arrow[0],
                            new Rectangle((int)(650 * LórumGame.scale), (int)(210 * LórumGame.scale2),
                                (int)(60 * LórumGame.scale2), (int)(60 * LórumGame.scale2)), null,
                            Color.White, 0,
                            new Vector2(0, 0), SpriteEffects.None, 0.0f);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        spriteBatch.Draw(Arrow[0],
                            new Rectangle((int)(380 * LórumGame.scale), (int)(170 * LórumGame.scale2),
                                (int)(60 * LórumGame.scale2), (int)(60 * LórumGame.scale2)), null,
                            Color.White, 11,
                            new Vector2(0, 0), SpriteEffects.None, 0.0f);
                        break;
                    case 7:
                    case 8:
                    case 9:
                        spriteBatch.Draw(Arrow[0],
                            new Rectangle((int)(170 * LórumGame.scale), (int)(245 * LórumGame.scale2),
                                (int)(60 * LórumGame.scale2), (int)(60 * LórumGame.scale2)), null,
                            Color.White, 22,
                            new Vector2(0, 0), SpriteEffects.None, 0.0f);
                        break;
                    case 10:
                    case 11:
                    case 0:
                        spriteBatch.Draw(Arrow[0],
                            new Rectangle((int)(440 * LórumGame.scale), (int)(345 * LórumGame.scale2),
                                (int)(60 * LórumGame.scale2), (int)(60 * LórumGame.scale2)), null,
                            Color.White, 33,
                            new Vector2(0, 0), SpriteEffects.None, 0.0f);
                        break;
                }
            }

            /*    if (Equals(debug, true))
                    {
                        var fps = (int)Math.Round(1.0 / gameTime.ElapsedGameTime.TotalSeconds);
                        spriteBatch.DrawString(_font, "FPS:" + fps, new Vector2(1, 1), Color.White);
                        spriteBatch.DrawString(_font, "MouseX:" + Mouse.GetState().X + " Y:" + Mouse.GetState().Y,
                                               new Vector2(1, 15), Color.White);
                        spriteBatch.DrawString(_font, "Piros:" + Main.Piros, new Vector2(1, 35), Color.White);
                        spriteBatch.DrawString(_font, "Zold:" + Main.Zöld, new Vector2(1, 50), Color.White);
                        spriteBatch.DrawString(_font, "Makk:" + Main.Makk, new Vector2(1, 65), Color.White);
                        spriteBatch.DrawString(_font, "Tok:" + Main.Tök, new Vector2(1, 80), Color.White);
                    }*/
            if (player2showspeech)
            {
                spriteBatch.Draw(Speech[0],
                    new Rectangle((int)(640 * LórumGame.scale), (int)(150 * LórumGame.scale2),
                        (int)(150 * LórumGame.scale2), (int)(125 * LórumGame.scale2)), Color.White);
                switch (player2showspeech_mode)
                {
                    case 1:
                        spriteBatch.DrawString(_font2, "Eladom a kezdést",
                            new Vector2(644 * LórumGame.scale, 154 * LórumGame.scale2), Color.White);
                        spriteBatch.DrawString(_font2, Main.Dealer1 + " pontért.",
                            new Vector2(644 * LórumGame.scale, 174 * LórumGame.scale2), Color.White);
                        spriteBatch.DrawString(_font2, "-Elfogadom",
                            new Vector2(654 * LórumGame.scale, 204 * LórumGame.scale2), Color.LightGreen);
                        break;
                }
            }

            if (player3showspeech)
            {
                spriteBatch.Draw(Speech[2],
                    new Rectangle((int)(480 * LórumGame.scale), (int)(25 * LórumGame.scale2),
                        (int)(150 * LórumGame.scale2), (int)(125 * LórumGame.scale2)), Color.White);
                switch (player3showspeech_mode)
                {
                    case 1:
                        spriteBatch.DrawString(_font2, "Eladom a kezdést",
                            new Vector2(484 * LórumGame.scale, 55 * LórumGame.scale2), Color.White);
                        spriteBatch.DrawString(_font2, Main.Dealer2 + " pontért.",
                            new Vector2(484 * LórumGame.scale, 75 * LórumGame.scale2), Color.White);
                        spriteBatch.DrawString(_font2, "-Elfogadom",
                            new Vector2(504 * LórumGame.scale, 105 * LórumGame.scale2), Color.LightGreen);
                        break;
                }
            }

            if (player4showspeech)
            {
                spriteBatch.Draw(Speech[1],
                    new Rectangle((int)(60 * LórumGame.scale), (int)(150 * LórumGame.scale2),
                        (int)(150 * LórumGame.scale2), (int)(125 * LórumGame.scale2)), Color.White);
                switch (player4showspeech_mode)
                {
                    case 1:
                        spriteBatch.DrawString(_font2, "Eladom a kezdést",
                            new Vector2(60 * LórumGame.scale, 154 * LórumGame.scale2), Color.White);
                        spriteBatch.DrawString(_font2, Main.Dealer3 + " pontért.",
                            new Vector2(60 * LórumGame.scale, 174 * LórumGame.scale2), Color.White);
                        spriteBatch.DrawString(_font2, "-Elfogadom",
                            new Vector2(70 * LórumGame.scale, 204 * LórumGame.scale2), Color.LightGreen);
                        break;
                }
            }

            if (player2showtick)
                spriteBatch.Draw(Tick,
                    new Rectangle((int)(745 * LórumGame.scale), (int)(204 * LórumGame.scale2),
                        (int)(15 * LórumGame.scale2), (int)(15 * LórumGame.scale2)), Color.White);
            if (player3showtick)
                spriteBatch.Draw(Tick,
                    new Rectangle((int)(594 * LórumGame.scale), (int)(105 * LórumGame.scale2),
                        (int)(15 * LórumGame.scale2), (int)(15 * LórumGame.scale2)), Color.White);
            if (player4showtick)
                spriteBatch.Draw(Tick,
                    new Rectangle((int)(161 * LórumGame.scale), (int)(204 * LórumGame.scale2),
                        (int)(15 * LórumGame.scale2), (int)(15 * LórumGame.scale2)), Color.White);
            if (showhintprice)
                spriteBatch.DrawString(_font, "Segítség ára:" + Main.SegítségÁr,
                    new Vector2(680 * LórumGame.scale, 500 * LórumGame.scale2), Color.White);

            spriteBatch.End();
            mouseStatePrevious = mouseStateCurrent;
            if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                var alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        public static Texture2D KártyaSzám(int number)
        {
            //Képek lekérése
            switch (Main.Pakli)
            {
                case 1:
                    switch (number)
                    {
                        case 1:
                            return CardsDeck1[1];
                        case 2:
                            return CardsDeck1[2];
                        case 3:
                            return CardsDeck1[3];
                        case 4:
                            return CardsDeck1[4];
                        case 5:
                            return CardsDeck1[5];
                        case 6:
                            return CardsDeck1[6];
                        case 7:
                            return CardsDeck1[7];
                        case 8:
                            return CardsDeck1[8];
                        case 9:
                            return CardsDeck1[9];
                        case 10:
                            return CardsDeck1[10];
                        case 11:
                            return CardsDeck1[11];
                        case 12:
                            return CardsDeck1[12];
                        case 13:
                            return CardsDeck1[13];
                        case 14:
                            return CardsDeck1[14];
                        case 15:
                            return CardsDeck1[15];
                        case 16:
                            return CardsDeck1[16];
                        case 17:
                            return CardsDeck1[17];
                        case 18:
                            return CardsDeck1[18];
                        case 19:
                            return CardsDeck1[19];
                        case 20:
                            return CardsDeck1[20];
                        case 21:
                            return CardsDeck1[21];
                        case 22:
                            return CardsDeck1[22];
                        case 23:
                            return CardsDeck1[23];
                        case 24:
                            return CardsDeck1[24];
                        case 25:
                            return CardsDeck1[25];
                        case 26:
                            return CardsDeck1[26];
                        case 27:
                            return CardsDeck1[27];
                        case 28:
                            return CardsDeck1[28];
                        case 29:
                            return CardsDeck1[29];
                        case 30:
                            return CardsDeck1[30];
                        case 31:
                            return CardsDeck1[31];
                        case 32:
                            return CardsDeck1[32];
                        case 0:
                            break;
                    }

                    break;
                case 2:
                    switch (number)
                    {
                        case 1:
                            return CardsDeck2[1];
                        case 2:
                            return CardsDeck2[2];
                        case 3:
                            return CardsDeck2[3];
                        case 4:
                            return CardsDeck2[4];
                        case 5:
                            return CardsDeck2[5];
                        case 6:
                            return CardsDeck2[6];
                        case 7:
                            return CardsDeck2[7];
                        case 8:
                            return CardsDeck2[8];
                        case 9:
                            return CardsDeck2[9];
                        case 10:
                            return CardsDeck2[10];
                        case 11:
                            return CardsDeck2[11];
                        case 12:
                            return CardsDeck2[12];
                        case 13:
                            return CardsDeck2[13];
                        case 14:
                            return CardsDeck2[14];
                        case 15:
                            return CardsDeck2[15];
                        case 16:
                            return CardsDeck2[16];
                        case 17:
                            return CardsDeck2[17];
                        case 18:
                            return CardsDeck2[18];
                        case 19:
                            return CardsDeck2[19];
                        case 20:
                            return CardsDeck2[20];
                        case 21:
                            return CardsDeck2[21];
                        case 22:
                            return CardsDeck2[22];
                        case 23:
                            return CardsDeck2[23];
                        case 24:
                            return CardsDeck2[24];
                        case 25:
                            return CardsDeck2[25];
                        case 26:
                            return CardsDeck2[26];
                        case 27:
                            return CardsDeck2[27];
                        case 28:
                            return CardsDeck2[28];
                        case 29:
                            return CardsDeck2[29];
                        case 30:
                            return CardsDeck2[30];
                        case 31:
                            return CardsDeck2[31];
                        case 32:
                            return CardsDeck2[32];
                        case 0:
                            break;
                    }

                    break;
                case 3:
                    switch (number)
                    {
                        case 1:
                            return CardsDeck3[1];
                        case 2:
                            return CardsDeck3[2];
                        case 3:
                            return CardsDeck3[3];
                        case 4:
                            return CardsDeck3[4];
                        case 5:
                            return CardsDeck3[5];
                        case 6:
                            return CardsDeck3[6];
                        case 7:
                            return CardsDeck3[7];
                        case 8:
                            return CardsDeck3[8];
                        case 9:
                            return CardsDeck3[9];
                        case 10:
                            return CardsDeck3[10];
                        case 11:
                            return CardsDeck3[11];
                        case 12:
                            return CardsDeck3[12];
                        case 13:
                            return CardsDeck3[13];
                        case 14:
                            return CardsDeck3[14];
                        case 15:
                            return CardsDeck3[15];
                        case 16:
                            return CardsDeck3[16];
                        case 17:
                            return CardsDeck3[17];
                        case 18:
                            return CardsDeck3[18];
                        case 19:
                            return CardsDeck3[19];
                        case 20:
                            return CardsDeck3[20];
                        case 21:
                            return CardsDeck3[21];
                        case 22:
                            return CardsDeck3[22];
                        case 23:
                            return CardsDeck3[23];
                        case 24:
                            return CardsDeck3[24];
                        case 25:
                            return CardsDeck3[25];
                        case 26:
                            return CardsDeck3[26];
                        case 27:
                            return CardsDeck3[27];
                        case 28:
                            return CardsDeck3[28];
                        case 29:
                            return CardsDeck3[29];
                        case 30:
                            return CardsDeck3[30];
                        case 31:
                            return CardsDeck3[31];
                        case 32:
                            return CardsDeck3[32];
                        case 0:
                            break;
                    }

                    break;
            }

            //Vége
            return null;
        }

        public override void HandleInput(InputState input)
        {
            //egér használata
            mouseStateCurrent = Mouse.GetState();
            rectangle1 = new Rectangle(mouseStateCurrent.X, mouseStateCurrent.Y, 1, 1);
            rectangle2 = new Rectangle((int)(13 * LórumGame.scale), (int)(470 * LórumGame.scale2),
                (int)(110 * LórumGame.scale2), (int)(55 * LórumGame.scale2)); //új játék


            if (rectangle1.Intersects(rectangle2) && mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                SoundEffect[2].Play();
                Main.Pass();
            }

            rectangle2 = new Rectangle((int)(13 * LórumGame.scale), (int)(520 * LórumGame.scale2),
                (int)(110 * LórumGame.scale2), (int)(55 * LórumGame.scale2));
            if (rectangle1.Intersects(rectangle2) && mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                if (!Main.Új_játék_engedve) return;
                SoundEffect[2].Play();
                if (OptionsMenuScreen.hardmode && !Main.Játék_befejezve && !firtRun)
                    ÚjraosztásKérdés();
                else
                    Main.Jatekinditas();
            } //kártyák

            rectangle2 = Main.Player1CardId[2] > 0
                ? new Rectangle((int)(156 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(60 * LórumGame.scale2), (int)(155 * LórumGame.scale2))
                : new Rectangle((int)(156 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(100 * LórumGame.scale2), (int)(155 * LórumGame.scale2));
            if (rectangle1.Intersects(rectangle2) && mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                mouseStatePrevious.LeftButton == ButtonState.Released &&
                EnabledCard)
            {
                main.JátékosKártya1();
                SoundEffect[2].Play();
            }

            rectangle2 = Main.Player1CardId[3] > 0
                ? new Rectangle((int)(216 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(60 * LórumGame.scale2), (int)(155 * LórumGame.scale2))
                : new Rectangle((int)(216 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(100 * LórumGame.scale2), (int)(155 * LórumGame.scale2));
            if (rectangle1.Intersects(rectangle2) && mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                mouseStatePrevious.LeftButton == ButtonState.Released &&
                EnabledCard)
            {
                main.JátékosKártya2();
                SoundEffect[2].Play();
            }

            rectangle2 = Main.Player1CardId[4] > 0
                ? new Rectangle((int)(276 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(60 * LórumGame.scale2), (int)(155 * LórumGame.scale2))
                : new Rectangle((int)(276 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(100 * LórumGame.scale2), (int)(155 * LórumGame.scale2));
            if (rectangle1.Intersects(rectangle2) && mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                mouseStatePrevious.LeftButton == ButtonState.Released &&
                EnabledCard)
            {
                main.JátékosKártya3();
                SoundEffect[2].Play();
            }

            rectangle2 = Main.Player1CardId[5] > 0
                ? new Rectangle((int)(336 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(60 * LórumGame.scale2), (int)(155 * LórumGame.scale2))
                : new Rectangle((int)(336 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(100 * LórumGame.scale2), (int)(155 * LórumGame.scale2));
            if (rectangle1.Intersects(rectangle2) && mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                mouseStatePrevious.LeftButton == ButtonState.Released)
                if (EnabledCard)
                {
                    main.JátékosKártya4();
                    SoundEffect[2].Play();
                }

            rectangle2 = Main.Player1CardId[6] > 0
                ? new Rectangle((int)(396 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(60 * LórumGame.scale2), (int)(155 * LórumGame.scale2))
                : new Rectangle((int)(396 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(100 * LórumGame.scale2), (int)(155 * LórumGame.scale2));
            if (rectangle1.Intersects(rectangle2))
                if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                    mouseStatePrevious.LeftButton == ButtonState.Released && EnabledCard)
                {
                    main.JátékosKártya5();
                    SoundEffect[2].Play();
                }

            rectangle2 = Main.Player1CardId[7] > 0
                ? new Rectangle((int)(456 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(60 * LórumGame.scale2), (int)(155 * LórumGame.scale))
                : new Rectangle((int)(456 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(100 * LórumGame.scale2), (int)(155 * LórumGame.scale2));
            if (rectangle1.Intersects(rectangle2) && mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                mouseStatePrevious.LeftButton == ButtonState.Released)
                if (EnabledCard)
                {
                    main.JátékosKártya6();
                    SoundEffect[2].Play();
                }

            rectangle2 = Main.Player1CardId[8] > 0
                ? new Rectangle((int)(516 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(60 * LórumGame.scale2), (int)(155 * LórumGame.scale2))
                : new Rectangle((int)(516 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                    (int)(100 * LórumGame.scale2), (int)(155 * LórumGame.scale2));
            if (rectangle1.Intersects(rectangle2))
                if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                    mouseStatePrevious.LeftButton == ButtonState.Released && EnabledCard)
                {
                    main.JátékosKártya7();
                    SoundEffect[2].Play();
                }

            rectangle2 = new Rectangle((int)(576 * LórumGame.scale), (int)(440 * LórumGame.scale2),
                (int)(100 * LórumGame.scale2), (int)(155 * LórumGame.scale2));
            if (rectangle1.Intersects(rectangle2) && mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                mouseStatePrevious.LeftButton == ButtonState.Released &&
                EnabledCard)
            {
                main.JátékosKártya8();
                SoundEffect[2].Play();
            }

            //Üzletelés érzékelése
            if (Main.Adás_Engedve)
            {
                rectangle2 = new Rectangle((int)(654 * LórumGame.scale), (int)(204 * LórumGame.scale2),
                    (int)(126 * LórumGame.scale2), (int)(14 * LórumGame.scale2));
                if (rectangle1.Intersects(rectangle2))
                {
                    player2showtick = true;
                    if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                        mouseStatePrevious.LeftButton == ButtonState.Released)
                    {
                        player2showtick = false;
                        player2showspeech = false;
                        player3showspeech = false;
                        player4showspeech = false;
                        player2showspeech_mode = 0;
                        player3showspeech_mode = 0;
                        player4showspeech_mode = 0;
                        Computer1.Executed = false;
                        Computer2.Executed = false;
                        Computer3.Executed = false;
                        if (Main.Kezdőjátékos == 1)
                        {
                            RollerTime = 1;
                            Main.JátékFolyamatban = true;
                            EnabledCard = false;
                            Main.Adás_Engedve = false;
                            Main.Új_játék_engedve = false;
                            Main.Passz_Engedve = false;
                            Main.Pontok1 += Main.Dealer1;
                            Main.Pontok2 -= Main.Dealer1;
                            Main.PointsWin += Main.Dealer1;
                            Main.Statfrissítés();
                        }
                        else
                        {
                            Main.Pontok1 -= Main.Dealer1;
                            Main.Pontok2 += Main.Dealer1;
                            RollerTime = 10;
                            Main.JátékFolyamatban = true;
                            EnabledCard = true;
                            Main.Adás_Engedve = false;
                            Main.Új_játék_engedve = true;
                            Main.Passz_Engedve = false;
                            Main.PointsLose += Main.Dealer1;
                            Main.Statfrissítés();
                        }

                        SoundEffect[7].Play();
                    }
                }
                else
                {
                    player2showtick = false;
                }

                rectangle2 = new Rectangle((int)(504 * LórumGame.scale), (int)(105 * LórumGame.scale2),
                    (int)(126 * LórumGame.scale2), (int)(14 * LórumGame.scale2));
                if (rectangle1.Intersects(rectangle2))
                {
                    player3showtick = true;
                    if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                        mouseStatePrevious.LeftButton == ButtonState.Released)
                    {
                        player3showtick = false;
                        player2showspeech = false;
                        player3showspeech = false;
                        player4showspeech = false;
                        player2showspeech_mode = 0;
                        player3showspeech_mode = 0;
                        player4showspeech_mode = 0;
                        Computer1.Executed = false;
                        Computer2.Executed = false;
                        Computer3.Executed = false;
                        if (Main.Kezdőjátékos == 1)
                        {
                            RollerTime = 4;
                            Main.JátékFolyamatban = true;
                            EnabledCard = false;
                            Main.Adás_Engedve = false;
                            Main.Új_játék_engedve = false;
                            Main.Passz_Engedve = false;
                            Main.Pontok1 += Main.Dealer2;
                            Main.Pontok3 -= Main.Dealer2;
                            Main.PointsWin += Main.Dealer2;
                            Main.Statfrissítés();
                        }
                        else
                        {
                            Main.Pontok1 -= Main.Dealer2;
                            Main.Pontok3 += Main.Dealer2;
                            RollerTime = 10;
                            Main.JátékFolyamatban = true;
                            EnabledCard = true;
                            Main.Adás_Engedve = false;
                            Main.Új_játék_engedve = true;
                            Main.Passz_Engedve = false;
                            Main.PointsLose += Main.Dealer2;
                            Main.Statfrissítés();
                        }

                        SoundEffect[7].Play();
                    }
                }
                else
                {
                    player3showtick = false;
                }

                rectangle2 = new Rectangle((int)(70 * LórumGame.scale), (int)(204 * LórumGame.scale2),
                    (int)(126 * LórumGame.scale2), (int)(14 * LórumGame.scale2));
                if (rectangle1.Intersects(rectangle2))
                {
                    player4showtick = true;
                    if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                        mouseStatePrevious.LeftButton == ButtonState.Released)
                    {
                        player4showtick = false;
                        player2showspeech = false;
                        player3showspeech = false;
                        player4showspeech = false;
                        player2showspeech_mode = 0;
                        player3showspeech_mode = 0;
                        player4showspeech_mode = 0;
                        Computer1.Executed = false;
                        Computer2.Executed = false;
                        Computer3.Executed = false;
                        if (Main.Kezdőjátékos == 1)
                        {
                            RollerTime = 7;
                            Main.JátékFolyamatban = true;
                            EnabledCard = false;
                            Main.Adás_Engedve = false;
                            Main.Új_játék_engedve = false;
                            Main.Passz_Engedve = false;
                            Main.Pontok1 += Main.Dealer3;
                            Main.Pontok4 -= Main.Dealer3;
                            Main.PointsWin += Main.Dealer3;
                            Main.Statfrissítés();
                        }
                        else
                        {
                            Main.Pontok1 -= Main.Dealer3;
                            Main.Pontok4 += Main.Dealer3;
                            RollerTime = 10;
                            Main.JátékFolyamatban = true;
                            EnabledCard = true;
                            Main.Adás_Engedve = false;
                            Main.Új_játék_engedve = true;
                            Main.Passz_Engedve = false;
                        }

                        SoundEffect[7].Play();
                    }
                }
                else
                {
                    player4showtick = false;
                }
            }

            if (rectangle1.Intersects(player2))
                if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                    mouseStatePrevious.LeftButton == ButtonState.Released)
                    switch (player2showspeech_mode)
                    {
                        case 1:
                            player2showspeech = !player2showspeech;
                            break;
                    }

            if (rectangle1.Intersects(player3))
                if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                    mouseStatePrevious.LeftButton == ButtonState.Released)
                    switch (player3showspeech_mode)
                    {
                        case 1:
                            player3showspeech = !player3showspeech;
                            break;
                    }

            if (rectangle1.Intersects(player4))
                if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                    mouseStatePrevious.LeftButton == ButtonState.Released)
                    switch (player4showspeech_mode)
                    {
                        case 1:
                            player4showspeech = !player4showspeech;
                            break;
                    }

            rectangle2 = new Rectangle((int)(730 * LórumGame.scale), (int)(530 * LórumGame.scale2),
                (int)(40 * LórumGame.scale2), (int)(40 * LórumGame.scale2));
            if (Main.SegítségÁr > 0 && !Main.Játék_befejezve)
                if (rectangle1.Intersects(rectangle2))
                {
                    showhintprice = true;
                    if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                        mouseStatePrevious.LeftButton == ButtonState.Released)
                    {
                        int n;
                        const int i = 8;
                        var jelöltMakk = 0;
                        var jelöltPiros = 0;
                        var jelöltTök = 0;
                        var jelöltZöld = 0;
                        var pirosak = 0;
                        var zöldek = 0;
                        var makkok = 0;
                        var tökök = 0;
                        var lehetSegesKartyak = 0;
                        int calculatork;
                        for (n = 1; n <= i; n++)
                            if (Main.KöverkezőLap(Main.Player1CardId[n]))
                                lehetSegesKartyak++;
                        if (Main.Pontok1 - Main.SegítségÁr < 0)
                        {
                            SoundEffect[1].Play();
                            return;
                        }

                        if (lehetSegesKartyak == 0) return;
                        SoundEffect[2].Play();
                        showhintprice = false;
                        Main.Pontok1 -= Main.SegítségÁr;
                        for (n = 1; n <= i; n++)
                            switch (Main.Player1CardId[n])
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                    pirosak++;
                                    break;
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                    zöldek++;
                                    break;
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                case 21:
                                case 22:
                                case 23:
                                case 24:
                                    makkok++;
                                    break;
                                case 25:
                                case 26:
                                case 27:
                                case 28:
                                case 29:
                                case 30:
                                case 31:
                                case 32:
                                    tökök++;
                                    break;
                            }

                        for (n = 1; n <= i; n++)
                        {
                            if (!Main.KöverkezőLap(Main.Player1CardId[n])) continue;
                            switch (Main.Player1CardId[n])
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                    jelöltPiros = Main.Player1CardId[n];
                                    break;
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                    jelöltZöld = Main.Player1CardId[n];
                                    break;
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                case 21:
                                case 22:
                                case 23:
                                case 24:
                                    jelöltMakk = Main.Player1CardId[n];
                                    break;
                                case 25:
                                case 26:
                                case 27:
                                case 28:
                                case 29:
                                case 30:
                                case 31:
                                case 32:
                                    jelöltTök = Main.Player1CardId[n];
                                    break;
                            }
                        }

                        if (Main.Piros == 0 && Main.Zöld == 0 && Main.Makk == 0 && Main.Tök == 0)
                            calculatork = ComputerAI.Segítség();
                        else
                            calculatork = ComputerAI.Játék(pirosak, zöldek, makkok, tökök, jelöltPiros, jelöltZöld,
                                jelöltMakk,
                                jelöltTök, Main.Player1CardId[1], Main.Player1CardId[2], Main.Player1CardId[3],
                                Main.Player1CardId[4], Main.Player1CardId[5], Main.Player1CardId[6],
                                Main.Player1CardId[7], Main.Player1CardId[8], 1);
                        Main.helpcard = calculatork;
                        Main.PointsLose += Main.SegítségÁr;
                        Main.Statfrissítés();
                        Main.SegítségÁr = 0;
                        showwish = true;
                    }
                }
                else
                {
                    showhintprice = false;
                }

            mouseStatePrevious = mouseStateCurrent;
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            var playerIndex = ControllingPlayer.Value;

            var gamePadState = input.CurrentGamePadStates[(int)playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            var gamePadDisconnected = !gamePadState.IsConnected &&
                                      input.GamePadWasConnected[(int)playerIndex];
            controller = input.GamePadWasConnected[(int)ControllingPlayer];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
        }

        private void ÚjraosztásKérdés()
        {
            penalty = Main.Dealer1 + Main.Dealer2 + Main.Dealer3;
            if (penalty <= 0)
            {
                penalty = 0;
                const string message = "Szeretnél kérni újraosztást?Ez ingyenes.";
                var confirmQuitMessageBox = new MessageBoxScreen(message);
                confirmQuitMessageBox.Accepted += ÚjraosztásElfogadva;
                ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
            }
            else
            {
                var message = "Szeretnél kérni újraosztást?Ez " + penalty + " pontba kerül.";
                var confirmQuitMessageBox = new MessageBoxScreen(message);
                confirmQuitMessageBox.Accepted += ÚjraosztásElfogadva;
                ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
            }
        }

        private void ÚjraosztásElfogadva(object sender, PlayerIndexEventArgs e)
        {
            Main.Pontok1 -= penalty;
            Main.PointsLose += penalty;
            Main.Statfrissítés();
            Main.Jatekinditas();
        }

        private void DrawLine(SpriteBatch batch, Texture2D blank,
            float width, Color color, Vector2 point1, Vector2 point2)
        {
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            var length = Vector2.Distance(point1, point2);

            batch.Draw(blank, point1, null, color,
                angle, Vector2.Zero, new Vector2(length, width),
                SpriteEffects.None, 0);
        }
    }
}