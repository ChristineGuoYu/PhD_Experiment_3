   M S B A M L   `   `   `  ����U  QMixed_Initiative_Exp1_v0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Mixed_Initiative_Exp1_v0._1Mixed_Initiative_Exp1_v0._1  5      +    %Mixed_Initiative_Exp1_v0._1.StatusBar   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:Mixed_Initiative_Exp1_v0._1     5        � 	Resources  5	         MyButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� CornerRadius �� Triggers P� Property	 P� Value
 �� 
TargetName��%  &	        �� 5
   
   " ��6&   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � ��$ 5q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��2q�5      $	 4,2q�5      
������ $ 3��5      #��z��5      ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver	 . . �5   "   �� $
 border��6)    � ��
 �   �6T   5      5      5      5       5"      .��5#      � .+�5$   
   � .+�5&      �� -   5        $��NewMessages��5&      $��70��6'   $��80��63   $��10,10,360,10q�6>   $��Images/envelope-new.png��6T   6w   �� 5'      -   5        $��	NoticeNew��5'      $y�Hidden=�6%   $��25��6:   $��50��6F   $��
0,8,300,57q�6Q   $��Images/RedNotice.png��6e   6�   �� 5(      -   5        $��NoticeNew_Label��5(      $y�Hidden=�6+   $��20��6@   
�������$��17(�6`   $��
3,4,301,52q�6n   $��Center=�6�   6�   �� 5*      -   5        $��Archived_yes_Messages��5*      $��80��61   $��70��6=   $��255,13,125,-3q�6H   $��Images/GreenFolder.png��6_   6�   �� 5+      -   5        $��NoticeArchived_yes��5+      $y�Hidden=�6.   $��25��6C   $��50��6O   $��95,8,215,57q�6Z   $��Images/RedNotice.png��6o   6�   �� 5,      -   5        $��NoticeArchived_yes_Label��5,      $y�Hidden=�64   $��8��6J   
�������$��17(�6i   $��97,4,217,52q�6w   $��Center=�6�   6�   �� 5-      -   5        $��NoticeUnsure��5-      $y�Hidden=�6(   $��25��6=   $��50��6I   $��180,8,130,57q�6T   $��Images/RedNotice.png��6j   6�   �� 5.      -   5        $��NoticeUnsure_Label��5.      $y�Hidden=�6.   $��11��6C   
�������$��17(�6c   $��182,4,132,52q�6q   $��Center=�6�   6�   �� 5/      -	   5        $��NoticeManual��5/      $y�Hidden=�6(   $��25��6>   $��50��6J   $��309,8,91,57q�6U   $��Images/RedNotice.png��6j   6�   �� 50      -
   5        $��NoticeManual_Label��50      $y�Hidden=�6.   $��15��6C   
�������$��17(�6c   $��250,4,34,52q�6q   $��Center=�6�   6�   �� 52      -   5        $��Archived_no_Messages��52      $��368,10,18,8q�60   $��Images/bin.png��6E   �� 53      -   5        $��NoticeArchived_no��53      $y�Hidden=�6-   $��25��6B   $��50��6N   $��405,8,-5,57q�6Y   $��Images/RedNotice.png��6n   6�   �� 54      -   5        $��NoticeArchived_no_Label��54      $y�Hidden=�64   $��3��6I   
�������$��17(�6h   $��
412,4,1,52q�6v   $��Center=�6�   6�   �� 57      -   5        $��NewMessages_btn��57      $��80��6,   $��80��68   #��[  6D   
������ $��
10,5,360,5q�6�   6�   �� 58      -   5        $��Archived_yes_Messages_btn��58      $��80��66   $��80��6B   #��[  6O   
������ $��250,5,120,5q�6�   6�   �� 59      -   5         ��Name$ Archived_no_Messages_btn��59      $��80��67   $��80��6C   #��[  6P   
������ $��
362,5,13,5q�6�   6�   5:      5;      5<      