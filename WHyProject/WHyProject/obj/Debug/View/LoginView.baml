   M S B A M L   `   `   `  ����E  AWHyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WHyProject.ViewModelWHyProject.ViewModel  5   	       WHyProject.View.LoginView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   .local"clr-namespace:WHyProject.ViewModel  5   	   *ignorehttp://www.galasoft.ch/ignore  5   	     � DataContext  5   	   �� *	LoginVM+   Locator �� Source# [  .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   � $��1��6    5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    .+�5"      �� -   5         ��Name$ label��5"      $
��User��6#   $��0��62   $
��Left=�6?   $��Center=�6Z   $	��178��6w   $��0,0,20,0q�6�   �� 5#      -   5        $ 
label_Copy��5#      $��2��6(   $��Password��65   $
��Left=�6H   $��Center=�6c   $	��178��6�   $��0,0,20,0q�6�   �� 5$      -   5         ��Name$ textBox��5$      $��1��6'   $
��Left=�64   $��Center=�6v   $	��178��6�   $��0,0,20,0q�6�   ��6P   �� *
UserName+ �� Mode$ TwoWay=�6�   G� 5%      $
��Left=�6   $��Center=�66   $��3��6Q   $	��178��6�   $��0,0,20,0q�6�   ,   &WHyProject.ViewModel.PasswordBoxHelper   Password 6_   ��  �� Path$ PassWord�$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.  (System.Windows.Interactivity.Interaction	   	Behaviors	 5&      .   (WHyProject.ViewModel.PasswordBoxBehavior  5'      5)      �� 5*      -   5        
 ��Name$
 button��5*      $��4��6%   $��login��62   $
��Left=�6B   $��0,0,20,0q�6]   $��Center=�6o   $	��178��6�   $
��Bold"�6�   ��6�   �� *LoginCommand+5+       � 5-   
   -   5          �Name$ 	MainFrame��5-      $
��Left=�6#   $��Center=�6>   $��0,-58,-642.667,-253.667q�6Y   $	��817��6z   $
��1404��6�   $��3��6�   5/      50      