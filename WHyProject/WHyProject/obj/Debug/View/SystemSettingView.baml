   M S B A M L   `   `   `  ����  
WHyProject'    !WHyProject.View.SystemSettingView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0ignorehttp://www.galasoft.ch/ignore     5   	     � DataContext  5   	   �� *systemSetting+   Locator �� Source# [  .��5
      �  � 	Resources 5   
    �� 
TargetType  CheckBoxSytle �� Property �� Value �� Orientation d� 	IsChecked �� Mode	 A� Columns��%�  &	       �� 5      " ��6   .�5      ��  � �� 5      �� 5      .R�5      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5   "   �� $
��20,0q�6+   $	��120��69   ��6H   �� *Info+ 6a   �� *	IsCheck+$ TwoWay=��� 5   "   $
��20,0q�6(   $��20��6M   $��20��6X   ��66   �� *Img+6X   5      5      5      �� 5       � �� 5      �� 5      .R�5      A� $	 1��6*   5      5      5       5!      
 � ColumnDefinitions
 5%   
   �� 5&      $��* �6   �� 5'      $��* �6   �� 5(      $��* �6   .+�5+   
   �  � RowDefinitions 5,      �� 5-      $��* �6    �� 5.      $��8* �6    .+�50      �� $ Vertical=�6   $��Center=�60   .+�51      �� $��
10,10,20,5q�6   $��Center=�6T   $��Center=�6q   ��60   �� *SystemTextBlock[0]+6q   �� 52      $ 
Horizontal=�6   .+�53      �� $��MCUSystemSetting��6<   $��10,0,0,5q�6`   $��80��6r   $��20��6�   $��Center=�6�   $��Center=�6�   ��6   �� *
SetCommand+��6�   �� *ButtonSet[0]+�� 54      $��MCUSystemSetting��6=   $��10,0,0,5q�6b   $��80��6�   $��20��6�   $��Center=�6�   $��Center=�6�   ��6   �� *ReadCommand+��6t   �� *ButtonSet[1]+6�   55      56      �� 57      $��
10,10,20,0q�6   $��1��6*   #��[ 67   ��6^   �� *MCUSystemSetting+58      � 5:   
   $��1��6    5;      �� 5<      $��* �6    �� 5=      $��8* �6    .+�5?      �� $ Vertical=�6   $��Center=�60   .+�5@      �� $��
10,10,20,5q�6   $��Center=�6T   $��Center=�6q   ��60   �� *SystemTextBlock[1]+6q   �� 5A      $ 
Horizontal=�6   .+�5B      �� $��GridSystemSetting��6<   $��10,0,0,5q�6a   $��80��6�   $��20��6�   $��Center=�6�   $��Center=�6�   ��6   �� *
SetCommand+��6s   �� *ButtonSet[0]+6�   �� 5C      $��GridSystemSetting��6=   $��10,0,0,5q�6b   $��80��6�   $��20��6�   $��Center=�6�   $��Center=�6�   ��6   �� *ReadCommand+��6t   �� *ButtonSet[1]+6�   5D      5E      �� 5F      $��
10,10,20,0q�6   $��1��6*   #��[ 67   ��6^   �� *GridSystemSetting+5G      � 5I   
   $��2��6    5J      �� 5K      $��* �6    �� 5L      $��8* �6    .+�5N      �� $ Vertical=�6   .+�5O      �� $��
10,10,20,5q�6   $��Center=�6U   $��Center=�6r   ��61   �� *SystemTextBlock[2]+6r   �� 5P      $ 
Horizontal=�6   $��Center=�66   .+�5Q      �� $��10,0,0,5q�6   $��GenSystemSetting��6N   $��80��6�   $��20��6�   $��Center=�6�   $��Center=�6�   ��6/   �� *
SetCommand+��6r   �� *ButtonSet[0]+6�   �� 5R      $��10,0,0,5q�6   $��GenSystemSetting��6O   $��80��6�   $��20��6�   $��Center=�6�   $��Center=�6�   ��6/   �� *ReadCommand+��6s   �� *ButtonSet[1]+6�   5S      5T      �� 5U      $��
10,10,20,0q�6   $��1��6*   #��[ 67   ��6^   �� *GenSystemSetting+5V      5W      5X      