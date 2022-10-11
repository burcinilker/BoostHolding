using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoostHolding.Entities.Data
{
    public class Permission
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TypesOfPermissionId { get; set; }
        public int TotalDaysOff { get; set; }
        public DateTime DateOfRequest { get; set; } = DateTime.UtcNow;
        public DateTime DateOfStart { get; set; } =DateTime.UtcNow.AddDays(1);
        public DateTime DateOfEnd { get; set; }
        public string ApprovalStatus { get; set; }
        public Employee Employee { get; set; }
        public TypesOfPermission TypesOfPermission { get; set; }

        public void CalculateTotalDaysOff()
        {
            TotalDaysOff = (DateOfEnd - DateOfStart).Days;
        }

        //    Talep tarihi
        //İzin Türü
        //Başlangıç tarihi(yarından başlamalı)
        //(izin>1 yıl => 14 gün, izin>6 yıl =>20 gün),babalık 5,doğum 3
        //Bitiş Tarihi(max tarihi aşmamalı)
    }
}
