using Foundation;
using System;
using System.Json;
using System.Linq;
using UIKit;
using Xamarin.Auth;
using Facebook.LoginKit;
using AssetsLibrary;
using Facebook.CoreKit;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using Facebook.ShareKit;

namespace MekaApp
{
    public partial class AnaViewController : UIViewController
    {
        public Account Kullanici;
        private UIImagePickerController imagePicker;

        public AnaViewController(IntPtr handle) : base(handle)
        {
            Kullanici = new Account();
        }

        public override void LoadView()
        {
            base.LoadView();
            ArkayaArkaplanKoy();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Profile.Notifications.ObserveDidChange((sender, e) => {

                if (e.NewProfile == null)
                    return;

                FaceVerileriGetir();
            });


            // Perform any additional setup after loading the view, typically from a nib.
            if (Profile.CurrentProfile != null)
            {
                FaceVerileriGetir();
            }
            else
            {
                //resim yükle tusunu göster
                BresimSec.TouchUpInside += (s, e) =>
                {
                    // create a new picker controller
                    imagePicker = new UIImagePickerController()
                    {

                        // set our source to the photo library
                        SourceType = UIImagePickerControllerSourceType.PhotoLibrary,

                        // set what media types
                        MediaTypes = new string[] { "public.image" }
                    };
                    imagePicker.FinishedPickingMedia += ResimSecti;
                    imagePicker.Canceled += ResimSecmeyiIptalEtti;

                    // show the picker
                    PresentViewController(imagePicker, true, null);
                    //NavigationController.PresentModalViewController(imagePicker, true);
                    //UIPopoverController picc = new UIPopoverController(imagePicker);

                };
                TBEmail.ReturnKeyType = UIReturnKeyType.Done;
                TBAdSoyad.ReturnKeyType = UIReturnKeyType.Done;
                TBKullaniciAdi.ReturnKeyType = UIReturnKeyType.Done;
                TBSifre.ReturnKeyType = UIReturnKeyType.Done;
                TBSifreTekrar.ReturnKeyType = UIReturnKeyType.Done;
                TBSifre.SecureTextEntry = true;
                TBSifreTekrar.SecureTextEntry = true;
            }
            //FacebooktanGetirAsync();
            this.TBSifre.ShouldReturn += (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            this.TBAdSoyad.ShouldReturn += (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            this.TBEmail.ShouldReturn += (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            this.TBSifreTekrar.ShouldReturn += (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            this.TBKullaniciAdi.ShouldReturn += (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
        }

        private void FaceVerileriGetir()
        {
            TBAdSoyad.Enabled = false;
            TBEmail.Enabled = false;
            TBAdSoyad.Text = Profile.CurrentProfile.Name;
            KullanicininResmi.Image = UIImage.LoadFromData(NSData.FromUrl(Profile.CurrentProfile.ImageUrl(ProfilePictureMode.Normal, new CoreGraphics.CGSize(128, 128))));
            BresimSec.Hidden = true;
            // Ask for the info that the user allowed to show
            var fields = "?fields=id,name,email";
            var request = new GraphRequest("/" + Profile.CurrentProfile.UserID + fields, null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
            var requestConnection = new GraphRequestConnection();
            requestConnection.AddRequest(request, (connection, result, error) =>
            {
                // Handle if something went wrong with the request
                if (error != null)
                {
                    ShowMessageBox("Hata", error.Description, "Ok", null, true);
                    return;
                }

                NSDictionary userInfo = (result as NSDictionary);

                // Show the info user that allowed to show
                // If the user haven't assigned anything to his/her information,
                // it will be null even if the user allowed to show it.
                if (userInfo["email"] != null)
                    TBEmail.Text = userInfo["email"].ToString();
            });
            requestConnection.Start();
        }

        private void ArkayaArkaplanKoy()
        {
            string indexhtml = @"<!DOCTYPE html>
<html>
<head>
<script src=""js/fss.min.js""></script>
<style>body{background:#111118;margin:0;}.container{position: absolute;height: 100%;width: 100%;}</style>
  </head>
  <body>
     <div id=""container"" class=""container"">
   <div id=""output"" class=""container"">
   </div>
</div>
<script src=""js/example.js""></script>
</body></html>";
            string contentDirectoryPath = Path.Combine(NSBundle.MainBundle.BundlePath, "Content/");
            KayitEkraniWebView.LoadHtmlString(indexhtml, new NSUrl(contentDirectoryPath, true));
        }

        private void ResimSecmeyiIptalEtti(object sender, EventArgs e)
        {
            imagePicker.DismissModalViewController(true);
        }

        // Revoke all the permissions that you had granted, you will need to login again for ask the permissions again
        // You can also revoke a single permission, for more info, visit this link:
        // https://developers.facebook.com/docs/facebook-login/permissions/v2.3#revoking
        void RevokeAllPermissions(string userId)
        {
            // Create the request for delete all the permissions
            var request = new GraphRequest("/" + userId + "/permissions", null, AccessToken.CurrentAccessToken.TokenString, null, "DELETE");
            var requestConnection = new GraphRequestConnection();
            requestConnection.AddRequest(request, (connection, result, error) =>
            {
                // Handle if something went wrong
                if (error != null)
                {
                    ShowMessageBox("Error...", error.Description, "Ok", null, true);
                    return;
                }

                // If the revoking was successful, logout from FB
                if ((result as NSDictionary)["success"].ToString() == "1")
                    InvokeOnMainThread(() =>
                    {
                        ShowMessageBox("Successful", "All permissions have been revoked", "Ok", null, true);
                        var login = new LoginManager();
                        login.LogOut();

                    });
                else
                    InvokeOnMainThread(() => ShowMessageBox("Ups...", "A problem has ocurred", "Ok", null, true));

            });
            requestConnection.Start();
        }

        void ShowMessageBox(string title, string message, string cancelButton, string[] otherButtons, bool PopUpmi)
        {
            var Actionshit = UIAlertControllerStyle.ActionSheet;
            if (PopUpmi)
            {
                Actionshit = UIAlertControllerStyle.Alert;
            }
            var alert = UIAlertController.Create(title, message, Actionshit);

            alert.AddAction(UIAlertAction.Create(cancelButton, UIAlertActionStyle.Cancel, null));
            //alert.AddAction(UIAlertAction.Create("Snooze", UIAlertActionStyle.Default, action => Snooze()));
            //if (alert.PopoverPresentationController != null)
            //alert.PopoverPresentationController.BarButtonItem = myItem;
            PresentViewController(alert, animated: true, completionHandler: null);
        }

        private void ResimSecti(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            // determine what was selected, video or image
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    Console.WriteLine("Image selected");
                    isImage = true;
                    break;
                case "public.video":
                    Console.WriteLine("Video selected");
                    break;
            }

            // get common info (shared between images and video)
            if (e.Info[new NSString("UIImagePickerControllerReferenceUrl")] is NSUrl referenceURL)
                Console.WriteLine("Url:" + referenceURL.ToString());

            // if it was an image, get the other image info
            if (isImage)
            {
                // get the original image
                if (e.Info[UIImagePickerController.OriginalImage] is UIImage originalImage)
                {
                    // do something with the image
                    Console.WriteLine("got the original image");
                    KullanicininResmi.Image = originalImage; // display
                }
            }
            else
            { // if it's a video
              // get video url
                UIAlertView _error = new UIAlertView("Nasıl Lan?", "Nasıl başardın video yüklemeyi ibne", null, "Tamam", null);

                _error.Show();
            }
            // dismiss the picker
            imagePicker.DismissModalViewController(true);
        }

        public async void FacebooktanGetirAsync()
        {
            if (Kullanici != null)
            {
                var request = new OAuth2Request(
                       "GET",
                       new Uri("https://graph.facebook.com/me?fields=name,picture,cover,birthday"),
                       null,
                       Kullanici);

                var fbResponse = await request.GetResponseAsync();

                var fbUser = JsonValue.Parse(fbResponse.GetResponseText());

                var name = fbUser["name"];
                var id = fbUser["id"];
                var picture = fbUser["picture"]["data"]["url"];
                var cover = fbUser["cover"]["source"];

                TBAdSoyad.Text = name;
                KullanicininResmi.Image = UIImage.LoadFromData(NSData.FromUrl(new NSUrl(picture)));
            }

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }


        partial void Logout_TouchUpInside(UIButton sender)
        {
            var hesap = AccountStore.Create().FindAccountsForService("Meka").FirstOrDefault();
            if (hesap != null)
            {
                AccountStore.Create().Delete(hesap, "Meka");
            }
            RevokeAllPermissions(Profile.CurrentProfile.UserID);
        }

        partial void BkayitOl_TouchUpInside(UIButton sender)
        {
            var alert = UIAlertController.Create("Kayıt Ol", "Bilgileri doğru girdiğinizden emin misiniz?", UIAlertControllerStyle.ActionSheet);

            alert.AddAction(UIAlertAction.Create("Hayır", UIAlertActionStyle.Cancel, null));
            alert.AddAction(UIAlertAction.Create("Eminim", UIAlertActionStyle.Default, action => KayitOl()));
            //if (alert.PopoverPresentationController != null)
            //alert.PopoverPresentationController.BarButtonItem = myItem;
            PresentViewController(alert, animated: true, completionHandler: null);
        }

        public void KayitOl()
        {
            if (Regex.Match(TBEmail.Text.ToString(), @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                if (KullanicininResmi.Image == null)
                {
                    ShowMessageBox("Resim yüklemediniz", "Herhangi bir resim yüklemediniz", "Ok", null, true);
                }
                else
                {
                    if (TBAdSoyad.Text != "" && TBKullaniciAdi.Text != "" && TBSifre.Text != "")
                    {
                        if (TBSifre.Text == TBSifreTekrar.Text)
                        {
                            KayitOlma Ko = new KayitOlma();
                            if (Profile.CurrentProfile != null)
                            {
                                Ko.facelekayitolmus = true;
                                Ko.facebookid = Profile.CurrentProfile.UserID;
                            }
                            else
                            {
                                Ko.facelekayitolmus = false;
                            }
                            NSData imageData = KullanicininResmi.Image.AsJPEG(0.5f);
                            Ko.AvatarBase64 = imageData.GetBase64EncodedData(NSDataBase64EncodingOptions.None).ToString();
                            Ko.Email = TBEmail.Text.ToString();
                            Ko.adsoyad = TBAdSoyad.Text.ToString();
                            Ko.sifre = TBSifre.Text.ToString();
                            Ko.Kullaniciadi = TBKullaniciAdi.Text.ToString();
                            string json = JsonConvert.SerializeObject(Ko);
                            WebClient wc = new WebClient();
                            wc.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
                            wc.Headers.Add(HttpRequestHeader.Accept, "application/json, text/javascript, */*; q=0.01");

                            byte[] dataBytes = Encoding.UTF8.GetBytes(json);
                            byte[] responseBytes = wc.UploadData(new Uri("https://mekaapp.com/api/kayit"), "POST", dataBytes);
                            string responseString = Encoding.UTF8.GetString(responseBytes);
                            Account kullanici = new Account();
                            kullanici.Username = Ko.Kullaniciadi;
                            kullanici.Properties.Add("Email", Ko.Email);
                            kullanici.Properties.Add("Avatar", Ko.AvatarBase64);
                            kullanici.Properties.Add("adsoyad", Ko.adsoyad);
                           // kullanici.Properties.Add("id", responseString);
                            AccountStore.Create().Save(kullanici, "Meka");
                            ShowMessageBox("Debug", responseString, "Tamam", null, true);

                            //try
                            //{
                            //    responseBytes = wc.UploadData(new Uri("https://mekaapp.com/api/kayit"), "POST", dataBytes);
                            //    string responseString = Encoding.UTF8.GetString(responseBytes);
                            //    ShowMessageBox("Debug", responseString, "Tamam", null, true);

                            //}
                            //catch (Exception)
                            //{

                            //}
                            //if (Ko.facelekayitolmus)
                            //{
                            //    var alert = UIAlertController.Create("Davet et", "Arkadaşlarınızı uygulamamıza davet etmek ister misiniz?", UIAlertControllerStyle.ActionSheet);

                            //    alert.AddAction(UIAlertAction.Create("Hayır", UIAlertActionStyle.Cancel, null));
                            //    alert.AddAction(UIAlertAction.Create("Evet", UIAlertActionStyle.Default, action => FacedenDavetYolla()));
                            //    //if (alert.PopoverPresentationController != null)
                            //    //alert.PopoverPresentationController.BarButtonItem = myItem;
                            //    PresentViewController(alert, animated: true, completionHandler: null);
                                
                            //}
                            PerformSegue("ArkadaslariniCagir", new NSObject());
                        }
                        else
                        {
                            ShowMessageBox("Şifreler uyuşmuyor", "Girdiğiniz şifreler birbirleriye uyuşmuyor", "Anladım", null, true);
                        }
                    }
                    else
                    {
                        ShowMessageBox("Eksik Bilgi", "Tüm alanları eksiksiz girdiğinize emin olun", "Tamam", null, true);
                    }
                }
            }
            else
            {
                ShowMessageBox("Email Hatalı", "Email'inizi düzgün girdiğinizden emin olun.", "Ok", null, true);
            }
        }

        private static void FacedenDavetYolla()
        {
            // Perform any additional setup after loading the view
            AppInviteContent aic = new AppInviteContent()
            {
                AppLinkURL = new NSUrl("https://mekaapp.com"),
                PreviewImageURL = new NSUrl("https://mekaapp.com/images/mobile2.png")
            };
            AppInviteDialog aid = new AppInviteDialog();
            aid.Content = aic;
            aid.Show();
        }
    }

    public class KayitOlma
    {
        public string facebookid { get; set; }
        public bool facelekayitolmus { get; set; }
        public string Email { get; set; }
        public string adsoyad { get; set; }
        public string sifre { get; set; }
        public string AvatarBase64 { get; set; }
        public string Kullaniciadi { get; set; }
    }
}