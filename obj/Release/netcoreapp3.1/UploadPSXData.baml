   M S B A M L   `   `   `  ���� M  IPSXDataFetchingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PSXDataFetchingAppPSXDataFetchingApp  &     PSXDataFetchingApp.UploadPSXData   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  ,local clr-namespace:PSXDataFetchingApp    � Title$  Data Extractor Utility�� � Icon$ Images/web-scrap2.png��$	��550��$	��920�� � WindowStartupLocation$ CenterScreen=�.���� $��10q�
�������._��  � RowDefinitions �� $��2* ��� $��2* ��� $	��14* ��� $��* � � ColumnDefinitions �� $��* �.+�� -    �Name$ HeaderColor��$��0�����#f0a500 �� $��* �.+��� -    ��Name$ HeaderImage��$��Images/BOP.png��$
��Left=�$��5q�"  WpfAnimatedGif.ImageBehavior   AnimatedSource$ Images/bop.gif��$
��Fill=�$	��200���� $��0��$$��Market Summary Closing Details��$��26(�$��/Fonts/#Lato)�$
��Bold"�$��Center=� �� TextAlignment$ Center=�$��Center=�$��0 0 10 0q�� $��0��$��Right=�$��5q� �� $��* ��� $��* �.+��� -   	 ��Name$	 txtDate��$��0��$��/Fonts/#Lato)�$��Bottom=�$��0 0 2 0q��� -   $	 	txtStatus��$��1��$��/Fonts/#Lato)�$	��Top=�$��0 2 0 0q�� $��1��$��1 5 1 5q����#fff$��35�� �� $��* �$��75���� $��3* �$	��260���� $��1* �$��60���� $��1* �$��60���� $��1* �$��60���� $��* ��� $��3* �$	��280��.+��� -   
 ��Name$
 btnBack��$��0��$��/Fonts/#Raleway)�$��70��$��25��$��Center=�$
��Left=�$��2 0 0 0q� � ToolTip$ Go To Main Menu������ .+��  �� $��* ��� $��2* �.+��� $��0��$��Images/back.png��$��Uniform=�$��15���� $��1��$�� Back ��$��Center=�$��13(�$��50��(  "System.Windows.Controls.DatePicker  -   $��datepsxpicker��$��1��$��/Fonts/#Raleway)�$	��250��$	��250��$
��Left=�$��Bottom=�$��Center=�$��10 0 0 0q�$��5q�$��25��$3 -Select Date To Fetch Closing Market Summary..��$��13(��� -   $
 btnGet��$��2��$��/Fonts/#Lato-Bold)�$��13(�$��50��$��50��$��36��$��Center=�$��Center=�
������ 
������ $��5q�$  Get Closing Market Summary������ $��Center=�$��Center=�.+��� $��Images/play.png��$��Uniform=��� -   $
 btnSave��$��3��$��5q�$��/Fonts/#Lato-Bold)�$��13(�$��40��$��40��$��40��$��Center=�$��Center=�
�������
������ $ Generate Report������ $��Center=�$��Center=�.+��� $��Images/excel.jpg��$
��Fill=��� -	   $
 btnReset��$��4��$��5q�$��/Fonts/#Lato-Bold)�$��13(�$��40��$��40��$��30��$
��Left=�$��Center=�
�������
������ ���� $��Center=�$��Center=�.+��� $��Images/reset.png��$��1q�$
��Fill=�$��25��$ Refresh Information��� $��6��$
��Left=�.+��� -
    ��Name$ SearchTermTextBox��$	��205��$��25��$��13(�$��Center=�$
��Left=�$��5q��� -   $	 	txtSearch�� J� IsHitTestVisible$ False��$��Enter Search Term Here��$��Center=�$
��Left=�$��10,0,0,0q�
�������$��13(�$��/Fonts/#Raleway)�����  �� 
TargetType" ��.���  �� Property � y� �� Value$ 	Collapsed=� �� Triggers ��  �� Binding �� *Text+ �� ElementName$ SearchTermTextBox�� �� Value  .a���  � y�$ Visible=��� -   $
 	btnSearch��$��6��$��5q�$��/Fonts/#Lato-Bold)�$��13(�$��60��$��40��$��30��$��Right=�$��Center=�
�������
������ ���� $��Center=�$��Center=�.+��� $��Images/search.png��$��1q�$
��Fill=�$��25��$ Refresh Information���� -    ��Name$ loadingImage��$y�Hidden=�$��2��
�������._��� $��Center=�$��Center=�$��Uniform=�$ Images/loading.gif��$��Images/Exclaimation.png���� -   $ 	FundImage��$��2��$��Images/uploadPSX.jpg��$��Center=�$��Center=�$
��Fill=��� -    ��Name$ list1��$��2��$
��Auto��$y�Hidden=�$��/Fonts/#Lato)�$
��Left=� � 	Resources    	txtAppDep��%^   '��       &	  %     �� " ��.���  � ��$
 Left=��� " ��.���  � ��
 �  �� �� View �� -   gridView�� �� AllowsColumnReorder$
 true�� �� ColumnHeaderToolTip$ Market Summary��.P��� -   col1�� �� Width$ 55������ ���#455A64���#fff$��16(�$��Sr. No.��$��2q� �� CellTemplate �� .R��� $
��Left=�$
 Left=�$��60������ *SERIAL+�� -   col2��$	 160������ ���#455A64���#fff$��16(�$
��NAME��$��2q� �� .R��� $
��Left=�$
 Left=����� *NAME+�� -   col3��$ 80������ ���#455A64���#fff$��16(�$��SYMBOL��$��2q� �� .R��� $
��Left=�$
 Left=����� *SYMBOL+�� -   col4��$ 70������ ���#455A64���#fff$��16(�$
��LDCP��$��2q� 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Right=�$��70��.+��� $��Right=�$ Right=����� *LDCP+�� -   col5��$ 70������ ���#455A64���#fff$��16(�$
��OPEN��$��2q�   .R�� $��Right=�$��95��.+��� $��Right=����� *OPEN+�� -   col6��$ 70������ ���#455A64���#fff$��16(�$
��HIGH��$��2q�   .R�� $��Right=�$��95��.+��� $��Right=����� *HIGH+�� -   col7��$ 70������ ���#455A64���#fff$��16(�$	��LOW��$��2q�   .R�� $��Right=�$��95��.+��� $��Right=����� *LOW+�� -   col9��$	 170������ ���#455A64���#fff$��16(�$��CLOSING��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *CHANGE+�� -   	col10��$	 105������ ���#455A64���#fff$��16(�$��VOLUME��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *VOLUME+� $��3��
�������$��22�� �� $��* ��� $��6* ��� $��* �.+��� -   $��	statusBar��$��Bottom=�$��0��$��Stretch=�$��Center=�
������ 
���   �.@��� .���� -   $��	lblStatus��$��Status: Ready��$��/Fonts/#Lato)�$��11(�
������ �� .��� -    �Name$ progressBar��� $��1��$��Center=�$��Center=� �� $��* ��� $��2* �.+��� $��14(�$��Center=�$��Data Reference:��$��Fonts/#Lato)�
���   �$
��Bold"��� -   $
 
btnPsxLink��
������ $��0q�$��1��$��Center=�$��13(�$(��" https://dps.psx.com.pk/downloads ��$��Fonts/#Raleway-Black)�
����  �$��DemiBold"�$��0 3 0 0q��� -   $��
statusBar2��$��Bottom=�$��2��$��Right=�$��Bottom=�
������ 
���   �.@��� $��Right=�$��Bottom=�$��60��
������ .���� 
������ ._��� -   $ 	imgStatus��$ Images/tick.gif��$��Images/tick.png��$��60��$��60��