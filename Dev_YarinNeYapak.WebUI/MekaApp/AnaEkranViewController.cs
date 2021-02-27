using Facebook.CoreKit;
using Facebook.ShareKit;
using Foundation;
using System;
using System.IO;
using System.Linq;
using UIKit;
using Xamarin.Auth;


namespace MekaApp
{
    public partial class AnaEkranViewController : UIViewController
    {
        public Account Kullanici;

        public AnaEkranViewController(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ArkayaArkaplanKoy();
        }

        partial void Logout_TouchUpInside(UIButton sender)
        {
            var hesap = AccountStore.Create().FindAccountsForService("Meka").FirstOrDefault();
            if (hesap != null)
            {
                AccountStore.Create().Delete(hesap, "Meka");
                Profile.CurrentProfile.Dispose();
            }
        }

        partial void UIButton1672_TouchUpInside(UIButton sender)
        {
            // Perform any additional setup after loading the view
            AppInviteContent aic = new AppInviteContent()
            {
                AppLinkURL = new NSUrl("https://mekaapp.com"),
                PreviewImageURL = new NSUrl("https://mekaapp.com/images/mobile2.png"),
                 Destination = AppInviteDestination.Facebook
            };
            AppInviteDialog aid = new AppInviteDialog();
            aid.Content = aic;
            aid.Show();
        }
        private void ArkayaArkaplanKoy()
        {
            string contentDirectoryPath = Path.Combine(NSBundle.MainBundle.BundlePath, "Content/");

            string indexhtml = @"
<!DOCTYPE html>
<html lang=""en"">
  <head>
  <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
<link rel=""stylesheet"" type=""text/css"" href=""css/normalize.css""/>
    <link rel=""stylesheet"" type=""text/css"" href=""css/icons.css""/>
         <link rel=""stylesheet"" type=""text/css"" href=""css/demo.css""/>
              <link rel=""stylesheet"" type=""text/css"" href=""css/component.css""/>
                   <script src=""js/modernizr-custom.js""></script>
                                              </head>
                          <body>
                          <div class=""container"">
<div class=""content"">
<p class=""mobile-message"">Scroll down to the button</p>
<div class=""component"" data-path-start=""M280,466c0,0.13-0.001,0.26-0.003,0.39c-0.002,0.134-0.004,0.266-0.007,0.396
	C279.572,482.992,266.307,496,250,496h-2.125H51.625H50c-16.316,0-29.592-13.029-29.99-29.249c-0.003-0.13-0.006-0.261-0.007-0.393
	C20.001,466.239,20,466.119,20,466l0,0c0-0.141,0.001-0.281,0.003-0.422C20.228,449.206,33.573,436,50,436h1.625h196.25H250
    c16.438,0,29.787,13.222,29.997,29.608C279.999,465.738,280,465.869,280,466L280,466z"" data-path-listen=""M181,466c0,0.13-0.001,0.26-0.003,0.39c-0.002,0.134-0.004,0.266-0.007,0.396
	C180.572,482.992,167.307,496,151,496h-2.125h2.75H150c-16.316,0-29.592-13.029-29.99-29.249c-0.003-0.13-0.006-0.261-0.007-0.393
	C120.001,466.239,120,466.119,120,466l0,0c0-0.141,0.001-0.281,0.003-0.422C120.228,449.206,133.573,436,150,436h1.625h-2.75H151
    c16.438,0,29.787,13.222,29.997,29.608C180.999,465.738,181,465.869,181,466L181,466z"" data-path-player=""M290,40c0,0.13-0.001,380.26-0.003,380.39c-0.002,0.134,0.006,24.479,0.003,24.609 c0,3.095-2.562,5.001-5,5.001h-27.125H41.625H15c-1.875,0-5-1.25-5-5.001c-0.003-0.13,0.004-24.509,0.003-24.641 C10.001,420.239,10,40.119,10,40l0,0c0-0.141-0.002-24.859,0-25c0,0,0-5,5-5h26.625h216.25H285c2.438,0,5,1.906,5,5 C290.002,15.13,290,39.869,290,40L290,40z"">
<svg class=""morpher"" width=""300"" height=""500"">
<path class=""morph__button"" d=""M280,466c0,0.13-0.001,0.26-0.003,0.39c-0.002,0.134-0.004,0.266-0.007,0.396
	C279.572,482.992,266.307,496,250,496h-2.125H51.625H50c-16.316,0-29.592-13.029-29.99-29.249c-0.003-0.13-0.006-0.261-0.007-0.393
	C20.001,466.239,20,466.119,20,466l0,0c0-0.141,0.001-0.281,0.003-0.422C20.228,449.206,33.573,436,50,436h1.625h196.25H250
    c16.438,0,29.787,13.222,29.997,29.608C279.999,465.738,280,465.869,280,466L280,466z""/>
</svg>
<button class=""button button--start"">
<span class=""button__content button__content--start"">Bugün Ne Yapsam?</span>
<span class=""button__content button__content--listen""><span class=""icon icon--microphone""></span></span>
</button>
<div class=""player player--hidden"">
<img class=""player__cover"" src=""img/Gramatik.jpg"" alt=""Water 4 The Soul by Gramatik""/>
<div class=""player__meta"">
<h3 class=""player__track"">Yakındaki Etkinlikler Yükleniyor</h3>
<h3 class=""player__album"">
<span class=""player__album-name"">Bu biraz zaman alabilir.</span>
</h3>
</div>
</div>
</div>
</div>
</div>
<script src=""js/classie.js""></script>
  <script src=""js/snap.svg-min.js""></script>
   <script src=""js/main.js""></script>
    </body>
</html>";
            ShazamButton.LoadHtmlString(indexhtml, new NSUrl(contentDirectoryPath, true));
            ShazamButton.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
            ShazamButton.ScrollView.ScrollEnabled = false;
        }
    }
}