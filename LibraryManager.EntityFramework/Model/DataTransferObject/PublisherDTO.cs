﻿namespace LibraryManager.EntityFramework.Model
{
   public class PublisherDTO : Publisher
   {
      public int NumberOfBook { get => Books.Count; }
      public string Note { get => (this.Status != true) ? "Đã ẩn" : ""; } 

      public PublisherDTO() : base()
      {
      }

      public PublisherDTO(Publisher publisherRaw) : base()
      {
         if (publisherRaw != null)
         {
            this.Id = publisherRaw.Id;
            this.Name = publisherRaw.Name;
            this.PhoneNumber = publisherRaw.PhoneNumber;
            this.Address = publisherRaw.Address;
            this.Email = publisherRaw.Email;
            this.Website = publisherRaw.Website;
            this.Status = publisherRaw.Status;

            this.Books = publisherRaw.Books;
         }
      }

      public Publisher GetBaseModel()
      {
         return new Publisher()
         {
            Name = this.Name,
            PhoneNumber = this.PhoneNumber,
            Address = this.Address,
            Email = this.Email,
            Website = this.Website,
            Status = this.Status
         };
      }
   }
}