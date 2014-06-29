using System;
using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Order
{
	[ XmlRoot( "Order" ) ]
	public sealed class ShopVisibleOrder
	{
		[ XmlElement( ElementName = "OrderID" ) ]
		public int OrderId { get; set; }

		[ XmlElement( ElementName = "OrderNumber" ) ]
		public int OrderNumber { get; set; }

		[ XmlElement( ElementName = "OrderDate" ) ]
		public DateTime OrderDate { get; set; }

		[ XmlElement( ElementName = "OrderStatusId" ) ]
		public int OrderStatusId { get; set; }

		[ XmlElement( ElementName = "OrderItems" ) ]
		public ShopVisibleOrderItems OrderItems { get; set; }

		[ XmlElement( ElementName = "OrderAmountTotal" ) ]
		public decimal OrderAmountTotal { get; set; }

		[ XmlElement( ElementName = "OrderShippingAddress1" ) ]
		public string OrderShippingAddress1 { get; set; }

		[ XmlElement( ElementName = "OrderShippingAddress2" ) ]
		public string OrderShippingAddress2 { get; set; }

		[ XmlElement( ElementName = "OrderShippingCity" ) ]
		public string OrderShippingCity { get; set; }

		[ XmlElement( ElementName = "OrderShippingStateProvince" ) ]
		public string OrderShippingStateProvince { get; set; }

		[ XmlElement( ElementName = "OrderShippingZip" ) ]
		public string OrderShippingZip { get; set; }

		[ XmlElement( ElementName = "OrderShippingCountry" ) ]
		public string OrderShippingCountry { get; set; }

		[ XmlElement( ElementName = "OrderCustomer" ) ]
		public ShopVisibleOrderCustomer OrderCustomer { get; set; }
	}

	public enum OrderStatusEnum
	{
		PartiallyCompleteOutOfStock = 1,
		Ordered = 4,
		OrderComplete = 6,
		OrderDeclined = 7,
		PartiallyCompleteBackorder = 8,
		ReturnRequested = 9,
		Returned = 10,
		OrderCancelled = 11,
		ProcessedShippingPending = 12,
		PartiallyCompletePreOrder = 14,
		ProcessedShippedDestinationCity = 15,
		PossibleFraud = 16,
		PaymentDenied = 19,
		OrderedCreditCardAuthorized = 20,
		ProcessedSentForFulfillment = 21,
		ProcessedFulfillmentDelay = 22,
		AwaitingPayment = 25,
		PartiallyCompleteShippingPending = 31,
		PartiallyCompleteCancelledItems = 34,
		ProcessedPendingReview = 35,
		LocalPickup = 36,
		OrderChargeback = 37,
	}
}