namespace ReachOutAuth.Models.Enums
{
    public static class OrderStatus
    {
        public enum Status
        {
            PaymentPending = 1,
            ShipmentPending,
            Completed,
            Problem,
            Cancelled,
            Fraud,
            NeedsApproval,
            Batched,
            OnHold
        }
    }
}
