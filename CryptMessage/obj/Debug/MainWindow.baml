   M S B A M L   `   `   `  ����G  CCryptMessage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:CryptMessageCryptMessage  5   	       CryptMessage.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:CryptMessage     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   ���	#FFFF7500$��0q�6%  .��5	      �  � ShowGridLines .  $��0q�6!    � ColumnDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       �� 5!      �� 5"      �� 5#      �� 5$      �� 5%      �� 5&      �� 5'      �� 5(      �� 5)      �� 5*      .+�5,   
   ]� $��0,0,10,0q�6    J� RenderTransformOrigin$ 0.569,0.572&�6&   $��10��6J   .+�5-      o� $	��Top=�6   $��17��6(   ���	#FF240C5A$��	0,0,-10,0q�6K   .@�5.      m� $��_File��6   $	��Top=�6*   $
��Left=�6B   ���	#FF8045FD.@�5/      m� $
��_New��6   m� 50      $��_Open��6   m� 51      $��_Save��6   �� 52      m� 53      $��_Exit��6   54      m� 55      -   5         m�Name$ SettingsMnu��55      $��Settings��60   ���	#FF8045FF6\   m� 56      -   5        $ HomeMnu��56      $
��Home��6,   ���	#FF8045FF6P   m� 57      -   5        $ 
FriendsMnu��57      $��Friends��6/   ���	#FF8045FF6Y   m� 58      -   5        $ AboutMnu��58      $��About��6-   ���	#FF8045FF6S   m� 59      -   5        $ 	LogoutMnu��59      $��Logout��6.   ���	#FF8045FF6V   5:      �� 5;      -   5         ��Name$ About1��5;      $����The purpose of this app is to have a reasonably secure method of communication to use for those who wish thier words to be kept private.��6%   $	��401��6�   $	��Top=�6�   $y�Hidden=�6�   5<      �� 5=   
   -   5        $ UsernameDisplayLbl��5=      $��Username��6,   $
��Left=�6?   $	��Top=�6Z   $��19��6r   $��23��6�   $	��163��6�   $��	0,-3,0,-2q�6�   $��2��6�   �� 5>   
   -	   5         ��Name$ 	MsgTxtBox��5>      $
��Left=�6%   $��23��6@    �� TextWrapping$
 Wrap=�6L   $��Message��6`   $	��Top=�6o   $	��527��6�   $y�Visible=�6�   $��18��6�   $��1��6�   $��2��6�   $��7��6�   $��5��6�   ���	#FF1280B46�   �� 5?   
   -
   5        	 ��Name$	 
MsgSendBtn��5?      $
��Send��6%   $
��Left=�64   $	��Top=�6O   $��75��6g   $y�Visible=�6r   $��18��6�   $��10��6�   $��2��6�   $��6��6�   ���	#FF8045FF6�   �� 5@   
   -   5        $	 sendFriendRequestBtn��5@      $��Send Request��6/   $	��Top=�6F   $y�Hidden=�6^   $��2��6r   $��8��6   ���	#FFA06060$
��Left=�6�   $	��158��6�   $��2��6�   $��21��6�   6�   �� 5A   
   -   5        $	 DelFriendBtn��5A      $��Delete Friend��6'   $	��Top=�6?   $y�Hidden=�6W   $��4��6k   $��8��6x   ���	#FFA06060$
��Left=�6�   $	��158��6�   $��2��6�   $��21��6�   6�   �� 5B   
   -   5        $ NewFriendLbl��5B      $��Username��6&   $
��Left=�69   $	��Top=�6T   $y�Hidden=�6l   $��1��6�   $��2��6�   $��79��6�   $��0,17,0,0q�6�   $��2��6�   �� 5C   
   -   5        $ NewFriendTxtBox��5C      $
��Left=�6+   $��21��6F   $
 Wrap=�6R   $	��Top=�6f   $	��318��6~   $y�Hidden=�6�   $��2��6�   $��3��6�   $��4��6�   ���	#FFC812126�   �� 5D   
   -   5        $ listLbl��5D      $��Friends:��6!   $��0,18,0,0q�64   $��2��6F   �� 5E   
   -   5        
 ��Name$
 	ConvoList��5E      $
��Left=�6%   $	��Top=�6@   $��2��6X   $��15��6e   $	��314��6w   ���	#FF1280B4$��79��6�   �� 5F   
   -   5        $ 
senderName��5F      $��Label��6$   $
��Left=�64   $	��Top=�6O   $ 0.553,1.962&�6g   $��57��6�   $y�Hidden=�6�   $��1��6�   $��15��6�   $��	12,19,0,0q�6�   $��2��6�   �� 5G   
   -   5        $ sendersMessage��5G      $��Label��6(   $
��Left=�68   $	��Top=�6S   $ 0.553,1.962&�6k   $��57��6�   $y�Hidden=�6�   $��3��6�   $��15��6�   $��	10,19,0,0q�6�   $��2��6�   �� 5H   
   -   5        $ usernameLbl��5H      $��Username��6%   $
��Left=�68   $	��Top=�6S   $ 0.794,0.577&�6k   $��9��6�   $��4��6�   $��23��6�   $��0,-2,0,0q�6�   $��2��6�   �� 5I   
   -   5        $ usernameTxtBox��5I      $��23��6*   $
 Wrap=�66   $	��Top=�6J   $ 1.233,2.957&�6b   $��9��6�   $��2��6�   $��5��6�    � IsSharedSizeScope . $��1��6�   ���	#FF1280B4$��2��6�   $��0,0,39,0q�6
  �� 5J   
   -   5        $ loginPasswordLbl��5J      $��Password��6*   $
��Left=�6=   $	��Top=�6X   $��10��6p   $��4��6~   $��0,-4,0,0q�6�   G� 5K   
   -   5         G�Name$ loginPassBox��5K      $	��Top=�6,    G� PasswordChar$ *��6D   $��10��6U   $��5��6c    . $��2��6�   $��0,0,39,0q�6�   $��2��6�   ���	#FF1280B4$��21��6�   �� 5L   
   -   5        $	 LoginBtn��5L      $��Log In��6#   $
��Left=�64   $	��Top=�6O   $��11��6g   $��4��6u   $��3��6�   $	��239��6�   $��3��6�   ���	#FF8045FF6�   �� 5M   
   -   5        $	 
NewUserBtn��5M      $��Create New User��6%   $��4��6?   $��3��6S   $��	78,19,0,0q�6c   $��12��6v   $��2��6�   ���	#FF8045FF6�   �� 5N   
   -   5        $ LoginStatusLbl��5N      $��Login Status��6(   $��4��6?   $
��Left=�6O   $��27��6j   $��6��6v   $	��Top=�6�   $	��239��6�   $��0,18,0,0q�6�   $��3��6�   $��3��6�   �� 5O   
   -   5        $ debugLbl��5O      $��Server Debug��6"   $��4��69   $
��Left=�6I   $��26��6d   $��0,18,0,0q�6p   $��4��6�   $��3��6�   $	��Top=�6�   $	��160��6�   $��2��6�   �� 5P   
   -   5        $ user1Lbl��5P      $�� ��6"   $
��Left=�6-   $	��Top=�6H   $��1��6`   $��0,16,0,0q�6p   $��14��6�   $��2��6�   $��79��6�   $��26��6�   
�������6�   �� 5Q   
   -   5        $ msg1Lbl��5Q      $�� ��6!   $
��Left=�6,   $	��Top=�6G   $��2��6_   $��15��6o   $��6��6}   $	��476��6�   $��26��6�   $��0,-5,0,0q�6�   $y�Visible=�6�   
�������$��2��6�   �� 5R   
   -   5        $ user2Lbl��5R      $�� ��6"   $
��Left=�6-   $	��Top=�6H   $��1��6`   $��12��6p   $��2��6~   $��79��6�   $��26��6�   
�������$��0,16,0,0q�6�   �� 5S   
   -   5        $ msg2Lbl��5S      $�� ��6!   $
��Left=�6,   $	��Top=�6G   $��2��6_   $��12��6o   $��6��6}   $	��476��6�   $��26��6�   $y�Visible=�6�   
�������$��0,16,0,0q�6�   $��2��6�   �� 5T   
   -   5        $ user3Lbl��5T      $�� ��6"   $
��Left=�6-   $��1��6H   $��10��6X   $��2��6f   $��79��6w   
�������$��0,17,0,0q�6�   �� 5U   
   -    5        $ msg3Lbl��5U      $�� ��6!   $
��Left=�6,   $	��Top=�6G   $��2��6_   $��0,17,0,0q�6o   $��10��6�   $��2��6�   $��6��6�   $	��476��6�   $��25��6�   $y�Visible=�6�   
�������6�   �� 5V   
   -!   5        $ msg1Date��5V      $��2021-04-26T03:44:56.984Z��6"   $
��Left=�6E   $	��Top=�6`   $��8��6x   $��0,16,0,0q�6�   $��14��6�   $��2��6�   $��26��6�   $��82��6�   $��2��6�    J� Opacity$ 0Q�6�   �� 5W   
   -"   5        $ msg1ID��5W      $	��xxx��6"   $
��Left=�6-   $	��Top=�6H   $��8��6`   $��0q�6p   $��13��6{   $��2��6�   $��26��6�   $��82��6�   $��2��6�   $ 0Q�6�   �� 5X   
   -#   5        $	 CreateUserBtn��5X      $��Create User��6(   $
��Left=�6>   $	��Top=�6Y   $	��239��6q   $��4��6}   $��13��6�   $��21��6�   $��3��6�   ���	#FF8045FF$y�Hidden=�6�   6�   �� 5Y   
   -$   5        $ newUsernameLbl��5Y      $��Username��6(   $
��Left=�6;   $	��Top=�6V   $ 0.794,0.577&�6n   $��8��6�   $��4��6�   $��23��6�   $��0,0,0,0q�6�   $��3��6�   $y�Hidden=�6�   �� 5Z   
   -%   5        $ newUsernameTxtBox��5Z      $��23��6-   $
 Wrap=�69   $	��Top=�6M   $ 1.233,2.957&�6e   $��8��6�   $��2��6�   $��5��6�    . $��1��6�   ���	#FFC81212$��2��6�   $��0,0,0,0q�6  $y�Hidden=�6  �� 5[   
   -&   5        $ newEmailLbl��5[      $��Email��6%   $
��Left=�65   $	��Top=�6P   $��4��6h   $��0,19,0,0q�6x   $��8��6�   $��2��6�   $��80��6�   $y�Hidden=�6�   �� 5\   
   -'   5        $ newEmailTxtBox��5\      $��2��6*   $
��Left=�6>   $��23��6Y   $��2��6e   $
 Wrap=�6v   $	��Top=�6�   $	��159��6�   $��5��6�   $��9��6�   ���	#FFC81212$y�Hidden=�6�   $��2��6�   .��6  .��5`      .��5a   !   .��5e      5f      �� 5g   
   -(   5        $ newPasswordLbl��5g      $��Password��6(   $
��Left=�6;   $	��Top=�6V   $��9��6n   $��4��6{   $��0,18,0,0q�6�   $��2��6�   $y�Hidden=�6�   G� 5h   
   -)   5        $ 
newPassBox��5h      $	��Top=�6*   $ *��6B   $��10��6S   $��5��6a    . $��2��6�   $��0,0,0,0q�6�   $��3��6�   ���	#FFC81211$y�Hidden=�6�   6�   �� 5i   
   -*   5        $ repeatNewPasswordLbl��5i      $��Repeat Password��6.   $
��Left=�6H   $	��Top=�6c   $��10��6{   $��3��6�   $��	40,18,0,0q�6�   $��3��6�   $��2��6�   $y�Hidden=�6�   G� 5j   
   -+   5        $ repeatNewPassBox��5j      $	��Top=�60   $ *��6H   $��11��6Y   $��5��6g    . $��2��6�   $��0,0,0,0q�6�   $��4��6�   ���	#FFC81211$y�Hidden=�6�   6�   �� 5k   
   -,   5        $ createUserMessageLbl��5k      $�� ��6.   $
��Left=�69   $	��Top=�6T   $��4��6l   $��0,18,0,0q�6|   $��11��6�   $��2��6�   $��3��6�   $	��239��6�   �� 5l   
   --   5        $ newFriendRequestLbl��5l      $�� ��6-   $
��Left=�68   $	��Top=�6S   $��1��6k   $	��158��6{   $��0,15,0,0q�6�   $��2��6�   $��2��6�   $
��Bold"�6�   �� 5m   
   -.   5        $
 friendRequestList��5m      $��4��6-   $
��Left=�6A   $	��252��6\   $��12��6i   $	��Top=�6{   $	��318��6�   $��2��6�   $��4��6�   ���	#FF1280B4$y�Hidden=�6�   5o      �� 5p   
   -/   5        $ 	reqBtnBkg��5p      $
��Left=�6#   $	��Top=�6>   $ 0.395,0.476&�6V   $��6��6z   $��4��6�   $��2��6�   $	��158��6�   $	��252��6�   $��12��6�   ���	#FF1280B4$y�Hidden=�6�   �� 5q   
   -0   5        $	 requestAccept��5q      $��Accept��6(   ���	#FF02B902$��16(�6P   $
��Left=�6^   $	��Top=�6y   $	��158��6�   $��6��6�   $��0,1,0,0q�6�   $��4��6�   $��2��6�   $��41��6�   $��2��6�   $
��Bold"�6�   $y�Hidden=�6  6"  �� 5r   
   -1   5        $	 requestDeny��5r      $
��Deny��6&   ���	#FFE40000$��16(�6L   $
��Left=�6Z   $	��Top=�6u   $	��158��6�   $��6��6�   $��7��6�   $��2��6�   $��42��6�   $��2��6�   $
��Bold"�6�   $y�Hidden=�6�   6  5s      5u      