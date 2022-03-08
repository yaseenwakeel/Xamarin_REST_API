using SecondProject.Model;
using SecondProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecondProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JSONPlaceholder_REST_API : ContentPage
    {
        public JSONPlaceholder_REST_API()
        {
            InitializeComponent();
        }

        private void EditBtn_Clicked(object sender, EventArgs e)
        {
            var vm=BindingContext as JSONPlaceholder_REST_API_ViewModel;
            var data = (sender as Button).BindingContext as Post;
            vm.OnEditCommand.Execute(data);
            //vm.OnEditCommand.Execute(data);

        }

        private void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as JSONPlaceholder_REST_API_ViewModel;
            var data = (sender as Button).BindingContext as Post;
            vm.OnDeleteCommand.Execute(data);
        }
    }
}