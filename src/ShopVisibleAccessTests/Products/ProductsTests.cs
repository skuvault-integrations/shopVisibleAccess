﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NUnit.Framework;
using ShopVisibleAccess;
using ShopVisibleAccess.Models;
using ShopVisibleAccess.Models.Product;

namespace ShopVisibleAccessTests.Products
{
	public class ProductsTests
	{
		private ShopVisibleFactory _factory;
		private ShopVisibleCredentials _credentials;

		[ SetUp ]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\ShopVisibleCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
				this._credentials = new ShopVisibleCredentials( testConfig.ClientName, testConfig.Guid );

			this._factory = new ShopVisibleFactory();
		}

		[ Test ]
		public void GetProducts()
		{
			var service = this._factory.CreateProductsService( this._credentials );
			var inventory = service.GetInventory();

			inventory.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetProductsAsync()
		{
			var service = this._factory.CreateProductsService( this._credentials );
			var inventory = await service.GetInventoryAsync();

			inventory.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void UpdateProducts()
		{
			var service = this._factory.CreateProductsService( this._credentials );
			var products = new ShopVisibleProductsInventory
			{
				Products = new List< ShopVisibleProductInventory >
				{
					new ShopVisibleProductInventory
					{
						Quantity = 0,
						Sku = "MWW291Z"
					}
				}
			};

			service.UpdateProducts( products );
		}

		[ Test ]
		public async Task UpdateProductsAsync()
		{
			var service = this._factory.CreateProductsService( this._credentials );
			var products = new ShopVisibleProductsInventory
			{
				Products = new List< ShopVisibleProductInventory >
				{
					new ShopVisibleProductInventory
					{
						Quantity = 0,
						Sku = "MWW291Z"
					}
				}
			};

			await service.UpdateProductsAsync( products );
		}
	}
}