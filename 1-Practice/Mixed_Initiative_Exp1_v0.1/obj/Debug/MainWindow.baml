   M S B A M L   `   `   `  ����U  QMixed_Initiative_Exp1_v0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Mixed_Initiative_Exp1_v0._1Mixed_Initiative_Exp1_v0._1  5   	   ,    &Mixed_Initiative_Exp1_v0._1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:Mixed_Initiative_Exp1_v0._1     5   	     � Title$L  FIntelligent Database - Rainbow Research Group, University of Cambridge��5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowState$ 	Maximized=�5
   	    � Icon$ Images/Mails.png��6!    � 
ResizeMode$ CanMinimize=�6:   $	��900��6S   $
��1400��6`   .��5      � $��0,0,0,0q�6    � RowDefinitions 5   
   �� 5      $
��115* �6   �� 5      $
��321* �6   .+�5   
   +   %Mixed_Initiative_Exp1_v0._1.StatusBar  -   5          Name$ 
Status_Bar��5      $y�Hidden=�6.   $��90��6B   $��902,10,0,129.5q�6N   �� 5   
   -   5        $��Unsure_Messages_label��5      $b��\Please judge whether or not each of these                      messages is about 'delivery'.��6-   $��24(�6�   $��34,166,136,13.5q�6�   $y�Hidden=�6�   �� 5   
   -   5        $��Unsure_Messages_num_label��5      $��4��61   $��40(�6=   
���  ��$��Center=�6\   $��465,151,807,18.5q�6�   $y�Hidden=�6�   �� 5   
   -   5        $!��Archived_yes_Messages_label��5      $Q��KWe have classified                      messages that are about 'delivery'.��63   $��24(�6�   $��38,166,241,13.5q�6�   $y�Hidden=�6�   �� 5   
   -   5        $%��Archived_yes_Messages_num_label��5      $��6��67   $��40(�6C   
���  ��$��Center=�6b   $��241,151,1057,18.5q�6�   $y�Hidden=�6�   �� 5!   
   -   5        $ ��Archived_no_Messages_label��5!      $U��OWe have classified                      messages that are not about 'delivery'.��62   $��24(�6�   $��38,166,241,13.5q�6�   $y�Hidden=�6�   �� 5"   
   -   5        $$��Archived_no_Messages_num_label��5"      $��8��66   $��40(�6B   
���  ��$��Center=�6b   $��241,151,1057,18.5q�6�   $y�Hidden=�6�   �� 5*   
   -   5        $��updating_label��5*      $ ��Updating system formula...��6&   $y�Hidden=�6K   $��30(�6_   $��Center=�6m   $��515,10,515,551q�6�   $��1��6�   6   0Mixed_Initiative_Exp1_v0._1.Table_UnsureMessages  5+   
   -	   5          Name$ Unsure_Messages��5+   %   $y�Hidden=�6?   $��6,10.5,68,131.5q�6S   $��1��6l   =   7Mixed_Initiative_Exp1_v0._1.Table_Archived_yes_Messages  5-   
   -
   5          Name$ Archived_yes_Messages��5-   ,   $y�Hidden=�6K   $��6,10.5,68,131.5q�6_   $��1��6x   <   6Mixed_Initiative_Exp1_v0._1.Table_Archived_no_messages  5.   
   -   5        	  Name$	 Archived_no_Messages��5.   +   $y�Hidden=�6I   $��6,10.5,68,131.5q�6]   $��1��6v   �� 58   
   -   5        $��Start_Rhythmic_Timer��58      $y�Hidden=�6.   $	��150��6B   $	��250��6O   $��25(�6[   $��Start Task 1��6k   6�   �� 59   
   -   5        $��Stop_Rhythmic_Timer��59      $y�Hidden=�6-   $��Stop Task 1��6A   $��15(�6W   $��1249,10,10,170q�6e   $��50��6}   $	��125��6�   6�   �� 5;   
   -   5        $��Start_Random_Timer��5;      $y�Hidden=�6+   $��Start Task 2��6?   $	��150��6W   $	��250��6d   $��25(�6p   6�   �� 5<   
   -   5        $��Stop_Random_Timer��5<      $y�Hidden=�6+   $��Stop Task 2��6?   $��15(�6U   $��1249,10,10,170q�6c   $��50��6{   $	��125��6�   6�   �� 5>   
   -   5        $��Start_Manual��5>      $y�Hidden=�6%   $��Start Task 3��69   $	��150��6P   $	��250��6]   $��25(�6i   6�   �� 5?   
   -   5        $��Next_btn��5?      $y�Hidden=�6!   $��	Show Next��66   $	��250��6J   $	��250��6V   $��25(�6c   $��1379,57,-235,334q�6r   $��1��6�   �� 5A   
   -   5        $��Start_Aligned_Timer��5A      $y�Hidden=�6,   $��Start Task 4��6@   $	��150��6X   $	��250��6e   $��25(�6q   6�   �� 5B   
   -   5        $��Stop_Aligned_Timer��5B      $y�Hidden=�6+   $��Stop Task 4��6?   $��15(�6U   $��1249,10,10,170q�6c   $��50��6{   $	��125��6�   6�   �� 5E   
   -   5        $��Task1Btn��5E      $��Task 1��6"   $��40(�63   $	��150��6A   $	��250��6N   $��1��6q   $��38,126,1106,365q�6~   6�   �� 5F   
   -   5        $��Task2Btn��5F      $��Task 2��6"   $��40(�63   $	��150��6A   $	��250��6N   $��1��6q   $��381,126,763,365q�6~   6�   �� 5G   
   -   5        $��Task3Btn��5G      $��Task 3��6"   $��40(�63   $	��150��6A   $	��250��6N   $��1��6q   $��728,126,416,365q�6~   6�   �� 5H   
   -   5        $��Task4Btn��5H      $��Task 4��6"   $��40(�63   $	��150��6A   $	��250��6N   $��1��6q   $��1076,126,68,365q�6~   
 J� RenderTransformOrigin$
 1.115,-0.319&�6�   6�   -   'Mixed_Initiative_Exp1_v0._1.TaskRatings  5I   
   -   5          Name$ Task_Ratings��5I      $��Center=�62   $��Center=�6O   $y�Hidden=�6j   6�   5O      5P      