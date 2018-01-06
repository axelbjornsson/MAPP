using UIKit;

namespace MovieApp.iOS.Controllers
{
    public class TabBarController : UITabBarController
    {
        public bool reloadTopRated;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.TabBar.BackgroundColor = iOS.Stylesheet.tabBarBackgroundColor;
            this.TabBar.TintColor = iOS.Stylesheet.tabBarTintColor;
            this.SelectedIndex = 0;
            this.ViewControllerSelected += (sender, e) => {
                if (this.SelectedIndex == 0) {
                    this.reloadTopRated = true;
                }
            }; 
        }

    }
}