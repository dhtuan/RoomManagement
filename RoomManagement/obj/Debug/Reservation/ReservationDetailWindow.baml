   M S B A M L   `   `   `  ����I  ERoomManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:RoomManagementRoomManagement  5   	   *    $RoomManagement.EditReservationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:RoomManagement     5   	   G CHandyControl, Version=2.3.0.0, Culture=neutral, PublicKeyToken=null0hc'https://handyorg.github.io/handycontrol  5   	     � Title$  Reservation Detail��5	   	   $	��650��6$   $	��800��61    � WindowStartupLocation$ CenterScreen=�6=    � 
ResizeMode$ NoResize=�6b   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��25* �6   �� 5      $
��771* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��0.37* �6   �� 5      $��4* �6   �� 5      $
��auto �6   �� 5      $��0.63* �6   .+�5   
   � -   5        $��Information��5      $��2��6"   $��
10,10,10,0q�66    5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      � $��0,10,0,0q�6   $
��Left=�6%    5      �� 5      $��* �6$   �� 5      $��* �6$   .+�5!      $  HandyControl.Controls.ComboBox  -   5        $��RoomIDComboBox��5!   +   $��0��6   $	��350��6A   $��30��6M   $��10q�6Y   $��Center=�6e    �� DisplayMemberPath$ ID��5"       �� SelectedValuePath$ ID��6(   {�.   �� 
IsReadOnly . (  "HandyControl.Controls.TitleElement   
TitleWidth$ 70 �6c   	   TitleAlignment$
	 Left=�6�   
   Title$

 Room��6�     5$      -   5        $��GuestIDComboBox��5$   +   $��1��6   $	��350��6B   $��30��6N   $��10q�6Z   $��Center=�6f   $
 Name��5%      $ ID��6*   {�.   . $ 70 �6e   $
	 Left=�6�   $
 Guest��6�   5&      � 5'      $��0,10,0,0q�6   $��1��6%   $��Right=�65    5(      �� 5)      $��* �6$   �� 5*      $��* �6$   .+�5-      &   HandyControl.Controls.DatePicker  -   5        $��StartDateDatePicker��5-   -   $��0��6    $	��350��6H   $��30��6T   $��10q�6`   $��Center=�6l   $ 70 �5.      $
	 Left=�60   $
 
Start Date��6U   {�.  6w     50      -   5        $��EndDateDatePicker��50   -   $��1��6    $	��350��6F   $��30��6R   $��10q�6^   $��Center=�6j   $ 70 �51      $
	 Left=�60   $
 End Date��6U   {�.  6u   53      54      � 55   
   $��1��6   $��2��6   $
��20,0q�60   .+�56      �� .+�57      #  HandyControl.Controls.Divider  58      59      � 5:   
   -   5        $��Table��5:      $��2��6   $��2��6)   .+�5;      &   System.Windows.Controls.DataGrid  -   5        $��reservationDataGrid��5;   @   $��1��6      AutoGenerateColumns .  $��Stretch=�6[   $��Stretch=�6y      GridLinesVisibility$
 None=�5<         HeadersVisibility$ Column=�6D      CanUserAddRows .     SelectionMode$ Single=�6v      
IsReadOnly . $��20,10q�5=   1   ��6@   �� *
tempList+ �� UpdateSourceTrigger$ PropertyChanged=�   Columns 5>      0  *System.Windows.Controls.DataGridTextColumn  5?      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 67   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6B   �� *Service.Name+  5@      $ Price��6)   $
 1.5* 68    6E   �� *Service.Price+  5A      $
 Date��6)   $
 1.5* 67    6D   �� *Date+5D      5E      � 5F   
   $��3��6   $��2��6   $��10q�60   $��Right=�6<   .+�5G      ��  �� Orientation$ 
Horizontal=�6   .+�5H      �� $
��10,0q�6   $��Center=�6*   Total:6M   �� 5I      -   5        $��TotalTextBox��5I      $
��10,0q�60   $��Center=�6>   $��20(�6Y   $06k   5J      5K      � 5L   
   $��4��6   $��2��6   $��0,0,0,20q�60   .+�5M      �� -	   5        $
��Done��5M      $��80��6!   .��6?   Done6E   5N      5O      5P      