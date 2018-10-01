using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using VkSearchWPF.Config;
using VkSearchWPF.Models;
using VkSearchWPF.Vk;

namespace VkSearchWPF.ViewModels
{
    public class SearchPageViewModel: BaseViewModel
    {
        const string CONFIG_FILE = "searchconfig.json";

        #region Fields
        private string query = string.Empty;
        private int count = 0;
        private ObservableCollection<UserModel> results = new ObservableCollection<UserModel>();
        #endregion
        #region Properties
        public string Query {
            get => query;
            set
            {
                SetProperty(ref query, value);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }
        public int Count { get => count; set => SetProperty(ref count, value); }
        public ObservableCollection<UserModel> Results { get => results; set => SetProperty(ref results, value); }
        #endregion
        #region Commands
        public AsyncCommand SearchCommand { get; }
        #endregion

        public SearchPageViewModel()
        {
            SearchCommand = new AsyncCommand(Search, (obj) => Query.Length > 0);
        }

        private SearchConfig GetSearchConfig()
        {
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), CONFIG_FILE);
            if (!File.Exists(configPath))
            {
                MessageBox.Show($"{CONFIG_FILE} not found", "Config Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                File.WriteAllText(configPath, JsonConvert.SerializeObject(new SearchConfig()));
            }
            return JsonConvert.DeserializeObject<SearchConfig>(File.ReadAllText(configPath));
        }

        private async Task Search()
        {
            var config = GetSearchConfig();
            var searchParams = new Dictionary<string, string>
            {
                ["access_token"] = config.AccessToken,
                ["lang"] = config.Language,
                ["fields"] = "photo_max",
                ["count"] = "1000",
                ["q"] = Query
            };
            try
            {
                var result = await VkAccess.CallMethod("users.search", searchParams);
                if (result.Success)
                {
                    Results.Clear();
                    var items = result.Response["items"].ToObject<List<UserModel>>();
                    Results = new ObservableCollection<UserModel>(items);
                    Count = result.Response["count"].ToObject<int>();
                } else if (result.Error != null)
                {
                    MessageBox.Show(result.Error.ErrorMsg, "VK API Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
