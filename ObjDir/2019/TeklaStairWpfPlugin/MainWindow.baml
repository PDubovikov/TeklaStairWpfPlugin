   M S B A M L   `   `   `  ����a  ]Tekla.Structures.Dialog, Version=2020.0.0.0, Culture=neutral, PublicKeyToken=2f04dbe497b71114xQclr-namespace:Tekla.Structures.Dialog.UIControls;assembly=Tekla.Structures.Dialog"Tekla.Structures.Dialog.UIControls  5      N JTeklaStairWpfPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TeklaStairWpfPluginTeklaStairWpfPlugin 5      bFclr-namespace:Tekla.Structures.Dialog;assembly=Tekla.Structures.DialogTekla.Structures.Dialog  5	      $   TeklaStairWpfPlugin.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      h
UIControlsQclr-namespace:Tekla.Structures.Dialog.UIControls;assembly=Tekla.Structures.Dialog     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:TeklaStairWpfPlugin     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      VtsdFclr-namespace:Tekla.Structures.Dialog;assembly=Tekla.Structures.Dialog     5	      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
        � Title$  Лестница 1.450��5      $	��500��5      $	��800��6    � 
ResizeMode$ NoResize=�6   $��Stretch=�5       � 	Resources 5      .  (TeklaStairWpfPlugin.EmptyStringConverter   StringEmptyConverter+  %TeklaStairWpfPlugin.MinValueConverter  ValueMinConverver �� MergedDictionaries �� Source�� 5   
    5      �� 5      $e _pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml?�6%   �� 5      $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml?�6%   �� 5      $z tpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml?�6%   �� 5      $s mpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml?�6%   %:   &	        &	        5      6)     5      6&   5      .��5      �  � ColumnDefinitions 5   
   �� 5       $��* �6    � RowDefinitions 5"   
   �� 5#      $
��Auto �6   $��24��6*   �� 5$      �� 5%      $
��Auto �6   $��24��6*   .+�5'   
   #  MaterialDesignThemes.Wpf.Card    MaterialDesignBackground#���  6   $��0��6V   $��8(�6c   $��Calibri)�6p   .��5(      4   .Tekla.Structures.Dialog.UIControls.WpfSaveLoad  $��0��5)      $��0,0,0,0q�5*      $��Stretch=�5+      $	��Top=�5,      5-        5/   
   #���  6   $��1��6V   .��50      �� .��51      �� .+�52      9� $��Stretch=�6   $��Лестницы��6=   $��16(�6O   $��Calibri)�6]   $
��Bold"�6r   .��53      ��  ) %MaterialDesignNavigatilRailTabControl#��[ 6%   $	��250��6d    �� SelectedIndex 6t   �� *StairTypeIndex+ �� Mode$ TwoWay=�	 �� 	Converter#	 [ 
 �� ConverterParameter$
 1��.@�54      ��  ' #MaterialDesignNavigationRailTabItem#��[ 6&   $��1��6c   $��1��6m   $y�	Collapsed=�6x   �� 55      #��[ 6&   $	��100��6c   $	��100��6o   ��56   "   �� 57   &   $��Images/Type12_256p.png��6,   $��74��6L   $��70��6X   .��59   "   � .+�5:   &   �� $��Images/Type16_backOff.png��6,   $��21,0,356.6,5.6q�6O   �� 5;   &   $
��Left=�6.   $��30��6I   $��	42,96,0,0q�6U    �� TextWrapping$
 Wrap=�6h   $	��Top=�6|   $��70��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   $��14(�5<   .   $��Calibri)�6<   $
��Bold"�6Q   ��5=   .   �� *StairOffset+$ TwoWay=�#	 [ $

 1000��.��6�   z�5>   *   V� 5?   .   .~�5@   2   �� �� 5A   2    �� AngleX$ -0.062Q�6@   �� 5B   2    �� Angle$ 12.793Q�6B   T� 5C   2   5D   /   .��5E   C   5F   '   �� 5G   &   $
��Left=�6.   $��30��6I   $��234,210,0,0q�6U   $
 Wrap=�6j   $	��Top=�6~   $��59��6�   $ 0.5,0.5&�6�   $��14(�5H   .   $��Calibri)�6<   $
��Bold"�6Q   ��5I   .   �� *
StairWidth+$ TwoWay=�#	 [ $	
 700��.��6�   z�5J   *   V� 5K   .   .~�5L   2   �� �� 5M   2   $ 0.049Q�6@   �� 5N   2   $ -10.12Q�6B   T� 5O   2   5P   /   .��5Q   C   5R   '   �� 5S   &   $
��Left=�6/   $��118,186,0,0q�6J   $	��Top=�6_   $��40��6w   $��30��6�   $��14(�6�   $
��Bold"�6�   $��Calibri)�6�    6�   �� *AngleOfStairIndex+$ TwoWay=�#	 [ $
 0��.@�5T   *   ��  �� 
IsSelected . .��6I   456M   �� 5U   *   .��67   606;   5V   '   �� 5X   &   $��Профиль��6,   $
��Left=�6>   $��
287,69,0,0q�6Y   $	��Top=�6m   $��73��6�   $��14(�6�   $��Calibri)�6�   $
��Bold"�6�   �� 5Y   &   $��Материал��6,   $
��Left=�6?   $��
287,98,0,0q�6Z   $	��Top=�6n   $ 
0.31,0.398&�6�   $��73��6�   $��14(�6�   $��Calibri)�6�   $
��Bold"�6�   :   4Tekla.Structures.Dialog.UIControls.WpfFilterCheckBox  5Z   &   $	��Top=�6C   $
��Left=�6[   $��
365,77,0,0q�6v      AttributeName$ stringerProfile��6�   �� 5[   &   -   5         ��Name$ txtBox_ProfileStringers��5[   .   $	��120��6O   $��30��6[   $	��Top=�6g   $
��Left=�6   $
 Wrap=�5]   -   $��
385,65,0,0q�6A   $��14(�6U   ��5\   -   �� *StringerProfile+$ TwoWay=�#	 [  $
 U16P_8240_97��5]   U   :   4Tekla.Structures.Dialog.UIControls.WpfProfileCatalog  5^   &   -   5          Name$ prflStringerCatalog��5^   C   $	��Top=�6`   $
��Left=�6x   $��
520,70,0,0q�5_   _   $ 0.604,-1.527&�6s     5`   &   $
��Left=�6C   $	��Top=�6^   $��365,106,0,0q�6v   $ stringerMaterial��6�   �� 5a   &   -   5        $ txtBox_MaterialStringers��5a   .   $	��120��6P   $��30��6\   $	��Top=�6h   $
��Left=�6�   $��
385,95,0,0q�6�   $
 Wrap=�5b   �   $��14(�6�   ��6.   �� *StringerMaterial+$ TwoWay=�#	 [  $
 C245-4��6�   ;   5Tekla.Structures.Dialog.UIControls.WpfMaterialCatalog  5c   &   -   5          Name$ mtrlStringersCatalog��5c   D   $	��Top=�6b   $��0q�6z   $��6(�6�   $
��Left=�6�   $��520,105,0,0q�6�   5e   -   5g   #   5i      �� 5j      #��[ 6&   $	��100��6c   $	��100��6o   ��5k   "   �� 5l   &   $��Images/Type21_256.png��6,   $��74��6K   $��70��6W   .��5n   "   � .+�5o   &   �� $��Images/Type2_backOff.png��6,   $
��Left=�6N   $	��230��6i   $��	65,10,0,0q�6v   $	��Top=�6�   $	��295��6�   �� 5p   &   $
��Left=�6.   $��28��6I   $��207,196,0,0q�6U   $
 Wrap=�6j   $	��Top=�6~   $��70��6�   $ 0.5,0.5&�6�   $��14(�5q   .   $��Calibri)�6<   $
��Bold"�6Q   ��5r   .   �� *
StairWidth+$ TwoWay=�#	 [ $	
 700��.��6�   z�5s   *   V� 5t   .   .~�5u   2   �� �� 5v   2   �� 5w   2   $ -10.373Q�6B   T� 5x   2   5y   /   .��5z   C   5{   '   �� 5|   &   $
��Left=�6/   $��
76,168,0,0q�6J   $	��Top=�6^   $��40��6v   $��30��6�   $��14(�6�   $��Calibri)�6�   $
��Bold"�6�    6�   �� *AngleOfStairIndex+$ TwoWay=�#	 [ $
 0��.@�5}   *   ��  . .��6I   456M   �� 5~   *   .��67   606;   5   '   �� 5�   &   $��Профиль��6,   $
��Left=�6>   $��
287,69,0,0q�6Y   $	��Top=�6m   $��73��6�   $��14(�6�   $��Calibri)�6�   $
��Bold"�6�   �� 5�   &   $��Материал��6,   $
��Left=�6?   $��
287,98,0,0q�6Z   $	��Top=�6n   $ 
0.31,0.398&�6�   $��73��6�   $��14(�6�   $��Calibri)�6�   $
��Bold"�6�     5�   &   $	��Top=�6C   $
��Left=�6[   $��
365,77,0,0q�6v   $ stringerProfile��6�   �� 5�   &   -   5        $ textBox_ProfileStringers��5�   .   $	��120��6P   $��30��6\   $	��Top=�6h   $
��Left=�6�   $
 Wrap=�5�   -   $��
385,65,0,0q�6A   $��14(�6U   ��5�   -   �� *StringerProfile+$ TwoWay=�#	 [  $
 U16P_8240_97��5�   U     5�   &   -   5        $ profileStringerCatalog��5�   C   $	��Top=�6c   $
��Left=�6{   $��
520,71,0,0q�5�   b   $ 0.604,-1.527&�6v     5�   &   $
��Left=�6C   $	��Top=�6^   $��365,106,0,0q�6v   $ stringerMaterial��6�   �� 5�   &   -   5        $ textBox_MaterialStringers��5�   .   $	��120��6Q   $��30��6]   $	��Top=�6i   $
��Left=�6�   $��
385,95,0,0q�6�   $
 Wrap=�5�   �   $��14(�6�   ��6.   �� *StringerMaterial+$ TwoWay=�#	 [  $
 C245-4��6�     5�   &   -   5        $ materialStringersCatalog��5�   D   $	��Top=�6f   $��0q�6~   $��6(�6�   $
��Left=�6�   $��520,106,0,0q�6�   5�   -   5�   #   5�      �� 5�      #��[ 6&   $	��100��6c   $	��100��6o   ��5�   "   �� 5�   &   $��Images/Type31_256p.png��6,   $��74��6L   $��70��6X   .��5�   "   � .+�5�   &   �� $��Images/Type3_backOff.png��6,   $
��Left=�6N   $	��230��6i   $��	45,30,0,0q�6v   $	��Top=�6�   $	��295��6�   �� 5�   &   $
��Left=�6.   $��28��6I   $��247,215,0,0q�6U   $
 Wrap=�6j   $	��Top=�6~   $��70��6�   $ 0.5,0.5&�6�   $��14(�5�   .   $��Calibri)�6<   $
��Bold"�6Q   ��5�   .   �� *
StairWidth+$ TwoWay=�#	 [ $	
 700��.��6�   z�5�   *   V� 5�   .   .~�5�   2   �� �� 5�   2   �� 5�   2   $ -16.192Q�6B   T� 5�   2   5�   /   .��5�   C   5�   '   �� 5�   &   $
��Left=�6.   $��28��6I   $��	60,80,0,0q�6U   $
 Wrap=�6h   $	��Top=�6|   $��60��6�   $��14(�6�   $��Calibri)�6�   $
��Bold"�6�   $ 0.5,0.5&�5�   .   ��6N   �� *StairOffset+$ TwoWay=�#	 [ $

 1000��.��6�   z�5�   *   V� 5�   .   .~�5�   2   �� �� 5�   2   �� 5�   2   $ 14.52Q�6B   T� 5�   2   5�   /   .��5�   C   5�   '   �� 5�   &   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ OS��6/   $
��Left=�6S   $��107,177,0,0q�6n   $	��Top=�6�   $��40��6�   $��30��6�   $��14(�5�   /   $��Calibri)�6=   $
��Bold"�6R    5�   /   �� *AngleOfStairIndex+$ TwoWay=�#	 [ $
 0��.@�5�   *   ��  . .��6I   456M   �� 5�   *   .��67   606;   5�   '   �� 5�   &   $��Профиль��6,   $
��Left=�6>   $��
287,69,0,0q�6Y   $	��Top=�6m   $��67��6�   $��14(�6�   $��Calibri)�6�   $
��Bold"�6�   �� 5�   &   $��Материал��6,   $
��Left=�6?   $��
287,98,0,0q�6Z   $	��Top=�6n   $ 
0.31,0.398&�6�   $��68��6�   $��14(�6�   $��Calibri)�6�   $
��Bold"�6�     5�   &   $	��Top=�6C   $
��Left=�6[   $��
365,77,0,0q�6v   $ stringerProfile��6�   �� 5�   &   -	   5        $  txtBox_ProfileStringersExt��5�   .   $	��120��6R   $��30��6^   $	��Top=�6j   $
��Left=�6�   $
 Wrap=�5�   -   $��
385,65,0,0q�6A   $��14(�6U   $��Calibri)�6c   $
��Bold"�6x   ��5�   -   �� *StringerProfile+$ TwoWay=�#	 [  $
 U16P_8240_97��5�   x     5�   &   -
   5        $ profileStringerCatalogExt��5�   C   $	��Top=�6f   $
��Left=�6~   $��
520,71,0,0q�5�   e   $ 0.604,-1.527&�6y     5�   &   $
��Left=�6C   $	��Top=�6^   $��365,106,0,0q�6v   $ stringerMaterial��6�   �� 5�   &   -   5        $" textBox_MaterialStringersExt��5�   .   $	��120��6T   $��30��6`   $	��Top=�6l   $
��Left=�6�   $��
385,95,0,0q�6�   $
 Wrap=�5�   .   $��14(�6B   $��Calibri)�6P   $
��Bold"�6e   ��5�   .   �� *StringerMaterial+$ TwoWay=�#	 [  $
 C245-4��5�   e     5�   &   -   5        $! materialStringersCatalogExt��5�   D   $	��Top=�6i   $��0q�6�   $��6(�6�   $
��Left=�6�   $��520,106,0,0q�6�   5�   -   5�   #   5�      5�      5�      �� 5�        MaterialDesignDivider#���  6   $��1��6R   $��Stretch=�6^    J� SnapsToDevicePixels . 6|   9� 5�      $��Stretch=�6   $��Ступени��6=   $��20(�6N   $��Calibri)�6\   $
��Bold"�6q   .��5�      ��  + 'MaterialDesignToolVerticalToggleListBox#��[ 6"    6c   �� *StepTypeIndex+$ TwoWay=�#	 [ $
 0��.@�5�      �� .��5�   "   �� $��Images/StepType1_256p.png��6(   $��96��6K   5�      �� 5�      .��5�   "   �� $��Images/StepType2_256p.png��6(   $��96��6K   5�      �� 5�      .��5�   "   �� $��Images/StepType3_256p.png��6(   $��96��6K   5�      5�      5�      �� 5�      #���  6   $��1��6R   $��Stretch=�6]    . 6{   9� 5�      $��Stretch=�6   $��Ограждения��6=   $��16(�6Q   $
��Bold"�6_   $��Calibri)�6q   .��5�      ��  �� Orientation$ Vertical=�6%   $��4q�6<   .+�5�      	�    MaterialDesignTabRadioButton#��[ 6*   $��4q�6`    d� 	IsChecked$
 TrueY�6k   $��
СЛЕВА��6|   	� 5�      #��[ 6*   $��4q�6`   $ FalseY�6k   $��СПРАВА��6}   	� 5�      #��[ 6*   $��4q�6`   $ FalseY�6k   $��ВСЕ��6}   5�      5�      5�      5�      5�        5�   
   #���  6   $��2��6V   $��8(�6c   $��Calibri)�6p   .��5�      G	   ATekla.Structures.Dialog.UIControls.WpfOkApplyModifyGetOnOffCancel	  -   5        5�      5�      5�      5�      