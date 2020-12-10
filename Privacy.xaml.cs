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
    /// Interaction logic for Privacy.xaml
    /// </summary>
    public partial class Privacy : Window
    {
        public Privacy()
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

        private void distributionPolicy_Click(object sender, RoutedEventArgs e)
        {
            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            Bold MainHeading = new Bold(new Run("Distribution Policy"));
            Run MainContent = new Run("This is the distribution policy of the software. ");
            

            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(MainHeading);
            myParagraph.Inlines.Add(MainContent);
            

            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richPrivacy.Document = myFlowDoc;
        }

        private void copyrightPolicy_Click(object sender, RoutedEventArgs e)
        {
            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            Bold MainHeading = new Bold(new Run("Copywrite Policy"));
            Run MainContent = new Run("This is the copywrite policy of the software. ");


            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(MainHeading);
            myParagraph.Inlines.Add(MainContent);

            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richPrivacy.Document = myFlowDoc;
        }

        private void softwarePolicy_Click(object sender, RoutedEventArgs e)
        {
            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            Bold MainHeading = new Bold(new Run("Software Policy"));
            Run MainContent = new Run("The information contained through this app is published in good faith and we believes that the provided information is accurate as at the date of publication but no representation or warranty of accuracy, express or implied, is given of any kind and no obligation or responsibility in respect of any errors, omissions, interruptions or delays in service which may occur is accepted by us. We shall have no liability for any loss or damage arising out of the use or reliance on the information provided including without limitation, any loss of profit or any other damage, direct or consequential. No information on this app constitutes investment, tax, legal or any other advice. ");


            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(MainHeading);
            myParagraph.Inlines.Add(MainContent);

            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richPrivacy.Document = myFlowDoc;
        }

        private void vendorPolicy_Click(object sender, RoutedEventArgs e)
        {
            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Create a Run of plain text and some bold text.
            Bold MainHeading = new Bold(new Run("Vendor Policy"));
            Run MainContent = new Run("This is the vendor policy of the software. ");


            // Create a paragraph and add the Run and Bold to it.
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(MainHeading);
            myParagraph.Inlines.Add(MainContent);

            // Add the paragraph to the FlowDocument.
            myFlowDoc.Blocks.Add(myParagraph);

            RichTextBox myRichTextBox = new RichTextBox();

            // Add initial content to the RichTextBox.
            richPrivacy.Document = myFlowDoc;
        }
    }
}
