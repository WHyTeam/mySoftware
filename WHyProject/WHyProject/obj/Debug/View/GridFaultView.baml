   M S B A M L   `   `   `  ����  
WHyProject#    WHyProject.View.GridFaultView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0ignorehttp://www.galasoft.ch/ignore     5   	     � DataContext  5   	   �� *	gridFault+   Locator �� Source# [  .��5
      �  � 	Resources 5   
    �� 
TargetType  ListBoxDataSytle �� Property �� Value �� Orientation A� Columns��%~  &	       �� 5      " ��6   .�5      ��  � �� 5      �� 5      .R�5      �� $ 
Horizontal=�6*   .+�5   "   �� $	��120��6,   ��6;   �� *Info+�� 5   "   $��20��6?   $��20��6J   ��6(   �� *Img+6J   5      5      5      �� 5       � �� 5      �� 5      .R�5      A� $ 1��6*   5      5      5      5        � RowDefinitions 5"   
   �� 5#      $��* �6   �� 5$      $��9* �6   .+�5'   
   � 	 � ColumnDefinitions	 5(      �� 5)      $�� * �6#   �� 5*      $�� * �6#   �� 5+      $�� * �6#   �� 5,      $�� * �6#   .+�5.      �� $��Center=�6   $��Center=�65   $��
10,10,20,0q�6P   ��6d   �� *GridFaultText[0]+�� 5/      $��Center=�6   $��Center=�65   $��
10,10,20,0q�6P   $��1��6d   ��6t   �� *GridFaultText[1]+�� 50      $��Center=�6   $��Center=�65   $��
10,10,20,0q�6P   $��2��6d   ��6t   �� *GridFaultText[2]+�� 51      $��Center=�6   $��Center=�65   $��
10,10,20,0q�6P   $��3��6d   ��6t   �� *GridFaultText[3]+52      � 54   
   $��1��6   	 55      �� 56      $�� * �6#   �� 57      $�� * �6#   �� 58      $�� * �6#   �� 59      $�� * �6#   .+�5;      �� $��
10,10,20,0q�6   #��[ 6*   ��6T   �� *GridFaultData1+�� 5<      $��1��6   $��
10,10,20,0q�6&   #��[ 6:   ��6d   �� *GridFaultData2+�� 5=      $��2��6   $��
10,10,20,0q�6'   #��[ 6;   ��6e   �� *GridFaultData3+�� 5>      $��3��6   $��
10,10,20,0q�6'   #��[ 6;   ��6e   �� *GridFaultData4+5?      5@      5A      