   M S B A M L   `   `   `  ����K  GHistProjTemplate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HistProjTemplateHistProjTemplate  5   	   !    HistProjTemplate.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HistProjTemplate     5   	     � Title$   ЕГЭ по истории��5   	   $	��800��6    $	��800��6-    � WindowStartupLocation$ CenterScreen=�69    � Icon$ Icon/Icon.jpg��6_    � 
ResizeMode$ NoResize=�6t   .��5
      �� .+�5   
   o� 
�������$��16(�6&   $��Verdana)�64   $��20q�6I   $	��Top=�6V   .@�5      m� -   5        $��Начать тест��5      6A   m� 5      -   5        $��Выбрать тест��5      6K   m� 5      -   5        $��Добавить��5      .@�5      m� -   5        $��Раздел��5      6F   m� 5      -   5        $��Тест��5      6A   5      m� 5      -   5        $��
Выйти��5      6&   5      �� 5   
   -   5         ��Name$ TextBlockInfo��5   (   $
��Wrap=�6   $��10q�6?   $	��Top=�6K   $��Arial)�6`   $��22(�6s   $��Center=�6�   $��Center=�6�   6�   �� 5   
    �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ MapImage��5      $	��600��6&   $	��350��63   $��20q�6?   6M   �� 5      -	   5         ��Name$ QuestionAnswerPanel��5      $��20q�66   $	��360��6B   .+�5      �� $��10q�6   $	��340��6+   $	��420��67   .��5      �� -
   5        $ QuestionBlock��5   U   $��5q�6    $
��Wrap=�6+   $	��Top=�6@   $��15(�6l   $��Arial)�6z   6�   5      �� 5      -   5         ��Name$ 	AnswerBox��5   K   $��10q�6   	 �� 	MaxLength$		 100��6&   $	��Top=�66   $��15(�6^   $��Arial)�6l   6�   �� 5      -   5        $ ButtonPanel��5   5   $��Bottom=�6   $ 
Horizontal=�6J   .+�5      �� -   5        
 ��Name$
 PrevQButton��5   L   $��30��6   $	��150��6)   
�������$��20,10,10,10q�6a   $��15(�6v   $��Arial)�6�   .��6�   #!Предыдущий вопрос6�   �� 5      -   5        $
 NextQButton��5   L   $��30��6   $	��150��6)   
�������$��10q�6a   $��15(�6m   $��Arial)�6{   .��6�   !Следующий вопрос6�   5       �� 5!      -   5        $
 EndTestButton��5!   ^   $��30��6   $	��150��6%   $	��Top=�62   
�������$��10q�6u   $��15(�6�   $��Arial)�6�   .��6�   Закончить тест6�   5"      5#      5%      5&      