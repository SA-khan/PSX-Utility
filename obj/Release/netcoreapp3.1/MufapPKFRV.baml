   M S B A M L   `   `   `  ���� M  IPSXDataFetchingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PSXDataFetchingAppPSXDataFetchingApp  #    PSXDataFetchingApp.MufapPKFRV   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  ,local clr-namespace:PSXDataFetchingApp    � Title$  Data Extractor Utility�� � Icon$ Images/web-scrap2.png��$	��600��$
��1250�� � WindowStartupLocation$ CenterScreen=�.���� $��10q�
�������._��  � RowDefinitions �� $��2* ��� $��2* ��� $	��14* ��� $��* � � ColumnDefinitions �� $��* �.+�� -    �Name$ HeaderColor��$��0�����#f0a500 �� $��* �.+��� -    ��Name$ HeaderImage��$��Images/BOP.png��$
��Left=�$��5q�"  WpfAnimatedGif.ImageBehavior   AnimatedSource$ Images/bop.gif��$
��Fill=�$	��200���� $��0��$��MUFAP PKFRV Summary��$��26(�$��/Fonts/#Lato)�$
��Bold"�$��Center=� �� TextAlignment$ Center=�$��Center=�$��0 0 10 0q�� $��0��$��Right=�$��5q� �� $��* ��� $��* �.+��� -   	 ��Name$	 txtDate��$��0��$��/Fonts/#Lato)�$��Bottom=�$��0 0 2 0q��� -   $	 	txtStatus��$��1��$��/Fonts/#Lato)�$	��Top=�$��0 2 0 0q�� $��1��$��1 5 1 5q����#fff$��30�� �� $��* ��� $��4* ��� $��2* �$��60���� $��2* �$��60���� $��2* �$��60���� $��* ��� $��2* ��� $��* ��� $��2* ��� $��2* �.+��� -   
 ��Name$
 btnBack��$��0��$��/Fonts/#Lato)�$��25��$��Center=�$��70��$
��Left=�$��5 0 0 0q� � ToolTip$ Go To Main Menu������ .+�� $��70�� �� $��* ��� $��2* �.+��� $��Images/back.png��$��Uniform=�$��Center=�$��Center=�$��5 0 0 0q��� $��1��$
��Back��$��Fonts/#Raleway)�$��14(�$��Center=�$��Center=�$��0 0 5 0q�(  "System.Windows.Controls.DatePicker  -   $��pkfrvDatepicker��$��1��$��Tahoma)�$	��150��$��Stretch=�$��2q�$��25��$: 4Select Date to fetch PKFRV Data from MUFAP Website..��   DisplayDateStart
 1/1/09�� -   $
 btnGet��$��2��$��/Fonts/#Lato-Bold)�$��13(�$��50��$��50��$��30��$��Right=�$��Center=�
������ 
������ $D >Click to fetch selected date PKFRV details from MUFAP website.������ $��Center=�$��Center=�.+��� $��Images/play.png��$
��Fill=�$��40���� -   $
 btnSave��$��3��$��/Fonts/#Lato-Bold)�$��13(�$��30��$��30��$��Center=�$��Center=�
�������
������ $ Generate Excel Report������ $��Center=�$��Center=�$��30��.+��� $��Images/excel.jpg��$��Uniform=�$��30���� -	   $
 btnReset��$��4��$��/Fonts/#Lato-Bold)�$��13(�$��30��$��Center=�$��Center=�
�������
������ ���� $��Center=�$��Center=�$��50��$��30��.+��� $��Images/reset.jpg��$��Uniform=�$��40��$ Refresh Information���� -
   $	 	lblFundID��$��5��$y�Hidden=�$��FUND ID:��$
��Bold"�
�������$��/Fonts/#Lato)�$��Center=�$��Right=�$��5q��� -   $��	txtFundID��$��6��$y�Hidden=�${�False��$��/Fonts/#Lato)�$	��120��$��Stretch=�$��5q�$��Center=��� -   $	 lblFundNAME��$��7��$��
FUND NAME:��$y�Hidden=�$
��Bold"�
�������$��/Fonts/#Lato)�$��Center=�$��Right=�$��5q��� -   $��txtFund��$��8��$y�Hidden=�${�False��$��/Fonts/#Lato)�$	��120��$��Stretch=�$��5q�$��Center=��� -   $
 btnAdd��$��9��$y�Hidden=�$��5q�$	��ADD��${�False��$��/Fonts/#Lato)�$��13(�$��70��$��20��$��Stretch=�$��Center=��� -    ��Name$ loadingImage��$y�Hidden=�$��2��
�������._��� $��Center=�$��Center=�$��Uniform=�$ Images/loading.gif��$��Images/Exclaimation.png���� -   $ 	FundImage��$��2��$��Images/uploadPSX.jpg��$��Center=�$��Center=�$
��Fill=��� -    ��Name$ list1��$��2��$
��Auto��$y�Hidden=�$��/Fonts/#Lato)�$
��Left=� � 	Resources  �� 
TargetType �� Property �� Value   	txtAppDep��%^   '��       &	  %     �� " ��.���  � ��$
 Left=��� " ��.���  � ��
 �  �� �� View �� -   gridView�� �� AllowsColumnReorder$
 true�� �� ColumnHeaderToolTip$ MUFAP PKFRV Summary��.P��� -   col1�� �� Width$ 50������ ���#455A64���#fff$��16(�$��Sr. No.��$��2q� �� CellTemplate �� .R��� $
��Left=�$
 Left=����� *	PkfrvId+�� -   col2��$	 150������ ���#455A64���#fff$��16(�$��Floating Rate Bond��$��2q� �� .R��� $
��Left=�$
 Left=����� *FloatingRateBond+�� -   col3��$	 105������ ���#455A64���#fff$��16(�$��
Issue Date��$��2q� 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Right=�$	��105��.+��� $��Right=�$ Right=����� *IssuanceDate+�� -   col4��$	 115������ ���#455A64���#fff$��16(�$��Maturity Date��$��2q�   .R�� $��Right=�$	��115��.+��� $��Right=�$ Right=����� *MaturityDate+�� -   col5��$	 145������ ���#455A64���#fff$��16(�$��Coupon Frequency��$��2q�   .R�� $��Right=�$	��145��.+��� $��Center=�$ Center=�$	��145������ *CouponFrequency+�� -   col6��$ 70������ ���#455A64���#fff$��16(�$	��BMA��$��2q�   .R�� $��Right=�$��70��.+��� $��Right=�$ Right=����� *BMA+�� -   col7��$ 70������ ���#455A64���#fff$��16(�$��CANDM��$��2q�   .R�� $��Right=�$��70��.+��� $��Right=�$ Right=����� *CANDM+�� -   col8��$ 70������ ���#455A64���#fff$��16(�$
��CMKA��$��2q�   .R�� $��Right=�$��70��.+��� $��Right=�$ Right=����� *CMKA+�� -   col9��$ 70������ ���#455A64���#fff$��16(�$
��IONE��$��2q�   .R�� $��Right=�$��70��.+��� $��Right=�$ Right=����� *IONE+�� -   	col10��$ 70������ ���#455A64���#fff$��16(�$
��JSCM��$��2q�   .R�� $��Right=�$��70��.+��� $��Right=�$ Right=����� *JSCM+�� -   	col11��$ 70������ ���#455A64���#fff$��16(�$
��MCPL��$��2q�   .R�� $��Right=�$��70��.+��� $��Right=�$ Right=����� *MCPL+�� -   	col12��$ 70������ ���#455A64���#fff$��16(�$
��SCPL��$��2q�   .R�� $��Right=�$��70��.+��� $��Right=�$ Right=����� *SCPL+�� -   	col13��$ 70������ ���#455A64���#fff$��16(�$
��VCPL��$��2q�   .R�� $��Right=�$��70��.+��� $��Right=�$ Right=����� *VCPL+�� -    	col14��$ 70������ ���#455A64���#fff$��16(�$	��FMA��$��2q�   .R�� $��Right=�$��70��.+��� $��Right=�$ Right=����� *FMA+� $��3��
������� �� $��* ��� $��6* ��� $��* �.+��� -!   $��	statusBar��$��Bottom=�$��0��$��Stretch=�$��Center=�
������ 
���   �$��1q�.@��� .���� -"   $��	lblStatus��$��Status: Ready��$��/Fonts/#Lato)�$��11(�
������ �� $��1��$��14(�$��Center=�$+��%Data Source: http://www.mufap.com.pk/��$��Fonts/#Lato)�
���   �$��Center=�$
��Bold"��� -#   $��
statusBar2��$��Bottom=�$��2��$��Right=�$��Bottom=�
������ 
���   �$��1q�.@��� $��Right=�$��Bottom=�$��50��
������ .���� 
������ ._��� -$   $ 	imgStatus��$ Images/tick.gif��$��Images/tick.png��$��30��$��30��