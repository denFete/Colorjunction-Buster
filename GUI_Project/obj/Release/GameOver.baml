   M S B A M L   `   `   `  ����   GUI_Project    GUI_Project.GameOver   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     $��./Resources/#Agency FB)�.��(� .���� 
������ $��0q�
���   �  �� CornerRadius$  0��$��Center=�$��Center=� �� Padding$ 0q�$	��300��$	��190��._�� ����  �� ImageSource$ Bilder/BusterClean.png�� j� Stretch$
 Fill=�.+��  � RowDefinitions �� $��50 ��� $��40 ��� $��40 ��� $��40 � � ColumnDefinitions �� �� .+��� $��0��$��2��
�������$��35(�$��	ExtraBold"�$��
Game Over!��$��Center=�$��Center=�$��	10,5,10,0q�� $��1��$��2�� �� �� .+��� $��0��$��Right=�$��Bottom=�$��0,0,0,0q�
�������$��20(�$
��Bold"�$��Score:���� -   $��	lblPoints��$��1��
�������$��Bottom=�$��20(�$
��Bold"�$��0���� -   $��lblHighscoreSaved��$��2��$�� ��
�������$��	ExtraBold"�$��15(�$��2��$��Center=�� -   $��
submitGrid��$��2��$��Center=�$��2�� �� �� .+��� -   $��
txbAddName��$��1q�
������ $��0��$��Center=� �� TextAlignment$ Center=� �� 	MaxLength$ 11��$��12(�$��18��$��70��$��0,0,5,0q�
���   �.������  �� EndPoint$ 0.5,1&�	 �� 
StartPoint$	 0.5,0&�.��� 
 � Color$
 	#FF939393�� � Offset$ 1Q�� $
 White��$ 0.236Q�� $
 	#FF939393��� $
 	#FFDADADA��$ 0.735Q�.���� -   $��1��
�������$��20��$��Submit��$��70��$��	Agency FB)�$��15(�$��5q�   GlassButton#���   $
��Left=��� $��2��$��5�� �� Orientation$ 
Horizontal=�$��Center=�$��0,0,5,0q�.+��� -   $��btnNewGameGO��$��70��
�������$��Restart��$��15(�$��20��$��5q�#���   �� -   $��	btnMenuGO��$��70��
�������$
��Menu��$��15(�$��20��$��5q�#���   