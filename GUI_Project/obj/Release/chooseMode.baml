   M S B A M L   `   `   `  ����   GUI_Project    GUI_Project.chooseMode   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     $��./Resources/#Agency FB)�.��(� $��0,10,0,0q�.���� $��0q�
���   �  �� CornerRadius$  0��$��Center=�$��Center=� �� Padding$ 0q�$��10,10,10,10q�._��  � ColumnDefinitions �� $	��100 ��� $	��100 � � RowDefinitions �� $��15 ��� $	��100 �����  �� ImageSource$ Bilder/BusterClean.png�� j� Stretch$
 Fill=�.+��� -      
picButtons#���   $��10��$��12��$��10(�$��1��$
��Bold"�|�.  $	��Top=�$��Center=�$��0q�$��0,4,5,99q�$��Center=�$��Right=�
�������$��2����Z� ���� $ Bilder/xButton.png��$ Uniform=�.���� ����  �� 
TargetType" ��.���  �� Property � �� �� Value$ Bilder/xButton.png��	 �� Triggers	 P� 
 P� Property
 �@J�IsMouseOver P� Value . . ���  � ��$ Bilder/xButtonBlack.png��� $��2��$��1�� �� $��40 ��� $��40 �.+��� -   $	��art��$��0��
�������$��	10,5,10,0q�$��25(�$��	ExtraBold"�$��Choose Mode!��$��Center=��� $��1�� �� Orientation$ 
Horizontal=�$��Center=�$	��Top=�$��50��.+��� -   $��btnRunMode1��$��20��$��50��
�������$��	 Classic ��$��12(�  GlassButton#���  $��	35,0,10,0q��� -   $��btnRunMode2��$��20��$��50��
�������$��	 Surival ��$��12(�#���  $��0q��� -   $��btnRunMode3��$��20��$��50��
�������$�� Free ��$��12(�#���  $��10,15,35,15q�