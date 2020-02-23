using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Temp_XamarinForms1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void OnEnterPressed(object sender, EventArgs e)
        {
            //ObjectCommandIsOn.LoginCommand.Invoke();
            // OR
            // Login Command Logic Can go here, but use a ViewModel like a normal Person at least.
            ResultLabel.Text = "Greeting " + InputStr.Text;

        }
    }
}
