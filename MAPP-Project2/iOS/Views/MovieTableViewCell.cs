using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace MovieApp.iOS.Views
{
    public class MovieTableViewCell : UITableViewCell
    {
        private const double ImageHeight = 100;
        private const double ImageWidth = 66;
        private readonly UIImageView imageView;
        private readonly UILabel titleLabel;
        private readonly UILabel castLabel;

        public MovieTableViewCell(NSString cellId) : base(UITableViewCellStyle.Default, 
                                                        cellId)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Default;

            this.imageView = new UIImageView()
            {
                Frame = new CGRect(0, 0, ImageWidth, ImageHeight)
            };

            this.titleLabel = new UILabel()
            {
                Frame = new CGRect(ImageWidth + 10, 20, this.ContentView.Bounds.Width - ImageWidth + 10, 25),
                TextColor = iOS.Stylesheet.textColor,
                Font = UIFont.FromName(iOS.Stylesheet.primaryFont, 20)
            };

            this.castLabel = new UILabel()
            {
                Frame = new CGRect(ImageWidth + 10, 55, this.ContentView.Bounds.Width - ImageWidth + 10, 25),
                Font = UIFont.FromName(iOS.Stylesheet.primaryFont, 12),
                TextColor = iOS.Stylesheet.textColor,
                BackgroundColor = UIColor.Clear
            };

            this.ContentView.AddSubviews(new UIView[] {
                this.imageView,
                this.titleLabel,
                this.castLabel
            });

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        internal void UpdateCell(MovieDTO movie)
        {
            this.imageView.Image = UIImage.FromFile(movie.imagePath);
            this.titleLabel.Text = movie.title + " (" + movie.releaseYear + ")";
            this.castLabel.Text = movie.cast;
        }
    }
}
