   M S B A M L   `   `   `  ���� M  IPSXDataFetchingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PSXDataFetchingAppPSXDataFetchingApp  (    "PSXDataFetchingApp.FundPopupWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  ,local clr-namespace:PSXDataFetchingApp    � Title$  Data Extractor Utility�� � Icon$ Images/web-scrap2.png��$	��350��$	��400�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�.���  � RowDefinitions �� $��3* ��� $	��10* ��� $��2* �.+�� -    �Name$ header��$��0��
��� ���.+��� $��Center=�$��Center=�
���   �._�� $��Center=�$��Center=�
������� � ColumnDefinitions �� $��* ��� $��2* �.+��� $��Images/alert.png��$��1q�"  WpfAnimatedGif.ImageBehavior   AnimatedSource$ Images/alert.gif��$��35��$��Uniform=��� $��1��$��Closing Percentage Alert ��$��Center=�$��Center=�$��18(�$��\Fonts\#Lato-Bold)� �� Padding$ 0 8 5 5q�� $��1��.+��  �� $��* ��� $	��15* ��� $��* �.+�� -   $ headerright��
��� ����� $��1��
�������._�� 
�������$��1q� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��2* �.+�� 
������� �� $��* ��� $��* �.+��� $
��DATE��$��10 2 2 2q�$��Center=��� -   	 ��Name$	 txtDate��$��1��$
��Date��$��5 2 2 2q�$��Center=�� $��1��
������� �� $��* ��� $��* �.+��� $��	FUND NAME��$��10 2 2 2q�$��Center=��� -   $	 txtFundName��$��1��$��	Fund Name��$��5 2 2 2q�$��Center=�� $��2��
������� �� $��* ��� $��* �.+��� $
��NAME��$��10 2 2 2q�$��Center=��� -   $	 txtShareName��$��1��$
��Name��$��5 2 2 2q�$��Center=�� $��3��
������� �� $��* ��� $��* �.+��� $��SYMBOL��$��10 2 2 2q�$��Center=��� -   $	 txtShareSymbol��$��1��$��Symbol��$��5 2 2 2q�$��Center=�� $��4��
������� �� $��* ��� $��* �.+��� $��QUANTITY��$��10 2 2 2q�$��Center=��� -   $	 txtShareQuantity��$��1��$��Quantity��$��5 2 2 2q�$��Center=�� $��5��
������� �� $��* ��� $��* �.+��� $��AVERAGE PRICE��$��10 2 2 2q�$��Center=��� -   $	 txtShareAveragePrice��$��1��$��Average Price��$��5 2 2 2q�$��Center=�� $��6��
������� �� $��* ��� $��* �.+��� $��	BOOK COST��$��10 2 2 2q�$��Center=��� -	   $	 txtShareBookCost��$��1��$��	Book Cost��$��5 2 2 2q�$��Center=�� $��7��
������� �� $��* ��� $��* �.+��� $��MARKET PRICE��$��10 2 2 2q�$��Center=��� -
   $	 txtShareMarketPrice��$��1��$��Market Price��$��5 2 2 2q�$��Center=�� $��8��
������� �� $��* ��� $��* �.+��� $��MARKET VALUE��$��10 2 2 2q�$��Center=��� -   $	 txtShareMarketValue��$��1��$��Market Value��$��5 2 2 2q�$��Center=�� $��9��
������� �� $��* ��� $��* �.+��� $��APP. / (DEP.)��$��10 2 2 2q�$��Center=��� -   $	 txtShareAppDep��$��1��$��App Dep��$��5 2 2 2q�$��Center=�� $��10��
������� �� $��* ��� $��* �.+��� $��CLOSING PERCENTAGE��$��10 2 2 2q�$��Center=��� -   $	 txtShareClosingPercentage��$��1��$��Closing Percentage��$��5 2 2 2q�$��Center=�� $��11�� �� $��* ��� $��* �.+�� .+��� ._��  �� $��* ��� $��* �.+��� -   
 ��Name$
 btnBack��$y�Hidden=�$
��BACK��$��5q��� -   $
 btnNext��$y�Hidden=�$
��NEXT��$��1��$��5q�� $��1��.+��� ._��  �� $��* ��� $��* �.+��� -   $
 btnOK��$��OK��$��5q��� -   $
 	btnCancel��$��Cancel��$��1��$��5q�� -   $ 
headerleft��$��2��
��� ���� -   $ headerbottom��$��2��
��� ���