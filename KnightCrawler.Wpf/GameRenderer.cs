namespace KnightCrawler.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    class GameRenderer : FrameworkElement
    {
        ImageBrush[] floorBrushes = new ImageBrush[8];

        public void Initialize()
        {
            for(int i = 0; i < floorBrushes.Length; i++)
            {
                this.floorBrushes[i] = new ImageBrush(
                new BitmapImage(
                    new Uri(
                        string.Format("floor_{0}.png", i + 1),
                        UriKind.RelativeOrAbsolute)));
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {

        }
    }
}
