﻿using EntityFrameworkCore.Triggers;
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

        public bool IsSoftDeleted => Deleted.HasValue;
        public void SoftDelete() => Deleted = DateTime.Now;
        public void SoftRestore() => Deleted = null;

        public static event OnInsertCallback OnInsert;
        public static event OnUpdateCallback OnUpdate;
        public static event OnDeleteCallback OnDelete;

        static EntryBase()
        {
            Triggers<EntryBase<TEntryKey>>.Inserting += entry =>
            {
                entry.Entity.Inserted = entry.Entity.Updated = DateTime.Now;
                OnInsert?.Invoke(entry.Entity);
            };

            /*Triggers<EntryBase<TEntryKey>>.Inserted += entry =>
            {
                entry.Entity.OnInsert(entry.Entity);
            };*/

            Triggers<EntryBase<TEntryKey>>.Updating += entry =>
            {
                entry.Entity.Updated = DateTime.Now;
                OnUpdate?.Invoke(entry.Entity);
            };

            /*Triggers<EntryBase<TEntryKey>>.Updated += entry =>
            {
                entry.Entity.OnUpdate(entry.Entity);
            };*/

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
