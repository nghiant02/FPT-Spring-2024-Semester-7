   M S B A M L   `   `   `  ����F  BReadCsvFile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ImportCSVFileToDBImportCSVFileToDB  5   	   "    ImportCSVFileToDB.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ImportCSVFileToDB     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ txtPath��5
      $
��Left=�6#   $��
244,46,0,0q�6>    �� TextWrapping$
 Wrap=�6R   $	��Top=�6f   $	��156��6~   $��28��6�   �� 5   
   -   5         ��Name$ 	btnBrowse��5      $	��...��6$   $
��Left=�62   $��
454,50,0,0q�6M   $	��Top=�6a   $��20��6y   $��41��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgCsv��5      $��	0,101,0,0q�6"   5      5      