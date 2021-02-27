// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MekaApp
{
    [Register ("AnaEkranViewController")]
    partial class AnaEkranViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton logout { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIWebView ShazamButton { get; set; }

        [Action ("Logout_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Logout_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton1672_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton1672_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (logout != null) {
                logout.Dispose ();
                logout = null;
            }

            if (ShazamButton != null) {
                ShazamButton.Dispose ();
                ShazamButton = null;
            }
        }
    }
}