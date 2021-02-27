// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MekaApp
{
    [Register ("AnaViewController")]
    partial class AnaViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView AnaEkran { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BkayitOl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BresimSec { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIWebView KayitEkraniWebView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView KullanicininResmi { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton logout { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TBAdSoyad { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TBEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TBKullaniciAdi { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TBSifre { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TBSifreTekrar { get; set; }

        [Action ("BkayitOl_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BkayitOl_TouchUpInside (UIKit.UIButton sender);

        [Action ("Logout_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Logout_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (AnaEkran != null) {
                AnaEkran.Dispose ();
                AnaEkran = null;
            }

            if (BkayitOl != null) {
                BkayitOl.Dispose ();
                BkayitOl = null;
            }

            if (BresimSec != null) {
                BresimSec.Dispose ();
                BresimSec = null;
            }

            if (KayitEkraniWebView != null) {
                KayitEkraniWebView.Dispose ();
                KayitEkraniWebView = null;
            }

            if (KullanicininResmi != null) {
                KullanicininResmi.Dispose ();
                KullanicininResmi = null;
            }

            if (logout != null) {
                logout.Dispose ();
                logout = null;
            }

            if (TBAdSoyad != null) {
                TBAdSoyad.Dispose ();
                TBAdSoyad = null;
            }

            if (TBEmail != null) {
                TBEmail.Dispose ();
                TBEmail = null;
            }

            if (TBKullaniciAdi != null) {
                TBKullaniciAdi.Dispose ();
                TBKullaniciAdi = null;
            }

            if (TBSifre != null) {
                TBSifre.Dispose ();
                TBSifre = null;
            }

            if (TBSifreTekrar != null) {
                TBSifreTekrar.Dispose ();
                TBSifreTekrar = null;
            }
        }
    }
}