﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client;
using HackerNews.Indexer.Domain;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;

namespace ElasticSearch.Tests
{
	/// <summary>
	///  Tests that test whether the query response can be successfully mapped or not
	/// </summary>
	[TestFixture]
	public class IndicesTest : BaseElasticSearchTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;


		protected void TestDefaultAssertions(QueryResponse<ElasticSearchProject> queryResponse)
		{
			Assert.True(queryResponse.IsValid);
			Assert.Null(queryResponse.ConnectionError);
			Assert.True(queryResponse.Total > 0, "No hits");
			Assert.True(queryResponse.Documents.Any());
			Assert.True(queryResponse.Documents.Count() > 0);
			Assert.True(queryResponse.Shards.Total > 0);
			Assert.True(queryResponse.Shards.Successful == queryResponse.Shards.Total);
			Assert.True(queryResponse.Shards.Failed == 0);
			Assert.That(queryResponse.ElapsedMilliseconds, Is.InRange(0, 200));
				
		}
		[Test]
		public void test_clear_cache()
		{
			var client = this.ConnectedClient;
			var status = client.ClearCache();
			Assert.True(status.Success);
		}
		[Test]
		public void test_clear_cache_specific()
		{
			var client = this.ConnectedClient;
			var status = client.ClearCache(ClearCacheOptions.Filter | ClearCacheOptions.Bloom);
			Assert.True(status.Success);
		}
		[Test]
		public void test_clear_cache_generic_specific()
		{
			var client = this.ConnectedClient;
			var status = client.ClearCache<ElasticSearchProject>(ClearCacheOptions.Filter | ClearCacheOptions.Bloom);
			Assert.True(status.Success);
		}
		[Test]
		public void test_clear_cache_generic_specific_indices()
		{
			var client = this.ConnectedClient;
			var status = client.ClearCache(new List<string> { Settings.DefaultIndex, Settings.DefaultIndex + "_clone" }, ClearCacheOptions.Filter | ClearCacheOptions.Bloom);
			Assert.True(status.Success);
		}
	}
}
