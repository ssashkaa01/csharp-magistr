   M S B A M L   `   `   `  ����@  <Paint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PaintPaint  5   	       Paint.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.7.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   localclr-namespace:Paint  5   	     � Title$  Paint��5	   	   $	��450��6   $	��800��6$   .��5
      ]� .+�5   
   o� $	��Top=�6   .@�5      m� $��Файл��6   .@�5      m� -   5        $��Зберегти��5      6-   m� 5      -   5        $��Відкрити��5      6-   5      m� 5      $��Інструменти��6   .@�5      m� -   5        $��
Кисть��5      6*   m� 5      -   5        $��
Гумка��5      6*   m� 5      -   5        $��
Лінія��5      6*   m� 5      -   5        $��Прямокутник��5      60   m� 5      -   5        $��Коло��5      6)   5      5      �� 5   
   $	��Top=�6    �� Orientation$ 
Horizontal=�6*   $��10q�6C   .+�5      �� -   5        $��
Колір��5      $��75��6%   60   �� 5      $��Розмір кисті:��6   $��Center=�6-   $��10,0,0,0q�6H   �� 5      -	   5        $��brushSizeSlider��5       � Minimum$ 1Q�6,    � Maximum$ 20Q�68    � Value$ 5Q�6E   $	��100��6O   $��10,0,0,0q�6[   6m   5      �� 5   
   -
   5        $��drawingCanvas��5      
�������5    5   5!      5"      