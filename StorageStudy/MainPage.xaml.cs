namespace StorageStudy
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Windows.Storage;
    using Windows.Storage.Search;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The Start_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            var txtFile = await KnownFolders.DocumentsLibrary.GetFileAsync("Test.txt"); //これもCドライブしかわからないようだった
            Debug.WriteLine(txtFile.DisplayName);
            Debug.WriteLine(txtFile.Path);

            //https://docs.microsoft.com/ja-jp/windows/uwp/files/quickstart-reading-and-writing-files より
            var buffer = await FileIO.ReadBufferAsync(txtFile);
            using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer))
            {
                string text = dataReader.ReadString(buffer.Length);
                Debug.WriteLine(text);
            }



            //var testFile = await KnownFolders.PicturesLibrary.GetFileAsync("Test.txt");
            QueryOptions queryOption = new QueryOptions
        (CommonFileQuery.OrderByTitle, new string[] { ".mp3", ".mp4", ".wma" }); //見つからなかった。Cドライブ限定のようだ


            queryOption.FolderDepth = FolderDepth.Deep;

            Queue<IStorageFolder> folders = new Queue<IStorageFolder>();

            var files = await KnownFolders.PicturesLibrary.CreateFileQueryWithOptions
              (queryOption).GetFilesAsync();

            foreach (var file in files)
            {
                Debug.WriteLine(file.DisplayName);
            }


        }
    }
}
