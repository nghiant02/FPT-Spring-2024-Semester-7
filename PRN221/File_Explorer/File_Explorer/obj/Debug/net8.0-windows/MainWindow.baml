   M S B A M L   `   `   `  ����H  DFile_Explorer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:File_ExplorerFile_Explorer  5   	       File_Explorer.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:File_Explorer     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   �� $��Folder Name��6   $
��Left=�6&   $��169,123,0,0q�6A   $	��Top=�6V   $��42��6n   $	��130��6z   $��18(�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ txtfolderPath��5      $��Center=�6)   $��	0,129,0,0q�6F    �� TextWrapping$
 Wrap=�6Y   $	��Top=�6m   $	��192��6�   $��30��6�   �� 5   
   -   5         ��Name$ btnBrowseFiles��5      $��Browse Files��6)   $��Center=�6@   $��	0,176,0,0q�6]   $	��Top=�6p   $��16(�6�   6�   �� 5   
   -   5         ��Name$ listView��5      $��	0,217,0,0q�6%    �� View 5      �� 5      .P�5      �� $
��Type��6%    �� Width$ 50��63    �� CellTemplate 5      �� 5      .R�5   "   �� $��16��6@   $��16��6K   ��6(   �� *Icon+6K   5      5      �� 5      $
��Name��6%   $	 200��6Y    �� DisplayMemberBinding 63   �� *Name+6Y   �� 5      $
��Path��6%   $	 400��6Y    63   �� *Path+6Y   5      	 � 	Resources	 5         ItemContextMenu��%�   &	        �� 5      .@�5      m� -   5        $��Rename��5      6/   m� 5      -   5        $��Delete��5      6/   5      5       ��5!      �� 5"      
 �� 
TargetType"
 ��6   .�5#      ��  �� Property �@�ContextMenu �� Value# [  64   5$      5&      5'      5(      