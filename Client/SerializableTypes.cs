using System;

// This namespace contains WCF-serializable types for
// different Dwolla responses. 
using System.Collections.Generic;

namespace Dwolla.SerializableTypes
{
    public class DwollaResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Response { get; set; }
    }

    public class MassPayJob
    {
        public string Id { get; set; }
        public string UserJobId { get; set; }
        public bool AssumeCosts { get; set; }
        public string FundingSource { get; set; }
        // TODO: Floats as currency is bad, will change when it's due time.
        public double Total { get; set; }
        public double Fees { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public MassPayItemSummary ItemSummary { get; set; }
    }

    public class MassPayItemSummary
    {
        public int Count { get; set; }
        public int Completed { get; set; }
        public int Successful { get; set; }
    }

    public class FundingSource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Verified { get; set; }
        public string ProcessingType { get; set; }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public Uri Image { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    public class UserNearby
    {
        public string Id { get; set; }
        public Uri Image { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Delta { get; set; }
    }

    public class UserBasic
    {
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
    }

    public class UserFull
    {
        public string City { get; set; }
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
    }

    public class AutoWithdrawalStatus
    {
        public bool Enabled { get; set; }
        public string FundingId { get; set; }
    }

    public class RequestUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Uri Image { get; set; }
    }

    public class Request
    {
        public string Id { get; set; }
        public RequestUser Source { get; set; }
        public RequestUser Destination { get; set; }
        public double Amount { get; set; }
        public string Notes { get; set; }
        public string DateRequested { get; set; }
        public string Status { get; set; }
        public string Transaction { get; set; }
        public RequestUser CancelledBy { get; set; }
        public string DateCancelled { get; set; }
        public bool SenderAssumeFee { get; set; }
        public bool SenderAssumeAdditionalFee { get; set; }
        public Dictionary<string, string> AdditionalFees { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
 
    }

    public class RequestFulfilled
    {
        public string Id { get; set; }
        public int RequestId { get; set; }
        public double Amount { get; set; }
        public string SentDate { get; set; }
        public string ClearingDate { get; set; }
        public string Status { get; set; }
        public RequestUser Source { get; set; }
        public RequestUser Destination { get; set; }
    }
}
