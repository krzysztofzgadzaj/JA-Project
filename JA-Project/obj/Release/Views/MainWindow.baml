   M S B A M L   `   `   `  ���� E  AJA-Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:JA_Project
JA_Project      JA_Project.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:JA_Project       � Title$  
MainWindow��$	��450��$	��800��.��� .+��� 
�������._��  � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � � RowDefinitions �� $��* ��� $
��0.5* ��� $��3* ��� $��* �.+��� $��0��$��0��$��10q�$��Center=�$��Center=�.+�	� $��C#�� d� 	IsChecked �� *
IfCSharp+	� $	��ASM�� �� *IfAsm+�� $��0 8 0 0q�$��1��$��0��$��2��$��Center=�$��Center=� �� Orientation$ 
Horizontal=�.+��� $��Type scale: ���� 
������ $��0q�$��20������ *Scale+��    
MainButton#��[  $��1��$��0��$��Load picture������ *LoadPictureCommand+�� $��Center=�$��0��$��2��$��2��$��10q�.+��� $ 
Horizontal=�.+��� $��Number of threads:���� $��0q�
������ $��Right=� �� TextAlignment$ Right=�$��40������  �� ElementName$ slValue�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=��� -   $��slValue��	 � Minimum$	 1Q�
 � Maximum$
 64Q� �� TickPlacement$ BottomRight=� �� TickFrequency$ 1Q� �� IsSnapToTickEnabled .  � Value �� *ThreadsNumber+�� #��[  $��4��$��0��$��Extend picture������ *ExtendPictureCommand+�� $ 
Horizontal=�$��0��$��5��$��10q�$��Center=�.+��� $��Time (in ms): ��$��12(��� $��12(����� *TimeInMiliSec+�� $��20q�$��0��$��2��$��2������ *OriginalImage+�� $��20q�$��2��$��1��$��3��$��4������ *ExtendedPicture+