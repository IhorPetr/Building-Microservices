using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StatlerWaldorfCorp.LocationService.DatabaseInfrastructure;
using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService.Persistence;

namespace StatlerWaldorfCorp.LocationService.Repository
{
    public class LocationRecordRepository : ILocationRecordRepository
    {
        private LocationDbContext db;

        public LocationRecordRepository(LocationDbContext db)
        {
            this.db = db;
        }

        public LocationRecord Add(LocationRecord locationRecord)
        {
            this.db.Add(locationRecord);
            this.db.SaveChanges();
            return locationRecord;
        }

        public LocationRecord Update(LocationRecord locationRecord)
        {
            this.db.Entry(locationRecord).State = EntityState.Modified;
            this.db.SaveChanges();
            return locationRecord;
        }

        public LocationRecord Delete(Guid memberId, Guid recordId)
        {
            LocationRecord locationRecord = this.Get(memberId, recordId);
            this.db.Remove(locationRecord);
            this.db.SaveChanges();
            return locationRecord;
        }

        public LocationRecord Get(Guid memberId, Guid recordId)
        {
            return this.db.LocationRecords.SingleOrDefault(Ir=> Ir.MemberID==memberId && Ir.ID==recordId);
        }

        public LocationRecord GetLatestForMember(Guid memberId)
        {
            LocationRecord locationrecord = this.db.LocationRecords
                .Where(Ir => Ir.MemberID == memberId).OrderBy(Ir => Ir.Timestamp).Last();
            return locationrecord;
        }

        public ICollection<LocationRecord> AllForMember(Guid memberId)
        {
            return this.db.LocationRecords.Where(lr => lr.MemberID == memberId).OrderBy(lr => lr.Timestamp).ToList();
        }
    }
}
