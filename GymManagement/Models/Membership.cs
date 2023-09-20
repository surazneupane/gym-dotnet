using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymManagement.Models
{
    public class Membership
    {
        public int ID { get; set; }
        public Guid MemberID { get; set; }
        public int MembershipTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPaid { get; set; } // Added payment status
        public string UserName { get; set; }
        public Register Member { get; set; }
        public MembershipType MembershipType { get; set; }
    }

    public class MembershipType
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; } 
        public List<Membership> Memberships { get; set; }
    }

    public class Class
    {
        public int ID { get; set; }
        public string ClassName { get; set; }
        public List<TrainingSession> TrainingSessions { get; set; }
    }

    public class TrainingSession
    {
        public int ID { get; set; }
        public int ClassID { get; set; }
        public Guid StaffID { get; set; }
        public DateTime DateTime { get; set; } 
        public int Capacity { get; set; }
        public Class Class { get; set; }
        public Register Staff { get; set; }
    }

    // Schedule Model
    public class Schedule
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public int TrainingSessionID { get; set; }
        public Register Member { get; set; }
        public TrainingSession TrainingSession { get; set; }
    }

    public class MembershipViewModel
    {
        public List<Register> Users { get; set; }
        public List<MembershipType> MembershipTypes { get; set; }
        public List<Membership> Memberships { get; set; }
    }

    public class TrainingSessionViewModel
    {
        public List<Class> Classes { get; set; }
        public List<Register> Staff { get; set; }
        public List<TrainingSession> TrainingSessions { get; set; }

        public Func<Guid, string> GetUsernameForStaffID { get; set; }
    }

    public class EditMembershipViewModel
    {
        public Membership Membership { get; set; }
        public List<MembershipType> MembershipTypes { get; set; }
    }

    public class TrainingSessionEditViewModel
    {
        public TrainingSession TrainingSession { get; set; }
        public List<Register> Staff { get; set; }

        // Additional properties to hold selected class name and staff username
        public string SelectedClassName { get; set; }
        public string SelectedStaffUsername { get; set; }
    }

    public class MemberDashboardViewModel
    {
        public Register User { get; set; }
        public List<Membership> Memberships { get; set; }
        public List<MembershipType> MembershipTypes { get; set; }
        public List<TrainingSessionNewViewModel> TrainingSessions { get; set; } // Add this property

    }

    public class SummaryDataViewModel
    {
        public string MembershipType { get; set; }
        public int MemberCount { get; set; }
    }

    public class InterestRecordViewModel
    {
        public int SessionID { get; set; }
        public Guid MemberID { get; set; }
        public string ClassName { get; set; }
        public string UserName { get; set; }
        // Add more properties as needed
    }


    public class SummaryReportViewModel
    {
        public List<SummaryDataViewModel> SummaryData { get; set; }
        public List<InterestRecordViewModel> InterestRecordData { get; set; }
    }


    public class TrainingSessionNewViewModel
    {
        public List<TrainingSession> TrainingSessions { get; set; }

    }

    public class InterestRecord
    {
        public int ID { get; set; }
        public Guid MemberID { get; set; }
        public int SessionID { get; set; }
    }
    public class Attendance
    {
        public int ID { get; set; }
        public Guid MemberID { get; set; }
        public DateTime CheckInDateTime { get; set; }
    }


    public class AttendanceViewModel
    {
        public List<AttendanceRecordViewModel> AttendanceRecords { get; set; }
    }

    public class AttendanceRecordViewModel
    {
        public string ClassName { get; set; }
        public string UserName { get; set; }
        public DateTime CheckInDateTime { get; set; }
    }


}