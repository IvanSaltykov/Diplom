   M S B A M L   `   `   `  ����W  SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ComboBoxToggleButton  �� 
TargetType � ColumnDefinitions ��Name �� CornerRadius J� SnapsToDevicePixels F�Name �� Stretch �� Triggers P� Property	 P� Value
 �� 
TargetName �� Property �� Value  ComboBoxTextBox  CustomComboBoxStyle �� 
TargetType d�Name �� 	ClickMode d� 	IsChecked �� Mode �� RelativeSource ��Name J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name
 � Tag ��Name  CustomTextBoxStyle �Name �� IsDropDownOpen � AllowsTransparency �Name � ActualWidth �� MaxDropDownHeight  � OverridesDefaultStyle! K� IsItemsHost-   'System.Windows.Input.KeyboardNavigation"    DirectionalNavigation# ]� 
Conditions$ �� Property% �� Value& P� 
SourceName�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %  &	        &	     &	 �    7   7  �� 5      "  d�63   .R�5   
   �  5      �� 5      �� 5      $��30 �6#   .+�5      �� $ Border��6   $��2��5      $ 5��5      ���#333333$��2q�5       . 5      �� 5      $��1��6   ._�5      F� $ Arrow��6   F����        ���( ��( јaQ     јaQ P����̰( � �    P����        ���#F0F0F0$
 Fill=�5      $��7��5      $��10��5      $��Center=�5       $��Center=�5!      5"      5#       5$   
   P� 5%       �@J�IsMouseOver	 . . �5&      �� $
 Border��6B    � �� �	#FF03A9F45'      P� 5(       �@J�IsKeyboardFocused	 . . �5)      �� $
 Border��6B    � �� �	#FF03A9F4�� 5*      $
 Border��6J    � ��$ 0.5 0.5 0.5 3q�64   6J   5+      5-      �� 5/      "  ��6.   .R�50   
   �� $ PART_ContentHost��6   |�.  #��z��52      53      �� 55      " ��6(   .�56   
   ��  � ��$ 35��6#   �� 57   
    � ��$	 250��6"   �� 58   
    �@�Cursor$
 Hand��6#   �� 59   
    � ��$ Roboto)�6'   �� 5:   
    � ��$ 11(�6%   �� 5;   
    � �� �#3333336'   �� 5<   
    � ��$ Center=�60   �� 5=   
    � ��$ 0 10 0 0q�6#   �� 5>   
    � �� 5?      �� 5@      "  ��6"   .R�5A      � .+�5B      d� $ ToggleButton��6'   8��  $��2��5D   '   |�.  $ Press=�5F   '    5G   '   �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+�� 5H      $ ContentSite��6+    .  #��z 5J   +   #��z 5K   +   #��z��5L   +   $��	11 0 25 0q�5M   +   $��Center=�5N   +   $
��Left=�5O   +   {�. 5P   +   �� 5Q      $ 	Watermark��6    $��Center=�5R       $
��Left=�5S       ���#F0F0F0$y�	Collapsed=�5U       $��10 0 0 0q�5V       #��z 5W       $��Roboto)�5X       $��11(�5Y       �� 5Z      $ PART_EditableTextBox��6"   8�� $
��Left=�5\   "   $��Center=�5]   "   |�. $y�Hidden=�5_   "   � 5`      $ Popup��6    $��Bottom=�5a       #��z 5b        . |�.  $��Slide=�5e       .��5f      � $ DropDown��6#    . #��z 5h   #   #��z 5i   #   .+�5j   "   �� $ DropDownBorder��6)   $��1q�5k   )   ���#F0F0F0���#F0F0F05m   )   �� 5n   "   $��4 6 4 6q�6/    .   . ���#333333$��Roboto)�5r   /   $��11(�5s   /   �� 5t   "   ! . $" 	Contained=�5u   -   5w   #   5x      5y      5z       5{      ]� 5|      # 5}      �� 5~   "   $ �@��Text$%  ��6<   ./�5�      �� $
 	Watermark��6K    � y�$ Visible=�6;   6K   5�      P� 5�       �@��HasItems	 .  . �5�      �� $
 DropDownBorder��6%    � ��$ 95��6V   5�      P� 5�       �@��
IsGrouping	 . . �5�      ��  � �� .  6N   5�      P� 5�      $& Popup��6"    �@�AllowsTransparency	 . . �5�      �� $
 DropDownBorder��6%    �@��CornerRadius$ 4��6Y   �� 5�      $
 DropDownBorder��6%    � ��$ 0 2 0 0q�6S   5�      5�      5�      5�      5�      