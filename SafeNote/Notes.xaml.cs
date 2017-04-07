using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
        ObservableCollection<Note> dataList = new ObservableCollection<Note>();
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        StorageFile notes;
        string fileName = "notes.txt";

        public Notes()
        {
            this.InitializeComponent();
        }

        public async void saveNotes()
        {
            notes = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            foreach(var item in dataList)
            {
                await FileIO.AppendTextAsync(notes, item.title + Environment.NewLine + item.body + Environment.NewLine);
            }

        }
        public async void saveNote()
        {
            notes = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            foreach (var item in dataList)
            {
                await FileIO.AppendTextAsync(notes, item.title + Environment.NewLine + item.body + Environment.NewLine);
            }

        }
        public async void loadNotes()
        {
            /*Note n1 = new Note() { title = "Facebook", body = "password111" };
            Note n2 = new Note() { title = "Twitter", body = "password77" };
            Note n3 = new Note() { title = "Instagram", body = "password2" };

            dataList.Add(n1);
            dataList.Add(n2);
            dataList.Add(n3);*/

                   notes = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                  var info = await FileIO.ReadLinesAsync(notes);
                   int count = 0;
                   Note n = new Note();

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
                            dataList.Add(n);
                           
                       }
                       count++;
                   }

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            listView.ItemsSource = dataList;
            loadNotes();
            //saveNotes();
        }
    }

    class Note
    {
        public string title { get; set; }
        public string body { get; set; }
    }
}
