   M S B A M L   `   `   `  ����I  ERoomManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:RoomManagementRoomManagement  5   	   )    #RoomManagement.AddReservationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:RoomManagement     5   	   G CHandyControl, Version=2.3.0.0, Culture=neutral, PublicKeyToken=null0hc'https://handyorg.github.io/handycontrol  5   	     � Title$  Add Reservation��5	   	   $	��650��6!   $	��800��6.    � WindowStartupLocation$ CenterScreen=�6:    � 
ResizeMode$ NoResize=�6_   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��25* �6   �� 5      $
��771* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��0.37* �6   �� 5      $��4* �6   �� 5      $��0.63* �6   .+�5   
   � -   5        $��Information��5      $��2��6"   $��
10,10,10,0q�66    5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      � $��0,10,0,0q�6   $
��Left=�6%    5      �� 5      $��* �6$   �� 5      $��* �6$   .+�5       $  HandyControl.Controls.ComboBox  -   5        $��RoomIDComboBox��5    +   $��0��6   $	��350��6A   $��30��6M   $��10q�6Y   $��Center=�6e   '  !HandyControl.Controls.InfoElement   	Necessary .  �� DisplayMemberPath$ ID��5!       �� SelectedValuePath$ ID��6(    �� 
IsReadOnly . (  "HandyControl.Controls.TitleElement	   
TitleWidth$	 60 �6Q   
   TitleAlignment$

 Left=�6p      Title$
 Room��6�   6�     5#      -   5        $��GuestIDComboBox��5#   +   $��1��6   $	��350��6B   $��30��6N   $��10q�6Z   $��Center=�6f    . $
 Name��5$      $ ID��6*    . $	 60 �6S   $

 Left=�6r   $ Guest��6�   5%      � 5&      $��0,10,0,0q�6   $��1��6%   $��Right=�65    5'      �� 5(      $��* �6$   �� 5)      $��* �6$   .+�5,      &   HandyControl.Controls.DatePicker  -   5        $��StartDateDatePicker��5,   -   $��0��6    $	��350��6H   $��30��6T   $��10q�6`   $��Center=�6l    . $	 80 �5-      $

 Left=�60   $ 
Start Date��6U     5/      -   5        $��EndDateDatePicker��5/   -   $��1��6    $	��350��6F   $��30��6R   $��10q�6^   $��Center=�6j    . $	 80 �50      $

 Left=�60   $ End Date��6U   6u   51      52      � 53   
   $��1��6   $��2��6   $
��20,0q�60   .+�54      �� .+�55      #  HandyControl.Controls.Divider  56      57      � 58   
   -   5        $��Table��58      $��2��6   $��2��6)    59      �� 5:      $
��auto �6    �� 5;      $��* �6    �� 5<      $
��auto �6    .+�5>      � $��10q�6   $
��Left=�6   .+�5?      �� $��1��6    �� Orientation$ 
Horizontal=�6-   $��Center=�6F   $
��Left=�6a   .+�5@        -   5        $��ServiceComboBox��5@   "   $	��350��69   $��30��6E   $��10q�6Q   $��Center=�6]   $
 Name��5A      $ ID��6*   |�.   �� 
IsEditable .  .    Placeholder$ Please select service��6w   $	 70 �5B      $

 Left=�60   $ Service��6U   �� 5C      -   5        $��AddServiceButton��5C      $
��15,0q�65   $	��Add��6C   6Q   �� 5D      -	   5        $��RemoveServiceButton��5D      $	��5,0q�68   $��Remove��6E   6V   5E      5F      &   System.Windows.Controls.DataGrid  5H      -
   5        $��reservationDataGrid��5H   @   $��1��6      AutoGenerateColumns .  $��Stretch=�6[   $��Stretch=�6y      GridLinesVisibility$
 None=�5I         HeadersVisibility$ Column=�6D      CanUserAddRows .     SelectionMode$ Single=�6v      
IsReadOnly . $��20,10q�5J   1   ��6@   �� *
tempList+ �� UpdateSourceTrigger$ PropertyChanged=� � ContextMenu 5K      �� 5L      .@�5M      m� -   5        $��Remove��5M   #   63   5N         Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5Q      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6)      Width5	  /System.Windows.Controls.DataGridLengthConverter$ 2*	 67   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 6B   �� *ServiceName+  5R      $ Price��6)   $
 1.5*	 68    6E   �� *ServicePrice+  5S      $
 Date��6)   $
 1.5*	 67    6D   �� *Date+5V      � 5X      $��2��6   $��10q�6    $��Right=�6,   .+�5Y      �� $ 
Horizontal=�6   .+�5Z      �� $
��10,0q�6    $��Center=�6.   Total:6Q   �� 5[      -   5        $��TotalTextBox��5[       $
��10,0q�64   $��Center=�6B   $��20(�6]   $06o   5\      5]      5^      � 5_   
   $��3��6   $��2��6   $��0,0,0,20q�60    5`      �� 5a      $��* �6#   �� 5b      $��* �6#   �� 5c      $��* �6#   �� 5d      $��* �6#   �� 5e      $��* �6#   �� 5f      $��* �6#   .+�5h      �� -   5        $��	AddButton��5h   %   $��2��6   $��80��66   .��6Y   Add6^   �� 5i      -   5        $��Cancel��5i   %   $��3��6   $��80��63    �� IsCancel . .��6N   Cancel6V   5j      5k      5l      