﻿using System.Windows;

namespace LibraryManager.EntityFramework.View
{
   /// <summary>
   /// Interaction logic for AddAuthorWindow.xaml
   /// </summary>
   public partial class AddAuthorWindow : Window
   {
      public AddAuthorWindow()
      {
         InitializeComponent();
         txtNickName.Focus();
      }
   }
}