   M S B A M L   `   `   `  ����I  ERoomManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:RoomManagementRoomManagement  5   	   $    RoomManagement.EditGuestWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:RoomManagement     5   	   G CHandyControl, Version=2.3.0.0, Culture=neutral, PublicKeyToken=null0hc'https://handyorg.github.io/handycontrol  5   	     � Title$  
Edit Guest��5	   	   $	��300��6   $	��455��6)    � WindowStartupLocation$ CenterScreen=�65    � 
ResizeMode$ NoResize=�6Z   .��5
      �  � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��* �6   .+�5   
   � -   5         �Name$ Field��5       5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    .+�5      #  HandyControl.Controls.TextBox  -   5        $��NameTextBox��5   &   $��0��6   $	��400��69   $��30��6E   $��10q�6Q   $��Center=�6]      ShowClearButton .    TextType$ Common=�5   (   '  !HandyControl.Controls.InfoElement   Placeholder$ Please enter name��6:      	Necessary . (  "HandyControl.Controls.TitleElement	   
TitleWidth$	 90 �5      
   TitleAlignment$

 Left=�60      Title$
 Name��6U     5      -   5        $��IdentityCardTextBox��5   &   $��1��6   $	��400��6A   $��30��6M   $��10q�6Y   $��Center=�6e    . $ Common=�5   (   $  Please enter identity card��6:    . $	 90 �5      $

 Left=�60   $ Identity Card��6U     5      -   5        $��TelephoneTextBox��5   &   $��2��6   $	��400��6>   $��30��6J   $��10q�6V   $��Center=�6b    . $ Common=�5   (   $ Please enter telephone��6:    . $	 90 �5       $

 Left=�60   $ 	Telephone��6U   5"      � 5#   
   $��1��6    � ColumnDefinitions 5$      �� 5%      $��* �6#   �� 5&      $��* �6#   �� 5'      $��* �6#   �� 5(      $��* �6#   .+�5*      �� -   5        $��	UpdateBtn��5*   %   $��1��6   $��80��66   .��6Y   Update6a   �� 5+      -   5        $��Cancel��5+   %   $��2��6   $��80��63    �� IsCancel . .��6N   Cancel6V   5,      5-      5.      