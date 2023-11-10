﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Snake.Assets;
using Snake.GameObjects;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            { GridValue.Empty, Images.Empty },
            { GridValue.Snake, Images.Body },
            { GridValue.Food, Images.Food }
        };
        private static readonly LinkedList<ImageSource> BodyParts = new LinkedList<ImageSource>(
            new ImageSource[]
            {
                Images.BodyGreen, 
                Images.BodyOrange,
                Images.BodyYellow, 
                Images.BodyRed,
                Images.BodyLightBlue, 
                Images.BodyBlue
            });
            
        private readonly int rows = 15, cols = 15;
        private readonly Image[,] GridImages;
        private GameState gameState;
        private bool gameRunning;
        private List<GridValue> SnakeGrids;
        public MainWindow()
        {
            InitializeComponent();
            GridImages = SetupGrid();
            gameState = new GameState(rows, cols);
        }

        public List<GridValue> getSnakeGrids()
        {
            List<GridValue> snakeBodyGrids = new List<GridValue>();
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (gameState.Grid[r, c] == GridValue.Snake)
                    {
                        snakeBodyGrids.Add(gameState.Grid[r, c]);
                    }
                }
            }
        }

        private async Task RunGame()
        {
            Draw();
            await ShowCountDown();
            Overlay.Visibility = Visibility.Hidden;
            await GameLoop();
            await ShowGameOver();
            gameState = new GameState(rows, cols);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
             case Key.Left:
                 gameState.ChangeDirection(Direction.Left);
                 break;
             case Key.Right:
                 gameState.ChangeDirection(Direction.Right);
                 break;
             case Key.Up:
                 gameState.ChangeDirection(Direction.Up);
                 break;
             case Key.Down:
                 gameState.ChangeDirection(Direction.Down);
                 break;
            }
        }
        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }

            if (!gameRunning)
            {
                gameRunning = true;
                await RunGame();
                gameRunning = false;
            }
        }
        private async Task GameLoop()
        {
            while (!gameState.GameOver)
            {
                await Task.Delay(200);
                gameState.Move();
                Draw();
            }
        }
        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty
                    };
                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }

            return images;
        }

        private void MainWindow_OnKeyDown_(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Draw()
        {
            DrawGrid();
            ScoreText.Text = $"SCORE {gameState.Score}";
        }

        private void DrawGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    GridValue gridValue = gameState.Grid[r, c];
                    
                    GridImages[r, c].Source = gridValToImage[gridValue];
                }
            }
        }

        private async Task ShowCountDown()
        {
            for (int i = 3; i >= 1; i--)
            {
                OverlayText.Text = i.ToString();
                await Task.Delay(500);
            }
        }

        private async Task ShowGameOver()
        {
            await Task.Delay(500);
            Overlay.Visibility = Visibility.Visible;
            OverlayText.Text = "Press any key to start";
        }
    }
}