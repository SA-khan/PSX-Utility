using PSXDataFetchingApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for Disclaimer.xaml
    /// </summary>
    public partial class Disclaimer : Window
    {
        public DataContext _context;
        public Disclaimer()
        {
            InitializeComponent();

            //Client Specific Properties
            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Client"]))
                {
                    if (ConfigurationManager.AppSettings["Client"].Equals("BOP"))
                    {
                        // Header Background Color 
                        var bc = new BrushConverter();
                        HeaderColor.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#f0a500");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/BOP.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(HeaderImage, image);
                    }
                    else if (ConfigurationManager.AppSettings["Client"].Equals("HBL"))
                    {
                        // Header Background Color
                        var bc = new BrushConverter();
                        HeaderColor.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#008269");

                        //Setting Logo
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = ResourceAccessor.Get("Images/HBL.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(HeaderImage, image);
                    }
                }
            }
            catch { }

            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            Bold MainHeading = new Bold(new Run("General Disclaimer"));
            LineBreak lineBreak1 = new LineBreak();
            LineBreak lineBreak2 = new LineBreak();
            Run MainContent = new Run("This application disclaimer informs readers that the views, thoughts, and opinions expressed in the text belong solely to the author employer and organization, and not necessarily to the committee or other group or individual. ");


            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(MainHeading);
            myParagraph.Inlines.Add(lineBreak1);
            myParagraph.Inlines.Add(lineBreak2);
            myParagraph.Inlines.Add(MainContent);


            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richDisclaimer.Document = myFlowDoc;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow cls = new MainWindow(_context);
            cls.Show();
            this.Hide();
        }
    }
}
