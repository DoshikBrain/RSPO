   M S B A M L   `   `   `  ���� C  ?Sub_Zero, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Sub_ZeroSub_Zero      Sub_Zero.LogWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.FeatherIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.JamIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.BoxIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag	 cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah
 dMahApps.Metro.IconPacks.Zondicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.VaadinIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.SimpleIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Entypo, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.FontAwesome, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.ForkAwesome, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Typicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Unicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Ionicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a^ ZMahApps.Metro.IconPacks, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.WeatherIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Coolicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai eMahApps.Metro.IconPacks.PicolIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Octicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Fontisto, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.FileIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.MaterialLight, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.PixelartIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.MaterialDesign, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Codicons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Modern, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.EvaIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am  iMahApps.Metro.IconPacks.BootstrapIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai! eMahApps.Metro.IconPacks.RPGAwesome, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah" dMahApps.Metro.IconPacks.Fontaudio, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah# dMahApps.Metro.IconPacks.RemixIcon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af$ bMahApps.Metro.IconPacks.Microns, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai% eMahApps.Metro.IconPacks.RadixIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137axIcon-http://metro.mahapps.com/winfx/xaml/iconpacks!     	 
                        ! " # $ % zd2http://schemas.microsoft.com/expression/blend/2008!     	 
                        ! " # $ % �mc;http://schemas.openxmlformats.org/markup-compatibility/2006!     	 
                        ! " # $ % blocalclr-namespace:Sub_Zero!     	 
                        ! " # $ %   � Title$  	LogWindow��$	��720��$
��1088�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency . 
������  � 	Resources    	BoolToVis��%   &	        �� .���� -   ���#eff2ef �� CornerRadius$ 30��._��  � ColumnDefinitions �� $	��200 ��� $��* �.+��  � RowDefinitions �� $
��auto ��� $��* �.+��� $��
20 28 0 35q�  	topButton#��[ .��. 	 (MahApps.Metro.IconPacks.PackIconMaterial     Kind$
 Menu=�$��15��$��15���� $��1�����#623ed0$ 	0 50 0 30��._�]� .+��� $��80��$��80��	 �� Padding$	 3q�
�������$	 100��$	��Top=�$
��Left=�$��
20 -30 0 0q�._�A� -   ���� 
 �� ImageSource$
 /ImageUsers/Patrick.jpg���� $!��Главное это вы.��
�������$��19(�$��SemiBold"�$	��Top=� �� TextAlignment$ Center=�$��	5 25 0 10q��� $��25q�$	��Top=�$	��0.8�����#7e5dfc�� $	��Top=�.+���   
menuButton#��[ .����  �� Orientation$ 
Horizontal=�.+�  $ HomeVariant=�  menuButtonIcon#��[ �� $+��%Панель Инструментов���� -   #��[ .���� $ 
Horizontal=�.+�  $ FileDocumentMultiple=�#��[ �� $+��%Распичатаь документ���� -    ��Name$ btZacazOrCar��#��[ .���� $ 
Horizontal=�.+�  $ PacMan=�#��[ �� -    ��Name$ 	txtBlock2��$��Заказчики���� #��[ .���� $ 
Horizontal=�.+�  $ Timer=�#��[ �� $!��Скоро появится���� -   #��[ $��Bottom=�$��Bottom=�$��
15 0 15 20q�.���� $ 
Horizontal=�.+�  $ Logout=�#��[ �� $��
Выход��� $��1��$��30 20 20 20q� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $
��auto �.+�� $	��300��$
��Left=�.+��� $��	20,0,30,0q�$��Center=����#b0b9c6 J� IsHitTestVisible .   K� ZIndex$ 1��$��Поиск ... ��y���  �� ElementName$ FindOnTable�� �� Path$ Text.IsEmpty� �� 	Converter# [  �� -    ��Name$ FindOnTable��  textBoxSearch#��[   $ Magnify=�$��15��$��15��$��Center=�$��0 0 15 0q����#b0b9c6$��Right=��� #��[ $��0 0 20 0q����#dcdcdc$��Right=�.��  $	 Cog=��� #��[ $��0 0 55 0q�$��Right=�.��  $
 Bell=��� -   $ txtBlockNameTable��$#��Таблица посылок��$��1��$��32(�$��SemiBold"�$��0 20 0 0q�$
��Left=����#121518�� -	   $ btAddBoxOrCar��  	AddButton#��[ $
��Left=�$��
638,24,0,4q�$��1��.���� $ 
Horizontal=�.+�  $
 Plus=�$��Center=�$��11��$��11��$��0 1 8 0q��� -
   $ txtBlock��$0��*Добавить новую посылку���� $��1��$��3�����#dae2ea$��	0 -1 0 10q��� $��2��$ 
Horizontal=�$��0 10 0 0q�.+��� -   $ btName��$��Название��  	tabButton#��[ $��79���� -   $ btWeight��$��Вес��#��[ $��58���� -   $ btDimension��$��Размер��#��[ �� -   $ btCar��$��Автомобиль��#��[ $��99���� -   $ btCityOt��$!��Город доставки��#��[ $	��111���� -   $ btCityDo��$#��Город получения��#��[ $	��122��&   System.Windows.Controls.DataGrid  -     Name$ DgBox��$y�Hidden=�  DataGridStyle1#���    DataGridCellStyle1   	CellStyle# �   	 DataGridColumnHeaderStyle1   ColumnHeaderStyle# � 	  
 DataGridRowStyle1   RowStyle# � 
 $��5��   Columns 4  .System.Windows.Controls.DataGridCheckBoxColumn  ,  &System.Windows.Controls.DataGridColumn   CanUserResize .     Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto   CheckBoxStyle11  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [    HeaderTemplate �� .R��� #��[ 0  *System.Windows.Controls.DataGridTextColumn     Header$ #��   
IsReadOnly .  .  $
 auto     Binding  �� *Id+4  .System.Windows.Controls.DataGridTemplateColumn  $% Название посылки�� . $ * !   CellTemplate! �� .R��� $ 
Horizontal=�.+��� $��Center=����� *	NameBox+  $ Клиент�� .  .  $ *   �� *
IdCustomer+  $ Вес�� .  .  $ *   �� *Weight+  $ Размер�� .  .  $ *   �� *Value+  $! Город доставки�� .  .  $ *   �� *
SityDostav+  $# Город получения�� .  .  $ *   �� *
SityOtprav+  $ Автомбиль�� .  .  $ *   �� *IdCar+  $ Дата прибытия�� .  .  $ *   �� *Date+  $ Стоимость�� .  .  $ *   �� *Price+  $ Действия�� . $ * ! �� .R��� $ 
Horizontal=�.+���   gridEditButton#��[ .��  $ PencilOutline=�  gridButtonIcon#��[ ��   gridRemoveButton#��[ $��5 0 0 0q�.��  $ DeleteOutline=�#��[   -   $ DgCar��#���  # �  # � 	 # � 
 $��5��   $ #�� .  .  $
 auto   �� *Id+  $% Марка автомобиля�� . $ * ! �� .R��� $ 
Horizontal=�.+��� $��Center=����� *	MarkOfCar+  $ 
Номер�� .  . $ *   �� *	NumberCar+  $ Водитель�� .  .  $ *   �� *name+  $ Макс вес�� .  . $ *   �� *MaxDostupWeight+  $ Макс объём�� .  . $ *   �� *MaxDostupVelue+  $ Действия�� . $ * ! �� .R��� $ 
Horizontal=�.+��� -   #��[ .��  $ PencilOutline=�#��[ �� -   #��[ $��5 0 0 0q�.��  $ DeleteOutline=�#��[ 