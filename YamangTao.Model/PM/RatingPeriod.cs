using System;
using System.Collections.Generic;
using YamangTao.Core.Common;

namespace YamangTao.Model.PM
{
    public class RatingPeriod : IIdentifyableEntity<int>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public virtual IEnumerable<Ipcr> Ipcrs { get; set; }
        public int EntityId => Id;
    }
}