   M S B A M L   `   `   `  ���� M  IPSXDataFetchingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PSXDataFetchingAppPSXDataFetchingApp  #    PSXDataFetchingApp.FundBucket   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  ,local clr-namespace:PSXDataFetchingApp    � Title$  Data Extractor Utility��$��/Fonts/#Lato)�$	��650��$
��1107�� � Icon$ /Images/web-scrap2.png�� � WindowStartupLocation$ CenterScreen=�.���� $��10q�
�������._��  � RowDefinitions �� $��2* ��� $��2* ��� $	��14* ��� $��* � � ColumnDefinitions �� $��* �.+�� -    �Name$ HeaderColor��$��0�����#f0a500 �� $��* �.+��� -    ��Name$ HeaderImage��$��Images/BOP.png��$
��Left=�$��5q�"  WpfAnimatedGif.ImageBehavior   AnimatedSource$ Images/bop.gif��$
��Fill=�$	��200���� $��0��$��Fund Bucket��$��26(�$��/Fonts/#Lato)�$
��Bold"�$��Center=� �� TextAlignment$ Center=�$��Center=�$��0 0 10 0q�� $��0��$��Right=�$��5q� �� $��* ��� $��* �.+��� -   	 ��Name$	 txtDate��$��0��$��/Fonts/#Lato)�$��Bottom=�$��0 0 2 0q��� -   $	 	txtStatus��$��1��$��/Fonts/#Lato)�$	��Top=�$��0 2 0 0q�� $��1��$��1 5 1 5q����#fff$��30�� �� $��* ��� $��4* ��� $��2* �$��60���� $��2* �$��60���� $��2* �$��60���� $��* ��� $��2* ��� $��* ��� $��2* ��� $��2* �.+��� -   
 ��Name$
 btnBack��$��0��$��/Fonts/#Lato)�$��20��$��Center=�$��40��$��Center=�$��0 0 0 0q� � ToolTip$ Go To Main Menu������ .+��� $��Images/back.png��$��Uniform=�$��15���� -   $��	comboFund��$y�Hidden=�$��1��$��Tahoma)�$	��150��$��Stretch=�$��5q�$��20��$% Select Fund To Fetch Its Shares���� -   $
 btnGet��$y�Hidden=�$��2��$��/Fonts/#Lato-Bold)�$��13(�$��50��$��50��$��30��$��Right=�$��Center=�
������ 
������ $��5q�$ Get Shares of Fund������ $��Center=�$��Center=�.+��� $��Images/play.png��$
��Fill=��� -   $
 btnSave��$y�Hidden=�$��3��$��5q�$��/Fonts/#Lato-Bold)�$��13(�$��30��$��30��$��30��$��Center=�$��Center=�
�������
������ $ Generate Report������ $��Center=�$��Center=�.+��� $��Images/report.png��$��Uniform=��� -	   $
 btnReset��$y�Hidden=�$��4��$��5q�$��/Fonts/#Lato-Bold)�$��13(�$��40��$��40��$��30��$
��Left=�$��Center=�
�������
������ ���� $��Center=�$��Center=�.+��� $��Images/reset.png��$��1q�$
��Fill=�$��25��$ Refresh Information���� -
   $	 	lblFundID��$��5��$y�Hidden=�$��FUND ID:��$
��Bold"�
�������$��/Fonts/#Lato)�$��Center=�$��Right=�$��5q��� -   $��	txtFundID��$��6��$y�Hidden=�${�False��$��/Fonts/#Lato)�$	��120��$��Stretch=�$��5q�$��Center=��� -   $	 lblFundNAME��$��7��$��
FUND NAME:��$y�Hidden=�$
��Bold"�
�������$��/Fonts/#Lato)�$��Center=�$��Right=�$��5q��� -   $��txtFund��$��8��$y�Hidden=�${�False��$��/Fonts/#Lato)�$	��120��$��Stretch=�$��5q�$��Center=��� -   $
 btnAdd��$��9��$y�Hidden=�$��5q�$	��ADD��${�False��$��/Fonts/#Lato)�$��13(�$��70��$��20��$��Stretch=�$��Center=��� -    ��Name$ loadingImage��$y�Hidden=�$��2��
�������._��� $��Center=�$��Center=�$��Uniform=�$ Images/loading.gif��$��Images/Exclaimation.png���� -   $ 	FundImage��$��2��$��Images/uploadPSX.jpg��$��Center=�$��Center=�$
��Fill=��� -    ��Name$ list1��$��2��$
��Auto��$y�Hidden=�$��/Fonts/#Lato)�$
��Left=� � 	Resources  �� 
TargetType �� Property �� Value   	txtAppDep��%^   '��       &	  %     �� " ��.���  � ��$
 Left=��� " ��.���  � ��
 �  �� �� View �� -   gridView�� �� AllowsColumnReorder$
 true�� �� ColumnHeaderToolTip$ Market Summary��.P��� -   col1�� �� Width$ 50������ ���#455A64���#fff$��16(�$��Sr. No.��$��2q� �� CellTemplate �� .R��� $
��Left=�$
 Left=����� *SB_ID+�� -   col2��$	 250������ ���#455A64���#fff$��16(�$
��Name��$��2q� �� .R��� $
��Left=�$
 Left=����� *FSB_SHARE_NAME+�� -   col3��$ 70������ ���#455A64���#fff$��16(�$��Symbol��$��2q� �� .R��� $
��Left=�$ Center=����� *FSB_SHARE_SYMBOL+�� -   col4��$ 80������ ���#455A64���#fff$��16(�$��Quantity��$��2q� 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Right=�$��70��.+��� $��Right=�$ Right=����� *FSB_SHARE_QUANTITY+�� -   col5��$ 85������ ���#455A64���#fff$��16(�$��
Avg. Price��$��2q�   .R�� $��Right=�$��95��.+��� $��Right=����� *FSB_SHARE_AVG_PRICE+�� -   col6��$	 105������ ���#455A64���#fff$��16(�$��	Book Cost��$��2q�   .R�� $��Right=�$��95��.+��� $��Right=����� *FSB_SHARE_BOOK_COST+�� -   col7��$	 105������ ���#455A64���#fff$��16(�$��Market Price��$��2q�   .R�� $��Right=�$��95��.+��� $��Right=����� *FSB_SHARE_MARKET_PRICE+�� -   col8��$	 105������ ���#455A64���#fff$��16(�$��Market Value��$��2q�   .R�� $��Right=�$	��100��.+��� $��Right=����� *FSB_SHARE_MARKET_VALUE+�� -   col9��$	 105������ ���#455A64���#fff$��16(�$��App. / (Dep.)��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *FSB_SHARE_APP_DEP+�� -   	col10��$	 105������ ���#455A64���#fff$��16(�$��(%) Closing��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *FSB_SHARE_PERCENTAGE_CLOSING+� $��3��
������� �� $��* ��� $��6* ��� $��* �.+��� -   $��	statusBar��$��Bottom=�$��0��$��Stretch=�$��Center=�
������ 
���   �$��1q�.@��� .���� -   $��	lblStatus��$��Status: Ready��$��/Fonts/#Lato)�$��11(�
������ �� .��� -    �Name$ progressBar���� $��1��$��14(�$��Center=�$h��bThe Data of Market Rates are getting from Pakistan Stock Exchange Website With 5 Minutes Interval.��$��Fonts/#Lato)�
���   �$��Center=�$
��Bold"��� -    $��
statusBar2��$��Bottom=�$��2��$��Right=�$��Bottom=�
������ 
���   �$��1q�.@��� $��Right=�$��Bottom=�$��50��
������ .���� 
������ ._��� -!   $ 	imgStatus��$ Images/tick.gif��$��Images/tick.png��$��30��$��30��