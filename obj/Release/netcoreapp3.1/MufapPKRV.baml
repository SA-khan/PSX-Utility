   M S B A M L   `   `   `  ���� M  IPSXDataFetchingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PSXDataFetchingAppPSXDataFetchingApp  "    PSXDataFetchingApp.MufapPKRV   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  ,local clr-namespace:PSXDataFetchingApp    � Title$  Data Extractor Utility�� � Icon$ Images/web-scrap2.png��$	��650��$	��920�� � WindowStartupLocation$ CenterScreen=�.���� $��10q�
�������._��  � RowDefinitions �� $��2* ��� $��2* ��� $	��14* ��� $��* � � ColumnDefinitions �� $��* �.+�� -    �Name$ HeaderColor��$��0�����#f0a500 �� $��* �.+��� -    ��Name$ HeaderImage��$��Images/BOP.png��$
��Left=�$��5q�"  WpfAnimatedGif.ImageBehavior   AnimatedSource$ Images/bop.gif��$
��Fill=�$	��200���� $��0��$��MUFAP PKRV Summary��$��26(�$��/Fonts/#Lato)�$
��Bold"�$��Center=� �� TextAlignment$ Center=�$��Center=�$��0 0 10 0q�� $��0��$��Right=�$��5q� �� $��* ��� $��* �.+��� -   	 ��Name$	 txtDate��$��0��$��/Fonts/#Lato)�$��Bottom=�$��0 0 2 0q��� -   $	 	txtStatus��$��1��$��/Fonts/#Lato)�$	��Top=�$��0 2 0 0q�� $��1��$��1 5 1 5q����#fff$��35�� �� $��* �$��81���� $��3* �$	��350���� $��2* �$��60���� $��2* �$��60���� $��2* �$��60���� $��* �.+��� -   
 ��Name$
 btnBack��$��0��$��/Fonts/#Lato)�$��30��$��Center=�$��70��$
��Left=�$��5 0 0 0q� � ToolTip$ Go To Main Menu������ .+�� $��70��$��25�� �� $��* ��� $��2* �.+��� $��Images/back.png��$��Uniform=�$��15��$��Center=��� $��1��$
��Back��$��Center=�$��13(�$��Fonts/#Raleway)�(  "System.Windows.Controls.DatePicker  -   $��pkrvDatepicker��$��1��$��Fonts/#Raleway)�$��Center=�$��5 0 0 0q�$	��250��$	��450��$��Stretch=�$��30��$ Select Date..���� -   $
 btnGet��$��2��$��/Fonts/#Lato-Bold)�$��13(�$��50��$��30��$��Center=�$��Center=�
������ 
������ $ Get Shares of Fund������ $��Center=�$��Center=�$��50��.+��� $��Images/play.png��$��Uniform=��� -   $
 btnSave��$��3��$��/Fonts/#Lato-Bold)�$��13(�$��30��$��Center=�$��Center=�
�������
������ $ Generate Excel Report������ $��Center=�$��Center=�$��30��$��35��.+��� $��Images/excel.jpg��$
��Fill=��� -	   $
 btnReset��$��4��$��5q�$��/Fonts/#Lato-Bold)�$��13(�$��40��$��40��$��30��$
��Left=�$��Center=�
�������
������ ���� $��Center=�$��Center=�.+��� $��Images/reset.png��$��1q�$
��Fill=�$��25��$ Refresh Information���� -
    ��Name$ loadingImage��$y�Hidden=�$��2��
�������._��� $��Center=�$��Center=�$��Uniform=�$ Images/loading.gif��$��Images/Exclaimation.png���� -   $ 	FundImage��$��2��$#��Images/mufapMarketSummary.jpg��$$ Images/mufapMarketSummary2.gif��$��Center=�$��Center=�$
��Fill=��� -    ��Name$ list1��$��2��$
��Auto��$y�Hidden=�$��/Fonts/#Lato)�$
��Left=� � 	Resources  �� 
TargetType �� Property �� Value 
  PkrvId��%^   '��       &	  %     �� " ��.���  � ��$
 Left=��� " ��.���  � ��
 ����� �� View �� -   gridView�� �� AllowsColumnReorder$
 true�� �� ColumnHeaderToolTip$ MUFAP PKRV Summary��.P��� -   col1�� �� Width$ 50������ ���#455A64���#fff$��16(�$��Sr. No.��$��2q� �� CellTemplate �� .R��� $��50��$��Right=�$ Right=����� *PkrvId+�� -   col2��$	 260������ ���#455A64���#fff$��16(�$��Tenor��$��2q� �� .R��� $	��260��$��Right=�$ Right=����� *Tenor+�� -   col3��$	 270������ ���#455A64���#fff$��16(�$��Mid Rate��$��2q� 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Right=�$	��270��.+��� $��Right=�$ Right=����� *	MidRate+�� -   col4��$	 270������ ���#455A64���#fff$��16(�$��Change��$��2q�   .R�� $��Right=�$	��270��.+��� $��Right=�$ Right=����� *Change+� $��3��
������� �� $��* ��� $��6* ��� $��* �.+��� -   $��	statusBar��$��Bottom=�$��0��$��Stretch=�$��Center=�
������ 
���   �$��1q�.@��� .���� -   $��	lblStatus��$��Status: Ready��$��/Fonts/#Lato)�$��11(�
������ �� $��1��$��14(�$��Center=�$,��&Data Source: http://www.mufap.com.pk/ ��$��Fonts/#Lato)�
���   �$��Center=�$
��Bold"��� -   $��
statusBar2��$��Bottom=�$��2��$��Right=�$��Bottom=�
������ 
���   �$��1q�.@��� $��Right=�$��Bottom=�$��50��
������ .���� 
������ ._��� -   $ 	imgStatus��$ Images/tick.gif��$��Images/tick.png��$��30��$��30��