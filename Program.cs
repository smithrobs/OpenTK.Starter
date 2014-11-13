using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTK.Starter
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            const int sizeX = 640;
            const int sizeY = 480;

            Int64 tickCount = 0;
            Int64 expectedTickCount = 1; // need this so UpdateFrame only updates once per render
            using (var game = new GameWindow(sizeX, sizeY, GraphicsMode.Default, "OpenTK.Starter", GameWindowFlags.Fullscreen))
            {
                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;
                    game.CursorVisible = false;
                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                game.UpdateFrame += (sender, e) =>
                {
                    if (expectedTickCount > tickCount)
                        return;

                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }

                    // game update logic goes here

                    expectedTickCount++;
                };

                game.RenderFrame += (sender, e) =>
                {
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                    // render graphics goes here

                    game.SwapBuffers();
                    tickCount++;
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}