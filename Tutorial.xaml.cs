using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

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

            ButtonAutomationPeer peer = new ButtonAutomationPeer(btnIntroduction);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();

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

            btnIntroduction.Background = Brushes.Gray;
            btnSystemRequirement.Background = Brushes.Gray;
            btnPrerequisite.Background = Brushes.DarkGray;
            btnInstallation.Background = Brushes.Gray;
            btnConfiguration.Background = Brushes.Gray;
            btnUseCases.Background = Brushes.Gray;

            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            TextBlock prerequisiteHeading = new TextBlock();
            prerequisiteHeading.Text = "Prerequisite Guide";
            prerequisiteHeading.FontSize = 26;
            prerequisiteHeading.FontFamily = new FontFamily("Arial");
            prerequisiteHeading.FontWeight = FontWeights.Bold;

            //Bold MainHeading = new Bold(new Run("Prerequisite Guide"));
            LineBreak linBreak1 = new LineBreak();
            LineBreak linBreak2 = new LineBreak();
            LineBreak linBreak3 = new LineBreak();
            LineBreak linBreak4 = new LineBreak();
            LineBreak linBreak5 = new LineBreak();
            LineBreak linBreak6 = new LineBreak();
            LineBreak linBreak7 = new LineBreak();
            LineBreak linBreak8 = new LineBreak();
            TextBlock prerequisite1 = new TextBlock();
            prerequisite1.Text = "1. You will require to install .NET Core Runtime 3.1.7";
            prerequisite1.FontSize = 14;
            prerequisite1.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite2 = new TextBlock();
            prerequisite2.Text = "   (windowsdesktop-runtime-3.1.7-win-x64) from below URL,";
            prerequisite2.FontSize = 14;
            prerequisite2.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite3 = new TextBlock();
            prerequisite3.FontSize = 14;
            prerequisite3.FontFamily = new FontFamily("Arial");


            Hyperlink hyperlink = new Hyperlink();
            hyperlink.NavigateUri = new Uri("https://dotnet.microsoft.com/download/dotnet-core/3.1");
            hyperlink.RequestNavigate += Hyperlink_RequestNavigate;
            prerequisite3.Text = "   https://dotnet.microsoft.com/download/dotnet-core/3.1";
            prerequisite3.FontWeight = FontWeights.Bold;
            prerequisite3.FontStyle = FontStyles.Italic;
            prerequisite3.Foreground = Brushes.Blue;
            prerequisite3.Inlines.Add(hyperlink);


            TextBlock prerequisite4 = new TextBlock();
            prerequisite4.Text = "2. You will require to setup MS SQL Server Database and Run the Scripts.txt file from";
            prerequisite4.FontSize = 14;
            prerequisite4.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite5 = new TextBlock();
            prerequisite5.Text = "    the DBScripts folder Or Run all the scripts placed in DBScript that are starting";
            prerequisite5.FontSize = 14;
            prerequisite5.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite6 = new TextBlock();
            prerequisite6.Text = "    with a number.";
            prerequisite6.FontSize = 14;
            prerequisite6.FontFamily = new FontFamily("Arial");

            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(prerequisiteHeading);
            myParagraph.Inlines.Add(linBreak1);
            myParagraph.Inlines.Add(linBreak2);
            myParagraph.Inlines.Add(prerequisite1);
            myParagraph.Inlines.Add(linBreak3);
            myParagraph.Inlines.Add(prerequisite2);
            myParagraph.Inlines.Add(linBreak4);
            myParagraph.Inlines.Add(prerequisite3);
            myParagraph.Inlines.Add(linBreak5);
            myParagraph.Inlines.Add(linBreak6);
            myParagraph.Inlines.Add(prerequisite4);
            myParagraph.Inlines.Add(linBreak7);
            myParagraph.Inlines.Add(prerequisite5);
            myParagraph.Inlines.Add(linBreak8);
            myParagraph.Inlines.Add(prerequisite6);

            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richPrivacy.Document = myFlowDoc;
        }

        private void btnInstallation_Click(object sender, RoutedEventArgs e)
        {
            btnIntroduction.Background = Brushes.Gray;
            btnSystemRequirement.Background = Brushes.Gray;
            btnPrerequisite.Background = Brushes.Gray;
            btnInstallation.Background = Brushes.DarkGray;
            btnConfiguration.Background = Brushes.Gray;
            btnUseCases.Background = Brushes.Gray;

            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            TextBlock prerequisiteHeading = new TextBlock();
            prerequisiteHeading.Text = "Installation Guide";
            prerequisiteHeading.FontSize = 26;
            prerequisiteHeading.FontFamily = new FontFamily("Arial");
            prerequisiteHeading.FontWeight = FontWeights.Bold;

            //Bold MainHeading = new Bold(new Run("Prerequisite Guide"));
            LineBreak linBreak1 = new LineBreak();
            LineBreak linBreak2 = new LineBreak();
            LineBreak linBreak3 = new LineBreak();
            LineBreak linBreak4 = new LineBreak();
            LineBreak linBreak5 = new LineBreak();
            LineBreak linBreak6 = new LineBreak();
            LineBreak linBreak7 = new LineBreak();
            LineBreak linBreak8 = new LineBreak();
            TextBlock prerequisite1 = new TextBlock();
            prerequisite1.Text = "1. Copy the Directory in your desired location,";
            prerequisite1.FontSize = 14;
            prerequisite1.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite2 = new TextBlock();
            prerequisite2.Text = "2. Create the shortcut of PSXDataFetchingApp.exe file on your desktop";
            prerequisite2.FontSize = 14;
            prerequisite2.FontFamily = new FontFamily("Arial");


            TextBlock prerequisite3 = new TextBlock();
            prerequisite3.Text = "3. Launch the executable (.exe) file.";
            prerequisite3.FontSize = 14;
            prerequisite3.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite4 = new TextBlock();
            prerequisite4.Text = "";
            prerequisite4.FontSize = 14;
            prerequisite4.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite5 = new TextBlock();
            prerequisite5.Text = "";
            prerequisite5.FontSize = 14;
            prerequisite5.FontFamily = new FontFamily("Arial");

            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(prerequisiteHeading);
            myParagraph.Inlines.Add(linBreak1);
            myParagraph.Inlines.Add(linBreak2);
            myParagraph.Inlines.Add(prerequisite1);
            myParagraph.Inlines.Add(linBreak3);
            myParagraph.Inlines.Add(linBreak4);
            myParagraph.Inlines.Add(prerequisite2);
            myParagraph.Inlines.Add(linBreak5);
            myParagraph.Inlines.Add(linBreak6);
            myParagraph.Inlines.Add(prerequisite3);
            myParagraph.Inlines.Add(linBreak7);
            myParagraph.Inlines.Add(prerequisite4);
            myParagraph.Inlines.Add(linBreak8);
            myParagraph.Inlines.Add(prerequisite5);
            

            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richPrivacy.Document = myFlowDoc;

        }

        private void btnConfiguration_Click(object sender, RoutedEventArgs e)
        {
            btnIntroduction.Background = Brushes.Gray;
            btnSystemRequirement.Background = Brushes.Gray;
            btnPrerequisite.Background = Brushes.Gray;
            btnInstallation.Background = Brushes.Gray;
            btnConfiguration.Background = Brushes.DarkGray;
            btnUseCases.Background = Brushes.Gray;

            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            TextBlock prerequisiteHeading = new TextBlock();
            prerequisiteHeading.Text = "Configuration Guide";
            prerequisiteHeading.FontSize = 26;
            prerequisiteHeading.FontFamily = new FontFamily("Arial");
            prerequisiteHeading.FontWeight = FontWeights.Bold;

            //Bold MainHeading = new Bold(new Run("Prerequisite Guide"));
            LineBreak linBreak1 = new LineBreak();
            LineBreak linBreak2 = new LineBreak();
            LineBreak linBreak3 = new LineBreak();
            LineBreak linBreak4 = new LineBreak();
            LineBreak linBreak5 = new LineBreak();
            LineBreak linBreak6 = new LineBreak();
            LineBreak linBreak7 = new LineBreak();
            LineBreak linBreak8 = new LineBreak();
            LineBreak linBreak9 = new LineBreak();
            LineBreak linBreak10 = new LineBreak();
            LineBreak linBreak11 = new LineBreak();
            LineBreak linBreak12 = new LineBreak();
            LineBreak linBreak13 = new LineBreak();
            LineBreak linBreak14 = new LineBreak();
            LineBreak linBreak15 = new LineBreak();
            LineBreak linBreak16 = new LineBreak();
            LineBreak linBreak17 = new LineBreak();
            LineBreak linBreak18 = new LineBreak();

            TextBlock prerequisite1 = new TextBlock();
            prerequisite1.Text = "1. Make sure that database is setup and all scripts of the database have";
            prerequisite1.FontSize = 14;
            prerequisite1.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite2 = new TextBlock();
            prerequisite2.Text = "    been executed without any error as explained in prerequisite guide.";
            prerequisite2.FontSize = 14;
            prerequisite2.FontFamily = new FontFamily("Arial");


            TextBlock prerequisite3 = new TextBlock();
            prerequisite3.Text = "2. Launch the executable (.exe) file.";
            prerequisite3.FontSize = 14;
            prerequisite3.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite4 = new TextBlock();
            prerequisite4.Text = "3. You will see the Configuration Manager Button on top of application as shown below,";
            prerequisite4.FontSize = 14;
            prerequisite4.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite5 = new TextBlock();
            prerequisite5.Text = "    ";
            prerequisite5.FontSize = 14;
            prerequisite5.FontFamily = new FontFamily("Arial");

            Image imageConfigurationManager = new Image();
            imageConfigurationManager.Name = "imageConfigurationManager";
            BitmapImage image = new BitmapImage();
            
            image.BeginInit();
            image.UriSource = ResourceAccessor.Get("Images/Tutorial/ConfigurationManagerIconView.png");
            image.SourceRect = new Int32Rect(232, 40, 600, 80);
            //image.SourceRect = new Int32Rect(720, 40, 80, 20);
            //image.DecodePixelHeight = 10;
            //image.DecodePixelWidth = 30;
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imageConfigurationManager, image);
            prerequisite5.Inlines.Add(imageConfigurationManager);
            prerequisite5.Width = 400;
            prerequisite5.Height = 450;

            


            TextBlock prerequisite6 = new TextBlock();
            prerequisite6.Text = "4. Enter Your Organisation Symbol as shown in below,";
            prerequisite6.FontSize = 14;
            prerequisite6.FontFamily = new FontFamily("Arial");

            //
            TextBlock prerequisite7 = new TextBlock();
            prerequisite7.Text = "    ";
            prerequisite7.FontSize = 14;
            prerequisite7.FontFamily = new FontFamily("Arial");

            Image imageConfigurationManagerScreen = new Image();
            imageConfigurationManagerScreen.Name = "imageConfigurationManagerScreen";
            BitmapImage image2 = new BitmapImage();

            image2.BeginInit();
            image2.UriSource = ResourceAccessor.Get("Images/Tutorial/ConfigurationManagerIconView.png");
            image2.SourceRect = new Int32Rect(0, 0, 600, 80);
            //image.SourceRect = new Int32Rect(720, 40, 80, 20);
            //image.DecodePixelHeight = 10;
            //image.DecodePixelWidth = 30;
            image.EndInit();
            ImageBehavior.SetAnimatedSource(imageConfigurationManagerScreen, image2);
            prerequisite7.Inlines.Add(imageConfigurationManagerScreen);
            prerequisite7.Width = 400;
            prerequisite7.Height = 450;

            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(prerequisiteHeading);
            myParagraph.Inlines.Add(linBreak1);
            myParagraph.Inlines.Add(linBreak2);
            myParagraph.Inlines.Add(prerequisite1);
            myParagraph.Inlines.Add(linBreak3);
            myParagraph.Inlines.Add(linBreak4);
            myParagraph.Inlines.Add(prerequisite2);
            myParagraph.Inlines.Add(linBreak5);
            myParagraph.Inlines.Add(linBreak6);
            myParagraph.Inlines.Add(prerequisite3);
            myParagraph.Inlines.Add(linBreak7);
            myParagraph.Inlines.Add(linBreak8);
            myParagraph.Inlines.Add(prerequisite4);
            myParagraph.Inlines.Add(linBreak9);
            myParagraph.Inlines.Add(prerequisite5);
            myParagraph.Inlines.Add(linBreak10);
            myParagraph.Inlines.Add(linBreak11);
            myParagraph.Inlines.Add(prerequisite6);
            myParagraph.Inlines.Add(linBreak12);
            myParagraph.Inlines.Add(prerequisite7);



            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            ///FlowDocumentScrollViewer _fDSV = new FlowDocumentScrollViewer();
            //_fDSV.
            //FlowDocumentReader _fdr = new FlowDocumentReader();
            //_fdr.Document = myFlowDoc;

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richPrivacy.Document = myFlowDoc;
            //richPrivacy.Document = _fdr;


        }

        private void btnUseCases_Click(object sender, RoutedEventArgs e)
        {
            btnIntroduction.Background = Brushes.Gray;
            btnSystemRequirement.Background = Brushes.Gray;
            btnPrerequisite.Background = Brushes.Gray;
            btnInstallation.Background = Brushes.Gray;
            btnConfiguration.Background = Brushes.Gray;
            btnUseCases.Background = Brushes.DarkGray;

            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            TextBlock prerequisiteHeading = new TextBlock();
            prerequisiteHeading.Text = "Use Case Guide";
            prerequisiteHeading.FontSize = 26;
            prerequisiteHeading.FontFamily = new FontFamily("Arial");
            prerequisiteHeading.FontWeight = FontWeights.Bold;

            //Bold MainHeading = new Bold(new Run("Prerequisite Guide"));
            LineBreak linBreak1 = new LineBreak();
            LineBreak linBreak2 = new LineBreak();
            LineBreak linBreak3 = new LineBreak();
            LineBreak linBreak4 = new LineBreak();
            LineBreak linBreak5 = new LineBreak();
            LineBreak linBreak6 = new LineBreak();
            LineBreak linBreak7 = new LineBreak();
            LineBreak linBreak8 = new LineBreak();
            LineBreak linBreak9 = new LineBreak();
            LineBreak linBreak10 = new LineBreak();
            LineBreak linBreak11 = new LineBreak();
            LineBreak linBreak12 = new LineBreak();
            LineBreak linBreak13 = new LineBreak();
            LineBreak linBreak14 = new LineBreak();
            LineBreak linBreak15 = new LineBreak();
            LineBreak linBreak16 = new LineBreak();

            TextBlock prerequisite1 = new TextBlock();
            prerequisite1.Text = "PSX Market Summary:";
            prerequisite1.FontSize = 14;
            prerequisite1.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite2 = new TextBlock();
            prerequisite2.Text = " Click on this option will get the updated data from Pakistan Stock Exchange Market Summary with 5 minutes time interval difference.";
            prerequisite2.FontSize = 14;
            prerequisite2.FontWeight = FontWeights.Bold;
            prerequisite2.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite3 = new TextBlock();
            prerequisite3.Text = "Fund Market Summary:";
            prerequisite3.FontSize = 14;
            prerequisite3.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite4 = new TextBlock();
            prerequisite4.Text = " Click on this option will get the scrips that have investments on the specific fund, the fund and share details are get from ipams database and the current market price get from the Pakistan Stock Exchange Website.";
            prerequisite4.FontSize = 14;
            prerequisite4.FontWeight = FontWeights.Bold;
            prerequisite4.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite5 = new TextBlock();
            prerequisite5.Text = "PSX File Upload :";
            prerequisite5.FontSize = 14;
            prerequisite5.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite6 = new TextBlock();
            prerequisite6.Text = " Click on this option will get closing rates of the file whose data is selected from Date Picker.";
            prerequisite6.FontSize = 14;
            prerequisite6.FontWeight = FontWeights.Bold;
            prerequisite6.FontFamily = new FontFamily("Arial");


            TextBlock prerequisite7 = new TextBlock();
            prerequisite7.Text = "MUFAP Market Summary:";
            prerequisite7.FontSize = 14;
            prerequisite7.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite8 = new TextBlock();
            prerequisite8.Text = " Click on this option will crawl data from MUFAP market summary page and show to the end user.  ";
            prerequisite8.FontSize = 14;
            prerequisite8.FontWeight = FontWeights.Bold;
            prerequisite8.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite9 = new TextBlock();
            prerequisite9.Text = "MUFAP PKRV & PKFRV Option:";
            prerequisite9.FontSize = 14;
            prerequisite9.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite10 = new TextBlock();
            prerequisite10.Text = " Click on these options will download PKRV and PKFRV file from MUFAP website, extract data and show to the end users.";
            prerequisite10.FontSize = 14;
            prerequisite10.FontWeight = FontWeights.Bold;
            prerequisite10.FontFamily = new FontFamily("Arial");

            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(prerequisiteHeading);
            myParagraph.Inlines.Add(linBreak1);
            myParagraph.Inlines.Add(linBreak2);
            myParagraph.Inlines.Add(prerequisite1);
            myParagraph.Inlines.Add(prerequisite2);
            myParagraph.Inlines.Add(linBreak3);
            myParagraph.Inlines.Add(linBreak4);
            myParagraph.Inlines.Add(prerequisite3);
            myParagraph.Inlines.Add(prerequisite4);
            myParagraph.Inlines.Add(linBreak5);
            myParagraph.Inlines.Add(linBreak6);
            myParagraph.Inlines.Add(prerequisite5);
            myParagraph.Inlines.Add(prerequisite6);
            myParagraph.Inlines.Add(linBreak7);
            myParagraph.Inlines.Add(linBreak8);
            myParagraph.Inlines.Add(prerequisite7);
            myParagraph.Inlines.Add(prerequisite8);
            myParagraph.Inlines.Add(linBreak9);
            myParagraph.Inlines.Add(linBreak10);
            myParagraph.Inlines.Add(prerequisite9);
            myParagraph.Inlines.Add(prerequisite10);


            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richPrivacy.Document = myFlowDoc;

        }

        private void btnSystemRequirement_Click(object sender, RoutedEventArgs e)
        {
            btnIntroduction.Background = Brushes.Gray;
            btnSystemRequirement.Background = Brushes.DarkGray;
            btnPrerequisite.Background = Brushes.Gray;
            btnInstallation.Background = Brushes.Gray;
            btnConfiguration.Background = Brushes.Gray;
            btnUseCases.Background = Brushes.Gray;

            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            TextBlock prerequisiteHeading = new TextBlock();
            prerequisiteHeading.Text = "System Requirement Guide";
            prerequisiteHeading.FontSize = 26;
            prerequisiteHeading.FontFamily = new FontFamily("Arial");
            prerequisiteHeading.FontWeight = FontWeights.Bold;

            //Bold MainHeading = new Bold(new Run("Prerequisite Guide"));
            LineBreak linBreak1 = new LineBreak();
            LineBreak linBreak2 = new LineBreak();
            LineBreak linBreak3 = new LineBreak();
            LineBreak linBreak4 = new LineBreak();
            LineBreak linBreak5 = new LineBreak();
            LineBreak linBreak6 = new LineBreak();
            LineBreak linBreak7 = new LineBreak();
            LineBreak linBreak8 = new LineBreak();
            LineBreak linBreak9 = new LineBreak();
            LineBreak linBreak10 = new LineBreak();
            LineBreak linBreak11 = new LineBreak();
            LineBreak linBreak12 = new LineBreak();
            LineBreak linBreak13 = new LineBreak();
            LineBreak linBreak14 = new LineBreak();
            LineBreak linBreak15 = new LineBreak();
            LineBreak linBreak16 = new LineBreak();

            TextBlock prerequisite1 = new TextBlock();
            prerequisite1.Text = "Supported Operating System:";
            prerequisite1.FontSize = 14;
            prerequisite1.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite2 = new TextBlock();
            prerequisite2.Text = " Windows / Macintosh / Linux";
            prerequisite2.FontSize = 14;
            prerequisite2.FontWeight = FontWeights.Bold;
            prerequisite2.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite3 = new TextBlock();
            prerequisite3.Text = "Processor:";
            prerequisite3.FontSize = 14;
            prerequisite3.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite4 = new TextBlock();
            prerequisite4.Text = " x86 or x64";
            prerequisite4.FontSize = 14;
            prerequisite4.FontWeight = FontWeights.Bold;
            prerequisite4.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite5 = new TextBlock();
            prerequisite5.Text = "RAM :";
            prerequisite5.FontSize = 14;
            prerequisite5.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite6 = new TextBlock();
            prerequisite6.Text = " 512 MB (minimum), 1 GB (recommended)";
            prerequisite6.FontSize = 14;
            prerequisite6.FontWeight = FontWeights.Bold;
            prerequisite6.FontFamily = new FontFamily("Arial");


            TextBlock prerequisite7 = new TextBlock();
            prerequisite7.Text = "Hard Disk:";
            prerequisite7.FontSize = 14;
            prerequisite7.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite8 = new TextBlock();
            prerequisite8.Text = " Up to 2 GB of available space may be required. However, 300 MB  ";
            prerequisite8.FontSize = 14;
            prerequisite8.FontWeight = FontWeights.Bold;
            prerequisite8.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite9 = new TextBlock();
            prerequisite9.Text = "Hard Disk:";
            prerequisite9.Foreground = Brushes.White;
            prerequisite9.FontSize = 14;
            prerequisite9.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite10 = new TextBlock();
            prerequisite10.Text = " free space is required in boot drive even if you are installing in other drive.";
            prerequisite10.FontSize = 14;
            prerequisite10.FontWeight = FontWeights.Bold;
            prerequisite10.FontFamily = new FontFamily("Arial");

            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(prerequisiteHeading);
            myParagraph.Inlines.Add(linBreak1);
            myParagraph.Inlines.Add(linBreak2);
            myParagraph.Inlines.Add(prerequisite1);
            myParagraph.Inlines.Add(prerequisite2);
            myParagraph.Inlines.Add(linBreak3);
            myParagraph.Inlines.Add(linBreak4);
            myParagraph.Inlines.Add(prerequisite3);
            myParagraph.Inlines.Add(prerequisite4);
            myParagraph.Inlines.Add(linBreak5);
            myParagraph.Inlines.Add(linBreak6);
            myParagraph.Inlines.Add(prerequisite5);
            myParagraph.Inlines.Add(prerequisite6);
            myParagraph.Inlines.Add(linBreak7);
            myParagraph.Inlines.Add(linBreak8);
            myParagraph.Inlines.Add(prerequisite7);
            myParagraph.Inlines.Add(prerequisite8);
            myParagraph.Inlines.Add(linBreak9);
            myParagraph.Inlines.Add(linBreak10);
            myParagraph.Inlines.Add(prerequisite9);
            myParagraph.Inlines.Add(prerequisite10);


            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richPrivacy.Document = myFlowDoc;

        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // for .NET Core you need to add UseShellExecute = true
            // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
            Process p = Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void btnIntroduction_Click(object sender, RoutedEventArgs e)
        {
            btnIntroduction.Background = Brushes.DarkGray;
            btnSystemRequirement.Background = Brushes.Gray;
            btnPrerequisite.Background = Brushes.Gray;
            btnInstallation.Background = Brushes.Gray;
            btnConfiguration.Background = Brushes.Gray;
            btnUseCases.Background = Brushes.Gray;
            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            TextBlock prerequisiteHeading = new TextBlock();
            prerequisiteHeading.Text = "Data Extractor Utility Getting Started Guide";
            prerequisiteHeading.FontSize = 26;
            prerequisiteHeading.FontFamily = new FontFamily("Arial");
            prerequisiteHeading.FontWeight = FontWeights.Bold;

            //Bold MainHeading = new Bold(new Run("Prerequisite Guide"));
            LineBreak linBreak1 = new LineBreak();
            LineBreak linBreak2 = new LineBreak();
            LineBreak linBreak3 = new LineBreak();
            LineBreak linBreak4 = new LineBreak();
            LineBreak linBreak5 = new LineBreak();
            LineBreak linBreak6 = new LineBreak();
            LineBreak linBreak7 = new LineBreak();
            LineBreak linBreak8 = new LineBreak();
            TextBlock prerequisite1 = new TextBlock();
            prerequisite1.Text = "Data Extractor Utility is an application that crawls Pakistan Stock Exchange (PSX) and";
            prerequisite1.FontSize = 14;
            prerequisite1.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite2 = new TextBlock();
            prerequisite2.Text = "Mutual Funds Association of Pakistan (MUFAP) Websites data, filter it";
            prerequisite2.FontSize = 14;
            prerequisite2.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite3 = new TextBlock();
            prerequisite3.Text = "and use this data for further use. We believe this utility will help to";
            prerequisite3.FontSize = 14;
            prerequisite3.FontFamily = new FontFamily("Arial");


            TextBlock prerequisite4 = new TextBlock();
            prerequisite4.Text = "view and understand it in a more meaningful manner. If you would like to";
            prerequisite4.FontSize = 14;
            prerequisite4.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite5 = new TextBlock();
            prerequisite5.Text = "make a contribution of this utility feel free to add your efforts and do";
            prerequisite5.FontSize = 14;
            prerequisite5.FontFamily = new FontFamily("Arial");

            TextBlock prerequisite6 = new TextBlock();
            prerequisite6.Text = "the pull request.";
            prerequisite6.FontSize = 14;
            prerequisite6.FontFamily = new FontFamily("Arial");

            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(prerequisiteHeading);
            myParagraph.Inlines.Add(linBreak1);
            myParagraph.Inlines.Add(linBreak2);
            myParagraph.Inlines.Add(prerequisite1);
            myParagraph.Inlines.Add(linBreak3);
            myParagraph.Inlines.Add(prerequisite2);
            myParagraph.Inlines.Add(linBreak4);
            myParagraph.Inlines.Add(prerequisite3);
            myParagraph.Inlines.Add(linBreak5);
            myParagraph.Inlines.Add(prerequisite4);
            myParagraph.Inlines.Add(linBreak6);
            myParagraph.Inlines.Add(prerequisite5);
            myParagraph.Inlines.Add(linBreak7);
            myParagraph.Inlines.Add(prerequisite6);

            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richPrivacy.Document = myFlowDoc;
        }
    }
}
