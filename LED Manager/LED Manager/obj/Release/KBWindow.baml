   M S B A M L   `   `   `  ����   LED Manager    Wpf.KBWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $��KBWindowLed��$��Center=�$��Center=�  � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=����	#FFFFFFFF���	#FF000000$	��540�� � Topmost . $	��232�� � Title$ LED Keyboard Setup��
 � Top$ 13�� � WindowStyle$ SingleBorderWindow=�.��� $	��518��$	��186�����	#FF2B2B2B � ColumnDefinitions .+��� -   $��	boxGreenL��$	��255��$��0��$
��Left=�$	��Top=�$��72��$��22��$
��Bold"�$��40.5,31,0,0q�$��0��$��Lucida Console)�$��20(� � Cursor$
 Hand��.������ .���� -   $��boxRedL��$	��255��$��0��$
��Left=�$	��Top=�$��72��$��22��$
��Bold"�$��
40.5,8,0,0q�$��0��$��Lucida Console)�$��20(�$
 Hand���� -   $��boxBlueL��$	��255��$��0��$
��Left=�$	��Top=�$��72��$��22��$
��Bold"�$��40.5,54,0,0q�$��0��$��Lucida Console)�$��20(����	#FFFFFFFF$��1,1,1,1q����	#FFABADB3$
 Hand���� -   $��	setLeftKB��$��Set Left��$
��Left=�$	��Top=�$��72��$��22��$
��Bold"�$��0��$��40.5,108,0,0q�$��0��$��1,1,1,1q�$��12(�
 � Tag$ 	KBsetLeft��$ ScrollW��$��Segoe UI)��� -   $��boxRedM��$	��255��$��0��$
��Left=�$	��Top=�$��72��$��22��$
��Bold"�$��	140,8,0,0q�$��0��$��Lucida Console)�$��20(�$
 Hand���� -   $��	boxGreenM��$	��255��$��0��$
��Left=�$	��Top=�$��72��$��22��$
��Bold"�$��
140,31,0,0q�$��0��$��Lucida Console)�$��20(�$
 Hand���� -   $��boxBlueM��$	��255��$��0��$
��Left=�$	��Top=�$��72��$��22��$
��Bold"�$��
140,54,0,0q�$��0��$��Lucida Console)�$��20(�$
 Hand���� -	   $��setMidKB��$��
Set Center��$
��Left=�$	��Top=�$��71.999999999999972��$��22��$
��Bold"�$��0��$��140,108,0,0q�$��0��$ KBsetMid��$ ScrollN���� -
   $��boxRedR��$	��255�����	#FFFFFFFF$
��Bold"�$��0��$
��Left=�$	��Top=�$��72��$��22��$��	238,8,0,0q�$��0��$��Lucida Console)�$��20(�$
 Hand���� -   $��	boxGreenR��$	��255��$��0��$	��Top=�$��22��$
��Bold"�$��0��$��Lucida Console)�$��20(�$��72��$��Right=�$��
0,31,208,0q�$
 Hand���� -   $��boxBlueR��$	��255��$��0��$	��Top=�$��22��$��0��$��Lucida Console)�$
��Bold"�$��72��$��Right=�$��
0,54,208,0q�$��20(�$
 Hand���� -   $��
setRightKB��$��	Set Right��$	��Top=�$��21.999999999999943��$
��Bold"�$��0��$��0��$ 
KBsetRight��$��72��$��Right=�$��0,108,208,0q�$ ScrollE���� -   $��setAllKB��$��Full KB��$
��Bold"�$��0��$��0��$
��Left=�$	��Top=�$��140,132,0,0q�$��72��$��22�����	#FF000000$ KBsetAll��$ ScrollWE���� -   $��redLeftLabel��$��R��$��10(����	#00000000���	#FFFF0000$
��Bold"�$��22��$
��23.5��$	��Top=�$
��Left=�$��0��$��24,8,0,0q�$��0���� -   $��greenLeftLabel��$��G��$��10(����	#FF00FF19$��0��$
��Left=�$	��Top=�$
��23.5��$��22��$
��Bold"�$��	24,31,0,0q�$��0���� -   $��redMidLabel��$��R��$��10(�$��0��$
��Left=�$	��Top=�$
��23.5��$��22�����	#FFFF0000$
��Bold"�$��121.5,8,0,0q�$��0���� -   $��greenMidLabel��$��10(�$��0��$
��Left=�$	��Top=�$
��23.5��$��22��$��G�����	#FF00FF19$
��Bold"�$��121.5,31,0,0q�$��0���� -   $��blueMidLabel��$��10(�$��0��$
��Left=�$	��Top=�$
��23.5��$��22��$��B�����	#FF3399FF$
��Bold"�$��121.5,54,0,0q�$��0���� -   $��redRightLabel��$��R��$��10(�$��0��$
��Left=�$	��Top=�$
��23.5��$��22�����	#FFFF0000$
��Bold"�$��	221,8,0,0q�$��0���� -   $��greenRightLabel��$��10(�$��0��$
��Left=�$	��Top=�$
��23.5��$��22��$��G�����	#FF00FF19$
��Bold"�$��
221,31,0,0q�$��0���� -   $��blueRightLabel��$��10(�$��0��$
��Left=�$	��Top=�$
��23.5��$��22��$��B�����	#FF3399FF$
��Bold"�$��
221,54,0,0q�$��0���� -   $��backlightSetBox��$
��Bold"�$��0��$
��Left=�$	��Top=�$��22��$	��160��$��324.5,8,0,0q�$��0��$ ScrollNS���� -   $��backlightTimeoutBox��$
��Bold"�$��0��$	��Top=�$��22��$��0��$��160.00000000000011��$��Right=�$��0,31,33.4999999999999,0q�$ ScrollNS���� -   $��setAllKBLOGO��$��12(�$
��Bold"�$��0��$��0��$
��Left=�$	��Top=�$��140,155,0,0q�$��72��$��22��$��
KB & Cover��$ KBsetAllCover��$ 	ScrollAll���� -   $��	LblMiddle��$��
As Center:�����	#FFFFFFFF$
��Bold"�$��31��$��72.957218249999983��$��
0,0,379,16q�$��Bottom=�$��Right=�$��0��$��0���� -   $��blueLeftLabel��$��B��$
��Bold"�$��10(����	#FF3399FF$��0��$��0��$
��Left=�$	��Top=�$��	24,54,0,0q�$
��23.5��$��22���� -   $��kbEffectBox��$
��Bold"�$��0��$��0��$��Right=�$	��Top=�$��0,54,33.4999999999998,0q�$��160.00000000000034��$��22��$ ScrollNS���� -   $��backlightLogoBox��$
��Bold"�$��0��$��0��$��Right=�$	��Top=�$��0,77,33.4999999999998,0q�$��160.00000000000023��$��22��$ ScrollNS���� -   $��lblor�����	#FFFFFFFF$
��Bold"�$��OR��$��1,0,0,0q����	#FFFFFFFF$��0��$��0��$��Right=�$��Bottom=�$+��%0,0,-3.41060513164848E-13,68.36655959q�$
��32.5��$��29���� -   $��buttonDefaults��$��
ExtraBlack"�$��Defaults�����	#FF7F1C1C$��22.000000000000028��$��72��$��	0,0,208,9q�$��Bottom=�$��Right=�$��0��$��0��$ No���� -    $��
buttonExit��$��X��$
��Bold"�$��0��$��0��$
��Left=�$��Bottom=�$��485.5,0,0,9q�$��24.499999999999659��$��22�����	#FF7F1C1C$ No���� -!   $��colorLabelLeft�����	#FF51A8FF$�� ��|�. $��1,1,1,1q�$	 Pen�����	#FFABADB3$��0��$��0��$
��Left=�$	��Top=�$��40.5,77,0,0q�$��72��$��22��$
 Left���� -"   $��colorLabelMid�����	#FF51A8FF$�� ��|�. $��1,1,1,1q�$	 Pen�����	#FFABADB3$��0��$��0��$	��Top=�$��22��$��72��$
��Left=�$��
140,77,0,0q�$	 Mid���� -#   $��colorLabelRight�����	#FF51A8FF$�� ��|�. $��1,1,1,1q�$	 Pen�����	#FFABADB3$��0��$��0��$��Right=�$	��Top=�$��
0,77,208,0q�$��72��$��22��$ Right���� -$   $��colorLabelLogo�����	#FF51A8FF$�� ��|�. $��1,1,1,1q�$	 Pen�����	#FFABADB3$��0��$��0��$
��Left=�$	��Top=�$��412.5,108,0,0q�$��72��$��22��$ Cover���� -%   $��lblCover�����	#FFFFFFFF$��
Cover RGB:��$
��Bold"�$��30��$��330.54278175,108,0,0q�$	��Top=�$
��Left=�$��0��$��0��$��72.95721825���� $��0��$��0��$
��Left=�$��Bottom=�$&�� 340.1878978,0,0,5.09955540999999q�$��136.31210220000008��$��25.90044459�����	#FFCE9B3D$��Bugs? rzrwolf@ya.ru��	 � RowDefinitions	 