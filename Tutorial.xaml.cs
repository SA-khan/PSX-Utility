using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Window
    {
        public Tutorial()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow cls = new MainWindow();
            cls.Show();
            this.Hide();
        }

        private void btnPrerequisite_Click(object sender, RoutedEventArgs e)
        {
            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            Bold MainHeading = new Bold(new Run("Prerequisite Guide"));
            LineBreak linBreak1 = new LineBreak();
            LineBreak linBreak2 = new LineBreak();
            LineBreak linBreak3 = new LineBreak();
            LineBreak linBreak4 = new LineBreak();
            LineBreak linBreak5 = new LineBreak();
            LineBreak linBreak6 = new LineBreak();
            LineBreak linBreak7 = new LineBreak();
            LineBreak linBreak8 = new LineBreak();
            Run MainContent = new Run("=> You will require to install .NET Core Runtime 3.1.7 (windowsdesktop-runtime-3.1.7-win-x64) from https://dotnet.microsoft.com/download/dotnet-core/3.1");
            Run MainContent2 = new Run("=> You will require to setup MS SQL Server Database and Run Scripts Name file from the DBScripts folder. OR Run all the scripts placed in DBScript that are starting with number #.");

            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(MainHeading);
            myParagraph.Inlines.Add(linBreak1);
            myParagraph.Inlines.Add(linBreak2);
            myParagraph.Inlines.Add(linBreak3);
            myParagraph.Inlines.Add(MainContent);
            myParagraph.Inlines.Add(linBreak6);
            myParagraph.Inlines.Add(linBreak7);
            myParagraph.Inlines.Add(MainContent2);

            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richPrivacy.Document = myFlowDoc;
        }

        private void btnInstallation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnConfiguration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUseCases_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
