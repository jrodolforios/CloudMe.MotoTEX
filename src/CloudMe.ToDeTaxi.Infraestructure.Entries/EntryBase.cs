using EntityFrameworkCore.Triggers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries { 
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

        public bool IsSoftDeleted => Deleted != null;
        public void SoftDelete() => Deleted = DateTime.UtcNow;
        public void SoftRestore() => Deleted = null;

        //public event OnInsertCallback OnInsert;
        //public event OnUpdateCallback OnUpdate;
        //public event OnDeleteCallback OnDelete;

        static EntryBase()
        {
            Triggers<EntryBase<TEntryKey>>.Inserting += entry =>
            {
                entry.Entity.Inserted = entry.Entity.Updated = DateTime.UtcNow;
            };

            /*Triggers<EntryBase<TEntryKey>>.Inserted += entry =>
            {
                entry.Entity.OnInsert(entry.Entity);
            };*/

            Triggers<EntryBase<TEntryKey>>.Updating += entry =>
            {
                entry.Entity.Updated = DateTime.UtcNow;
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
            };

            /*Triggers<EntryBase<TEntryKey>>.Deleted += entry =>
            {
                entry.Entity.OnDelete(entry.Entity);
            };*/

        }
    }
}
