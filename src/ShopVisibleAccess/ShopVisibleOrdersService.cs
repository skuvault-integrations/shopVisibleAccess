using System;
using System.Globalization;
using System.Threading.Tasks;
using ShopVisibleAccess.Misc;
using ShopVisibleAccess.Models;
using ShopVisibleAccess.Models.Order;
using ShopVisibleAccess.OrderServices;

namespace ShopVisibleAccess
{
	public sealed class ShopVisibleOrdersService : IShopVisibleOrdersService
	{
		private readonly ShopVisibleCredentials _credentials;
		private readonly OrderServiceSoapClient _client;

		public ShopVisibleOrdersService(ShopVisibleCredentials credentials)
		{
			_credentials = credentials;
			_client = new OrderServiceSoapClient();
		}

		public ShopVisibleOrders GetOrders(DateTime startDateUtc, DateTime endDateUtc)
		{
			var orders = new ShopVisibleOrders();
			ActionPolicies.Submit.Do(() =>
			{
				var currentStartDate = startDateUtc;

				while (currentStartDate < endDateUtc)
				{
					var currentEndDate = currentStartDate.AddHours(1);
					if (currentEndDate > endDateUtc)
						currentEndDate = endDateUtc;

					var xmlnewOrders = _client.GetOrdersByDateTimeRange(
						_credentials.ClientName,
						_credentials.Guid,
						currentStartDate.ToString(CultureInfo.InvariantCulture),
						currentEndDate.ToString(CultureInfo.InvariantCulture),
						"true");
					var newOrders = XmlSerializeHelpers.Deserialize<ShopVisibleOrders>(xmlnewOrders.OuterXml);

					if (newOrders.Response.ResponseHasErrors && newOrders.Response.ResponseCode != "SUCCESS")
						throw new Exception(
							string.Format(
								"Sync Orders. Client: {0}, DateRange: ({1};{2}), IterationDateRange: ({3};{4}), ErrorDescription: {5}",
								_credentials.ClientName,
								startDateUtc, endDateUtc, currentStartDate, currentEndDate, newOrders.Response.ResponseDescription));

					orders.Orders.AddRange(newOrders.Orders);

					currentStartDate = currentEndDate;
				}
				
				if (ConcatModifiedOrders(startDateUtc, endDateUtc))
				{
					var xmlmodifiedOrders = _client.GetChangedOrdersByDateRange(_credentials.ClientName, _credentials.Guid,
						startDateUtc.ToString(CultureInfo.InvariantCulture), endDateUtc.ToString(CultureInfo.InvariantCulture), "true");
					var modifiedOrders = XmlSerializeHelpers.Deserialize<ShopVisibleOrders>(xmlmodifiedOrders.OuterXml);

					if (modifiedOrders.Response.ResponseHasErrors && modifiedOrders.Response.ResponseCode != "SUCCESS")
						throw new Exception(string.Format(
							"Sync Changed Orders. Client: {0}, DateRange: ({1};{2}), ErrorDescription: {3}", _credentials.ClientName,
							startDateUtc, endDateUtc, modifiedOrders.Response.ResponseDescription));

					orders.Orders.AddRange(modifiedOrders.Orders);
				}
			});

			return orders;
		}

		public async Task<ShopVisibleOrders> GetOrdersAsync(DateTime startDateUtc, DateTime endDateUtc)
		{
			var orders = new ShopVisibleOrders();
			await ActionPolicies.GetAsync.Do(async () =>
			{
				var currentStartDate = startDateUtc;

				while (currentStartDate < endDateUtc)
				{
					var currentEndDate = currentStartDate.AddHours(1);
					if (currentEndDate > endDateUtc)
						currentEndDate = endDateUtc;

					var xmlnewOrders = await _client.GetOrdersByDateTimeRangeAsync(
						_credentials.ClientName,
						_credentials.Guid,
						currentStartDate.ToString(CultureInfo.InvariantCulture),
						currentEndDate.ToString(CultureInfo.InvariantCulture),
						"true");
					var newOrders = XmlSerializeHelpers.Deserialize<ShopVisibleOrders>(xmlnewOrders.OuterXml);

					if (newOrders.Response.ResponseHasErrors && newOrders.Response.ResponseCode != "SUCCESS")
						throw new Exception(
							string.Format(
								"Sync Orders. Client: {0}, DateRange: ({1};{2}), IterationDateRange: ({3};{4}), ErrorDescription: {5}",
								_credentials.ClientName,
								startDateUtc, endDateUtc, currentStartDate, currentEndDate, newOrders.Response.ResponseDescription));

					orders.Orders.AddRange(newOrders.Orders);

					currentStartDate = currentEndDate;
				}

				if (ConcatModifiedOrders(startDateUtc, endDateUtc))
				{
					var xmlmodifiedOrders =
						await
							_client.GetChangedOrdersByDateRangeAsync(_credentials.ClientName, _credentials.Guid,
								startDateUtc.ToString(CultureInfo.InvariantCulture), endDateUtc.ToString(CultureInfo.InvariantCulture), "true");
					var modifiedOrders = XmlSerializeHelpers.Deserialize<ShopVisibleOrders>(xmlmodifiedOrders.OuterXml);

					if (modifiedOrders.Response.ResponseHasErrors && modifiedOrders.Response.ResponseCode != "SUCCESS")
						throw new Exception(string.Format(
							"Sync Changed Orders. Client: {0}, DateRange: ({1};{2}), ErrorDescription: {3}", _credentials.ClientName,
							startDateUtc, endDateUtc, modifiedOrders.Response.ResponseDescription));

					orders.Orders.AddRange(modifiedOrders.Orders);
				}
			});

			return orders;
		}

		private bool ConcatModifiedOrders(DateTime startDateUtc, DateTime endDateUtc)
		{
			return endDateUtc - startDateUtc < TimeSpan.FromDays(2);
		}
	}
}