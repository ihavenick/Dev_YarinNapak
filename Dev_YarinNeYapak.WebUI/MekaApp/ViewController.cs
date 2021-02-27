using Foundation;
using Geolocator.Plugin;
using System;
using System.IO;
using System.Json;
using UIKit;
using Xamarin.Auth;
using Facebook.LoginKit;
using Facebook.CoreKit;
using System.Collections.Generic;
using CoreGraphics;
using System.Linq;

namespace MekaApp
{
    public partial class ViewController : UIViewController
    {
        Account Kullanici;
        LoginButton loginView;
        List<string> readPermissions = new List<string> { "public_profile","email" };

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            View.BackgroundColor = UIColor.Black;
            ArkayaDunyaKoyAsync();
            // Set the Read and Publish permissions you want to get
            loginView = new LoginButton(new CGRect(82, 535, 64, 64))
            {
                LoginBehavior = LoginBehavior.Native,
                ReadPermissions = readPermissions.ToArray(),

            };
            // Handle actions once the user is logged in
            loginView.Completed += (sender, e) => {
                if (e.Error != null)
                {
                    // Handle if there was an error
                }

                if (e.Result.IsCancelled)
                {
                    // Handle if the user cancelled the login request
                }

                Account kullanici = new Account();
                kullanici.Username = "anan";
                AccountStore.Create().Save(kullanici, "Meka");
                PerformSegue("FacebooklaGiris", new NSObject());
                //AnaViewController AVC = new AnaViewController() { };
               // NavigationController.PushViewController(AVC, true);
                // Handle your successful login
            };
            // Handle actions once the user is logged out
            loginView.LoggedOut += (sender, e) => {
                // Handle your logout
                
                //Profile.CurrentProfile.Dispose();
                var hesap = AccountStore.Create().FindAccountsForService("Meka").FirstOrDefault();
                if (hesap != null)
                {
                    AccountStore.Create().Delete(hesap, "Meka");
                }
            };

            //string dosyaAdi = "World.gif"; // remember case-sensitive
            //string dosyaYolu = Path.Combine(NSBundle.MainBundle.BundlePath, dosyaAdi);
            //TestWebView.LoadRequest(new NSUrlRequest(new NSUrl(dosyaYolu, false)));

            TestWebView.ScalesPageToFit = true;
            // TestWebView.ScrollView.ContentInset = new UIEdgeInsets(-60, -670, -700, -1000);
            // Perform any additional setup after loading the view, typically from a nib.
            View.AddSubview(loginView);
            loginView.Hidden = true;
            InstagramlaGir.Hidden = true;
            EmailButton.Hidden = true;
            MekaLogo.Hidden = true;
            GoogleLaGir.Hidden = true;
            Yukleme.Hidden = false;
            Yukleme.StartAnimating();
        }

        public async void ArkayaDunyaKoyAsync()
        {
            var locator = CrossGeolocator.Current;

            locator.DesiredAccuracy = 100; //100 is new default

            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
           
            int enlem = Convert.ToInt32(position.Latitude);
            int boylam = Convert.ToInt32(position.Longitude);
            //TestWebView.LoadRequest(new NSUrlRequest(new NSUrl("https://mekaapp.com/MWorld?enlem=" + position.Latitude + "&boylam=" + position.Longitude, false)));
            //TestWebView.LoadRequest(new NSUrlRequest(new NSUrl("https://mekaapp.com/MWorld", false)));
            string indexhtml = @"<!DOCTYPE HTML>
<html>
<head>
    <script src=""js/compressed.js""></script>
     <script>
       var earth;
        function initialize() {
            var options = {
                sky: true,
                atmosphere: true,
                dragging: false,
                tilting: true,
                zooming: false,
                center: [46.8011, 8.2266],
                zoom: 2
            };
        earth = new WE.map('earth_div', options);
		var bounds = [[46, 8], [36.13343831, -112.10998535]];
        earth.setView([46.8011, 8.2266], 4);
		earth.setTilt(90);
        WE.tileLayer('http://tileserver.maptiler.com/nasa/{z}/{x}/{y}.jpg', {
            minZoom: 0,
            maxZoom: 50,
            attribution: 'NASA'
        }).addTo(earth);	
         var marker1 = WE.marker([" + enlem + ", " + boylam + @"]).addTo(earth);
		        var before = null;
        requestAnimationFrame(function animate(now) {
            var c = earth.getPosition();
            var elapsed = before? now - before: 0;
            before = now;
            earth.setCenter([c[0], c[1] + 0.1*(elapsed/30)]);
            requestAnimationFrame(animate);
        });
      }
    </script>
    <style>
        html, body {
            padding: 0;
            margin: 0;
        }

        #earth_div {
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            position: absolute !important;
        }
    </style>
    <title>Dünya haritası</title>
</head>
<body onload=""initialize()"">
    <div id=""earth_div""></div>
Anan
</body>
</html>";
            string contentDirectoryPath = Path.Combine(NSBundle.MainBundle.BundlePath, "Content/");
            TestWebView.LoadHtmlString(indexhtml, new NSUrl(contentDirectoryPath, true));
            TestWebView.LoadFinished += TestWebView_LoadFinished;
        }

        private void TestWebView_LoadFinished(object sender, EventArgs e)
        {
            InstagramlaGir.Hidden = false;
            loginView.Hidden = false;
            GoogleLaGir.Hidden = false;
            EmailButton.Hidden = false;
            MekaLogo.Hidden = false;
            Yukleme.StopAnimating();
            Yukleme.Hidden = true;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            var anaViewController = segue.DestinationViewController as AnaViewController;

            //set the Table View Controller’s list of phone numbers to the
            // list of dialed phone numbers

            if (anaViewController != null)
            {
                anaViewController.Kullanici = Kullanici;
            }
        }



        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {

            if (segueIdentifier == "FacebooklaGiris")
            {
                if (Kullanici==null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return base.ShouldPerformSegue(segueIdentifier, sender);
        }

        //partial void UIButton8_TouchUpInside(UIButton sender)
        //{
        //    // https://developers.facebook.com/apps/
        //    var auth = new OAuth2Authenticator(
        //        clientId: "1363036637075964",
        //        scope: "",
        //        authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
        //        redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
        //    var ui = auth.GetUI();

        //    ui.Title = "Facebook'la Giriş yap";

        //    auth.Completed += FacebookAuth_Tamamlandi;

        //    PresentViewController(ui, true, null);
        //}

        void FacebookAuth_Tamamlandi(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                //AccountStore.Create().Save(e.Account, "Facebook");
                Kullanici = e.Account;
                AccountStore.Create().Save(e.Account, "Meka");
            }

            DismissViewController(true, null);
            PerformSegue("FacebooklaGiris", new NSObject());
        }

        partial void InstagramlaGir_TouchUpInside(UIButton sender)
        {
            var auth = new OAuth2Authenticator(
                clientId: "f30b063a7b9e4e0e9e9df6b7de4ef0bc",
                scope: "basic",
               authorizeUrl: new Uri("https://api.instagram.com/oauth/authorize/"),
               redirectUrl: new Uri("https://mekaapp.com/login/insta?"));

            //auth.AllowCancel = allowCancel;

            // If authorization succeeds or is canceled, .Completed will be fired.
            auth.Completed += (s, ee) => {
                var token = ee.Account.Properties["access_token"];
            };

            var ui = auth.GetUI();


            auth.Completed += InstagramAuth_Tamamlandi;

            PresentViewController(ui, true, null);
        }

        private void InstagramAuth_Tamamlandi(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                Kullanici = e.Account;
                AccountStore.Create().Save(e.Account, "Meka");
            }

            DismissViewController(true, null);
            PerformSegue("InstagramGiris", new NSObject());
        }

        partial void EmailButton_TouchUpInside(UIButton sender)
        {
           // throw new NotImplementedException();
        }
    }
}