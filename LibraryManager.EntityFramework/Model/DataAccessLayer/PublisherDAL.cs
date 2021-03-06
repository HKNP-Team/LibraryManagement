﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryManager.EntityFramework.Model
{
   /// <summary>
   /// Class Data Access Layer for Publisher
   /// </summary>
   public class PublisherDAL : IDatabaseAccess<PublisherDTO, int>
   {
      public static PublisherDAL Instance
      {
         get
         {
            if (instance == null) { instance = new PublisherDAL(); }
            return instance;
         }
      }

      private PublisherDAL()
      {
      }

      public ObservableCollection<PublisherDTO> GetList(EStatusFillter fillter = EStatusFillter.AllStatus)
      {
         var listPublisherDTO = new ObservableCollection<PublisherDTO>();
         var listRaw = new List<Publisher>();

         switch (fillter)
         {
            case EStatusFillter.AllStatus:
               listRaw = EFProvider.Instance.Database.Publishers.ToList();
               break;

            case EStatusFillter.Active:
               listRaw = EFProvider.Instance.Database.Publishers.Where(x => x.Status == true).ToList();
               break;

            case EStatusFillter.InActive:
               listRaw = EFProvider.Instance.Database.Publishers.Where(x => x.Status == false).ToList();
               break;
         }

         foreach (var pub in listRaw) { listPublisherDTO.Add(new PublisherDTO(pub)); }
         return listPublisherDTO;
      }

      public void Add(PublisherDTO newPublisher)
      {
         var newPub = newPublisher.GetBaseModel();
         EFProvider.Instance.SaveEntity(newPub, EntityState.Added, true);
      }

      public void Update(PublisherDTO publisher)
      {
         var publisherUpdate = EFProvider.Instance.Database.Publishers.Where(x => x.Id == publisher.Id).SingleOrDefault();
         if (publisherUpdate != null)
         {
            publisherUpdate.Name = publisher.Name;
            publisherUpdate.PhoneNumber = publisher.PhoneNumber;
            publisherUpdate.Email = publisher.Email;
            publisherUpdate.Website = publisher.Website;
            publisherUpdate.Address = publisher.Address;
         }

         EFProvider.Instance.SaveEntity(publisherUpdate, EntityState.Modified, true);
      }

      public void ChangeStatus(int idPublisher)
      {
         var publisherUpdate = EFProvider.Instance.Database.Publishers.Where(x => x.Id == idPublisher).SingleOrDefault();

         if (publisherUpdate != null)
         {
            publisherUpdate.Status = publisherUpdate.Status != true;
         }

         EFProvider.Instance.SaveEntity(publisherUpdate, EntityState.Modified, true);
      }

      public void Delete(int idPublisher)
      {
         var publisherDelete = EFProvider.Instance.Database.Publishers.Where(x => x.Id == idPublisher).SingleOrDefault();

         if (publisherDelete != null)
         {
            EFProvider.Instance.Database.Publishers.Remove(publisherDelete);
            EFProvider.Instance.Database.SaveChanges();
         }
      }

      private static PublisherDAL instance;
   }
}