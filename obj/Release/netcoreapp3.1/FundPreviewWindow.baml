   M S B A M L   `   `   `  ���� M  IPSXDataFetchingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PSXDataFetchingAppPSXDataFetchingApp  *    $PSXDataFetchingApp.FundPreviewWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  ,local clr-namespace:PSXDataFetchingApp  -     � Title$  Data Extractor Utility��$��/Fonts/#Lato)�$	��650��$
��1115�� � Icon$ /Images/web-scrap2.png�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � 	Resources  �� 
TargetType �� Property �� Value��%2   '��       �� " ��.���  � ��$ Stretch=�.���� -    ��Name$ CriteriaSection��$��10q�
�������._�� 	 � RowDefinitions	 �� $��2* ��� $��2* �$��50���� $	��14* ��� $��* �
 � ColumnDefinitions
 �� $��* �.+�� -    �Name$ HeaderColor��$��0�����#f0a500
 �� $��* �.+��� -    ��Name$ HeaderImage��$��Images/BOP.png��$
��Left=�$��5q�"  WpfAnimatedGif.ImageBehavior   AnimatedSource$ Images/bop.gif��$
��Fill=�$	��200��� $��0��$��Center=�$��Center=�	 �� $��2* ��� $��* �.+��� $��Fund Market Summary��$��26(�$��/Fonts/#Raleway)�$
��Bold"� �� TextAlignment$ Center=��� $��1��$3��-Real Time Fund Market View of Your Portfolio ��$��12(�$��/Fonts/#Raleway)�$��Normal"�$ Center=�� $��0��$��Right=�$��10q�$	��250��	 �� $��* ��� $��* �.+�� $��0��
 �� $��60 ��� $��* �.+��� -    ��Name$ lblDate��$��0�� �� Padding$ 5q�
�������$
��DATE��$��/Fonts/#Lato)�$��Bottom=�$ Right=� J� Opacity$	 0.9Q��� -   $ txtDate��$��1��$ 5q�
�������$��/Fonts/#Lato)�$��Bottom=�$
��Bold"�� -   $ 
gridStatus��$��1��$y�Hidden=�
 �� $��60 ��� $��* �.+��� -   $ lbltxtStatus��$��0��$ 5q�
�������$��STATUS��$��/Fonts/#Lato)�$	��Top=�$ Right=�$	 0.9Q��� -	   $ 	txtStatus��$��1��$ 5q�
�������$��/Fonts/#Lato)�$	��Top=�$��Stretch=�$
 Left=�$��	ExtraBold"�� $��1��$��1 5 1 5q����#fff$��40��
 �� $��* �$��80���� $��2* �$	��160���� $��2* �$��90���� $��2* �$��90���� $��2* �$��90���� $��* ��� $��* ��� $��* ��� $��* ��� $��* �$��50��.+��� -
    ��Name$ btnBack��$��0��$��/Fonts/#Lato)�$��30��$��Center=�$��70��$��Center=�$��0 0 0 0q� � ToolTip$ Go To Main Menu������ $��Center=�$��Center=�.+�� $��70��$��30��$��1 0 1 0q�
 �� $��* ��� $��2* �.+��� $��Images/back.png��$��20��$��Uniform=�$��Right=�$��Center=��� $��1��$
��Left=�$��Center=�$
��Back��$��13(�$��Fonts/#Raleway)�$��4 0 0 0q��� -   $��	comboFund��$��1��$��Fonts/#Raleway)�$��13(�$	��150��$��Stretch=�$��Center=�$��5q�$��30��$H BSelect Fund To Fetch Its Shares with their current market details.���� -   $ btnGet��$��2��$��/Fonts/#Lato-Bold)�$��13(�$��50��$��80��$��30��$��Center=�$��Center=�
�������$ Get Shares of Fund������ $��Center=�$��Center=�.+�� $��70��$��30��$��1 0 1 0q�
 �� $��* ��� $��* �.+��� $��Images/play.png��$��22��$��Uniform=�$��Right=�$��Center=��� $��1��$
��Left=�$��Center=�$��Go��$��13(�$��Fonts/#Raleway)�$��4 0 0 0q��� -   $ btnSave��$��3��$��5q�$��/Fonts/#Lato-Bold)�$��13(�$��30��$��80��$��30��$��Center=�$��Center=�
�������$ Generate Excel Report.������ $��Center=�$��Center=�.+�� $��70��$��30��$��1 0 1 0q�
 �� $��* ��� $��* �.+��� $��Images/excel.jpg��$��Uniform=�$��Right=�$��Center=��� $��1��$
��Left=�$��Center=�$��Excel��$��13(�$��Fonts/#Raleway)�$��4 0 0 0q��� -   $ btnReset��$��4��$��5q�$��/Fonts/#Lato-Bold)�$��13(�$��40��$��80��$��30��$��Center=�$��Center=�
����������� $��Center=�$��Center=�.+�� $��70��$��30��
 �� $��* ��� $��2* �.+��� $��Images/refresh2.png��$��22��$��Uniform=�$��Right=�$��Center=��� $��1��$
��Left=�$��Center=�$��Refresh��$��13(�$��Fonts/#Raleway)�$��1 0 0 0q�� $��Right=�$��5��.+��� -    ��Name$ 	txtSearch��$	��120��$
��Auto��$��30��$��13(�$��Center=�$��Stretch=�$��Center=�$��5q��� -   $ 
txtSearch1�� J� IsHitTestVisible$ False��$��Enter Search..��$��Center=�$
��Left=�$��10,0,0,0q�
�������$��13(�$��/Fonts/#Raleway)����� " ��.���  � y�$ 	Collapsed=� �� Triggers ��  �� Binding �� *Text+ �� ElementName$ 	txtSearch�� �� Value  .a���  � y�$ Visible=��� -    ��Name$ comboCategory��$��6��$
��Auto��$��30��$��Stretch=�$��Center=�$��13(��� -   $ 	btnSearch��$��7��$
��Left=�
������ $��Center=�$��80��$��30��
������ $ Search By Category.��.���� $��Center=�$��Center=�.+�� $��70��$��25��$��0 0 0 0q�
 �� $��* �.+��� $��Images/search.png��$��Uniform=�$��0q�$��Right=�$��Center=�� $��8��$��Center=�
����d�
 �� $��* ��� $��* �.+��� $��AUTO UPDATE��$��Center=�
�������$��12(�$ 2q�$	��0 2q�$��DemiBold"�� $��1��
�������
 �� $��* ��� $��* ��� $��* �.+��� $��0��$
�� OFF��$
��Bold"�$ 2q�$	��0 2q�
���   ��� -    ��Name$ sliderRecur��$��1�� � Value$ 1Q� � Maximum$ 1Q�$��2q�$	��0 2q��� $��2��$	��ON ��$
��Bold"�$ 2q�$	��0 2q�
���   ��� -   $ 	lblFundID��$��5��$y�Hidden=�$��FUND ID:��$
��Bold"�
�������$��/Fonts/#Lato)�$��Center=�$��Right=�$��5q��� -   $��	txtFundID��$��6��$y�Hidden=�${�False��$��/Fonts/#Lato)�$	��100��$��Stretch=�$��5q�$��Center=��� -   $ lblFundNAME��$��7��$��
FUND NAME:��$y�Hidden=�$
��Bold"�
�������$��/Fonts/#Lato)�$��Center=�$��Right=�$��5q��� -   $��txtFund��$��8��$y�Hidden=�${�False��$��/Fonts/#Lato)�$	��100��$��Stretch=�$��5q�$��Center=��� -   $ btnAdd��$y�Hidden=�$��9��$��5q�$��/Fonts/#Lato-Bold)�$��13(�$��40��$��40��$��30��$
��Left=�$��Center=�
�������
������ ���� $��Center=�$��Center=�.+��� $��Images/add.jpg��$��1q�$��Uniform=�$��20��$ Fund Bucket���� -   $ btnBorderBucket��$��9��$y�Hidden=�
�������$��45��$��45��$��30��$��Right=�._��� -   $ btnViewBucket��
�������$��1q�$��/Fonts/#Lato-Bold)�$��13(�$��Right=�$��Center=�
������ ��� $��40��
 �� $��2* ��� $��2* �.+��� -   $ txtBucketCount��$
�� () ��$
��Left=�$��Center=��� $��1��$��Images/bucket.png��$��Uniform=�$��Right=�$��20��$ Fund Bucket���� -   $ loadingImage��$y�Hidden=�$��2��
�������._��� $��Center=�$��Center=�$��Uniform=�$ Images/loading.gif��$��Images/Exclaimation.png���� -   $ 	FundImage��$��2��$��Images/stockMarket.jpg��$��Center=�$��Center=�$
��Fill=��� -    ��Name$ list1��$��2��$
��Auto��$y�Hidden=�$��/Fonts/#Lato)�$
��Left=�� -   $ Footer��$��3��
�������
 �� $��* ��� $��6* ��� $��* �.+��� -    $��	statusBar��$��Bottom=�$��0��$��Stretch=�$��Center=�
������ 
���   �$��1q�.@��� .��� 
 �� $��* ��� $��2* �.+��� $��0��$ 3q�$��Status:��$��/Fonts/#Lato)�$��11(�
��������� -!   $��	lblStatus��$ 3q�$��1��$��Ready��$��/Fonts/#Lato)�$��11(�
���  ��$
��Bold"�
���   ��� $��1��$��14(�$��Center=�$h��bThe Data of Market Rates are getting from Pakistan Stock Exchange Website With 5 Minutes Interval.��$��Fonts/#Lato)�
���   �$��Center=�$
��Bold"��� -"   $��
statusBar2��$��Bottom=�$��2��$��Right=�$��Bottom=�
�������
���   �$��1q�.@��� $��Right=�$��Bottom=�$��50��
������ .���� 
������ ._��� -#   $ 	imgStatus��$ Images/tick.gif��$��Images/tick.png��$��30��$��30��