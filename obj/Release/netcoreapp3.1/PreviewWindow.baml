   M S B A M L   `   `   `  ���� M  IPSXDataFetchingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PSXDataFetchingAppPSXDataFetchingApp  &     PSXDataFetchingApp.PreviewWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:PSXDataFetchingApp     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com    � Title$  Data Extractor Utility�� � Icon$ Images/web-scrap2.png��$	��620��$	��950�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�.���� $��10q�
�������._��  � RowDefinitions �� $��60 ��� $��* ��� $	��10* ��� $��* �$��30��.+�� -    �Name$ header��$��0��
�������$��40��.+��� $��2q�$
��Left=� �� Padding$ 5q�
�������._��� -    ��Name$ 	imgClient��$��/Images/BOP.png��� $��Center=�$��Center=� �� $��2* ��� $��* �.+��� $,��&Pakistan Stock Exchange Market Summary��$��/Fonts/#Raleway)�$��DemiBold"�$��Bottom=�$��Bottom=�$��20(��� $��1��$<��6View the Real Time Stocks of the Pakistan Stock Market��$	��Top=�$��Center=�$��1q�$��/Fonts/#Raleway)�$��14(�$	��Top=��� $��Right=�$ 5q�$��Center=�._�� $��0 0 10 0q� �� $
��Auto ��� $
��Auto �.+�� $��Stretch=�
������� � ColumnDefinitions �� $��* �$��40��$��40���� $��* �.+��� $ 0q�._��� -   	 ��Name$
	 Date��$
��Date��$��/Fonts/#Lato)�$��12(�
 �� TextAlignment$

 Left=� �� Padding$ 2q�$��DemiBold"��� $	 0 2q�$��1��
�������._��� -   $	 lblDate��$��Franklin Gothic)�
�������$��Normal$�$��12(�$

 Left=�$ 2q�� $��Stretch=�$��1��
������� �� $��* �$��40��$��40���� $��* �.+��� $ 0q�._��� -   $	 Status��$��Status ��$��/Fonts/#Lato)�$��12(�$��Stretch=�$

 Left=�$ 2q�$��DemiBold"��� $ 2q�$��1��
�������._��� -   $	 	lblStatus��$��Franklin Gothic)�
��� � �$��DemiBold"�$��12(�$��Stretch=�$

 Left=�$
��Left=�$ 2q�� $��1��
�������$��0 2 0 2q�.+��  �� $��90 ��� $��90 ��� $��90 ��� $��* �.+��� -    ��Name$ btnBack��$��0��$
��Left=�$��Center=�$��80��$��30��
������� � ToolTip$ Go Back��$��5 0 0 0q�.���� $��Center=�$��Center=�.+�� $��70��$��30��$��1 0 1 0q� �� $��* ��� $��2* �.+��� $��Images/back.png��$��20��$��Uniform=�$��Right=�$��Center=��� $��1��$
��Left=�$��Center=�$
��Back��$��13(�$��Fonts/#Raleway)�$��4 0 0 0q��� -   $ 
btnRefresh��$��1��$��Center=�$��Center=�$��80��$��30��
�������$ Refresh��.���� $��Center=�$��Center=�.+�� $��70��$��30��$��1 0 1 0q� �� $��* ��� $��3* �.+��� $��Images/refresh2.png��$��20��$��Uniform=�$��Right=�$��Center=��� $��1��$
��Left=�$��Center=�$��Refresh��$��13(�$��Fonts/#Raleway)�$��2 0 0 0q��� -	   $ 	btnExport��$��2��$��Center=�$��Center=�$��80��$��30��
�������$ Generate Excel Report.��.���� $��Center=�$��Center=�.+�� $��70��$��30��$��1 0 1 0q� �� $��* ��� $��2* �.+��� $��Images/excel.jpg��$��20��$��Uniform=�$��Right=�$��Center=��� $��1��$
��Left=�$��Center=�$��Excel��$��13(�$��Fonts/#Raleway)�$��4 0 0 0q�� $��3��$
��Left=�$��2 0 2 0q� �� $��* ��� $��* ��� $��* ��� $��* �.+�� $
��Left=�.+��� -
    ��Name$ 	txtSearch��$	��185��$��25��$��13(�$��Center=�$
��Left=�$��5q��� -   $	 
txtSearch1�� J� IsHitTestVisible$ False��$��Enter Search Term Here��$��Center=�$
��Left=�$��10,0,0,0q�
�������$��13(�$��/Fonts/#Raleway)�����  �� 
TargetType" ��.���  �� Property � y� �� Value$ 	Collapsed=� �� Triggers ��  �� Binding �� *Text+ �� ElementName$ 	txtSearch�� �� Value  .a���  � y�$ Visible=��� -    ��Name$ comboCategory��$��1��$	��170��$��25��$��Center=��� -   $ 	btnSearch��$��2��$��Center=�
������ $��Center=�$��80��$��30��
������ $ Search By Category.��.���� $��Center=�$��Center=�.+�� $��70��$��25��$��0 0 0 0q� �� $��* �.+��� $��Images/search.png��$��Uniform=�$��1q�$��Right=�$��Center=�� $��3��$��Center=�
����d� �� $��* ��� $��2* �.+��� $��	AUTO SYNC��$��Center=�$��Center=�
�������$��8(�$	 2 0q�$��0q�$��DemiBold"��� $��1��$ 1q�._�� 
������� �� $��* ��� $��3* �.+��� -   $	 lblOff��$��0��$	�� ON��$��9(�$
��Bold"�$ 2q�$	��0 2q��� -    ��Name$ sliderRecur��$��1�� � Value$ 1Q� � Maximum$ 1Q� �� Interval$ 1��$��2q�$	��0 2q�� $��3��$��Right=� �� $��* ��� $��* ��� $��* �.+�� $��0��$��15 0 5 0q� �� $��* ��� $��* �.+��� $��0��$��Volume: ��$��/Fonts/#Lato)�$��11(�$��Center=��� -   $	 	lblVolume��$��1��$
��Bold"�$��/Fonts/#Lato)�$��11(�$��Center=�� $��1��$��15 0 5 0q� �� $��* ��� $��* �.+��� $��0��$��Value: ��$��/Fonts/#Lato)�$��11(�$��Center=��� -   $	 lblValue��$��1��$
��Bold"�$��/Fonts/#Lato)�$��11(�$��Center=�� $��2��$��15 0 5 0q� �� $��* ��� $��* �.+��� $��0��$��Trades: ��$��/Fonts/#Lato)�$��11(�$��Center=��� -   $	 	lblTrades��$��2��$
��Bold"�$��/Fonts/#Lato)�$��11(�$��Center=��� -    ��Name$ list1��$��2��$
��Auto�� �� View �� -   gridView�� �� AllowsColumnReorder$
 true�� �� ColumnHeaderToolTip$ Market Summary��.P��� -   col0��  �� Width$  40������ ���#455A64���#fff$��16(�$	��Sr.��$��2q�$��30��! �� CellTemplate! 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Right=�$	��275��.+��� $��Right=����� *CurrentMarketSummaryId+�� -   col1��$	  250������ ���#455A64���#fff$��16(�$
��NAME��$��2q�$��30��!   .R�� $
��Left=�$	��280��.+��� $
��Left=����� *Name+�� -   col2��$	  100������ ���#455A64���#fff$��16(�$��SYMBOL��$��2q�!   .R�� $��Center=�$��Center=�$	��100��
�������.+��� $��Center=�$��Center=�$
 Center=����� *Symbol+�� -   	col23��$	  225������ ���#455A64���#fff$��16(�$��Category��$��2q�!   .R�� $
��Left=�$	��225��.+��� $
��Left=����� *
Category+�� -   col3��$	  100������ ���#455A64���#fff$��16(�$
��LDCP��$��2q�!   .R�� $��Right=�$	��100��.+��� $��Right=����� *Ldcp+�� -   col4��$	  100������ ���#455A64���#fff$��16(�$
��OPEN��$��2q�!   .R�� $��Right=�$	��100��.+��� $��Right=����� *Open+�� -   col5��$	  100������ ���#455A64���#fff$��16(�$
��HIGH��$��2q�!   .R�� $��Right=�$	��100��.+��� $��Right=����� *High+�� -   col6��$	  100������ ���#455A64���#fff$��16(�$	��LOW��$��2q�!   .R�� $��Right=�$	��100��.+��� $��Right=����� *Low+�� -   col7��$	  100������ ���#455A64���#fff$��16(�$��CURRENT��$��2q�!   .R�� $��Right=�$	��100��.+��� $��Right=����� *	Current+�� -   col8��$	  100������ ���#455A64���#fff$��16(�$��CHANGE��$��2q�!   .R�� $��Right=�$	��100��.+��� $��Right=����� *Change+�� -   col9��$	  100������ ���#455A64���#fff$��16(�$��VOLUME��$��2q�!   .R�� $��Right=�$	��100��.+��� $��Right=����� *Volume+� -    $ Footer��$��3��
������� �� $��2* ��� $��6* ��� $��2* �.+��� -!   $��	statusBar��$��Bottom=�$��0��$
��Left=�$��Center=�
������ 
���   �$��1q�.@��� .��� 
������  �� $��* ��� $��* �.+��� -"   $	 lblStatusMessage��$��0��$��Status ��$��/Fonts/#Lato)�$��Black"�$��12(�$ 5q��� -#   $	 StatusMessage��$��1��$��Ready��$��DemiBold"�$��/Fonts/#Lato)�$��12(�
�������$ 5q��� -$   " ��Name$" progressBarContainer��$��Bottom=�$��0��$��Right=�$��Center=�$��0q�
������ $y�Hidden=�.@��� .��� -%   # �Name$# progressBar��$��50��$��10���� $��1��$��14(�$��Center=�$W��QThe Data is getting from Pakistan Stock Exchange Website With 5 Minutes Interval.��$��Fonts/#Lato)�
���   �$��Center=�$
��Bold"��� -&   $��
statusBar2��$��Bottom=�$��2��$��Right=�$��Bottom=�
������ 
���   �$��1q�.@��� $��Right=�$��Bottom=�$��50��
������ .���� 
�������._��� -'   $ 	imgStatus��"  WpfAnimatedGif.ImageBehavior$   AnimatedSource$$ Images/tick.gif��$��Images/tick.png��$��30��$��30��