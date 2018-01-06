using CoreGraphics;
using UIKit;

namespace MovieApp.iOS.Controllers
{
    public class MovieDetailsViewController : UIViewController
    {
        private readonly MovieDTO movie;
        private const double StartX = 15;
        private const double StartY = 110;

        private const double DefaultHeight = 40;
        private const double DefaultPadding = 10;

        private const double ImageX = 100;
        private const double ImageY = 130;

        public MovieDetailsViewController(MovieDTO movie)
        {
            this.movie = movie;
        }

        public override void ViewDidLoad() 
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = iOS.Stylesheet.backgroundColor;
            this.Title = movie.title;

            UILabel headLabel = new UILabel()
            {
                Frame = new CGRect(StartX,
                                   StartY,
                                   this.View.Bounds.Width - StartX * 2,
                                   DefaultHeight),
                Font = UIFont.FromName(iOS.Stylesheet.primaryFont, 30),
                Text = movie.title + " (" + movie.releaseYear + ")",
                TextColor = iOS.Stylesheet.textColor
            };

            UILabel subHeadLabel = new UILabel()
            {
                Frame = new CGRect(StartX,
                                   StartY + DefaultHeight * 1,
                                   this.View.Bounds.Height - StartX,
                                   DefaultHeight),
                Text = movie.runtime + " min | " + movie.genres,
                TextColor = iOS.Stylesheet.textColor,
                Font = UIFont.FromName(iOS.Stylesheet.primaryFont, 14)
                               
            };

            UIImageView posterImage = new UIImageView()
            {
                Frame = new CGRect(StartX, 
                                   StartY + DefaultHeight * 2.5 + 20, 
                                   ImageX, 
                                   ImageY),
                Image = UIImage.FromFile(movie.imagePath)
            };

            UITextView descriptionLabel = new UITextView()
            {
                Frame = new CGRect(StartX + ImageX + DefaultPadding,
                                   StartY + DefaultHeight * 2.5,
                                   this.View.Bounds.Width - StartX * 2 - ImageX,
                                   this.View.Bounds.Height - StartY),
                Font = UIFont.FromName(iOS.Stylesheet.primaryFont, 14),
                Text = movie.overview,
                TextColor = iOS.Stylesheet.textColor
            };


            this.View.AddSubviews(new UIView[] {
                headLabel,
                subHeadLabel,
                posterImage,
                descriptionLabel
            });
        }
    }
}