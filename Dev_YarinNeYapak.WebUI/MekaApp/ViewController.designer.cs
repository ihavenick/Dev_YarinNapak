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
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton EmailButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GoogleLaGir { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton InstagramlaGir { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel MekaLogo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIWebView TestWebView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView Yukleme { get; set; }

        [Action ("EmailButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void EmailButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("InstagramlaGir_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void InstagramlaGir_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (EmailButton != null) {
                EmailButton.Dispose ();
                EmailButton = null;
            }

            if (GoogleLaGir != null) {
                GoogleLaGir.Dispose ();
                GoogleLaGir = null;
            }

            if (InstagramlaGir != null) {
                InstagramlaGir.Dispose ();
                InstagramlaGir = null;
            }

            if (MekaLogo != null) {
                MekaLogo.Dispose ();
                MekaLogo = null;
            }

            if (TestWebView != null) {
                TestWebView.Dispose ();
                TestWebView = null;
            }

            if (Yukleme != null) {
                Yukleme.Dispose ();
                Yukleme = null;
            }
        }
    }
}