   M S B A M L   `   `   `  ���� M  IPSXDataFetchingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PSXDataFetchingAppPSXDataFetchingApp  +    %PSXDataFetchingApp.MUFAPMarketSummary   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  ,local clr-namespace:PSXDataFetchingApp    � Title$  Data Extractor Utility�� � Icon$ Images/web-scrap2.png��$	��650��$
��1120�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�.���� $��10q�
�������._��  � RowDefinitions �� $��2* ��� $��2* ��� $	��14* ��� $��* � � ColumnDefinitions �� $��* �.+�� -    �Name$ HeaderColor��$��0�����#f0a500 �� $��* �.+��� -    ��Name$ HeaderImage��$��Images/BOP.png��$
��Left=�$��5q�"  WpfAnimatedGif.ImageBehavior   AnimatedSource$ Images/bop.gif��$
��Fill=�$	��200���� $��0��$��MUFAP Market Summary��$��26(�$��/Fonts/#Lato)�$
��Bold"�$��Center=�	 �� TextAlignment$	 Center=�$��Center=�$��0 0 10 0q�� $��0��$��Right=�$��5q� �� $��* ��� $��* �.+��� -   
 ��Name$
 txtDate��$��0��$��/Fonts/#Lato)�$��Bottom=�$��0 0 2 0q��� -   $
 	txtStatus��$��1��$��/Fonts/#Lato)�$	��Top=�$��0 2 0 0q�� $��1��$��1 5 1 5q����#fff$��40�� �� $��* �$��81���� $��3* �$	��260���� $��2* �$��80���� $��* �$	��100���� $��* �$	��100���� $��* ��� $��* �$��60��.+��� -    ��Name$ btnBack��$��0��$��/Fonts/#Lato)�$��30��$��Center=�$��70��$
��Left=� � ToolTip$ Go To Main Menu��$��5 0 0 0q����� $��70��$��30��$��Center=�$��2 0 2 0q�.+�� $��70��$��30�� �� $��* ��� $��2* �.+��� $��Images/back.png��$��Uniform=�$��Center=��� $��1��$
��Back��$��13(�$��Fonts/#Raleway)�$��Center=�$
��Left=��� -   $��comboCategory��$��1��$��Fonts/#Raleway)�$	��250��$	��250��$��1q�$��30��$ Select Category..��$��Center=��� -   $ btnGet��$��2��$��/Fonts/#Lato-Bold)�$��13(�$��50��$��70��$��30��$��Center=�$��Center=�
������ $��5q�$+ %Get MUFAP Fund Market Summary Details������ $��Center=�$��Center=�.+�� $��70��$��30�� �� $��* ��� $��* �.+��� $��Images/play.png��$��Uniform=��� $��1��$��GO��$��13(�$��Fonts/#Raleway)�$��2 0 0 0q�$��Center=��� -   $ btnSave��$��3��$��/Fonts/#Lato-Bold)�$��13(�$��90��$��30��$��Center=�$��Center=�
�������$ Generate Excel Report������ $��Center=�$��Center=�.+�� $��80��$��30�� �� $��* ��� $��* �.+��� $��Images/excel.jpg��$��Uniform=��� $��1��$��Excel��$��13(�$��Fonts/#Raleway)�$��2 0 0 0q�$��Center=�$
��Left=��� -	   $ btnReset��$��4��$��/Fonts/#Lato-Bold)�$��13(�$��95��$��30��$��Center=�$��Center=�
����������� $��Center=�$��Center=�$��80��$��30��.+�� $��80��$��30�� �� $��2* ��� $��2* �.+��� $��Images/reset.png��$��1q�$
��Fill=�$��25��$ Refresh Information���� $��1��$��Reset��$��13(�$��Fonts/#Raleway)�$��Center=�$
��Left=�$��2 0 0 0q�� $��5��$��Right=�.+��� -
    ��Name$ SearchTermTextBox��$	��205��$��25��$��13(�$��Center=�$
��Left=��� -   $
 	txtSearch��$��13(� J� IsHitTestVisible$ False��$��Enter Search Term Here��$��Center=�$
��Left=�$��10,0,0,0q�
�������$��/Fonts/#Raleway)�����  �� 
TargetType" ��.���  �� Property � y� �� Value$ 	Collapsed=� �� Triggers ��  �� Binding �� *Text+ �� ElementName$ SearchTermTextBox�� �� Value  .a���  � y�$ Visible=��� -   $ 	btnSearch��$��6��$��/Fonts/#Lato-Bold)�$��13(�$��60��$��40��$��30��$��Right=�$��Center=�
�������
������ ���� $��Center=�$��Center=�.+��� $��Images/search.png��$��1q�$
��Fill=�$��25��$��Center=�$ Search Information���� -    ��Name$ loadingImage��$y�Hidden=�$��2��
�������._��� $��Center=�$��Center=�$��Uniform=�$ Images/loading.gif��$��Images/Exclaimation.png���� -   $ 	FundImage��$��2��$#��Images/mufapMarketSummary.jpg��$$ Images/mufapMarketSummary2.gif��$��Center=�$��Center=�$
��Fill=��� -    ��Name$ list1��$��2��$
��Auto��$y�Hidden=�$��/Fonts/#Lato)�$
��Left=� � 	Resources    	txtAppDep��%^   '��       &	  %     �� " ��.���  � ��$
 Left=��� " ��.���  � ��
 �  �� �� View �� -   gridView�� �� AllowsColumnReorder$
 true�� �� ColumnHeaderToolTip$ Market Summary��.P��� -   col1�� �� Width$ 50������ ���#455A64���#fff$��16(�$��Sr. No.��$��2q� �� CellTemplate �� .R��� $
��Left=�$
	 Left=����� *MufapMarketSummaryId+�� -   col2��$	 250������ ���#455A64���#fff$��16(�$
��Name��$��2q� �� .R��� $
��Left=�$
	 Left=����� *Name+�� -   col4��$	 200������ ���#455A64���#fff$��16(�$��Category��$��2q� 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $
��Left=�$	��200��.+��� $��5 0 0 0q�$
��Left=�$
	 Left=�$	��200������ *
Category+�� -   col5��$ 85������ ���#455A64���#fff$��16(�$��Rating��$��2q�   .R�� $��Center=�$��95��.+��� $��Center=����� *Rating+�� -   col6��$	 105������ ���#455A64���#fff$��16(�$��Validity Date��$��2q�   .R�� $��Right=�$��95��.+��� $��Right=����� *ValidityDate+�� -   col7��$	 105������ ���#455A64���#fff$��16(�$	��NAV��$��2q�   .R�� $��Right=�$��95��.+��� $��Right=����� *Nav+�� -   col8��$	 105������ ���#455A64���#fff$��16(�$	��YTD��$��2q�   .R�� $��Right=�$	��100��.+��� $��Right=����� *Ytd+�� -   col9��$	 105������ ���#455A64���#fff$��16(�$	��MTD��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *Mtd+�� -   col3��$ 70������ ���#455A64���#fff$��16(�$��1 Day��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *_1Day+�� -   	col10��$	 105������ ���#455A64���#fff$��16(�$��15 Days��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *	_15Days+�� -   	col11��$	 105������ ���#455A64���#fff$��16(�$��30 Days��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *	_30Days+�� -   	col12��$	 105������ ���#455A64���#fff$��16(�$��90 Days��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *	_90Days+�� -   	col13��$	 105������ ���#455A64���#fff$��16(�$��180 Days��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *
_180Days+�� -   	col14��$	 105������ ���#455A64���#fff$��16(�$��270 Days��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *
_270Days+�� -   	col15��$	 105������ ���#455A64���#fff$��16(�$��365 Days��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *
_365Days+�� -    	col16��$	 105������ ���#455A64���#fff$��16(�$��TER %**��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *Ter+�� -!   	col17��$	 105������ ���#455A64���#fff$��16(�$��MF % **��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *Mf+�� -"   	col18��$	 105������ ���#455A64���#fff$��16(�$��SAM % **��$��2q�   .R�� $��Right=�$��80��.+��� $��Right=����� *Sandm+� $��3��
������� �� $��* ��� $��6* ��� $��* �.+��� -#   $��	statusBar��$��Bottom=�$��0��$��Stretch=�$��Center=�
������ 
���   �$��1q�.@��� .���� -$   $��	lblStatus��$��Status: Ready��$��/Fonts/#Lato)�$��11(�
������ �� $��1��$��14(�$��Center=�$M��GData Source: http://www.mufap.com.pk/nav_returns_performance.php?tab=01��$��Fonts/#Lato)�
���   �$��Center=�$
��Bold"��� -%   $��
statusBar2��$��Bottom=�$��2��$��Right=�$��Bottom=�
������ 
���   �$��1q�.@��� $��Right=�$��Bottom=�$��50��
������ .���� 
������ ._��� -&   $ 	imgStatus��$ Images/tick.gif��$��Images/tick.png��$��30��$��30��