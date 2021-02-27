using Foundation;
using System.Linq;
using UIKit;
using Xamarin.Auth;
using Facebook.CoreKit;


namespace MekaApp
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        private bool oturumActimi = false;
        string appId = "1363036637075964";
        string appName = "Meka";


        public override UIWindow Window
        {
            get;
            set;
        }


        //Public property to access our MainStoryboard.storyboard file
        public UIStoryboard MainStoryboard
        {
            get { return UIStoryboard.FromName("Main", NSBundle.MainBundle); }
        }

        //Creates an instance of viewControllerName from storyboard
        public UIViewController GetViewController(UIStoryboard storyboard, string viewControllerName)
        {
            return storyboard.InstantiateViewController(viewControllerName);
        }

        //Sets the RootViewController of the Apps main window with an option for animation.
        public void SetRootViewController(UIViewController rootViewController, bool animate)
        {
            if (animate)
            {
                var transitionType = UIViewAnimationOptions.TransitionFlipFromRight;

                Window.RootViewController = rootViewController;
                UIView.Transition(Window, 0.5, transitionType,
                                  () => Window.RootViewController = rootViewController,
                                  null);
            }
            else
            {
                Window.RootViewController = rootViewController;
            }
        }



        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {

            // This is false by default,
            // If you set true, you can handle the user profile info once is logged into FB with the Profile.Notifications.ObserveDidChange notification,
            // If you set false, you need to get the user Profile info by hand with a GraphRequest
            Profile.EnableUpdatesOnAccessTokenChange(true);
            Settings.AppID = appId;
            Settings.DisplayName = appName;
            //...
            if (AccessToken.CurrentAccessToken != null)
            {
                var requestConnection = new GraphRequestConnection();
                AccessToken.RefreshCurrentAccessToken((connection, result, error) =>
                {
                    // Handle if something went wrong with the request
                    if (error != null)
                    {
                       
                        return;
                    }

                    NSDictionary userInfo = (result as NSDictionary);

                    // Show the info user that allowed to show
                    // If the user haven't assigned anything to his/her information,
                    // it will be null even if the user allowed to show it.
                    
                });
                requestConnection.Start();
            }
            //isAuthenticated can be used for an auto-login feature, you'll have to implement this
            //as you see fit or get rid of the if statement if you want.
            var kayitlihesap = AccountStore.Create().FindAccountsForService("Meka").FirstOrDefault();
            if (kayitlihesap!=null || Profile.CurrentProfile!=null)
            {
                //We are already authenticated, so go to the main tab bar controller;
                var AnaEkranController = GetViewController(MainStoryboard, "AnaEkranViewController") as AnaEkranViewController;
                AnaEkranController.Kullanici = kayitlihesap;
                SetRootViewController(AnaEkranController, true);
            }
            else
            {
                //User needs to log in, so show the Login View Controlller
                var ViewController = GetViewController(MainStoryboard, "GirisEkrani") as ViewController;
                //ViewController.OnLoginSuccess += LoginViewController_OnLoginSuccess;
                SetRootViewController(ViewController, false);
            }


            // This method verifies if you have been logged into the app before, and keep you logged in after you reopen or kill your app.
            return ApplicationDelegate.SharedInstance.FinishedLaunching(application, launchOptions);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
            
        }
    }
}