using System;
using UIKit;
using CoreGraphics;

public class LoadingOverlay : UIView {
    // control declarations
    UIActivityIndicatorView activitySpinner;

    public LoadingOverlay (CGRect frame) : base (frame)
    {
        // configurable bits
        BackgroundColor = UIColor.White;
        Alpha = 1.0f;
        AutoresizingMask = UIViewAutoresizing.All;

        // derive the center x and y
        nfloat centerX = Frame.Width / 2;
        nfloat centerY = (Frame.Height - 60) / 2;

        // create the activity spinner, center it horizontall and put it 5 points above center x
        activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
        activitySpinner.Frame = new CGRect (
            centerX - (activitySpinner.Frame.Width / 2) ,
            centerY - activitySpinner.Frame.Height - 20 ,
            activitySpinner.Frame.Width,
            activitySpinner.Frame.Height);
        activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
        AddSubview (activitySpinner);
        activitySpinner.StartAnimating ();
    }

    /// <summary>
    /// Fades out the control and then removes it from the super view
    /// </summary>
    public void Hide ()
    {
        UIView.Animate (
            0.5, // duration
            () => { Alpha = 0; },
            () => { RemoveFromSuperview(); }
        );
    }
}