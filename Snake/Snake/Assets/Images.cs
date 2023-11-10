using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace Snake.Assets;

     
public static class Images
{

    public readonly static ImageSource Empty = LoadImage("Empty.png");
    public readonly static ImageSource Food = LoadImage("Food.png");
    public readonly static ImageSource Head = LoadImage("Head.png");
    public readonly static ImageSource Body = LoadImage("Body.png");
    public readonly static ImageSource BodyGreen = LoadImage("BodyGreen.png");
    public readonly static ImageSource BodyRed = LoadImage("BodyRed.png");
    public readonly static ImageSource BodyBlue = LoadImage("BodyBlue.png");
    public readonly static ImageSource BodyLightBlue = LoadImage("BodyLightBlue.png");
    public readonly static ImageSource BodyYellow = LoadImage("BodyYellow.png");
    public readonly static ImageSource BodyOrange = LoadImage("BodyOrange.png");
    public readonly static ImageSource DeadBody = LoadImage("DeadBody.png");
    public readonly static ImageSource DeadHead = LoadImage("DeadHead.png");
    private static ImageSource LoadImage(string filename)
    {
        return new BitmapImage(new Uri($"../Assets/SnakeAssets/{filename}", UriKind.Relative));
    } 
}