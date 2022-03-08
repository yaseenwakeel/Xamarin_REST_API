using Newtonsoft.Json;
using SecondProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace SecondProject.ViewModels
{
    public class JSONPlaceholder_REST_API_ViewModel : BaseViewModel
    {
        #region API_Properties

        private const string Url = "https://jsonplaceholder.typicode.com/posts";
        private HttpClient _httpClient = new HttpClient();
        #endregion

        #region Properties
        #region CommandProperties
        public Command OnUpdateCommand { get; set; }
        public Command OnDeleteCommand { get; set; }
        public Command OnAddCommand { get; set; }
        public Command OnEditCommand { get; set; }
        #endregion
        #region LayoutControlProperties

        private int _entryUserId;

        public int entryUserId
        {
            get { return _entryUserId;  }
            set
            {
                _entryUserId = value;
                NotifyPropertyChanged();
            }
        }

        private string _entryTitle;

        public string entryTitle
        {
            get { return _entryTitle; }
            set { _entryTitle = value; NotifyPropertyChanged(); }
        }
        private string _entryDescription;

        public string entryDescription
        {
            get { return _entryDescription; }
            set { _entryDescription = value; NotifyPropertyChanged(); }
        }

        private int _selectedPostId;

        public int SelectedPostId
        {
            get { return _selectedPostId; }
            set { _selectedPostId = value; NotifyPropertyChanged(); }
        }
        #endregion

        private List<Post> _postsList;
        public List<Post> PostsList
        {
            get { return _postsList; }
            set
            {
                _postsList = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Constructor

        public JSONPlaceholder_REST_API_ViewModel()
        {
            PostsList = new List<Post>();
            FetchApiData();

            OnAddCommand = new Command(OnAdd);
            OnEditCommand = new Command(OnEdit);
            OnUpdateCommand = new Command(OnUpdate);
            OnDeleteCommand = new Command(OnDelete);
        }
        #endregion



        #region Commands
        #region LinkConcatinationOnWebsite
        //PUT	/posts/1
        //PATCH	/posts/1
        //DELETE	/posts/1
        #endregion
        private async void OnAdd(object obj)
        {
            var new_post = new Post() { userId = entryUserId, title = entryTitle, body = entryDescription };
            var new_content = JsonConvert.SerializeObject(new_post);
            await _httpClient.PostAsync(Url, new StringContent(new_content));
            #region ResetFieldsToDefault

            entryTitle = string.Empty;
            entryDescription = string.Empty;
            entryTitle = null;
            #endregion
            FetchApiData(); // Display Updated Data
        }

        private void OnEdit(object obj)
        {
            var data = obj as Post;
            entryUserId = data.userId;
            entryTitle = data.title;
            entryDescription = data.body;
            SelectedPostId = data.id;
            //var update_post = new Post() { title = "xyz_Title", body = "xyz_Description" };
        }


        private async void OnDelete(object obj)
        {
            var data = obj as Post;
            await _httpClient.DeleteAsync(Url + "/" + data.id);
            FetchApiData();
        }

        private async void OnUpdate(object obj)
        {
            var update_post = new Post() { id = SelectedPostId, userId = entryUserId, title = entryTitle, body = entryDescription };
            var new_content = JsonConvert.SerializeObject(update_post);
            await _httpClient.PutAsync(Url + "/" + update_post.id, new StringContent(new_content));
            #region ResetFieldsToDefault

            entryTitle = string.Empty;
            entryDescription = string.Empty;
            entryTitle = null;
            #endregion
            FetchApiData();
        }
       
        private async void FetchApiData()
        {
            try
            {
                var content = await _httpClient.GetStringAsync(Url);
                var posts = JsonConvert.DeserializeObject<List<Post>>(content);
                PostsList = posts;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


    }
}
