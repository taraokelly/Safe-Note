using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SafeNote
{
    public sealed partial class Notes : Page
    {
        #region Variables

        ObservableCollection<Note> dataList = new ObservableCollection<Note>();
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        StorageFile notes;
        string fileName = "notes.txt";
        Note n = new Note();
        bool titleValid = false, bodyValid = false;

        #endregion

        #region Contstructor

        public Notes()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Gerneral Functions

        public async void saveNote(string title, string body)
        {
            notes = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            title = title.Replace("\n", String.Empty);
            body = body.Replace("\n", String.Empty);

            n = new Note() { title = title, body = body };

            dataList.Insert(0, n);

            await FileIO.AppendTextAsync(notes, title + Environment.NewLine + body + Environment.NewLine);
        }

        public async void loadNotes()
        {
            notes = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            var info = await FileIO.ReadLinesAsync(notes);
            int count = 0;

            foreach (var line in info)
            {
                if (count % 2 == 0)
                {
                    n = new Note();
                    n.title = line;
                }
                else
                {
                    n.body = line;
                    dataList.Insert(0, n);
                }
                count++;
            }
        }

        #endregion

        #region Naviagtion Events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            listView.ItemsSource = dataList;
            loadNotes();
        }

        #endregion

        #region Click Events

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(APIKey), null);
        }

        private void addNote_Click(object sender, RoutedEventArgs e)
        {
            saveNote(newTitle.Text, newBody.Text);
            newTitle.Text = "";
            newBody.Text = "";
        }

        private async void OnBackKeyPress(object sender, CancelEventArgs e)
        {
            //Code to disable the back button
            e.Cancel = true;
            var dialog = new MessageDialog("Do you wish to exit app?");
            dialog.Title = "SafeNote";

            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
            var result = await dialog.ShowAsync();

            if ((int)result.Id == 0)
            {
                Application.Current.Exit();

            }
        }

        #endregion

        #region Property Changed Events

        private void newTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (newTitle.Text.Trim().Length == 0)
            {
                addNote.IsEnabled = false;
                titleValid = false;
            }
            else if(String.IsNullOrEmpty(newTitle.Text))
            {
                addNote.IsEnabled = false;
                titleValid = false;
            }
            else
            {
                titleValid = true;

                if(bodyValid == true)
                {
                        addNote.IsEnabled = true;
                }
            }
        }

        private void newBody_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (newBody.Text.Trim().Length == 0)
            {
                addNote.IsEnabled = false;
                bodyValid = false;
            }
            else if (String.IsNullOrEmpty(newBody.Text))
            {
                addNote.IsEnabled = false;
                bodyValid = false;
            }
            else
            {
                bodyValid = true;
                if (titleValid == true)
                {
                    addNote.IsEnabled = true;          
                }
            }
        }

        #endregion
    }

    #region Note Object
    class Note
    {
        public string title { get; set; }
        public string body { get; set; }

        public override string ToString()
        {
            return title;
        }
    }
    #endregion
}