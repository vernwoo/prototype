using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace feelLuckyApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private void OnButtonClicked(object sender, EventArgs args)
        {
            labelMsg.Text = "You win a jackpot!";
        }
    }
}
