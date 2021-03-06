﻿using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryManager.EntityFramework.Model
{
   /// <summary>
   /// Class Data Access Layer for Borrow
   /// </summary>
   public class BorrowDAL
   {
      public static BorrowDAL Instance
      {
         get
         {
            if (instance == null) { instance = new BorrowDAL(); }
            return instance;
         }
      }

      private BorrowDAL()
      {
      }

      public ObservableCollection<BorrowDTO> GetListByMemberId(string memberId)
      {
         var result = new ObservableCollection<BorrowDTO>();
         foreach (var item in EFProvider.Instance.Database.Borrows.Where(x => x.MemberId == memberId && x.Status == true).ToList())
         {
            result.Add(new BorrowDTO(item));
         }
         return result;
      }

      public ObservableCollection<BorrowDTO> GetListByBookId(string bookId)
      {
         var result = new ObservableCollection<BorrowDTO>();
         foreach (var item in EFProvider.Instance.Database.Borrows.Where(x => x.BookId == bookId && x.Status == true).ToList())
         {
            result.Add(new BorrowDTO(item));
         }
         return result;
      }

      public ObservableCollection<BorrowDTO> GetListByDate(DateTime fromDate, DateTime toDate)
      {
         var result = new ObservableCollection<BorrowDTO>();
         foreach (var item in EFProvider.Instance.Database.Borrows.Where(x => x.Status == true).ToList())
         {
            if (item.BorrowDate.Date >= fromDate.Date && item.BorrowDate.Date <= toDate.Date)
            {
               result.Add(new BorrowDTO(item));
            }
         }
         return result;
      }

      public void Add(string memberId, string librarianId, string bookId)
      {
         var br = new Borrow() { BookId = bookId, MemberId = memberId, LibrarianId = librarianId, BorrowDate = DateTime.Now, Status = true };
         EFProvider.Instance.SaveEntity(br, EntityState.Added, true);
      }

      private static BorrowDAL instance;
   }
}