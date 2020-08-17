using System;
using MediatR;

namespace ShoppingCart.Business.Campaign
{
    public class CampaignCreatedEvent : INotification
    {
        public Guid ID { get; set; }
    }
}