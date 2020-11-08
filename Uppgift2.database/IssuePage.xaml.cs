using DataAccess.Data;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Uppgift2.database
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IssuePage : Page
    {
        private IEnumerable<Issue> issues { get; set; }

        public IssuePage()
        {
            this.InitializeComponent();
            LoadIssuesAsync().GetAwaiter();
        }

        private async Task LoadIssuesAsync()
        {
            issues = await SqliteContext.GetIssues();
            LoadActiveIssues();
            LoadClosedIssues();
        }

        private void LoadActiveIssues()
        {
            lvActiveIssues.ItemsSource = issues
                .Where(i => i.Status != "closed")
                .OrderByDescending(i => i.Created)
                .Take(SettingsContext.GetMaxItemsCount())
                .ToList();
        }

        private void LoadClosedIssues()
        {
            lvClosedIssues.ItemsSource = issues.Where(i => i.Status == "closed").ToList();
        }

        private async void CreateIssue_Click(object sender, RoutedEventArgs e)
        {
            await SqliteContext.CreateIssueAsync(
                new Issue
                {
                    Title = "CAS-" + Guid.NewGuid().ToString(),
                    Description = "Detta är ett ärende",
                    CustomerId = await SqliteContext.GetCustomerIdByName(cmbCustomers.SelectedItem.ToString())
                }
            );

            await LoadIssuesAsync();
        }
    }
}
