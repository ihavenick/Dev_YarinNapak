using System;
using Xamarin.Auth;
using Newtonsoft.Json;
using System.Json;
using UIKit;
using Foundation;
using System.Collections.Generic;

namespace MekaApp
{
    public partial class ViewController : UIViewController
    {
        Account Kullanici;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void UIButton8_TouchUpInside(UIButton sender)
        {
            // https://developers.facebook.com/apps/
            var auth = new OAuth2Authenticator(
                clientId: "1363036637075964",
                scope: "",
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
            var ui = auth.GetUI();
            ui.Title = "Facebook'la Giriş yap";

            auth.Completed += FacebookAuth_Tamamlandi;

            PresentViewController(ui, true, null);
        }
    

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {

            if (segueIdentifier == "FacebookOturumAcma")
            {
                
                if (Kullanici!=null)
                {
                    
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            return base.ShouldPerformSegue(segueIdentifier, sender);
        }

        async void FacebookAuth_Tamamlandi(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                AccountStore.Create().Save(e.Account, "Facebook");
                Kullanici = e.Account;
            }

            DismissViewController(true, null);
        }
    }
}