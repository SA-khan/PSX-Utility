   M S B A M L   `   `   `  ���� M  IPSXDataFetchingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:PSXDataFetchingAppPSXDataFetchingApp  &     PSXDataFetchingApp.Configuration   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:PSXDataFetchingApp     -     �Name$  MainWindow1�� � WindowStartupLocation$ CenterScreen=� � Title$ Data Extractor Utility��$	��600��$
��1000��.���� $��2q�
�������._��  � RowDefinitions �� $��2* �$��60���� $��* �$��50���� $��4* ��� $��* �$��30��.+�� -    �Name$ Header��$��0��$��Center=� � ColumnDefinitions �� $��* �$	��100���� $��3* ��� $��4* �.+��� 
������  �� Padding$ 5q�._��� -    ��Name$ 
ClientLogo��$��Images/BOP.png��$��70��$��Bottom=��� $��0��$��1��
������  �� Orientation$ 
Horizontal=�$��10 0 0 0q�.+��� $��C��$��Bottom=�$
��Left=�$��28(�$��
Font/#Lato)�$��1 0 0 -2q��� $��O��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��N��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��F��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��I��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��G��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��U��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��R��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��A��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��T��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��I��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��O��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��N��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��-��$��Bottom=�$
��Left=�$��22(�$��
Font/#Lato)�$��1 0 0 2q�
������ �� $��M��$��Bottom=�$
��Left=�$��22(�$��
Font/#Lato)��� $��A��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��N��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��A��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��G��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��E��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��R��$��Bottom=�$
��Left=�$��16(�$��
Font/#Lato)�$��1 0 0 2q��� $��1��$ 5q�
�������$��40��._�� $��Center=� �� $��* �$��70���� $��* ��� $��* ��� $��* �.+��� -   	 ��Name$	 btnBack��$��0��$��/Fonts/#Lato)�$��30��$��Center=�$��70��$��Center=�$��0 0 0 0q�
 � ToolTip$
 Go To Main Menu������ .+�� $��70�� �� $��* ��� $��2* �.+��� $��Images/back.png��$��Uniform=�$��20��$��Right=�$��Center=��� $��1��$
��Back��$��Center=�$
��Left=�$��13(�$��Fonts/#Raleway)��� $��3��._��  �� $��* ��� $��* �.+�(  "System.Windows.Controls.DatePicker  -     Name$ pkrvDatepicker��$��0��   SelectedDate 
2020-12-15$��Center=��� -   $	 btnUploadScripSymbols��$��1��$��/Fonts/#Lato)�$��30��$��Center=�$	��150��$��Right=�$��0 0 0 0q�$2
 ,Upload Pakistan Stock Exchange Scrip Symbols������ .+�� $	��150�� �� $��* ��� $��4* �.+��� $��Images/add.jpg��$��Uniform=�$��20��$��Center=�$��Center=��� $��1��$��Upload Symbols��$��Center=�$
��Left=�$��13(�$��Fonts/#Raleway)�� -   $
 Body��$��2�� �� $��* ��� $��* �$��40��$��40���� $��* �$��40��$��40���� $��* �$��40��$��40���� $��* �$��40��$��40���� $��* �$��40��$��40���� $��* �$��40��$��40���� $��* �$��40��$��40���� $��* �$��40��$��40���� $��2* �.+�� $��1�� �� $��* ��� $��2* ��� $��3* ��� $��* �.+��� $��1��$��Enter Client: ��$��14(�$��Fonts/#Lato)�$��Center=�$
��Left=� �� Padding$ 5q��� -    ��Name$ 	txtClient��$��2��$��30�� �� TextAlignment$
 Left=�$��15(�$��5q�$��5 0 5 0q��� -	   $	 	btnClient��$��3��$��30��$��80��$��80��$
��Left=�$��UPDATE��� $��2�� �� $��* ��� $��2* ��� $��3* ��� $��* �.+��� $��1��$!��Primary Connection String: ��$��14(�$��Fonts/#Lato)�$��Center=�$
��Left=�$ 5q��� -
   $ txtPrimaryConnection��$��2��$��30��$
 Left=�$��15(�$��5q�$��5 0 5 0q��� -   $	 btnPrimaryConnection��$��4��$��30��$��80��$��80��$
��Left=�$��UPDATE��� $��3�� �� $��* ��� $��2* ��� $��3* ��� $��* �.+��� $��1��$#��Secondary Connection String: ��$��14(�$��Fonts/#Lato)�$��Center=�$
��Left=�$ 5q��� -   $ txtSecondaryConnection��$��2��$��30��$
 Left=�$��15(�$��5q�$��5 0 5 0q��� -   $	 btnSecondaryConnection��$��3��$��30��$��80��$��80��$
��Left=�$��UPDATE��� $��4�� �� $��* ��� $��2* ��� $��3* ��� $��* �.+��� $��1��$��Is Symbol Exist: ��$��14(�$��Fonts/#Lato)�$��Center=�$
��Left=�$ 5q��� -   $ txtCompanyName��$��2��$��30��$
 Left=�$��15(�$��5q�$��5 0 5 0q��� -   $	 btnCompanyCheck��$��3��$��30��$��80��$��80��$
��Left=�$��CHECK���� -    ��Name$ lblCompanyStatus��$��1��$��STATUS ��$��14(�$��Fonts/#Lato)�$��Center=�$��Right=�$ 5q�� $��5�� �� $��* ��� $��2* ��� $��3* ��� $��* �.+��� $��1��$#��Insert Symbol ( ID , NAME ): ��$��14(�$��Fonts/#Lato)�$��Center=�$
��Left=�$ 5q��� -   $ txtID��$��1��$��30��$��40��$��60��$
 Left=�$��15(�$��5q�$��Right=��� -   $ txtName��$��2��$��30��$
 Left=�$��15(�$��5q�$��5 0 5 0q��� -   $	 btnInsertCompany��$��4��$��30��$��80��$��80��$
��Left=�$��INSERT��� $��6�� �� $��* ��� $��2* ��� $��3* ��� $��* �.+��� $��1��$��Symbol By Name: ��$��14(�$��Fonts/#Lato)�$��Center=�$
��Left=�$ 5q��� -   $ txtCompanyNameSecondary��$��2��$��30��$
 Left=�$��15(�$��5q�$��5 0 5 0q��� -   $$	 btnCompanySymbolCheckSecondary��$��3��$��30��$��80��$��80��$
��Left=�$��CHECK���� -   $$ lblCompanySymbolCheckSecondary��$��1��$��STATUS ��$��14(�$��Fonts/#Lato)�$��Center=�$��Right=�$ 5q�� $��7�� �� $��* ��� $��2* ��� $��3* ��� $��* �.+��� $��1��._�� .+��� $��Popup Alerts ��$��14(�$��Fonts/#Lato)�$��Center=�$
��Left=�$ 5q��� -    ��Name$ 
checkAlert��$��15��$��15(�$��15 5 5 5q�$��Center=�$��Right=�$��5q��� $��2��$ 	105 0 0 0q�._�� .+��� $��Email Alerts ��$��14(�$��Fonts/#Lato)�$��Center=�$
��Left=�$ 5q��� -   $ 
checkEmail��$��15��$��15(�$��15 5 5 5q�$��Center=�$��Right=�$��5q��� -   $	 btnAlert��$��3��$��30��$��80��$��80��$
��Left=�$��UPDATE���� -   $ 	lblAltert��$��1��$�� ��$��14(�$��Fonts/#Lato)�$��Center=�$��Right=�$ 5q�� $��8�� �� $��* ��� $��2* ��� $��3* ��� $��* �.+��� $��1��$��Fund Closing Percentage ��$��14(�$��Fonts/#Lato)�$��Center=�$
��Left=�$ 5q��� -   $ txtClosingPercentage��$��2��$��30��$
 Left=�$��15(�$��5 0 5 0q��� -   $	 btnClosingPercentage��$��3��$��30��$��80��$��80��$
��Left=�$��UPDATE���� -   $ lblClosingPercentage��$��1��$�� ��$��14(�$��Fonts/#Lato)�$��Center=�$��Right=�$ 5q�� -   $ 	statusBar��$��3��
������� �� $��* ��� $��4* ��� $��* �.+��� 
������ $��Center=�$
��Left=�$��5 0 0 0q�.@��� -   $ 	lblStatus��$��Status: Ready��$��14(��� $��3��
������ $��Right=�$��0 0 5 0q�.@��� -    $ 	imgStatus��$��Images/tick.png��$��Right=�$��Center=�