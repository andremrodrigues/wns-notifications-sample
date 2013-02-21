using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VSMWnsPushNotifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadPushTypes();
        }

        private void LoadPushTypes()
        {
            List<PushTypeViewModel> pushTypes = new List<PushTypeViewModel>();
            foreach (var pushType in Enum.GetValues(typeof(PushNotifier.PushType)))
            {
                pushTypes.Add(new PushTypeViewModel()
                {
                    Text = pushType.ToString(),
                    Value = Convert.ToInt32(pushType).ToString()
                });
            }

            PushType.ItemsSource = pushTypes;
            PushType.DisplayMemberPath = "Text";
            PushType.SelectedValuePath = "Value";
        }

        private void Notify_Click(object sender, RoutedEventArgs e)
        {
            WnsAuthorization.OAuthToken oAuthToken = WnsAuthorization.GetOAuthToken(SecretKey.Password, Sid.Password);
            PushNotifier notifier = new PushNotifier(PushUri.Text, oAuthToken.AccessToken);
            PushNotifier.PushType pushType = (PushNotifier.PushType)Enum.Parse(typeof(PushNotifier.PushType),
            PushType.SelectedValue.ToString());

            if (notifier.SendNotification(pushType, Message.Text, ImageSource.Text))
            {
                Status.Content = "Success!";
            }
            else
            {
                Status.Content = "Failed to send";
            }
        }
    }
}
