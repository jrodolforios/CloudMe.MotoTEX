using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Entries { 
    public class EntryBase<TEntryKey>
    {
        public TEntryKey Id { get; set; }

        public delegate void OnInsertCallback(EntryBase<TEntryKey> entry);
        public delegate void OnUpdateCallback(EntryBase<TEntryKey> entry);
        public delegate void OnDeleteCallback(EntryBase<TEntryKey> entry);

        public bool ForceDelete { get; set; } = false; // forces 'physical' deletion

        public virtual DateTime Inserted { get; private set; }
        public virtual DateTime Updated { get; private set; }
        public virtual DateTime? Deleted { get; private set; }

        public Guid? InsertUserId { get; private set; }
        public Guid? UpdateUserId { get; private set; }
        public Guid? DeleteUserId { get; private set; }

        public virtual Usuario InsertUser { get; set; }
        public virtual Usuario UpdateUser { get; set; }
        public virtual Usuario DeleteUser { get; set; }

        public bool IsSoftDeleted => Deleted.HasValue;
        public void SoftDelete() => Deleted = DateTime.Now;
        public void SoftRestore() => Deleted = null;

        public static event OnInsertCallback OnInsert;
        public static event OnUpdateCallback OnUpdate;
        public static event OnDeleteCallback OnDelete;

        static EntryBase()
        {
            /*Triggers<EntryBase<TEntryKey>>.GlobalInserting.Add<IHttpContextAccessor>(insertingEntry =>
            {
                insertingEntry.Entity.Inserted = insertingEntry.Entity.Updated = DateTime.Now;
                Guid.TryParse(insertingEntry.Service.HttpContext.User?.FindFirst("sub").Value, out Guid userID);
                insertingEntry.Entity.UpdateUserId = insertingEntry.Entity.InsertUserId = userID;
                OnInsert?.Invoke(insertingEntry.Entity);
            });*/

            /*Triggers<EntryBase<TEntryKey>>.Inserted += entry =>
            {
                entry.Entity.OnInsert(entry.Entity);
            };*/

            Triggers<EntryBase<TEntryKey>>.Inserting += entry =>
            {
                entry.Entity.Inserted = entry.Entity.Updated = DateTime.Now;
                OnInsert?.Invoke(entry.Entity);
            };

            /*Triggers<EntryBase<TEntryKey>>.GlobalUpdating.Add<IHttpContextAccessor>(updatingEntry =>
            {
                updatingEntry.Entity.Updated = DateTime.Now;
                Guid.TryParse(updatingEntry.Service.HttpContext.User?.FindFirst("sub").Value, out Guid userID);
                updatingEntry.Entity.UpdateUserId = userID;
                OnUpdate?.Invoke(updatingEntry.Entity);
            });*/

            Triggers<EntryBase<TEntryKey>>.Updating += entry =>
            {
                entry.Entity.Updated = DateTime.Now;
                OnUpdate?.Invoke(entry.Entity);
            };

            /*Triggers<EntryBase<TEntryKey>>.Updated += entry =>
            {
                entry.Entity.OnUpdate(entry.Entity);
            };*/

            /*Triggers<EntryBase<TEntryKey>>.GlobalDeleting.Add<IHttpContextAccessor>(deletingEntry =>
            {
                if (!deletingEntry.Entity.ForceDelete)
                {
                    Guid.TryParse(deletingEntry.Service.HttpContext.User?.FindFirst("sub").Value, out Guid userID);
                    deletingEntry.Entity.DeleteUserId = userID;

                    deletingEntry.Entity.SoftDelete();
                    deletingEntry.Cancel = true; // Cancels the deletion, but will persist changes with the same effects as EntityState.Modified
                }

                OnDelete?.Invoke(deletingEntry.Entity);
            });*/

            Triggers<EntryBase<TEntryKey>>.Deleting += entry =>
            {
                if (!entry.Entity.ForceDelete)
                {
                    entry.Entity.SoftDelete();
                    entry.Cancel = true; // Cancels the deletion, but will persist changes with the same effects as EntityState.Modified
                }

                OnDelete?.Invoke(entry.Entity);
            };

            /*Triggers<EntryBase<TEntryKey>>.Deleted += entry =>
            {
                entry.Entity.OnDelete(entry.Entity);
            };*/

        }
    }
}
